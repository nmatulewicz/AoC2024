namespace AOC.ConsoleApp.Solvers;

public interface ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines);
    public string SolveSecondChallenge(IEnumerable<string> lines);
}