
using AOC.ConsoleApp.Models;
using AOC.ConsoleApp.Models.Day15;

namespace AOC.ConsoleApp.Solvers;

public class Day15Solver : AbstractSolver
{
    private Warehouse _warehouse;
    private Warehouse _wideWarehouse;
    private IEnumerable<Direction> _moves;

    public Day15Solver(IEnumerable<string> lines) : base(lines)
    {
        _warehouse = GetWarehouse(lines);
        _wideWarehouse = GetWideWarehouse(lines);
        _moves = GetMoves(lines);
    }

    public override string SolveFirstChallenge()
    {
        foreach(var direction in _moves)
        {
            _warehouse.TryMoveRobot(direction);
        }

        var gpsCoordinates = _warehouse.GetAllBoxesGpsCoordinates();
        return gpsCoordinates.Sum().ToString();
    }

    public override string SolveSecondChallenge()
    {
        foreach (var direction in _moves)
        {
            _wideWarehouse.TryMoveRobot(direction);
        }

        var gpsCoordinates = _wideWarehouse.GetAllBoxesGpsCoordinates();
        return gpsCoordinates.Sum().ToString();
    }

    private IEnumerable<Direction> GetMoves(IEnumerable<string> lines)
    {
        var movesInputLines = new List<char>();
        var lineContainsMoves = false;
        foreach (var line in lines)
        {
            if (!lineContainsMoves)
            {
                if (string.IsNullOrWhiteSpace(line)) lineContainsMoves = true;
                continue;
            }

            movesInputLines.AddRange(line);
        }
        return movesInputLines.Select(character => character.ToDirection());
    }

    private Warehouse GetWarehouse(IEnumerable<string> lines)
    {
        var gridInputLines = new List<string>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line)) break;
            gridInputLines.Add(line);
        }
        var grid = new Grid(gridInputLines);
        return new Warehouse(grid);
    }

    private Warehouse GetWideWarehouse(IEnumerable<string> lines)
    {
        var gridInputLines = new List<string>();

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line)) break;
            var wideLine = line
                .Replace("#", "##")
                .Replace("O", "[]")
                .Replace(".", "..")
                .Replace("@", "@.");
            gridInputLines.Add(wideLine);
        }
        var grid = new Grid(gridInputLines);
        return new Warehouse(grid);
    }
}
