using System.Collections;

namespace AOC.ConsoleApp.Models;

public struct Grid<T> : IEnumerable<GridPosition<T>>
{
    public int NumberOfRows => _grid.Length;
    public int NumberOfColumns => _grid[0].Length;

    private T[][] _grid { get; set; }

    public Grid(IEnumerable<IEnumerable<T>> data)
    {
        _grid = data.Select(row => row.ToArray()).ToArray();
    }

    public T GetValue(int row, int column)
    {
        return _grid[row][column];
    }

    public void SetValue(int row, int column, T value)
    {
        _grid[row][column] = value;
    }

    public GridPosition<T> GetPosition(int row, int column)
    {
        return new GridPosition<T>(row, column, this);
    }

    IEnumerator<GridPosition<T>> IEnumerable<GridPosition<T>>.GetEnumerator()
    {
        return new GridEnumerator<T>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new GridEnumerator<T>(this);
    }
}
