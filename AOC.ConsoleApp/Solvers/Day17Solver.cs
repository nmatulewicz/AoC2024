using AOC.ConsoleApp.Models.Day17;

namespace AOC.ConsoleApp.Solvers;

public class Day17Solver : AbstractSolver
{
    private ComputerProgram _program;

    public Day17Solver(IEnumerable<string> lines) : this(lines.ToArray())
    {
        var linesAsArray = lines.ToArray();
    }

    public Day17Solver(string[] lines) : base(lines)
    {
        var registerA = int.Parse(lines[0].Split("A: ")[1]);
        var registerB = int.Parse(lines[1].Split("B: ")[1]);
        var registerC = int.Parse(lines[2].Split("C: ")[1]);

        var instructionsAsString = lines[4].Split("Program: ")[1];
        var instructions = instructionsAsString.Split(',').Select(int.Parse).ToArray();

        _program = new ComputerProgram(registerA, registerB, registerC, instructions);
    }

    public override string SolveFirstChallenge()
    {
        var output = _program.Execute();
        return string.Join(',', output);

        // 210462420 => incorrect 
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
