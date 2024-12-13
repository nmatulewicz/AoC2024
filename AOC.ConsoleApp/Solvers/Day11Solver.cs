
using AOC.ConsoleApp.Models.Day11;

namespace AOC.ConsoleApp.Solvers;

public class Day11Solver : AbstractSolver
{
    private readonly IEnumerable<long> _numbers;
    private NumberArrangement _numberArrangement => new NumberArrangement(_numbers, 25);

    public Day11Solver(IEnumerable<string> lines) : base(lines)
    {
        var line = lines.First();
        var numberStrings = line.Split(' ');
        _numbers = numberStrings.Select(long.Parse);
    }

    public override string SolveFirstChallenge()
    {
        var arrangement = _numberArrangement;
        while (arrangement.BlinksLeft > 0) arrangement.Blink();

        return arrangement.Arrangement.Count().ToString();
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }

}
