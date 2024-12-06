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

    private PageOrdering(Page[] ordering)
    {
        _ordering = ordering;
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

    public PageOrdering GetFixedOrdering()
    {
        var ordering = new Page[_ordering.Length];
        _ordering.CopyTo(ordering, 0);

        for (var i = 0; i < ordering.Length; i++)
        {
            var currentPage = ordering[i];
            for (var j = i - 1; j >= 0; j--)
            {
                var preceedingPage = ordering[j];
                if (!preceedingPage.CanPreceed(currentPage)) SwapPages(j, j + 1, ordering);
                else break;
            }
        }
        return new PageOrdering(ordering);
    }

    private void SwapPages(int index1, int index2, Page[] ordering)
    {
        var page1 = ordering[index1];
        var page2 = ordering[index2];

        ordering[index1] = page2;
        ordering[index2] = page1;
    }
}
