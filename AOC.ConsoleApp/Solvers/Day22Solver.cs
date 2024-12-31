using AOC.ConsoleApp.Models.Day22;

namespace AOC.ConsoleApp.Solvers;

public class Day22Solver : AbstractSolver
{
    private IEnumerable<SecretNumber> _secretNumbers;
    public Day22Solver(IEnumerable<string> lines) : base(lines)
    {
        _secretNumbers = _lines.Select(long.Parse).Select(number => new SecretNumber(number));
    }

    public override string SolveFirstChallenge()
    {
        IEnumerable<SecretNumber> secrets = _secretNumbers;
        for (var i = 0; i < 2000; i++)
        {
            var results = secrets
                .Select(secret => secret.MixWith(secret << 6).Prune())
                .Select(secret => secret.MixWith(secret >> 5).Prune())
                .Select(secret => secret.MixWith(secret << 11).Prune());
            secrets = results;
        }
        return secrets.Sum(secret => secret.Value).ToString();
        // 17347641344 ==> Too high
    }

    public override string SolveSecondChallenge()
    {
        throw new NotImplementedException();
    }
}
