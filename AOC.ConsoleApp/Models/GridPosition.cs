namespace AOC.ConsoleApp.Models;

public class GridPosition<T> : IEquatable<GridPosition<T>>
{
    public readonly int Row;
    public readonly int Column;
    public T Value
    {
        get => _grid.GetValue(Row, Column);
        set => _grid.SetValue(Row, Column, value);
    }

    public bool IsValidPosition => Row >= 0
            && Row < _grid.NumberOfRows
            && Column >= 0
            && Column < _grid.NumberOfColumns;

    private readonly Grid<T> _grid;

    public GridPosition(int row, int column, Grid<T> grid)
    {
        Row = row;
        Column = column;
        _grid = grid;
    }

    public GridPosition<T> GetNeighbour(int rowOffset, int columnOffset)
    {
        var neighbourRow = Row + rowOffset;
        var neighbourColumn = Column + columnOffset;
        
        return _grid.GetPosition(neighbourRow, neighbourColumn);
    }

    public GridPosition<T> GetNeighbour((int, int) offset)
    {
        return GetNeighbour(offset.Item1, offset.Item2);
    }

    public IEnumerable<GridPosition<T>> GetAllDirectNeighbours(bool includeDiagonalNeigbours = false)
    {
        var offsets = new List<(int, int)> { (0, -1), (0, 1), (1, 0), (-1, 0) };
        if (includeDiagonalNeigbours) offsets.AddRange([ (1, -1),(1, 1), (-1, -1), (-1, 1)]);

        return offsets
            .Select(GetNeighbour)
            .Where(neighbouringPosition => neighbouringPosition.IsValidPosition);
    }

    public override bool Equals(object? obj)
    {
        return obj is GridPosition<T> other && Equals(other);
    }

    public override string ToString()
    {
        return $"({Row}, {Column}): {Value}";
    }

    public bool Equals(GridPosition<T>? other)
    {
        if (other == null) return false;

        return Column == other.Column
            && Row == other.Row
            && _grid == other._grid;
    }

    public override int GetHashCode()
    {
        return Row.GetHashCode() ^ Column.GetHashCode();
    }
}
