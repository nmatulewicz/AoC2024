using AOC.ConsoleApp.Models.Day07;

namespace AOC.ConsoleApp.Solvers;

public class Day07Solver(IEnumerable<string> lines) : AbstractSolver(lines)
{
    private IEnumerable<Equation> _equations = GetEquations(lines);

    public override string SolveFirstChallenge()
    {
        var solvableEquations = _equations.Where(equation => equation.IsSolvable());
        return solvableEquations.Sum(equation => equation.TestValue).ToString();
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }

    private static List<Equation> GetEquations(IEnumerable<string> lines)
    {
        var equations = new List<Equation>();
        foreach (var line in lines)
        {
            var splitTestValueFromNumbers = line.Split(": ");
            var testValue = long.Parse(splitTestValueFromNumbers[0]);
            var numberStrings = splitTestValueFromNumbers[1];
            var numbers = numberStrings.Split(' ').Select(int.Parse).ToArray();
            equations.Add(new Equation(testValue, numbers));
        }
        return equations;
    }
}
