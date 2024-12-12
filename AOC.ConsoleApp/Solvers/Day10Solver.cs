
using AOC.ConsoleApp.Models;

namespace AOC.ConsoleApp.Solvers;

public class Day10Solver : AbstractSolver
{
    private readonly Grid _grid;
    public Day10Solver(IEnumerable<string> lines) : base(lines)
    {
        _grid = new Grid(lines);
    }

    public override string SolveFirstChallenge()
    {
        var zeros = _grid.Where(position => position.Value == '0');

        int totalTrailheadCount = 0;
        foreach (var zeroPosition in zeros)
        {
            var reachableNines = GetReachableNines(zeroPosition);
            var trailheadCount = reachableNines.Distinct().Count();
            totalTrailheadCount += trailheadCount;
        }
        return totalTrailheadCount.ToString();
    }

    private IEnumerable<GridPosition<char>> GetReachableNines(GridPosition<char> position)
    {
        var currentNumber = int.Parse(position.Value.ToString());
        if (currentNumber == 9) return [position];

        var nextNumber = currentNumber + 1;

        var neighboursToFurtherInvestigate = position.GetAllDirectNeighbours()
            .Where(neighbour => int.Parse(neighbour.Value.ToString()) == nextNumber);

        return neighboursToFurtherInvestigate.SelectMany(GetReachableNines);
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
