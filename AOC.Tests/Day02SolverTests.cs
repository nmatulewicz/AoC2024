using AOC.ConsoleApp.Solvers;

namespace AOC.Tests;

public class Day02SolverTests
{
    [Fact]
    public void SolveFirstChallenge_ShouldReturn1_WhenOneSafeLineIsProvided()
    {
        var lines = new string[] { "1 2 3" };
        var solver = new Day02Solver();

        var numberOfSaveLines = solver.SolveFirstChallenge(lines);

        Assert.Equal("1", numberOfSaveLines);
    }


    [Theory]
    [InlineData("1 1 2 3 4")]
    [InlineData("0 1 2 4 4")]
    [InlineData("0 1 2 4 2")]
    [InlineData("0 1 2 4 10")]
    public void SolveFirstChallenge_ShouldReturn0_WhenOneUnsafeLineIsProvided(string line)
    {
        var lines = new string[] { line };
        var solver = new Day02Solver();

        var numberOfSaveLines = solver.SolveFirstChallenge(lines);

        Assert.Equal("0", numberOfSaveLines);
    }
}