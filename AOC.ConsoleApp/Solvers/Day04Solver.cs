using AOC.ConsoleApp.Models;

namespace AOC.ConsoleApp.Solvers;
public class Day04Solver : ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        var grid = new Grid<char>(lines.Select(line => line.ToCharArray()));

        var totalCount = 0;
        foreach (var position in grid)
        {
            totalCount += CountHowManyTimesItIsTheXOfXmas(position);
        }
        return totalCount.ToString();
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        var grid = new Grid<char>(lines.Select(line => line.ToCharArray()));

        var totalCount = 0;
        foreach (var position in grid)
        {
            if (IsAOfXMas(position)) totalCount++;
        }
        return totalCount.ToString();
    }

    private bool IsAOfXMas(GridPosition<char> position)
    {
        if (position.Value != 'A') return false;

        var topLeft = position.GetNeighbour(-1, -1);
        var topRight = position.GetNeighbour(-1, 1);
        var bottomLeft = position.GetNeighbour(1, -1);
        var bottomRight = position.GetNeighbour(1, 1);

        if (!topLeft.IsValidPosition || !topRight.IsValidPosition || !bottomLeft.IsValidPosition || !bottomRight.IsValidPosition)
            return false;

        if (!(topLeft.Value == 'M' && bottomRight.Value == 'S')
            && !(topLeft.Value == 'S' && bottomRight.Value == 'M'))
            return false;

        if (!(topRight.Value == 'M' && bottomLeft.Value == 'S')
            && !(topRight.Value == 'S' && bottomLeft.Value == 'M'))
            return false;

        return true;
    }

    private int CountHowManyTimesItIsTheXOfXmas(GridPosition<char> position)
    {
        if (position.Value != 'X') return 0;

        var offsets = new List<(int, int)>
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1),
            (1, -1), (1, 0), (1, 1),
        };

        var count = 0;
        foreach (var offset in offsets)
        {
            var positionM = position.GetNeighbour(offset);
            if (!positionM.IsValidPosition || positionM.Value != 'M') continue;

            var positionA = positionM.GetNeighbour(offset);
            if (!positionA.IsValidPosition || positionA.Value != 'A') continue;

            var positionS = positionA.GetNeighbour(offset);
            if (positionS.IsValidPosition &&  positionS.Value == 'S') count++;
        }

        return count;
    }
}
