namespace AOC.ConsoleApp.Models;

public class Map
{
    public const char START = 'S';
    public const char END = 'E';
    public const char WALL = '#';

    private readonly Grid _map;

    public Map(Grid map)
    {
        _map = map;
    }

    public int GetLengthShortestPath()
    {
        var start = _map.Where(position => position.Value ==  START).First();
        var end = _map.Where(position => position.Value == END).First();

        var priorityQueue = new PriorityQueue<GridPosition<char>, int>();
        priorityQueue.Enqueue(start, 0);

        var bestLengthDictionary = _map.ToDictionary(position => position, position => int.MaxValue);
        bestLengthDictionary[start] = 0;

        while (priorityQueue.TryDequeue(out var position, out var pathLength))
        {
            if (position.Equals(end)) return pathLength;

            var neighbours = position.GetAllDirectNeighbours().Where(neighbour => neighbour.Value != WALL);
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
}
