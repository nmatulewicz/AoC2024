


namespace AOC.ConsoleApp.Models.Day17;

public class ComputerProgram
{
    public long RegisterA { get; set; }
    public long RegisterB { get; set; }
    public long RegisterC { get; set; }

    private int[] Instructions;

    private int _instructionPointer = 0;
    private List<long> _output = new List<long>();

    public ComputerProgram(int registerA, int registerB, int registerC, int[] instructions)
    {
        RegisterA = registerA;
        RegisterB = registerB;
        RegisterC = registerC;

        Instructions = instructions; 
        
        _instructionPointer = 0;
        _output = [];
    }

    public IEnumerable<long> Execute()
    {
        while (_instructionPointer < Instructions.Length)
        {
            var currentInstruction = (Instruction) Instructions[_instructionPointer];
            ExecuteInstruction(currentInstruction);
            _instructionPointer += 2;
        }

        return _output;
    }

    private void ExecuteInstruction(Instruction instruction)
    {
        var operand = Instructions[_instructionPointer + 1];
        var operandValue = instruction.TakesInputAsComboOperand() ? GetComboOperandValue(operand) : operand;

        switch (instruction)
        {
            case Instruction.Adv:
                Adv(operandValue);
                break;
            case Instruction.Bxl:
                Bxl(operandValue);
                break;
            case Instruction.Bst:
                Bst(operandValue);
                break;
            case Instruction.Jnz:
                Jnz(operandValue);
                break;
            case Instruction.Bxc:
                Bxc(operandValue);
                break;
            case Instruction.Out:
                Out(operandValue);
                break;
            case Instruction.Bdv:
                Bdv(operandValue);
                break;
            case Instruction.Cdv:
                Cdv(operandValue);
                break;
        }
    }

    private void Adv(long operandValue)
    {
        var numerator = RegisterA;
        var denominator = (long) Math.Pow(2, operandValue);
        RegisterA = numerator / denominator;
    }

    private void Bxl(long operandValue)
    {
        var left = RegisterB;
        var right = operandValue;

        var xor = left ^ right;
        RegisterB = xor;
    }

    private void Bst(long operandValue)
    {
        RegisterB = operandValue % 8;
    }

    private void Jnz(long operandValue)
    {
        if (RegisterA == 0) return;

        _instructionPointer = (int) operandValue;
        _instructionPointer -= 2;  // to correct for later increase of 2
    }

    private void Bxc(long operandValue)
    {
        RegisterB = RegisterB ^ RegisterC;
    }

    private void Out(long operandValue)
    {
        _output.Add(operandValue % 8);
    }

    private void Bdv(long operandValue)
    {
        var numerator = RegisterA;
        var denominator = (long)Math.Pow(2, operandValue);
        RegisterB = numerator / denominator;
    }

    private void Cdv(long operandValue)
    {
        var numerator = RegisterA;
        var denominator = (long)Math.Pow(2, operandValue);
        RegisterC = numerator / denominator;
    }

    private long GetComboOperandValue(int operand)
    {
        return operand switch
        {
            < 4 => operand,
            4 => RegisterA,
            5 => RegisterB,
            6 => RegisterC,
            _ => throw new InvalidOperationException("Operand values should be less than 7 for combo operands")
        };
    }
}
