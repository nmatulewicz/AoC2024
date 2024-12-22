using AOC.ConsoleApp.Models;
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
    }

    public override string SolveSecondChallenge()
    {
        for (int i = 0; true; i++)
        {
            var endPositions = _robots.Select(robot => robot.PositionAfterNSeconds(i));
            var uniqueEndPositions = endPositions.DistinctBy(position => (position.X, position.Y));
            //var positionsGroupedByQuadrant = endPositions.GroupBy(position => position.GetQuadrantId());
            //var countsPerQuadrant = positionsGroupedByQuadrant
            //    .Where(grouping => grouping.Key is not null)
            //    .Select(grouping => grouping.Count());

            
            var quadrants = uniqueEndPositions
                .Select(position => position.GetQuadrantId())
                .Where(quadrant => quadrant.HasValue)
                .Select(quadrant => quadrant!.Value);
            var countQuadrant1 = quadrants.Count(quadrant => quadrant is 1);
            var countQuadrant2 = quadrants.Count(quadrant => quadrant is 2);
            var countQuadrant3 = quadrants.Count(quadrant => quadrant is 3);
            var countQuadrant4 = quadrants.Count(quadrant => quadrant is 4);
            DrawTree(endPositions);
            Console.WriteLine("Aantal iteraties: " + i);

            //var positionsQuadrant1 = uniqueEndPositions.Where(position => position.GetQuadrantId() is 1 or 3);
            //if (positionsQuadrant1.All(position1 => uniqueEndPositions
            //    .Any(position2 => position1.Y == position2.Y && position2.X == SpaceWidth - 1 - position1.X)))
            //{
            //    DrawTree(endPositions);
            //    Console.WriteLine("Aantal iteraties: " + i);
            //}
        }
    }

    private void DrawTree(IEnumerable<Position> endPositions)
    {
        var data = new List<string>();  
        for (var i = 0; i < SpaceTallness; i++)
        {
            data.Add(new string('.', SpaceWidth));
        }

        var grid = new Grid(data);

        foreach (var position in endPositions)
        {
            grid.SetValue(position.Y, position.X, '*');
        }
        Console.WriteLine(grid.ToString());
    }
}
