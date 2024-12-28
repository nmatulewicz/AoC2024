namespace AOC.ConsoleApp.Solvers;

public class Day19Solver : AbstractSolver
{
    private IEnumerable<string> _towels;
    private IEnumerable<string> _designs;
    private IDictionary<string, long> _numberOfWaysDictionary;
    
    public Day19Solver(IEnumerable<string> lines) : base(lines)
    {
        _towels = GetTowels();
        _designs = GetDesigns();
        _numberOfWaysDictionary = new Dictionary<string, long>();
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
        long totalNumberOfWays = 0;
        foreach (var design in _designs)
        {
            totalNumberOfWays += NumberOfWays(design);
        }
        return totalNumberOfWays.ToString();
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

    private long NumberOfWays(string design)
    {
        if (string.IsNullOrEmpty(design)) return 1;
        if (_numberOfWaysDictionary.TryGetValue(design, out var numberOfWays)) return numberOfWays;

        numberOfWays = 0;
        foreach (var towel in _towels)
        {
            if (design.StartsWith(towel)) numberOfWays += NumberOfWays(design.Substring(towel.Length));
        }
        _numberOfWaysDictionary.Add(design, numberOfWays);

        return numberOfWays;
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
