using AOC.ConsoleApp.Solvers;

var lines = ReadLines("../../../Inputs/21_example.txt");
AbstractSolver solver = new Day21Solver(lines);

Console.WriteLine($"First solution: {solver.SolveFirstChallenge()}");
Console.WriteLine($"Second solution: {solver.SolveSecondChallenge()}");

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

