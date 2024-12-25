using System.Collections;
using System.Text;

namespace AOC.ConsoleApp.Models;

public class Grid : Grid<char>
{
    public Grid(IEnumerable<IEnumerable<char>> data) : base(data)
    {
    }

    public static Grid GenerateGrid(char filling, int rows, int columns) 
    {
        var line = new string(filling, columns);
        var lines = new List<string>();
        while (lines.Count < rows) lines.Add(line);
        return new Grid(lines);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var row in _grid)
        {
            stringBuilder.AppendLine(new string(row));
        }
        return stringBuilder.ToString();
    }
}
public class Grid<T> : IEnumerable<GridPosition<T>>
{
    public int NumberOfRows => _grid.Length;
    public int NumberOfColumns => _grid[0].Length;

    protected T[][] _grid { get; set; }

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
