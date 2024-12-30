
using AOC.ConsoleApp.Models;

namespace AOC.ConsoleApp.Solvers;
public class Day20Solver : AbstractSolver
{
    public const int TIME_TO_SAVE = 100;
    public const int CHEATING_TIME = 20;
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

                    if (indexInShortestPath2 >= indexInShortestPath1 + TIME_TO_SAVE + 1) numberOfCheatsSavingAtLeast100Picoseconds++;
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
            foreach (var (position2, indexPosition2) in shortestPath.Select((position, index) => (position, index)))
            {
                var manhattanDistance = ManhattanDistance(position1, position2);
                if (manhattanDistance > CHEATING_TIME) continue;

                var originalTimeDelta = indexPosition2 - indexPosition1;
                var savedTime = originalTimeDelta - manhattanDistance;

                if (savedTime >= TIME_TO_SAVE) numberOfCheatsSavingAtLeast100Picoseconds++;
            }
        }
        return numberOfCheatsSavingAtLeast100Picoseconds.ToString();
    }

    private int ManhattanDistance(GridPosition<char> position1, GridPosition<char> position2)
    {
        return Math.Abs(position2.Row - position1.Row) + Math.Abs(position2.Column - position1.Column);
    }
}
