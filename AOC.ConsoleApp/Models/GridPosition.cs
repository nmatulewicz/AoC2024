namespace AOC.ConsoleApp.Models;

public struct GridPosition<T>
{
    public T Value => _grid.GetValue(_row, _column);
    public bool IsValidPosition => _row >= 0
            && _row < _grid.NumberOfRows
            && _column >= 0
            && _column < _grid.NumberOfColumns;

    private readonly Grid<T> _grid;
    private readonly int _row;
    private readonly int _column;

    public GridPosition(int row, int column, Grid<T> grid)
    {
        _row = row;
        _column = column;
        _grid = grid;
    }

    public GridPosition<T> GetNeighbour(int rowOffset, int columnOffset)
    {
        var neighbourRow = _row + rowOffset;
        var neighbourColumn = _column + columnOffset;
        
        return new GridPosition<T>(neighbourRow, neighbourColumn, _grid);
    }

    public GridPosition<T> GetNeighbour((int, int) offset)
    {
        return GetNeighbour(offset.Item1, offset.Item2);
    }
}
