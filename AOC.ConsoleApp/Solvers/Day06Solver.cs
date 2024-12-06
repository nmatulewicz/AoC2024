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
        throw new NotImplementedException();
    }
}
