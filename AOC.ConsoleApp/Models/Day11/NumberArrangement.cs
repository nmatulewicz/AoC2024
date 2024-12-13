namespace AOC.ConsoleApp.Models.Day11;

public record NumberArrangement
{
    public NumberArrangement(IEnumerable<long> arrangement, int blinksLeft)
    {
        Arrangement = arrangement;
        BlinksLeft = blinksLeft;
    }

    public IEnumerable<long> Arrangement { get; private set; }
    public int BlinksLeft { get; private set; }
    
    public void Blink()
    {
        var arrangement = Arrangement.SelectMany(number =>
        {
            if (number == 0) return new List<long> { 1 };

            var numberString = number.ToString();
            if (numberString.Length % 2 == 0)
            {
                var splitIndex = numberString.Length / 2;
                return [
                    long.Parse(numberString.Substring(0, splitIndex)),
                    long.Parse(numberString.Substring(splitIndex)),
                ];
            }

            else
            {
                return [number * 2024];
            }
        });
        BlinksLeft--;
        Arrangement = arrangement;
    }
}
