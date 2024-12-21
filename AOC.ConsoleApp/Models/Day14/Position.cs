namespace AOC.ConsoleApp.Models.Day14;

public record Position() 
{ 
    public int X { get; set; } 
    public int Y { get; set; }

    public int SpaceWidth { get; set; }
    public int SpaceTallness { get; set; }

    public Position WithOffset((int, int) offset)
    {
        var x = (X + offset.Item1) % SpaceWidth;
        if (x < 0) x += SpaceWidth;
        
        var y = (Y + offset.Item2) % SpaceTallness;
        if (y < 0) y += SpaceTallness;

        return new Position
        {
            X = x,
            Y = y,
            SpaceWidth = SpaceWidth,
            SpaceTallness = SpaceTallness,
        };
    }

    public int? GetQuadrantId()
    {
        var middleIndexX = (SpaceWidth - 1) / 2;
        var middleIndexY = (SpaceTallness - 1) / 2;

        if (X < middleIndexX && Y < middleIndexY) return 1;
        if (X > middleIndexX && Y < middleIndexY) return 2;
        if (X < middleIndexX && Y > middleIndexY) return 3;
        if (X > middleIndexX && Y > middleIndexY) return 4;

        return null;
    }
}

