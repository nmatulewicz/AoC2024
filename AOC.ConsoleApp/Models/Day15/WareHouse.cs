
using System.Security.Cryptography.X509Certificates;

namespace AOC.ConsoleApp.Models.Day15;

public class Warehouse
{
    public const char ROBOT = '@';
    public const char EMPTY_SPACE = '.';
    public const char WALL = '#';
    public const char BOX = 'O';
    public const char WIDE_BOX_LEFT = '[';
    public const char WIDE_BOX_RIGHT = ']';

    private readonly Grid _map;

    private GridPosition<char> _positionRobot;

    public Warehouse(Grid map)
    {
        _map = map;
        _positionRobot = _map.First(IsRobot);
    }

    public IEnumerable<int> GetAllBoxesGpsCoordinates()
    {
        return _map.Where(position => position.Value is BOX or WIDE_BOX_LEFT).Select(GetGpsCoordinate);
    }

    public static int GetGpsCoordinate(GridPosition<char> position) 
    {
        return 100 * position.Row + 1 * position.Column;
    }


    public bool TryMoveRobot(Direction direction)
    {
        var nextPositionRobot = _positionRobot.GetNeighbour(direction.ToOffset());
        if (IsWall(nextPositionRobot)) return false;
        if (IsWideBox(nextPositionRobot) && !TryMoveWideBox(nextPositionRobot, direction)) return false;
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

    private bool TryMoveWideBox(GridPosition<char> positionBox, Direction direction)
    {
        if (direction is Direction.Up or Direction.Down) return TryMoveWideBoxUpOrDown(positionBox, direction);
        if (direction is Direction.Left) return TryMoveWideBoxLeft(positionBox);
        else return TryMoveWideBoxRight(positionBox);
    }

    private bool TryMoveWideBoxLeft(GridPosition<char> positionBox)
    {
        var currentBoxLeftPosition = positionBox.Value is WIDE_BOX_LEFT
            ? positionBox
            : positionBox.GetNeighbour(Direction.Left.ToOffset());
        var currentBoxRightPosition = currentBoxLeftPosition.GetNeighbour(Direction.Right.ToOffset());

        var nextLeftPosition = currentBoxLeftPosition.GetNeighbour(Direction.Left.ToOffset());
        if (IsWall(nextLeftPosition)) return false;
        if (IsWideBox(nextLeftPosition) && !TryMoveWideBoxLeft(nextLeftPosition)) return false;

        MoveWideBoxLeft(currentBoxLeftPosition, currentBoxRightPosition, Direction.Left);
        return true;
    }

    private bool TryMoveWideBoxRight(GridPosition<char> positionBox)
    {
        var currentBoxLeftPosition = positionBox.Value is WIDE_BOX_LEFT
            ? positionBox
            : positionBox.GetNeighbour(Direction.Left.ToOffset());
        var currentBoxRightPosition = currentBoxLeftPosition.GetNeighbour(Direction.Right.ToOffset());

        var nextRightPosition = currentBoxRightPosition.GetNeighbour(Direction.Right.ToOffset());
        if (IsWall(nextRightPosition)) return false;
        if (IsWideBox(nextRightPosition) && !TryMoveWideBoxRight(nextRightPosition)) return false;

        MoveWideBoxRight(currentBoxLeftPosition, currentBoxRightPosition, Direction.Right);
        return true;
    }

    private void MoveWideBoxLeft(GridPosition<char> currentBoxLeftPosition, GridPosition<char> currentBoxRightPosition, Direction left)
    {
        var nextBoxLeftPosition = currentBoxLeftPosition.GetNeighbour(Direction.Left.ToOffset());
        nextBoxLeftPosition.Value = WIDE_BOX_LEFT;
        currentBoxLeftPosition.Value = WIDE_BOX_RIGHT;
        currentBoxRightPosition.Value = EMPTY_SPACE;
    }

    private void MoveWideBoxRight(GridPosition<char> currentBoxLeftPosition, GridPosition<char> currentBoxRightPosition, Direction right)
    {
        var nextBoxRightPosition = currentBoxRightPosition.GetNeighbour(Direction.Right.ToOffset());
        nextBoxRightPosition.Value = WIDE_BOX_RIGHT;
        currentBoxRightPosition.Value = WIDE_BOX_LEFT;
        currentBoxLeftPosition.Value = EMPTY_SPACE;
    }

    private bool TryMoveWideBoxUpOrDown(GridPosition<char> postitionBox, Direction direction)
    {
        GridPosition<char> currentLeftPosition, currentRightPosition;
        if (postitionBox.Value is WIDE_BOX_LEFT)
        {
            currentLeftPosition = postitionBox;
            currentRightPosition = postitionBox.GetNeighbour(Direction.Right.ToOffset());
        }
        else
        {
            currentRightPosition = postitionBox;
            currentLeftPosition = postitionBox.GetNeighbour(Direction.Left.ToOffset());
        }

        if (!CanMove(currentLeftPosition, direction) || !CanMove(currentRightPosition, direction))
        {
            return false;
        }

        MoveWideBoxUpOrDown(currentLeftPosition, currentRightPosition, direction);
        return true;
    }

    private void MoveWideBoxUpOrDown(GridPosition<char> currentLeftPosition, GridPosition<char> currentRightPosition, Direction direction)
    {
        var nextLeftPosition = currentLeftPosition.GetNeighbour(direction.ToOffset());
        var nextRightPosition = currentRightPosition.GetNeighbour(direction.ToOffset());
        
        switch((nextLeftPosition.Value, nextRightPosition.Value))
        {
            case (EMPTY_SPACE, EMPTY_SPACE):
                break;
            case (WIDE_BOX_LEFT, WIDE_BOX_RIGHT):
                MoveWideBoxUpOrDown(nextLeftPosition, nextRightPosition, direction);
                break;
            case (WIDE_BOX_RIGHT, EMPTY_SPACE):
                MoveWideBoxUpOrDown(nextLeftPosition.GetNeighbour(Direction.Left.ToOffset()), nextLeftPosition, direction);
                break;
            case (EMPTY_SPACE, WIDE_BOX_LEFT):
                MoveWideBoxUpOrDown(nextRightPosition, nextRightPosition.GetNeighbour(Direction.Right.ToOffset()), direction);
                break;
            case (WIDE_BOX_RIGHT, WIDE_BOX_LEFT):
                MoveWideBoxUpOrDown(nextLeftPosition.GetNeighbour(Direction.Left.ToOffset()), nextLeftPosition, direction);
                MoveWideBoxUpOrDown(nextRightPosition, nextRightPosition.GetNeighbour(Direction.Right.ToOffset()), direction);
                break;
            default:
                throw new InvalidOperationException("Illegal move!!");

        }
        currentLeftPosition.Value = EMPTY_SPACE;
        nextLeftPosition.Value = WIDE_BOX_LEFT;
        currentRightPosition.Value = EMPTY_SPACE;
        nextRightPosition.Value = WIDE_BOX_RIGHT;
    }

    private bool CanMove(GridPosition<char> position, Direction direction)
    {
        var nextPosition = position.GetNeighbour(direction.ToOffset());
        if (IsWall(nextPosition)) return false;
        if (IsEmpty(nextPosition)) return true;
        
        if (nextPosition.Value is WIDE_BOX_LEFT)
        {
            return CanMove(nextPosition, direction) 
                && CanMove(nextPosition.GetNeighbour(Direction.Right.ToOffset()), direction);
        }
        if (nextPosition.Value is WIDE_BOX_RIGHT)
        {
            return CanMove(nextPosition, direction) 
                && CanMove(nextPosition.GetNeighbour(Direction.Left.ToOffset()), direction);
        }

        throw new NotImplementedException($"Did not expect input position with value '{position.Value}'");
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
        return position.Value is EMPTY_SPACE;
    }

    private bool IsWall(GridPosition<char> nextPositionRobot)
    {
        return nextPositionRobot.Value is WALL;
    }

    private bool IsBox(GridPosition<char> nextPositionRobot)
    {
        return nextPositionRobot.Value is BOX;
    }

    private bool IsWideBox(GridPosition<char> nextPositionRobot)
    {
        return nextPositionRobot.Value is WIDE_BOX_LEFT or WIDE_BOX_RIGHT;
    }

    private bool IsRobot(GridPosition<char> nextPositionRobot)
    {
        return nextPositionRobot.Value is ROBOT;
    }
}
