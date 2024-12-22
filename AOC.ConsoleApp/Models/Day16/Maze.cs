namespace AOC.ConsoleApp.Models.Day16;

public class Maze
{
    public const char WALL = '#';
    public const char START_POSITION = 'S';
    public const char END_POSITION = 'E';

    private readonly Grid _grid;
    private readonly Dictionary<(GridPosition<char> position, Direction incomingDirection), int> _scorePerGridPositionAndIncomingDirection;
    private readonly GridPosition<char> _startPosition;
    private readonly GridPosition<char> _endPosition;

    public Maze(Grid grid)
    {
        _grid = grid;
        _startPosition = grid.First(position => position.Value is START_POSITION);
        _endPosition = grid.First(position => position.Value is END_POSITION);

        _scorePerGridPositionAndIncomingDirection = InitializeScoreDictionaryWithMaxIntValue();
        _scorePerGridPositionAndIncomingDirection[(_startPosition, Direction.Right)] = 0;
    }

    public int FindLowestPossibleScore()
    {
        var priorityQueue = new PriorityQueue<(GridPosition<char> position, Direction incomingDirection), int>();
        priorityQueue.Enqueue((_startPosition, Direction.Right), 0);
        
        while (priorityQueue.Count > 0)
        {
            var (position, incomingDirection) = priorityQueue.Dequeue();
            var score = _scorePerGridPositionAndIncomingDirection[(position, incomingDirection)];
            if (IsEnd(position)) return score;

            foreach (var outgoingDirection in Enum.GetValues<Direction>())
            {
                var nextPosition = position.GetNeighbour(outgoingDirection.ToOffset());
                if (!nextPosition.IsValidPosition || IsWall(nextPosition)) continue;

                var currentScoreOfNextPosition = _scorePerGridPositionAndIncomingDirection[(nextPosition, outgoingDirection)];
                var newScoreOfNextPosition = outgoingDirection == incomingDirection ? score + 1 : score + 1001;

                if (newScoreOfNextPosition >= currentScoreOfNextPosition) continue;
                
                _scorePerGridPositionAndIncomingDirection[(nextPosition, outgoingDirection)] = newScoreOfNextPosition;
                priorityQueue.Enqueue((nextPosition, outgoingDirection), newScoreOfNextPosition);
            }
        }

        throw new Exception("Something went wrong. Priority queue should not be empty before finding end position.");
    }

    public IEnumerable<IEnumerable<GridPosition<char>>> FindAllShortestPaths()
    {
        var shortestPathScore = FindLowestPossibleScore();

        var queue = new Queue<(IEnumerable<GridPosition<char>> path, Direction incomingDirection, int score)>();
        queue.Enqueue(([_startPosition], Direction.Right, 0));

        var shortestPaths = new List<IEnumerable<GridPosition<char>>>();
        while (queue.Count > 0)
        {
            var (path, incomingDirection, score) = queue.Dequeue();
            var currentPosition = path.Last();

            if (IsEnd(currentPosition) && score == shortestPathScore) shortestPaths.Add(path);

            foreach (var outgoingDirection in Enum.GetValues<Direction>())
            {
                var nextPosition = currentPosition.GetNeighbour(outgoingDirection.ToOffset());
                if (!nextPosition.IsValidPosition || IsWall(nextPosition)) continue;
                if (path.Any(position => position.Row == nextPosition.Row && position.Column == nextPosition.Column)) continue;
                
                var currentScoreOfNextPosition = _scorePerGridPositionAndIncomingDirection[(nextPosition, outgoingDirection)];
                var newScoreOfNextPosition = outgoingDirection == incomingDirection ? score + 1 : score + 1001;

                if (newScoreOfNextPosition > currentScoreOfNextPosition) continue;
                if (newScoreOfNextPosition > shortestPathScore) continue;

                queue.Enqueue((path.Append(nextPosition), outgoingDirection, newScoreOfNextPosition));
            }
        }
        return shortestPaths;
    }

    private bool IsEnd(GridPosition<char> position)
    {
        return position.Value is END_POSITION;
    }

    private Dictionary<(GridPosition<char> position, Direction incomingDirection), int> InitializeScoreDictionaryWithMaxIntValue()
    {
        var dictionary = new Dictionary<(GridPosition<char>, Direction), int>();
        var nonWallPositions = _grid.Where(IsNotWall);
        foreach (var position in nonWallPositions) {
            foreach (var direction in Enum.GetValues<Direction>())
            {
                dictionary.Add((position, direction), int.MaxValue);
            }
        }
        return dictionary;
    }

    private bool IsWall(GridPosition<char> position)
    {
        return position.Value is WALL;
    }

    private bool IsNotWall(GridPosition<char> position)
    {
        return !IsWall(position);
    }
}
