
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
        throw new NotImplementedException();
    }
}
