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
