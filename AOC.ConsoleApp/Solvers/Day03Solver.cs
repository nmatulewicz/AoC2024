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
        var completeInput = lines.Aggregate((line1, line2) => line1 + line2);
        var validPartsOfInput = FindValidParts(completeInput);

        var validMultiplicationStrings = validPartsOfInput.Select(FindValidMultiplications).SelectMany(m => m);
        var validMultiplications = validMultiplicationStrings.Select(GetMultiplicationFactors);

        return validMultiplications.Select(factors => factors.Item1 * factors.Item2).Sum().ToString();
    }

    private IEnumerable<string> FindValidParts(string input)
    {
        var dontPattern = @"don't\(\)";
        var doPattern = @"do\(\)";
        
        var validParts = new List<string>();
        var remainingString = input;
        while (true)
        {
            var dontMatch = Regex.Match(remainingString, dontPattern);
            if (!dontMatch.Success)
            {
                validParts.Add(remainingString);
                break;
            }
            validParts.Add(remainingString.Substring(0, dontMatch.Index));

            remainingString = remainingString.Substring(dontMatch.Index);

            var nextDoMatch = Regex.Match(remainingString, doPattern);
            if (!nextDoMatch.Success) break;

            remainingString = remainingString.Substring(nextDoMatch.Index);
        }

        return validParts;
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