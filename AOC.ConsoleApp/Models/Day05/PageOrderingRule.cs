namespace AOC.ConsoleApp.Models.Day05;

public record PageOrderingRule
{
    /// <summary>
    /// Should only occur before Value2
    /// </summary>
    public int ValueOne { get; init; } 
    /// <summary>
    /// Should only occur after Value1
    /// </summary>
    public int ValueTwo { get; init; }
}
