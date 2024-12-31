
using AOC.ConsoleApp.Models.Day21;

namespace AOC.ConsoleApp.Solvers;

public class Day21Solver : AbstractSolver
{
    public Day21Solver(IEnumerable<string> lines) : base(lines)
    {
    }

    public override string SolveFirstChallenge()
    {
        long total = 0;
        foreach (var line in _lines)
        {
            var shortestSequenceLength = GetShortestSequenceLength(line);
            var numericPart = long.Parse(line.Substring(0, 3));
            var complexity = shortestSequenceLength * numericPart;
            total += complexity;
        }
        return total.ToString();
        // 112690 ==> Too high
    }

    private long GetShortestSequenceLength(string line, int levels = 3)
    {
        var shortestRoute = new NumericKeypad().GetShortestRoute(line, levels);
        return shortestRoute.Sum(tuple => (long) tuple.routePart.Length * tuple.count);
    }

    public override string SolveSecondChallenge()
    {
        long total = 0;
        foreach (var line in _lines)
        {
            var shortestSequenceLength = GetShortestSequenceLength(line, 26);
            var numericPart = long.Parse(line.Substring(0, 3));
            var complexity = shortestSequenceLength * numericPart;
            total += complexity;
        }
        return total.ToString();
    }
}
