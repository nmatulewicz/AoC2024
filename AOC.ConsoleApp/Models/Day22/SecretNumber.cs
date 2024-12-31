
namespace AOC.ConsoleApp.Models.Day22;

public class SecretNumber
{
    public readonly long Value;
    public SecretNumber(long value)
    {
        Value = value;
    }

    public SecretNumber MixWith(SecretNumber other)
    {
        return new SecretNumber(Value ^ other.Value);
    }

    public SecretNumber Prune()
    {
        return new SecretNumber(Value & (16777216 - 1));
    }

    public static SecretNumber operator <<(SecretNumber left, int right)
    {
        return new SecretNumber(left.Value << right);
    }

    public static SecretNumber operator >>(SecretNumber left, int right)
    {
        return new SecretNumber(left.Value >> right);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
