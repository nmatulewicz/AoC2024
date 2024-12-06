using AOC.ConsoleApp.Models;
using System;

namespace AOC.ConsoleApp.Models.Day06;

public class Map
{
    private const char OBSTACLE = '#';
    private const char EXPLORED_LOCATION = 'X';

    public bool IsCurrentLocationOutsideTheMap => !_currentLocation.IsValidPosition;

    private Grid<char> _map;
    private GridPosition<char> _currentLocation;
    private Direction _currentDirection;

    public Map(IEnumerable<IEnumerable<char>> map)
    {
        _map = new Grid<char>(map);
        _currentLocation = GetStartLocation();
        _currentDirection = Direction.Up;
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
        return _map.Count(position => position.Value == EXPLORED_LOCATION);
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
        _currentLocation = nextLocation;
        return true;
    }

    private static bool IsObstacle(GridPosition<char> position)
    {
        return position.IsValidPosition && position.Value == OBSTACLE;
    }
}

public enum Direction
{
    Up,  
    Down, 
    Left, 
    Right,
}
