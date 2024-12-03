using AOC.ConsoleApp.Solvers;

ISolver solver = new Day02Solver();

var linesFirstChallenge = ReadLines("../../../Inputs/02_01.txt");
Console.WriteLine($"First solution: {solver.SolveFirstChallenge(linesFirstChallenge)}");

var linesSecondChallenge = ReadLines("../../../Inputs/02_01.txt");
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

