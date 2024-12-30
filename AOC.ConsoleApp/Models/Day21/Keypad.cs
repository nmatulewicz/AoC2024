namespace AOC.ConsoleApp.Models.Day21;

public abstract class Keypad
{
    public const char ACTIVATE_BUTTON = 'A';
    public const char GAP = '#';

    private readonly Grid _keypad;
    private GridPosition<char> _currentPosition;

    public Keypad(Grid keypad)
    {
        _keypad = keypad;
        _currentPosition = keypad.Last(IsActiveButton);
    }

    /// <summary>
    /// Moves robot to button with provided value.
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>The route it took to move to the destination. 
    /// This is a simple route meaning is has as little turns as possible.</returns>
    public string MoveRobotTo(char destination)
    {
        var startPosition = _currentPosition;
        var endPosition = _keypad.First(position => position.Value == destination);

        _currentPosition = endPosition;
        return GetSimplestRoutes(startPosition, endPosition).First();
    }

    public string GetShortestRoute(string buttons, int levels)
    {
        if (levels == 1)
        {
            var route = string.Empty;
            var keypad = new DirectionalKeypad();
            foreach (var button in buttons)
            {
                route += keypad.MoveRobotTo(button);
            }
            return route;
        }

        var shortestRoute = string.Empty;
        foreach (var button in buttons)
        {
            var routes = GetSimplestRoutesTo(button);

            shortestRoute += routes.Select(route =>
            {
                var keypad = new DirectionalKeypad();
                var shortestRoute = keypad.GetShortestRoute(route, levels - 1);
                return shortestRoute;
            }).MinBy(route => route.Length);
            MoveRobotTo(button);
        }

        return shortestRoute;
    }

    private IEnumerable<string> GetSimplestRoutesTo(char destination)
    {
        var startPosition = _currentPosition;
        var endPosition = _keypad.First(position => position.Value == destination);

        if (startPosition.Equals(endPosition)) return [ACTIVATE_BUTTON.ToString()];
        return GetSimplestRoutes(startPosition, endPosition);
    }

    private IEnumerable<string> GetSimplestRoutes(GridPosition<char> startPosition, GridPosition<char> endPosition)
    {
        var columnDelta = endPosition.Column - startPosition.Column;
        var moveColumnChar = columnDelta > 0 ? '>' : '<';
        var horizontalMovementPart = new string(moveColumnChar, Math.Abs(columnDelta));

        var rowDelta = endPosition.Row - startPosition.Row;
        var moveRowChar = rowDelta > 0 ? 'v' : '^';
        var verticalMovementPart = new string(moveRowChar, Math.Abs(rowDelta));

        var routes = new List<string>();
        if (startPosition.GetNeighbour(rowDelta, 0).Value != GAP)
        {
            var routeMovingVerticallyFirst = verticalMovementPart + horizontalMovementPart + ACTIVATE_BUTTON;
            routes.Add(routeMovingVerticallyFirst);
        }
        if (startPosition.GetNeighbour(0, columnDelta).Value != GAP)
        {
            var routeMovingHorizontallyFirst = horizontalMovementPart + verticalMovementPart + ACTIVATE_BUTTON;
            routes.Add(routeMovingHorizontallyFirst);
        }
        return routes.Distinct();
    }

    private string GetBestRoute(GridPosition<char> startPosition, GridPosition<char> endPosition)
    {
        var columnDelta = endPosition.Column - startPosition.Column;
        var moveColumnChar = columnDelta > 0 ? '>' : '<';
        var horizontalMovementPart = new string(moveColumnChar, Math.Abs(columnDelta));

        var rowDelta = endPosition.Row - startPosition.Row;
        var moveRowChar = rowDelta > 0 ? 'v' : '^';
        var verticalMovementPart = new string(moveRowChar, Math.Abs(rowDelta));

        var routes = new List<string>();
        if ( startPosition.GetNeighbour(0, columnDelta).Value != GAP)
        {
            var routeMovingHorizontallyFirst = horizontalMovementPart + verticalMovementPart + ACTIVATE_BUTTON;
            routes.Add(routeMovingHorizontallyFirst);
        }
        if (true || startPosition.GetNeighbour(rowDelta, 0).Value != GAP)
        {
            var routeMovingVerticallyFirst = verticalMovementPart + horizontalMovementPart + ACTIVATE_BUTTON;
            routes.Add(routeMovingVerticallyFirst);
        }
        return routes.First();
    }

    private bool IsActiveButton(GridPosition<char> position)
    {
        return position.Value == ACTIVATE_BUTTON;
    }
}
