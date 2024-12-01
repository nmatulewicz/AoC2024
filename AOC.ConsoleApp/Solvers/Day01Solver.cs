namespace AOC.ConsoleApp.Solvers;

public class Day01Solver : ISolver
{
    public string SolveFirstChallenge(IEnumerable<string> lines)
    {
        List<int> leftNumbers, rightNumbers;
        GetLeftAndRightNumbers(lines, out leftNumbers, out rightNumbers);

        leftNumbers.Sort();
        rightNumbers.Sort();

        var totalDifference = 0;
        foreach (var (left, right) in leftNumbers.Zip(rightNumbers))
        {
            totalDifference += Math.Abs(left - right);
        }

        return totalDifference.ToString();
    }

    public string SolveSecondChallenge(IEnumerable<string> lines)
    {
        List<int> leftNumbers, rightNumbers;
        GetLeftAndRightNumbers(lines, out leftNumbers, out rightNumbers);

        var similarityScore = 0;
        foreach (var leftNumber in leftNumbers)
        {
            similarityScore += leftNumber * rightNumbers.Where(rightNumber => rightNumber == leftNumber).Count();
        }

        return similarityScore.ToString();
    }

    private static void GetLeftAndRightNumbers(IEnumerable<string> lines, out List<int> leftNumbers, out List<int> rightNumbers)
    {
        leftNumbers = new List<int>();
        rightNumbers = new List<int>();
        foreach (var line in lines)
        {
            var leftAndRight = line.Split("   ");
            leftNumbers.Add(int.Parse(leftAndRight[0]));
            rightNumbers.Add(int.Parse(leftAndRight[1]));
        }
    }


}
