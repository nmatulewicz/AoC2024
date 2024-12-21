using AOC.ConsoleApp.Models.Day14;

namespace AOC.ConsoleApp.Solvers;

public class Day14Solver : AbstractSolver
{
    public int SpaceWidth { get; set; } = 101;
    public int SpaceTallness { get; set;} = 103;

    private IEnumerable<Robot> _robots;
    public Day14Solver(IEnumerable<string> lines) : base(lines)
    {
        _robots = lines.Select(ToRobot);
    }

    private Robot ToRobot(string line)
    {
        var postionPart = line.Remove(0, 2).Split(" v=")[0];
        var x = int.Parse(postionPart.Split(',')[0]);
        var y = int.Parse(postionPart.Split(',')[1]);
        var position = new Position
        {
            X = x, 
            Y = y,
            SpaceWidth = SpaceWidth,
            SpaceTallness = SpaceTallness,
        };

        var velocityPart = line.Split(" v=")[1];
        var deltaX = int.Parse(velocityPart.Split(',')[0]);
        var deltaY = int.Parse(velocityPart.Split(',')[1]);
        var velocity = new Velocity { DeltaX = deltaX, DeltaY = deltaY };

        return new Robot(position, velocity);
    }

    public override string SolveFirstChallenge()
    {
        var endPositions = _robots.Select(robot => robot.PositionAfterNSeconds(100));
        var positionsGroupedByQuadrant = endPositions.GroupBy(position => position.GetQuadrantId());
        var countsPerQuadrant = positionsGroupedByQuadrant
            .Where(grouping => grouping.Key is not null)
            .Select(grouping => grouping.Count());
        return countsPerQuadrant.Aggregate((total, next) => total * next).ToString();

        // 60507 -> Too low
        // 217407960 -> Too low
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
