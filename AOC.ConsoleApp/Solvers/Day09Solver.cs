
using AOC.ConsoleApp.Models.Day09;

namespace AOC.ConsoleApp.Solvers;

public class Day09Solver : AbstractSolver
{
    private Disk _disk;

    public Day09Solver(IEnumerable<string> lines) : base(lines)
    {
        var line = lines.First();
        var diskMap = line.ToArray().Select(x => x.ToString()).Select(int.Parse).ToArray();
        _disk = new Disk(diskMap);
    }

    public override string SolveFirstChallenge()
    {
        _disk.RearrangeFiles();
        return _disk.CalculateChecksum().ToString();

        // 1327300675292 --> Too low 
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
