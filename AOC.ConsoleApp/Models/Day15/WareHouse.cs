
namespace AOC.ConsoleApp.Models.Day15;

public class Warehouse
{
    public char ROBOT = '@';
    public char EMPTY_SPACE = '.';
    public char WALL = '#';
    public char BOX = 'O';

    private readonly Grid _map;

    private GridPosition<char> _positionRobot;

    public Warehouse(Grid map)
    {
        _map = map;
        _positionRobot = _map.First(IsRobot);
    }

    public IEnumerable<int> GetAllBoxesGpsCoordinates()
    {
        return _map.Where(IsBox).Select(GetGpsCoordinate);
    }

    public static int GetGpsCoordinate(GridPosition<char> position) 
    {
        return 100 * position.Row + 1 * position.Column;
    }


    public bool TryMoveRobot(Direction direction)
    {
        var nextPositionRobot = _positionRobot.GetNeighbour(direction.ToOffset());
        if (IsWall(nextPositionRobot)) return false;
        if (IsBox(nextPositionRobot) && !TryMoveBox(nextPositionRobot, direction)) return false;

        MoveRobot(direction.ToOffset());
        return true;
    }

    private bool TryMoveBox(GridPosition<char> positionBox, Direction direction)
    {
        var nextPositionBox = positionBox.GetNeighbour(direction.ToOffset());
        if (IsWall(nextPositionBox)) return false; 
        if (IsBox(nextPositionBox) && !TryMoveBox(nextPositionBox, direction)) return false;

        MoveBox(positionBox, direction);
        return true;
    }

    public void MoveRobot((int row, int column) offset)
    {
        var nextPositionRobot = _positionRobot.GetNeighbour(offset);
        if (!IsEmpty(nextPositionRobot)) throw new InvalidOperationException("Cannot move robot to non-empty space");

        nextPositionRobot.Value = ROBOT;
        _positionRobot.Value = EMPTY_SPACE;
        _positionRobot = nextPositionRobot;
    }

    public void MoveBox(GridPosition<char> currentBoxPosition, Direction direction)
    {
        var nextPositionBox = currentBoxPosition.GetNeighbour(direction.ToOffset());
        if (!IsEmpty(nextPositionBox)) throw new InvalidOperationException("Cannot move box to non-empty space");

        nextPositionBox.Value = BOX;
        currentBoxPosition.Value = EMPTY_SPACE;
    }

    private bool IsEmpty(GridPosition<char> position)
    {
        return position.Value == EMPTY_SPACE;
    }

    private bool IsWall(GridPosition<char> nextPositionRobot)
    {
        return nextPositionRobot.Value == WALL;
    }

    private bool IsBox(GridPosition<char> nextPositionRobot)
    {
        return nextPositionRobot.Value == BOX;
    }

    private bool IsRobot(GridPosition<char> nextPositionRobot)
    {
        return nextPositionRobot.Value == ROBOT;
    }
}
