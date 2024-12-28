
using System.Linq;

namespace AOC.ConsoleApp.Models;

public class Map
{
    public const char START = 'S';
    public const char END = 'E';
    public const char WALL = '#';
    public const char EMPTY_SPACE = '.';

    private readonly Grid _map;

    public Map(Grid map)
    {
        _map = map;
    }

    public int GetLengthShortestPath()
    {
        return GetShortestPath().Count() - 1;
    }

    public IEnumerable<GridPosition<char>> GetWalls()
    {
        return _map.Where(position => position.Value == WALL);
    }

    public IEnumerable<GridPosition<char>> GetShortestPath()
    {
        var start = _map.Where(position => position.Value == START).First();
        var end = _map.Where(position => position.Value == END).First();

        var priorityQueue = new PriorityQueue<GridPosition<char>, int>();
        priorityQueue.Enqueue(start, 0);

        var bestLengthDictionary = _map.ToDictionary<GridPosition<char>, GridPosition<char>, (int bestLength, GridPosition<char>? previousPosition)>(
            position => position, 
            position => (int.MaxValue, null));
        bestLengthDictionary[start] = (0, null);

        while (priorityQueue.TryDequeue(out var position, out var pathLength))
        {
            if (position.Equals(end)) return DeterminePath(end, bestLengthDictionary);

            var neighbours = position.GetAllDirectNeighbours().Where(neighbour => neighbour.Value != WALL);
            foreach (var neighbour in neighbours)
            {
                var currentPathLength = bestLengthDictionary[neighbour].bestLength;
                var newPathLength = pathLength + 1;
                if (newPathLength >= currentPathLength) continue;

                bestLengthDictionary[neighbour] = (newPathLength, position);
                priorityQueue.Enqueue(neighbour, newPathLength);
            }
        }

        throw new Exception("Queue should not be empty before reaching end.");
    }

    private IEnumerable<GridPosition<char>> DeterminePath(GridPosition<char> end, Dictionary<GridPosition<char>, (int bestLength, GridPosition<char>? previousPosition)> bestLengthDictionary)
    {
        var bestPathLength = bestLengthDictionary[end].bestLength;
        var bestPathArray = new GridPosition<char>[bestPathLength + 1];

        var currentPosition = end;
        bestPathArray[bestPathLength] = currentPosition;
        while (bestLengthDictionary[currentPosition] is (int bestLength, GridPosition<char> previousPosition))
        {
            bestPathArray[bestLength] = currentPosition;
            currentPosition = previousPosition;
        }

        bestPathArray[0] = currentPosition;
        return bestPathArray;
    }
}
