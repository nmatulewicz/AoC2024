
using AOC.ConsoleApp.Models.Day09;

namespace AOC.ConsoleApp.Solvers;

public class Day09Solver : AbstractSolver
{
    private Disk _disk;

    public Day09Solver(IEnumerable<string> lines) : base(lines)
    {
        SetDisk(lines);
    }

    private void SetDisk(IEnumerable<string> lines)
    {
        var line = lines.First();
        var diskMap = line.ToArray().Select(x => x.ToString()).Select(int.Parse).ToArray();
        _disk = new Disk(diskMap);
    }

    public override string SolveFirstChallenge()
    {
        SetDisk(_lines);
        _disk.RearrangeFiles();
        return _disk.CalculateChecksum().ToString();
    }

    public override string SolveSecondChallenge()
    {
        SetDisk(_lines);
        _disk.RearrangeFilesPart2();
        return _disk.CalculateChecksum().ToString();
    }
}
