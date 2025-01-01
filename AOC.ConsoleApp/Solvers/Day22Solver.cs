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
        var prices = CalculatePrices2().Select(list => list.ToArray()).ToArray();
        var deltas = CalculateDeltas(prices);
        var sequences = GetSequences(deltas);
        var sequenceDictionary = ToDictionary(sequences);
        var groupingsBySequence = sequenceDictionary.Keys
            .GroupBy(key => key.sequence)
            .Select(grouping => (grouping.Key, grouping.Sum(tuple =>
            {
                var lineIndex = tuple.lineIndex;
                var roundIndex = sequenceDictionary[(lineIndex, grouping.Key)];
                var price = prices[lineIndex][roundIndex];
                return price;
            })));
        var bestSequenceAndPrice = groupingsBySequence.MaxBy(tuple => tuple.Item2);
        return bestSequenceAndPrice.Item2.ToString();

        // 1766 => too low
        // 1846 => too low
    }

    private IDictionary<(int lineIndex, (int, int, int, int) sequence), int> ToDictionary((int, int, int, int)[][] sequences)
    {
        var dictionary = new Dictionary<(int lineIndex, (int, int, int, int) sequence), int>();
        for (var lineIndex = 0; lineIndex < sequences.Length; lineIndex++)
        {
            var relevantSequences = sequences[lineIndex];
            for (var round = 4; round < relevantSequences.Length; round++)
            {
                dictionary.TryAdd((lineIndex, relevantSequences[round]), round);
            }
        }

        return dictionary;
    }

    private (int, int, int, int)[][] GetSequences(int[][] deltas)
    {
        var sequences = new (int, int, int, int)[deltas.Length][];
        for (var lineIndex = 0; lineIndex < sequences.Length; lineIndex++)
        {
            sequences[lineIndex] = new (int, int, int, int)[2000];
        }

        for (var lineIndex = 0; lineIndex < sequences.Length; lineIndex++)
        {
            var relevantDeltas = deltas[lineIndex];
            for (var round = 4; round < relevantDeltas.Length; round++)
            {
                sequences[lineIndex][round] = (relevantDeltas[round - 3],  relevantDeltas[round - 2], relevantDeltas[round - 1], relevantDeltas[round]); 
            }
        }

        return sequences;
    }

    private int[][] CalculateDeltas(IEnumerable<IEnumerable<int>> prices)
    {
        return CalculateDeltas(prices.Select(list => list.ToArray()).ToArray());
    }

    private int[][] CalculateDeltas(int[][] prices)
    {
        var deltas = new int[prices.Length][];
        for (var lineIndex = 0; lineIndex < deltas.Length; lineIndex++)
        {
            deltas[lineIndex] = new int[2000];
        }

        for (var lineIndex = 0; lineIndex < deltas.Length; lineIndex++)
        {
            var pricesPerRound = prices[lineIndex];
            for (var round = 1; round < pricesPerRound.Length; round++)
            {
                deltas[lineIndex][round] = pricesPerRound[round] - pricesPerRound[round - 1];
            }
        }

        return deltas;
    }

    private int[][] CalculatePrices()
    {
        int[][] prices = new int[_secretNumbers.Count()][];
        for (var i = 0; i < prices.Length; i++)
        {
            prices[i] = new int[2000];
            prices[i][0] = (int)(_secretNumbers.ElementAt(i).Value % 10);
        }

        IEnumerable<SecretNumber> secrets = _secretNumbers;
        for (var i = 1; i <= 1999; i++)
        {
            var results = secrets
                .Select(secret => secret.MixWith(secret << 6).Prune())
                .Select(secret => secret.MixWith(secret >> 5).Prune())
                .Select(secret => secret.MixWith(secret << 11).Prune());
            secrets = results;

            foreach (var (result, lineIndex) in results.Select((result, index) => (result, index)))
            {
                prices[lineIndex][i] = (int)(result.Value % 10);
            }
        }

        return prices;
    }

    private IEnumerable<IEnumerable<int>> CalculatePrices2()
    {
        var secrets = _secretNumbers.Select(initialSecret =>
        {
            var secrets = new List<SecretNumber> { initialSecret };
            while (secrets.Count < 2000)
            {
                var secret = secrets.Last();
                secret = secret.MixWith(secret << 6).Prune();
                secret = secret.MixWith(secret >> 5).Prune();
                secret = secret.MixWith(secret << 11).Prune();
                secrets.Add(secret);
            }
            return secrets;
        });

        var prices = secrets.Select(secretsPerLine => secretsPerLine.Select(secret => (int)secret.Value % 10));

        return prices;
    }
}
