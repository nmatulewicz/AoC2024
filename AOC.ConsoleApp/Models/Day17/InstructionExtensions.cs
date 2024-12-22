
namespace AOC.ConsoleApp.Models.Day17;

public static class InstructionExtensions
{
    public static bool TakesInputAsComboOperand(this Instruction instruction)
    {
        return instruction is 
            Instruction.Adv or Instruction.Bst or Instruction.Out or Instruction.Adv or Instruction.Cdv;
    }
}