﻿using AOC.ConsoleApp.Solvers;

ISolver solver = new Day03Solver();

var lines = ReadLines("../../../Inputs/03.txt");
Console.WriteLine($"First solution: {solver.SolveFirstChallenge(lines)}");
Console.WriteLine($"Second solution: {solver.SolveSecondChallenge(lines)}");

static IEnumerable<string> ReadLines(string fileName)
{
    var lines = new List<string>();

    var reader = new StreamReader(fileName);
    while (reader.ReadLine() is string line)
    {
        lines.Add(line);
    }
    reader.Close();

    return lines;
}

