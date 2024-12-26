using AOC.ConsoleApp.Models;

namespace AOC.ConsoleApp.Solvers;

public class Day19Solver : AbstractSolver
{
    private IEnumerable<string> _towels;
    private IEnumerable<string> _designs;

    public Day19Solver(IEnumerable<string> lines) : base(lines)
    {
        _towels = GetTowels();
        _designs = GetDesigns();
    }

    public override string SolveFirstChallenge()
    {
        var numberOfPossibleDesigns = 0;
        foreach (var design in _designs)
        {
            if (IsPossible(design)) numberOfPossibleDesigns++;
        }
        return numberOfPossibleDesigns.ToString();
    }

    public override string SolveSecondChallenge()
    { 
        throw new NotImplementedException();
    }

    private bool IsPossible(string design)
    {
        if (string.IsNullOrEmpty(design)) return true;

        foreach (var towel in _towels)
        {
            if (design.StartsWith(towel) && IsPossible(design.Substring(towel.Length))) 
                return true;
        }
        return false;
    }

    private IEnumerable<string> GetDesigns()
    {
        var indexFirstDesign = _lines
            .Select((line, index) => (line, index))
            .Where(indexedLine => string.IsNullOrWhiteSpace(indexedLine.line))
            .Select(indexedLine => indexedLine.index)
            .First() + 1;

        return _lines.Skip(indexFirstDesign);
    }

    private IEnumerable<string> GetTowels()
    {
        var towels = new List<string>();
        foreach (var line in _lines)
        {
            if (string.IsNullOrWhiteSpace(line)) return towels;
            var towelsFromLine = line.Split(", ");
            towels.AddRange(towelsFromLine);
        }
        return towels;
    }
}
