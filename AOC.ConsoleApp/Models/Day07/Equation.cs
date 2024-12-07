
namespace AOC.ConsoleApp.Models.Day07;

public record Equation(long TestValue, int[] Numbers)
{
    public long TestValue = TestValue;
    public int[] Numbers = Numbers;

    public bool IsSolvable(bool includeThirdOperator = false)
    {
        if (Numbers.Length == 1)
        {
            return TestValue == Numbers[0];
        }

        return IsSolvableUsingMultiplication(includeThirdOperator) || IsSolvableUsingAddition(includeThirdOperator) 
            || (includeThirdOperator && IsSolvableUsingConcatenation());
    }

    private bool IsSolvableUsingAddition(bool includeThirdOperator)
    {
        var currentNumber = Numbers[^1];
        return TestValue - currentNumber >= 0
            && new Equation(TestValue - currentNumber, Numbers[..^1]).IsSolvable(includeThirdOperator);
    }

    private bool IsSolvableUsingMultiplication(bool includeThirdOperator)
    {
        var currentNumber = Numbers[^1];
        return TestValue % currentNumber == 0
            && new Equation(TestValue / currentNumber, Numbers[..^1]).IsSolvable(includeThirdOperator);
    }

    private bool IsSolvableUsingConcatenation()
    {
        var currentNumber = Numbers[^1];
        var currentNumberAsString = currentNumber.ToString();
        var testValueAsString = TestValue.ToString();

        if (!testValueAsString.EndsWith(currentNumberAsString)) return false;

        var lengthToKeep = testValueAsString.Length - currentNumberAsString.Length;
        var newTestValueAsString = testValueAsString[..lengthToKeep];

        if (newTestValueAsString == string.Empty) return new Equation(0, Numbers[..^1]).IsSolvable(includeThirdOperator: true);
        
        return new Equation(long.Parse(newTestValueAsString), Numbers[..^1]).IsSolvable(includeThirdOperator: true);
    }
}
