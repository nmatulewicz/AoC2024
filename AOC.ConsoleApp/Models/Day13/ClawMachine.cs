using System.Security.Cryptography.X509Certificates;

namespace AOC.ConsoleApp.Models.Day13;

public class ClawMachine
{
    public const int BUTTON_A_COSTS = 3;
    public const int BUTTON_B_COSTS = 1;

    public Button ButtonA { get; set; }
    public Button ButtonB { get; set; }
    public Prize Prize { get; set; }

    public ClawMachine(Button buttonA, Button buttonB, Prize prize)
    {
        ButtonA = buttonA;
        ButtonB = buttonB;
        Prize = prize;
    }

    public bool TryWinPrize(out long countA, out long countB)
    {
        var countBNumerator = ButtonA.DeltaX * Prize.Y - ButtonA.DeltaY * Prize.X;
        var countBDenominator = ButtonA.DeltaX * ButtonB.DeltaY - ButtonA.DeltaY * ButtonB.DeltaX;
        if (countBNumerator % countBDenominator != 0)
        {
            countA = 0;
            countB = 0;
            return false;
        }
        countB = countBNumerator / countBDenominator;

        var countANumerator = Prize.X - countB * ButtonB.DeltaX;
        var countADenumerator = ButtonA.DeltaX;

        if (countANumerator % countADenumerator != 0)
        {
            countA = 0;
            countB = 0;
            return false;
        }

        countA = countANumerator / countADenumerator;
        return true;
    }
}
