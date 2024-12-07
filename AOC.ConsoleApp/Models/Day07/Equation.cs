
namespace AOC.ConsoleApp.Models.Day07;

public record Equation(long TestValue, int[] Numbers)
{
    public long TestValue = TestValue;
    public int[] Numbers = Numbers;

    public bool IsSolvable()
    {
        if (Numbers.Length == 1)
        {
            return TestValue == Numbers[0];
        }

        return IsSolvableUsingMultiplication() || IsSolvableUsingAddition();
    }

    private bool IsSolvableUsingAddition()
    {
        var currentNumber = Numbers[^1];
        return TestValue - currentNumber >= 0
            && new Equation(TestValue - currentNumber, Numbers[..^1]).IsSolvable();
    }

    private bool IsSolvableUsingMultiplication()
    {
        var currentNumber = Numbers[^1];
        return TestValue % currentNumber == 0
            && new Equation(TestValue / currentNumber, Numbers[..^1]).IsSolvable();
    }


}
