namespace AOC.ConsoleApp.Models.Day05;
public record Page
{
    public int PageNumber { get; }
    private readonly List<Page> _successors;

    public Page(int pageNumber)
    {
        _successors = new List<Page>();
        PageNumber = pageNumber;
    }

    public void AddSuccessor(Page successor)
    {
        _successors.Add(successor);
    }

    public bool IsPredecessorOf(Page other)
    {
        foreach (var successor in _successors)
        {
            if (successor.PageNumber == other.PageNumber) return true;
            if (successor.IsPredecessorOf(other)) return true;
        }
        return false;
    }

    public bool IsSuccessorOf(Page other)
    {
        return other.IsPredecessorOf(this);
    }

    public bool CanPreceed(Page other)
    {
        return !IsSuccessorOf(other);
    }

    public bool CanSucceed(Page other)
    {
        return !IsPredecessorOf(other);
    }
}
