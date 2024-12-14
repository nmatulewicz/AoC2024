namespace AOC.ConsoleApp.Models.Day12;

public class Plant(char type)
{
    public char Type { get; set; } = type;
    public Region? Region { get; set; }

    public bool HasRegion => Region is not null;

    public override string ToString()
    {
        return Type.ToString();
    }
}

public class Edge((int row, int column) p1, (int row, int column) p2)
{
    public (int row, int column) P1 = p1;
    public (int row, int column) P2 = p2;

    public bool IsHorizontal =>  P1.column == P2.column;
    public bool IsVertical => P1.row == P2.row;

    public void Turn()
    {
        (P2, P1) = (P1, P2);
    }

    public bool Touches(Edge other)
    {
        return P2 == other.P1;
    }

    public bool HasSameDirectionAs(Edge other)
    {
        if (IsHorizontal) return other.IsHorizontal;
        if (IsVertical) return other.IsVertical;

        throw new NotImplementedException(
            "Edge is expected to be either horizontal or vertical, but was neither.");
    }

    public override string ToString()
    {
        return $"P1{P1} P2{P2}";
    }
}
