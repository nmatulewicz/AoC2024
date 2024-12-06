using System.Data;

namespace AOC.ConsoleApp.Models.Day05;

public class PageOrdering
{
    public Page MiddlePage => _ordering[_ordering.Length / 2];

    private readonly Page[] _ordering;

    public PageOrdering(IEnumerable<int> ordering, IEnumerable<(int, int)> rules)
    {
        var relevantRules = rules.Where(rule => ordering.Contains(rule.Item1) && ordering.Contains(rule.Item2));

        _ordering = ordering.Select(pageNumber => new Page(pageNumber)).ToArray();
        foreach (var rule in relevantRules)
        {
            var precessor = _ordering.First(page => page.PageNumber == rule.Item1);
            var successor = _ordering.First(page => page.PageNumber == rule.Item2);

            precessor.AddSuccessor(successor);
        }
    }

    public bool IsValid()
    {
        for (var i = 0; i < _ordering.Length; i++)
        {
            var currentPage = _ordering[i];
            for (var j = i + 1; j < _ordering.Length; j++)
            {
                var succeedingPage = _ordering[j];
                if (!currentPage.CanPreceed(succeedingPage)) return false;
            }
        }
        return true;
    }
}
