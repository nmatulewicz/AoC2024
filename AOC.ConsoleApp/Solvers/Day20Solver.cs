
using AOC.ConsoleApp.Models;
using System.ComponentModel.Design;

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
        var shortestPath = map.GetShortestPath().ToList();

        var numberOfCheatsSavingAtLeast100Picoseconds = 0;
        foreach (var position in map.GetWalls())
        {
            var neighboursInShortestPath = position.GetAllDirectNeighbours().Where(shortestPath.Contains).ToArray();
            foreach (var neighbour1 in neighboursInShortestPath)
            {
                var indexInShortestPath1 = shortestPath.IndexOf(neighbour1);
                foreach (var neighbour2 in neighboursInShortestPath)
                {
                    var indexInShortestPath2 = shortestPath.IndexOf(neighbour2);

                    if (indexInShortestPath2 >= indexInShortestPath1 + 101) numberOfCheatsSavingAtLeast100Picoseconds++;
                }
            }
        }
        return numberOfCheatsSavingAtLeast100Picoseconds.ToString();
    }

    public override string SolveSecondChallenge()
    {
        var map = new Map(new Grid(_lines));
        var lengthShortestPathWithoutCheats = map.GetLengthShortestPath();
        var shortestPath = map.GetShortestPath().ToArray();

        var numberOfCheatsSavingAtLeast100Picoseconds = 0;
        foreach (var (position1, indexPosition1) in shortestPath.Select((position, index) => (position, index)))
        {
            var neighbouringWalls1 = position1.GetAllDirectNeighbours().Where(neighbour => neighbour.Value == Map.WALL);
            foreach (var position2 in shortestPath.Skip(indexPosition1 + 1 + 100))
            {
                var neighbouringWalls2 = position2.GetAllDirectNeighbours().Where(neighbour => neighbour.Value == Map.WALL);
                if (neighbouringWalls1.Any(wall1 => neighbouringWalls2.Any(wall2 => ManhattanDistance(wall1, wall2) <= 50 - 1 - 1)))
                    numberOfCheatsSavingAtLeast100Picoseconds++;
            }
        }
        return numberOfCheatsSavingAtLeast100Picoseconds.ToString();
        // 8024649 ==> Too high
        // 7921150 ==> Too high
    }

    private int ManhattanDistance(GridPosition<char> position1, GridPosition<char> position2)
    {
        return Math.Abs(position2.Row - position1.Row) + Math.Abs(position2.Column - position1.Column);
    }
}
