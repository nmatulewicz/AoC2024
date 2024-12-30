
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
            var shortestSequence = GetShortestSequence(line);
            var numericPart = long.Parse(line.Substring(0, 3));
            var complexity = shortestSequence.Count() * numericPart;
            total += complexity;
        }
        return total.ToString();
        // 112690 ==> Too high
    }

    private IEnumerable<char> GetShortestSequence(string line)
    {
        return new NumericKeypad().GetShortestRoute(line, levels: 3);
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
