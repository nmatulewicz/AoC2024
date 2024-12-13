
using AOC.ConsoleApp.Models.Day11;

namespace AOC.ConsoleApp.Solvers;

public class Day11Solver : AbstractSolver
{
    private readonly IEnumerable<long> _numbers;

    public Day11Solver(IEnumerable<string> lines) : base(lines)
    {
        var line = lines.First();
        var numberStrings = line.Split(' ');
        _numbers = numberStrings.Select(long.Parse);
    }

    public override string SolveFirstChallenge()
    {
        var arrangement = new NumberArrangement(_numbers, 25);
        while (arrangement.BlinksLeft > 0) arrangement.Blink();

        return arrangement.Arrangement.Select(tuple => tuple.count).Sum().ToString();
    }

    public override string SolveSecondChallenge()
    {
        
        var arrangement = new NumberArrangement(_numbers, 75);
        while (arrangement.BlinksLeft > 0) arrangement.Blink();

        return arrangement.Arrangement.Select(tuple => tuple.count).Sum().ToString();
    }

}
