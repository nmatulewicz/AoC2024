using AOC.ConsoleApp.Models.Day06;

namespace AOC.ConsoleApp.Solvers;

public class Day06Solver : ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        var map = new Map(lines.Select(line => line.ToCharArray()));
        while (!map.IsCurrentLocationOutsideTheMap)
        {
            map.Move();
        }

        return map.CountExploredLocations().ToString();
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        var map = new Map(lines.Select(line => line.ToCharArray()));
        while (!map.IsCurrentLocationOutsideTheMap)
        {
            map.Move();
        }

        var numberOfLoopCausingObstacles = 0;
        foreach (var position in map.GetExploredLocations())  // We only need to try place obstacles at positions that were explored.
        {
            var mapInput = lines.Select(line => line.ToCharArray()).ToArray();
                
            if (mapInput[position.Row][position.Column] == '^') continue;
            mapInput[position.Row][position.Column] = Map.OBSTACLE;
                
            map = new Map(mapInput);
            while (!map.IsCurrentLocationOutsideTheMap && !map.HasEnteredLoop)
            {
                map.Move();
            }

            if (map.HasEnteredLoop) numberOfLoopCausingObstacles++;
        }

        return numberOfLoopCausingObstacles.ToString();
    }
}
