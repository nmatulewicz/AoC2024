namespace AOC.ConsoleApp.Models.Day14;

public record Velocity()
{
    public int DeltaX { get; set; }
    public int DeltaY { get; set; }

    public (int deltaX, int deltaY) OffsetAfterNSeconds(int seconds)
    {
        return (DeltaX * seconds, DeltaY * seconds);
    }
}

