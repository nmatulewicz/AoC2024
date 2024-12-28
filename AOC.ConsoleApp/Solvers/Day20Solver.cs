
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
            //var neighbouringWalls1 = position1.GetAllDirectNeighbours().Where(neighbour => neighbour.Value == Map.WALL);
            foreach (var (position2, indexPosition2) in shortestPath.Select((position, index) => (position, index)))
            {
                var originalTimeDelta = indexPosition2 - indexPosition1;
                if (originalTimeDelta < 102) continue;

                var neighbours1 = new List<GridPosition<char>>();
                var neighbours2 = new List<GridPosition<char>>();
                if (position2.Row > position1.Row)
                {
                    neighbours1.Add(position1.GetNeighbour(1, 0));
                    neighbours2.Add(position2.GetNeighbour(-1, 0));
                }
                if (position2.Row < position1.Row)
                {
                    neighbours1.Add(position1.GetNeighbour(-1, 0));
                    neighbours2.Add(position2.GetNeighbour(1, 0));
                }
                if (position2.Column > position1.Column)
                {
                    neighbours1.Add(position1.GetNeighbour(0, 1));
                    neighbours2.Add(position2.GetNeighbour(0, -1));
                }
                if (position2.Column < position1.Column)
                {
                    neighbours1.Add(position1.GetNeighbour(0, -1));
                    neighbours2.Add(position2.GetNeighbour(0, 1));
                }

                if (neighbours1.Any(neighbour1 => neighbours2.Any(neighbour2 =>
                {
                    if (neighbour1.Value != Map.WALL || neighbour2.Value != Map.WALL) return false;
                    var manhattanDistance = ManhattanDistance(neighbour1, neighbour2);
                    if (manhattanDistance > 50 - 2) return false;

                    var savedTime = originalTimeDelta - manhattanDistance;
                    return savedTime >= 100;
                })))
                    numberOfCheatsSavingAtLeast100Picoseconds++;



                //var manhattanDistance = ManhattanDistance(position1, position2);
                //if (manhattanDistance > 50) continue;

                //var originalTimeDelta = indexPosition2 - indexPosition1;
                //var savedTime = originalTimeDelta - manhattanDistance;

                //if (savedTime >= 100) numberOfCheatsSavingAtLeast100Picoseconds++;
            }
        }
        return numberOfCheatsSavingAtLeast100Picoseconds.ToString();
        // 8024649 ==> Too high
        // 7921150 ==> Too high
        // 7813830 ==> Too high
        // 7722921 ==> Not right
        // 6472245 ==> Not right
    }

    private int ManhattanDistance(GridPosition<char> position1, GridPosition<char> position2)
    {
        return Math.Abs(position2.Row - position1.Row) + Math.Abs(position2.Column - position1.Column);
    }
}
