namespace AOC.ConsoleApp.Solvers;

public abstract class AbstractSolver
{
    protected IEnumerable<string> _lines;

    public AbstractSolver(IEnumerable<string> lines)
    {
        _lines = lines;
    }

    public abstract string SolveFirstChallenge();
    public abstract string SolveSecondChallenge();
}
