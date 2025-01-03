namespace AOC.ConsoleApp.Models.Day23;

public class Computer : IEquatable<Computer>
{
    public readonly string Name;

    public Computer(string name)
    {
        Name = name;
    }

    public override bool Equals(object? obj)
    {
        return obj is Computer other && other.Equals(this);
    }

    public bool Equals(Computer? other)
    {
        return other?.Name == Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
