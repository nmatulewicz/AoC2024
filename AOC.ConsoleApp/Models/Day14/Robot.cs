using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.ConsoleApp.Models.Day14;

public class Robot(Position position, Velocity velocity)
{
    public Position Position { get; set; } = position;
    public Velocity Velocity { get; set; } = velocity;

    public Position PositionAfterNSeconds(int seconds)
    {
        var offset = Velocity.OffsetAfterNSeconds(seconds);
        return Position.WithOffset(offset);
    }
}
