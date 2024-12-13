namespace AOC.ConsoleApp.Models.Day11;

public record NumberArrangement
{
    public NumberArrangement(IEnumerable<long> arrangement, int blinksLeft)
    {
        Arrangement = arrangement
            .GroupBy(number => number)
            .Select(grouping => (grouping.Key, (long)grouping.Count()));
        BlinksLeft = blinksLeft;
    }

    public IEnumerable<(long number, long count)> Arrangement { get; private set; }
    public int BlinksLeft { get; private set; }
    
    public void Blink()
    {
        var arrangement = Arrangement.SelectMany(tuple =>
        {
            if (tuple.number == 0) return new List<(long number, long count)> { (1, tuple.count) };

            var numberString = tuple.number.ToString();
            if (numberString.Length % 2 == 0)
            {
                var splitIndex = numberString.Length / 2;
                return [
                    (long.Parse(numberString.Substring(0, splitIndex)), tuple.count),
                    (long.Parse(numberString.Substring(splitIndex)), tuple.count)
                ];
            }

            else
            {
                return [(tuple.number * 2024, tuple.count)];
            }
        });

        Arrangement =  arrangement
            .GroupBy(tuple => tuple.number)
            .Select(grouping => (grouping.Key, grouping.Select(tuple => tuple.count).Sum()));

        BlinksLeft--;
        Console.WriteLine(string.Join(" ", Arrangement));
    }
}
