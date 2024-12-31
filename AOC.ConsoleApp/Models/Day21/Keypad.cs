namespace AOC.ConsoleApp.Models.Day21;

public abstract class Keypad
{
    public const char ACTIVATE_BUTTON = 'A';
    public const char GAP = '#';

    public static IDictionary<(string buttons, int levels), IEnumerable<(string routePart, int count)>> ShortestRouteDictionary = new Dictionary<(string, int), IEnumerable<(string, int)>>();

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

    public IEnumerable<(string routePart, int count)> GetShortestRoute(string buttons, int levels)
    {
        if (ShortestRouteDictionary.TryGetValue((buttons, levels), out var shortestRoute))
        {
            return shortestRoute;
        }

        if (levels == 1)
        {
            var routeParts = new List<string>();
            var keypad = new DirectionalKeypad();
            foreach (var button in buttons)
            {
                routeParts.Add(keypad.MoveRobotTo(button));
            }
            var route = routeParts.GroupBy(routePart => routePart).Select(grouping => (grouping.Key, grouping.Count()));
            ShortestRouteDictionary.Add((buttons, levels), route);
            return route;
        }

        var shortestRouteParts = new List<(string routePart, int count)>();
        foreach (var button in buttons)
        {
            var routes = GetSimplestRoutesTo(button);

            var shortestRouteForButton = routes.Select(route =>
            {
                var keypad = new DirectionalKeypad();
                var shortestRoute = keypad.GetShortestRoute(route, levels - 1);
                return shortestRoute;
            }).MinBy(GetTotalLength);
            shortestRouteParts.AddRange(shortestRouteForButton!);
            MoveRobotTo(button);
        }

        shortestRoute = shortestRouteParts
            .GroupBy(tuple => tuple.routePart)
            .Select(grouping => (grouping.Key, grouping.Sum(tuple => tuple.count)));
        ShortestRouteDictionary.Add((buttons, levels), shortestRoute);
        return shortestRouteParts;
    }

    private long GetTotalLength(IEnumerable<(string routePart, int count)> route)
    {
        return route.Sum(tuple => (long) tuple.routePart.Length * tuple.count);
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

    private bool IsActiveButton(GridPosition<char> position)
    {
        return position.Value == ACTIVATE_BUTTON;
    }
}
