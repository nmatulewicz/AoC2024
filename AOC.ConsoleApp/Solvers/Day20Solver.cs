
using AOC.ConsoleApp.Models;

namespace AOC.ConsoleApp.Solvers;

public class Day20Solver : AbstractSolver
{
    public Day20Solver(IEnumerable<string> lines) : base(lines)
    {

    }

    public override string SolveFirstChallenge()
    {
        var map = new Map(new Grid(_lines));
        var lengthShortestPathWithoutCheats = map.GetLengthShortestPath();

        var numberOfCheatsSavingAtLeast100Picoseconds = 0;
        foreach (var position in map.GetWalls())
        {
            position.Value = Map.EMPTY_SPACE;
            var lengthShortestPath = map.GetLengthShortestPath();
            var difference = lengthShortestPathWithoutCheats - lengthShortestPath;
            if (difference >= 100)
            {
                numberOfCheatsSavingAtLeast100Picoseconds++;
            }
            position.Value = Map.WALL;
        }
        return numberOfCheatsSavingAtLeast100Picoseconds.ToString();
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
