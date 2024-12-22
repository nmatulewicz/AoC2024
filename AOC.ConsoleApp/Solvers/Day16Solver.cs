using AOC.ConsoleApp.Models;
using AOC.ConsoleApp.Models.Day16;

namespace AOC.ConsoleApp.Solvers;

public class Day16Solver : AbstractSolver
{
    private Grid _grid;

    public Day16Solver(IEnumerable<string> lines) : base(lines)
    {
        _grid = new Grid(lines);
    }

    public override string SolveFirstChallenge()
    {
        var maze = new Maze(_grid);
        var lowestPossibleScore = maze.FindLowestPossibleScore();
        return lowestPossibleScore.ToString();
    }

    public override string SolveSecondChallenge()
    {
        var maze = new Maze(_grid);
        var shortestPaths = maze.FindAllShortestPaths();

        var distinctPositions = shortestPaths
            .SelectMany(path => path)
            .DistinctBy(position => (position.Row, position.Column));

        return distinctPositions.Count().ToString();
    }
}
