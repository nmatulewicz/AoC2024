namespace AOC.ConsoleApp.Models.Day21;

public class NumericKeypad : Keypad
{
    public NumericKeypad() : base(new Grid([
        "789",
        "456",
        "123",
        "#0A",
        ]))
    {
    }
}
