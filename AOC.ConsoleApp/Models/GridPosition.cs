namespace AOC.ConsoleApp.Models;

public struct GridPosition<T>
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
        
        return new GridPosition<T>(neighbourRow, neighbourColumn, _grid);
    }

    public GridPosition<T> GetNeighbour((int, int) offset)
    {
        return GetNeighbour(offset.Item1, offset.Item2);
    }
}
