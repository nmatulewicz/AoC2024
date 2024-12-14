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

    [Fact]
    public void SolveSecondChallenge_ShouldReturn143_WhenProvidedTheExampleInput_1()
    {
        var lines = new string[]
        {
            "AAAA",
            "BBCD",
            "BBCC",
            "EEEC",
        };
        var solver = new Day12Solver(lines);

        var numberOfSaveLines = solver.SolveSecondChallenge();

        Assert.Equal("80", numberOfSaveLines);
    }

    [Fact]
    public void SolveSecondChallenge_ShouldReturn143_WhenProvidedTheExampleInput_2()
    {
        var lines = new string[]
        {
            "EEEEE",
            "EXXXX",
            "EEEEE",
            "EXXXX",
            "EEEEE",
        };
        var solver = new Day12Solver(lines);

        var numberOfSaveLines = solver.SolveSecondChallenge();

        Assert.Equal("236", numberOfSaveLines);
    }

    [Fact]
    public void SolveSecondChallenge_ShouldReturn143_WhenProvidedTheExampleInput_3()
    {
        var lines = new string[]
        {
            "EEEEE",
            "EXEXE",
            "EEEEE",
            "EXEXE",
            "EEEEE",
        };
        var solver = new Day12Solver(lines);

        var numberOfSaveLines = solver.SolveSecondChallenge();

        Assert.Equal("436", numberOfSaveLines);
    }

    [Fact]
    public void SolveSecondChallenge_ShouldReturn143_WhenProvidedTheExampleInput_4()
    {
        var lines = new string[]
        {
            "AAAAAA",
            "AAABBA",
            "AAABBA",
            "ABBAAA",
            "ABBAAA",
            "AAAAAA",
        };
        var solver = new Day12Solver(lines);

        var numberOfSaveLines = solver.SolveSecondChallenge();

        Assert.Equal("368", numberOfSaveLines);
    }
}
