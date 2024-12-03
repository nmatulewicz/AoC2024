using System.Text.RegularExpressions;

namespace AOC.ConsoleApp.Solvers;

public class Day03Solver : ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        var completeInput = lines.Aggregate((line1, line2) => line1 + line2);
        
        IEnumerable<string> validMultiplicationStrings = FindValidMultiplications(completeInput);
        IEnumerable<(int, int)> validMultiplications = validMultiplicationStrings.Select(GetMultiplicationFactors);

        return validMultiplications.Select(factors => factors.Item1 * factors.Item2).Sum().ToString();
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<string> FindValidMultiplications(string input)
    {
        var pattern = @"mul\([0-9]+,[0-9]+\)";
        var matches = Regex.Matches(input, pattern);
        return matches.Select(match => match.Value);
    }

    private (int, int) GetMultiplicationFactors(string multiplicationString)
    {
        var pattern = @"[0-9]+";

        var matches = Regex.Matches(multiplicationString, pattern);
        if (matches.Count != 2)
            throw new Exception($"Something went wrong. " +
                $"Exactly two numbers were expected in '{multiplicationString}', but {matches.Count} matches were found.");

        return (int.Parse(matches[0].Value), int.Parse(matches[1].Value));
    }
}