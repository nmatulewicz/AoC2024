using AOC.ConsoleApp.Models.Day05;

namespace AOC.ConsoleApp.Solvers;

public class Day05Solver : ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        return SolveFirstChallenge(lines.ToArray());
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        return SolveSecondChallenge(lines.ToArray());
    }


    public string SolveFirstChallenge(string[] lines)
    {
        var emptyLineIndex = GetIndexEmptyLine(lines);

        var pageOrderingRules = lines[..emptyLineIndex].Select(GetPageOrderigRule);

        var pageOrderings = GetPageOrderings(lines[(emptyLineIndex + 1)..], pageOrderingRules);

        var validOrderings = pageOrderings.Where(ordering => ordering.IsValid());
        return validOrderings.Select(ordering => ordering.MiddlePage.PageNumber).Sum().ToString();
    }

    private IEnumerable<PageOrdering> GetPageOrderings(string[] lines, IEnumerable<(int, int)> rules)
    {
        var pageOrderings = new List<PageOrdering>();
        foreach (var line in lines)
        {
            var pageNumbers = line.Split(',').Select(int.Parse);
            var pageOrdering = new PageOrdering(pageNumbers, rules);
            pageOrderings.Add(pageOrdering);
        }
        return pageOrderings;
    }

    public string SolveSecondChallenge(string[] lines)
    {
        throw new NotImplementedException();
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
}
