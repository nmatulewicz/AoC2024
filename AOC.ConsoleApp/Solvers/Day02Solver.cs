namespace AOC.ConsoleApp.Solvers;

public class Day02Solver : ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        IEnumerable<IEnumerable<int>> numberSequences = GetNumberSequences(lines);

        var saveSequences = numberSequences.Where(sequence => IsSafe(sequence));

        return saveSequences.Count().ToString();
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        IEnumerable<IEnumerable<int>> numberSequences = GetNumberSequences(lines);

        var saveSequences = numberSequences.Where(sequence => IsSafe(sequence, useProblemDampner: true));

        return saveSequences.Count().ToString();
    }

    private static IEnumerable<IEnumerable<int>> GetNumberSequences(IEnumerable<string> lines)
    {
        return lines.Select(line => line.Split(" ").Select(number => int.Parse(number)));
    }

    private bool IsSafe(IEnumerable<int> numbers, bool useProblemDampner = false)
    {
        if (IsStrictlyDecreasing(numbers) || IsStrictlyIncreasing(numbers))
            return true;

        if (!useProblemDampner) return false;

        var numbersAsArray = numbers.ToArray();
        for (var i = 0; i < numbersAsArray.Length; i++)
        {
            var sequenceWithoutIthNumber = numbersAsArray[..i].Concat(numbersAsArray[(i + 1)..]);
            if (IsSafe(sequenceWithoutIthNumber)) return true;
        }

        return false;
    }

    private bool IsStrictlyIncreasing(IEnumerable<int> numbers)
    {
        if (numbers.Count() < 2) return true;

        var previousNumber = numbers.First();
        foreach (var number in numbers.Skip(1))
        {
            var difference = number - previousNumber;
            if (difference < 1 || difference > 3) return false;
            previousNumber = number;
        }

        return true;
    }

    private bool IsStrictlyDecreasing(IEnumerable<int> numbers)
    {

        if (numbers.Count() < 2) return true;

        var previousNumber = numbers.First();
        foreach (var number in numbers.Skip(1))
        {
            var difference = number - previousNumber;
            if (difference < -3 || difference > -1) return false;
            previousNumber = number;
        }

        return true;
    }
}
