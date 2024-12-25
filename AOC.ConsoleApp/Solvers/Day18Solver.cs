using AOC.ConsoleApp.Models;

namespace AOC.ConsoleApp.Solvers;

public class Day18Solver : AbstractSolver
{
    private IEnumerable<(int row, int column)> _corruptedBytes;
    public Day18Solver(IEnumerable<string> lines) : base(lines)
    {
        _corruptedBytes = lines.Select(line => (int.Parse(line.Split(',')[1]), int.Parse(line.Split(',')[0])));
    }

    public override string SolveFirstChallenge()
    {
        var first1024Bytes = _corruptedBytes.Take(1024);
        var grid = Grid.GenerateGrid('.', 71, 71);
        foreach (var corruptedByte in first1024Bytes)
        {
            grid.SetValue(corruptedByte.row, corruptedByte.column, '#');
        }
        var lengthShortestPath = FindLengthShortestPath(grid.GetPosition(0, 0), grid.GetPosition(69, 69), grid);
        return lengthShortestPath.ToString();
    }

    private int FindLengthShortestPath(GridPosition<char> start, GridPosition<char> end, Grid grid)
    {
        var priorityQueue = new PriorityQueue<GridPosition<char>, int>();
        priorityQueue.Enqueue(start, 0);

        var bestLengthDictionary = grid.ToDictionary(position => position, position => int.MaxValue);
        bestLengthDictionary[start] = 0;

        while(priorityQueue.TryDequeue(out var position, out var pathLength))
        {
            if (position.Row == 70 && position.Column == 70) return pathLength;

            var neighbours = position.GetAllDirectNeighbours().Where(neighbour => neighbour.Value != '#');
            foreach (var neighbour in neighbours)
            {
                var currentPathLength = bestLengthDictionary[neighbour];
                var newPathLength = pathLength + 1;
                if (newPathLength >= currentPathLength) continue;

                bestLengthDictionary[neighbour] = newPathLength;
                priorityQueue.Enqueue(neighbour, newPathLength);
            }
        }

        throw new Exception("Queue should not be empty before reaching end.");
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
