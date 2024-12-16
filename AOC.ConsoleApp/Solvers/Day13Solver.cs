using AOC.ConsoleApp.Models.Day13;

namespace AOC.ConsoleApp.Solvers;

public class Day13Solver : AbstractSolver
{
    private IEnumerable<ClawMachine> _machines;
    public Day13Solver(IEnumerable<string> lines) : base(lines)
    {
        var linesAsArray = lines.ToArray();
        _machines = GetClawMachines(linesAsArray);
    }

    public override string SolveFirstChallenge()
    {
        var results = _machines.Select(machine =>
        {
            var success = machine.TryWinPrize(out var countA, out var countB);
            return (success, countA, countB);
        });
        var wins = results.Where(tuple => tuple.success);
        var costOfWins = wins.Select(tuple => 3 * tuple.countA + 1 * tuple.countB); 
        return costOfWins.Sum().ToString();
    }

    public override string SolveSecondChallenge()
    {
        foreach (var machine in _machines)
        {
            machine.Prize.X += 10_000_000_000_000;
            machine.Prize.Y += 10_000_000_000_000;
        }
        return SolveFirstChallenge();
    }

    private IEnumerable<ClawMachine> GetClawMachines(string[] lines)
    {
        var machines = new List<ClawMachine>();
        for (int i = 0; i < lines.Count(); i += 4)
        {
            machines.Add(new ClawMachine(
                buttonA: ToButton(lines[i]),
                buttonB: ToButton(lines[i + 1]),
                prize: ToPrize(lines[i + 2])));
        }
        return machines;
    }

    private static Button ToButton(string line)
    {
        var deltaXString = line.Split(": X+")[1].Split(", Y+")[0];
        var deltaYString = line.Split(": X+")[1].Split(", Y+")[1];

        return new Button
        {
            DeltaX = int.Parse(deltaXString),
            DeltaY = int.Parse(deltaYString),
        };
    }

    private static Prize ToPrize(string line)
    {
        var xString = line.Split(": X=")[1].Split(", Y=")[0];
        var yString = line.Split(": X=")[1].Split(", Y=")[1];

        return new Prize
        {
            X = int.Parse(xString),
            Y = int.Parse(yString),
        };
    }
}
