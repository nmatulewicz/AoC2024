namespace AOC.ConsoleApp.Models.Day06;

public class Map
{
    public const char OBSTACLE = '#';
    public const char EXPLORED_LOCATION = 'X';

    public bool IsCurrentLocationOutsideTheMap => !_currentLocation.IsValidPosition;
    public bool HasEnteredLoop => _currentLocation.IsValidPosition && _usedEnteringDirectionsPerExploredLocation[(_currentLocation.Row, _currentLocation.Column)].Contains(_currentDirection);

    private Grid<char> _map;
    private GridPosition<char> _currentLocation;
    private Direction _currentDirection;
    private Direction _enteringDirection;
    private Dictionary<(int, int), List<Direction>> _usedEnteringDirectionsPerExploredLocation;

    public Map(IEnumerable<IEnumerable<char>> map)
    {
        _map = new Grid<char>(map);
        _currentLocation = GetStartLocation();
        _currentDirection = Direction.Up;

        _usedEnteringDirectionsPerExploredLocation = _map.ToDictionary(position => (position.Row, position.Column), position => new List<Direction>());
    }

    public void Move()
    {
        while(!TryMoveForward())
        {
            TurnRight();
        }
    }

    public int CountExploredLocations()
    {
        return GetExploredLocations().Count();
    }

    public IEnumerable<GridPosition<char>> GetExploredLocations()
    {
        return _map.Where(position => position.Value == EXPLORED_LOCATION); 
    }

    private GridPosition<char> GetStartLocation()
    {
        return _map.First(position => position.Value == '^');
    }

    private void TurnRight()
    {
        _currentDirection = _currentDirection switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new NotImplementedException()
        };
    }

    private bool TryMoveForward()
    {
        var movingOffset = _currentDirection switch
        {
            Direction.Left => (0, -1),
            Direction.Right => (0, 1),
            Direction.Up => (-1, 0),
            Direction.Down => (1, 0),
            _ => throw new NotImplementedException()
        };

        var nextLocation = _currentLocation.GetNeighbour(movingOffset);
        if (IsObstacle(nextLocation)) return false;

        _currentLocation.Value = EXPLORED_LOCATION;
        _usedEnteringDirectionsPerExploredLocation[(_currentLocation.Row, _currentLocation.Column)].Add(_enteringDirection);
        
        _currentLocation = nextLocation;
        _enteringDirection = _currentDirection;
        return true;
    }

    private static bool IsObstacle(GridPosition<char> position)
    {
        return position.IsValidPosition && position.Value == OBSTACLE;
    }
}
