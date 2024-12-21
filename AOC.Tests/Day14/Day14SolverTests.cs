using AOC.ConsoleApp.Solvers;

namespace AOC.Tests.Day14;

public class Day14SolverTests
{
    [Fact]
    public void SolveFirstChallenge_ShouldReturn143_WhenProvidedTheExampleInput_1()
    {
        var lines = new string[]
        {
            "p=0,4 v=3,-3",
            "p=6,3 v=-1,-3",
            "p=10,3 v=-1,2",
            "p=2,0 v=2,-1",
            "p=0,0 v=1,3",
            "p=3,0 v=-2,-2",
            "p=7,6 v=-1,-3",
            "p=3,0 v=-1,-2",
            "p=9,3 v=2,3",
            "p=7,3 v=-1,2",
            "p=2,4 v=2,-3",
            "p=9,5 v=-3,-3",
        };
        var solver = new Day14Solver(lines);
        solver.SpaceWidth = 11;
        solver.SpaceTallness = 7;

        var numberOfSaveLines = solver.SolveFirstChallenge();

        Assert.Equal("12", numberOfSaveLines);
    }
}
