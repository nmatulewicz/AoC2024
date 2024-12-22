using AOC.ConsoleApp.Models;
using AOC.ConsoleApp.Models.Day16;

namespace AOC.ConsoleApp.Solvers;

public class Day16Solver : AbstractSolver
{
    private Maze _maze;
    public Day16Solver(IEnumerable<string> lines) : base(lines)
    {
        var grid = new Grid(lines);
        _maze = new Maze(grid);
    }

    public override string SolveFirstChallenge()
    {
        var lowestPossibleScore = _maze.FindLowestPossibleScore();
        return lowestPossibleScore.ToString();
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
