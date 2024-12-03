using AOC.ConsoleApp.Solvers;

ISolver solver = new Day01Solver();

var linesFirstChallenge = ReadLines("../../../Inputs/01_01.txt");
Console.WriteLine($"First solution: {solver.SolveFirstChallenge(linesFirstChallenge)}");

var linesSecondChallenge = ReadLines("../../../Inputs/01_02.txt");
Console.WriteLine($"Second solution: {solver.SolveSecondChallenge(linesSecondChallenge)}");

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

