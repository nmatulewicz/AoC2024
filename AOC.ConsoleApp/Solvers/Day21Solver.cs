
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
        var numericKeypad = new NumericKeypad();
        var buttonsPressedByRobot1 = line.Aggregate(string.Empty, (totalRoute, nextDestination) => totalRoute + numericKeypad.MoveRobotTo(nextDestination));

        var firstDirectionalKeypad = new DirectionalKeypad();
        var buttonsPressedByRobot2 = buttonsPressedByRobot1.Aggregate(string.Empty, (totalRoute, nextDestination) => totalRoute + firstDirectionalKeypad.MoveRobotTo(nextDestination));

        var secondDirectionalKeypad = new DirectionalKeypad();
        var buttonsPressedByHuman = buttonsPressedByRobot2.Aggregate(string.Empty, (totalRoute, nextDestination) => totalRoute + secondDirectionalKeypad.MoveRobotTo(nextDestination));

        return buttonsPressedByHuman;
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
