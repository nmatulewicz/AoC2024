using AOC.ConsoleApp.Models.Day05;

namespace AOC.ConsoleApp.Solvers;

public class Day05Solver : ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        return SolveFirstChallenge(lines.ToArray());
    }
    public string SolveFirstChallenge(string[] lines)
    {
        var emptyLineIndex = GetIndexEmptyLine(lines);

        var pageOrderingRules = lines[..emptyLineIndex].Select(GetPageOrderigRule);
        var pageSequences = lines[(emptyLineIndex + 1)..].Select(line => line.Split(',').Select(s => int.Parse(s)));

        var pageOrderingDictionary = new Dictionary<int, List<int>>();
        foreach (var (value1, value2) in pageOrderingRules)
        {
            if (pageOrderingDictionary.TryGetValue(value1, out List<int>? list)) 
                list.Add(value2);
            else 
                pageOrderingDictionary.Add(value1, new List<int>());
        }

        var validSequences = pageSequences.Where(sequence => IsValid(sequence, pageOrderingDictionary));
        return validSequences.Select(MiddleElement).Sum().ToString();
    }

    private bool IsValid(IEnumerable<int> sequence, Dictionary<int, List<int>> pageOrderingDictionary)
    {
        for(int i = 1; i < sequence.Count(); i++)
        {
            if (pageOrderingDictionary.TryGetValue(sequence.ElementAt(i), out List<int>? successors)
                && successors.Contains(sequence.ElementAt(i - 1))) return false; 
        }
        return true;
    }

    private int MiddleElement(IEnumerable<int> sequence)
    {
        var middleIndex = sequence.Count() / 2;
        return sequence.ElementAt(middleIndex);
    }

    private (int, int) GetPageOrderigRule(string line)
    {
        var numbers = line.Split('|');
        return (int.Parse(numbers[0]), int.Parse(numbers[1]));
    }

    private int GetIndexEmptyLine(string[] lines)
    {
        var index = 0;
        while (!string.IsNullOrEmpty(lines[index])) index++;
        return index;
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        throw new NotImplementedException();
    }

   
}
