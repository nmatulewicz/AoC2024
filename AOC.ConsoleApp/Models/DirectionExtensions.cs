namespace AOC.ConsoleApp.Models;

public static class DirectionExtensions
{
    public static Direction ToDirection(this char directionChar)
    {
        return directionChar switch
        {
            '^' => Direction.Up,
            'v' => Direction.Down,
            '<' => Direction.Left,
            '>' => Direction.Right,
            _ => throw new InvalidOperationException($"Cannot convert character '{directionChar}' to {nameof(Direction)}")
        };
    }

    public static (int row, int column) ToOffset(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => (-1, 0),
            Direction.Down => (1, 0),
            Direction.Left => (0, -1),
            Direction.Right => (0, 1),
            _ => throw new InvalidOperationException($"Cannot process Direction {direction}"),
        };
    }
}
