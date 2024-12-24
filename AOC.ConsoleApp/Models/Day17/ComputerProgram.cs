


namespace AOC.ConsoleApp.Models.Day17;

public class ComputerProgram
{
    public int RegisterA { get; set; }
    public int RegisterB { get; set; }
    public int RegisterC { get; set; }

    private int[] Instructions;

    private int _instructionPointer = 0;
    private List<int> _output = new List<int>();

    public ComputerProgram(int registerA, int registerB, int registerC, int[] instructions)
    {
        RegisterA = registerA;
        RegisterB = registerB;
        RegisterC = registerC;

        Instructions = instructions; 
        
        _instructionPointer = 0;
        _output = [];
    }

    public IEnumerable<int> Execute()
    {
        while (_instructionPointer < Instructions.Length)
        {
            var currentInstruction = (Instruction) Instructions[_instructionPointer];
            ExecuteInstruction(currentInstruction);
            _instructionPointer += 2;
        }

        return _output;
    }

    public bool OutputsCopyOfItself()
    {
        while (_instructionPointer < Instructions.Length)
        {
            var currentInstruction = (Instruction)Instructions[_instructionPointer];
            ExecuteInstruction(currentInstruction);

            if (currentInstruction == Instruction.Out && !OutputCanBecomeEqualToInput())
            {
                return false;
            }
            _instructionPointer += 2;
        }

        return _output.Count() == Instructions.Length && OutputCanBecomeEqualToInput();
    }

    private bool OutputCanBecomeEqualToInput()
    {
        if (Instructions.Length < _output.Count()) return false;

        for (int i = 0; i < _output.Count(); i++) 
        {
            if (Instructions[i] != _output.ElementAt(i)) return false;
        }
        return true;
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

    private void Adv(int operandValue)
    {
        RegisterA = RegisterA >> operandValue;
    }

    private void Bxl(int operandValue)
    {
        RegisterB = RegisterB ^ operandValue;
    }

    private void Bst(int operandValue)
    {
        RegisterB = operandValue & 0b_0111;
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

    private void Out(int operandValue)
    {
        _output.Add(operandValue & 0b_0111);
    }

    private void Bdv(int operandValue)
    {
        RegisterB = RegisterA >> operandValue;
    }

    private void Cdv(int operandValue)
    {
        RegisterC = RegisterA >> operandValue;
    }

    private int GetComboOperandValue(int operand)
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
