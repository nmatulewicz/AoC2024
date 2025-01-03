using AOC.ConsoleApp.Models.Day23;

namespace AOC.ConsoleApp.Solvers;

public class Day23Solver : AbstractSolver
{
    private readonly LanParty _lanParty;
    public Day23Solver(IEnumerable<string> lines) : base(lines)
    {
        var connections = lines.Select(ToConnection);
        _lanParty = new LanParty(connections);
    }

    private (Computer, Computer) ToConnection(string line)
    { 
        var left = new Computer(line.Split('-')[0]);
        var right = new Computer(line.Split('-')[1]);
        return (left, right);
    }

    public override string SolveFirstChallenge()
    {
        var groupsOfThree = _lanParty.GetAllInterconnectedGroupsOfThreeComputers();
        var groupsWithAtLeastOneComputerStartingWithT = groupsOfThree
            .Where(group => group.Item1.Name.StartsWith("t") 
                || group.Item2.Name.StartsWith("t")
                || group.Item3.Name.StartsWith("t"));
        return groupsWithAtLeastOneComputerStartingWithT.Count().ToString();
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
