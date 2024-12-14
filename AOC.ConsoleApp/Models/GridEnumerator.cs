using System.Collections;

namespace AOC.ConsoleApp.Models;

public class GridEnumerator<T> : IEnumerator<GridPosition<T>>
{
    public GridPosition<T> Current => _grid.GetPosition(_currentRow, _currentColumn);

    object IEnumerator.Current => Current;

    private readonly Grid<T> _grid;
    private int _currentRow;
    private int _currentColumn;

    public GridEnumerator(Grid<T> grid)
    {
        _grid = grid;
        _currentRow = 0;
        _currentColumn = -1;
    }

    public void Dispose()
    {}

    public bool MoveNext()
    {
        // try moving to the right
        _currentColumn++;
        if (Current.IsValidPosition) 
            return true;
        
        // try moving to the start of the next row
        _currentRow++;
        _currentColumn = 0;
        if (Current.IsValidPosition)
            return true;

        return false;
    }

    public void Reset()
    {
        _currentRow = 0;
        _currentColumn = 0;
    }
}
