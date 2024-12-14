using AOC.ConsoleApp.Solvers;

namespace AOC.Tests.Day12;

public class Day12SolverTests
{
    [Fact]
    public void SolveFirstChallenge_ShouldReturn143_WhenProvidedTheExampleInput_1()
    {
        var lines = new string[]
        {
            "AAAA",
            "BBCD",
            "BBCC",
            "EEEC",
        };
        var solver = new Day12Solver(lines);

        var numberOfSaveLines = solver.SolveFirstChallenge();

        Assert.Equal("140", numberOfSaveLines);
    }
}
