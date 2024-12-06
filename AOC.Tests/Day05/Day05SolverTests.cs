using AOC.ConsoleApp.Solvers;

namespace AOC.Tests.Day05;
public class Day05SolverTests
{
    [Fact]
    public void SolveFirstChallenge_ShouldReturn143_WhenProvidedTheExampleInput_1()
    {
        var lines = new string[]
        {
            "47|53",
            "97|13",
            "97|61",
            "97|47",
            "75|29",
            "61|13",
            "75|53",
            "29|13",
            "97|29",
            "53|29",
            "61|53",
            "97|53",
            "61|29",
            "47|13",
            "75|47",
            "97|75",
            "47|61",
            "75|61",
            "47|29",
            "75|13",
            "53|13",
            "",
            "75,47,61,53,29",
            "97,61,53,29,13",
            "75,29,13",
            "75,97,47,61,53",
            "61,13,29",
            "97,13,75,29,47",
        };
        var solver = new Day05Solver();

        var numberOfSaveLines = solver.SolveFirstChallenge(lines);

        Assert.Equal("143", numberOfSaveLines);
    }


    [Fact]
    public void SolveSecondChallenge_ReturnZero_WhenOnlyValidSequencesAreProvided_1()
    {
        var lines = new string[]
        {
            "1|2",
            "3|1",
            "",
            "3,1,2"
        };
        var solver = new Day05Solver();

        var numberOfSaveLines = solver.SolveSecondChallenge(lines);

        Assert.Equal("0", numberOfSaveLines);
    }

    [Fact]
    public void SolveSecondChallenge_ReturnZero_WhenOnlyValidSequencesAreProvided_2()
    {
        var lines = new string[]
        {
            "10|2",
            "3|10",
            "",
            "10,1,2"
        };
        var solver = new Day05Solver();

        var numberOfSaveLines = solver.SolveSecondChallenge(lines);

        Assert.Equal("0", numberOfSaveLines);
    }

    [Fact]
    public void SolveSecondChallenge_ReturnTen_IfTenIsMiddleNumberOfOnlyInvalidSequence_2()
    {
        var lines = new string[]
        {
            "10|2",
            "3|10",
            "",
            "3,2,10"
        };
        var solver = new Day05Solver();

        var numberOfSaveLines = solver.SolveSecondChallenge(lines);

        Assert.Equal("10", numberOfSaveLines);
    }

    [Fact]
    public void SolveSecondChallenge_ReturnMiddleNumber_IfThereIsOnlyInvalidSequence_3()
    {
        var lines = new string[]
        {
            "10|2",
            "3|10",
            "20|2",
            "10|20",
            "2|1",
            "",
            "1,20,3,2,10",
        };
        var solver = new Day05Solver();

        var numberOfSaveLines = solver.SolveSecondChallenge(lines);

        Assert.Equal("20", numberOfSaveLines);
    }

    [Fact]
    public void SolveSecondChallenge_ReturnSumOfMiddleNumbers_IfThereAreTwoInvalidSequences()
    {
        var lines = new string[]
        {
            "10|2",
            "3|10",
            "20|2",
            "10|20",
            "2|1",
            "",
            "1,20,3,2,10",
            "3,2,10",
        };
        var solver = new Day05Solver();

        var numberOfSaveLines = solver.SolveSecondChallenge(lines);

        Assert.Equal("30", numberOfSaveLines);
    }
}
