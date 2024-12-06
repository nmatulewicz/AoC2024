using AOC.ConsoleApp.Models.Day05;

namespace AOC.Tests.Day05;

public class PageTests
{
    [Fact]
    public void IsPredecessorOf_ShouldReturnTrue_IfOtherWasAddedAsSuccessor()
    {
        var page = new Page(10);
        var sucessorPage = new Page(20);
        page.AddSuccessor(sucessorPage);

        var isPredecessor = page.IsPredecessorOf(sucessorPage);

        Assert.True(isPredecessor);
    }

    [Fact]
    public void IsSuccessorOf_ShouldReturnTrue_IfPageIsAddedToOtherAsSuccessor()
    {
        var page = new Page(20);
        var predecessor = new Page(40);
        predecessor.AddSuccessor(page);

        var isSuccessor = page.IsSuccessorOf(predecessor);

        Assert.True(isSuccessor);
    }

    [Fact]
    public void IsPredecessorOf_ShouldBeTransitive()
    {
        var page1 = new Page(10);
        var page2 = new Page(20);
        var page3 = new Page(13);

        page1.AddSuccessor(page2); 
        page2.AddSuccessor(page3);

        var isPredecessor = page1.IsPredecessorOf(page3);

        Assert.True(isPredecessor);
    }

    [Fact]
    public void IsSuccessorOf_ShouldBeTransitive()
    {
        var page1 = new Page(10);
        var page2 = new Page(20);
        var page3 = new Page(13);

        page1.AddSuccessor(page2); 
        page2.AddSuccessor(page3);

        var isSuccessor = page3.IsSuccessorOf(page1);

        Assert.True(isSuccessor);
    }

    [Fact]
    public void IsPredecessorOf_ShouldReturnFalse_IfNoSuccessorsWereAdded_1()
    {
        var page1 = new Page(10);
        var page2 = new Page(20);

        var isPredecessor = page1.IsPredecessorOf(page2);

        Assert.False(isPredecessor);
    }
    [Fact]
    public void IsPredecessorOf_ShouldReturnFalse_IfNoSuccessorsWereAdded_2()
    {
        var page1 = new Page(10);
        var page2 = new Page(20);

        var isPredecessor = page2.IsPredecessorOf(page1);

        Assert.False(isPredecessor);
    }

    [Fact]
    public void IsSuccessorOf_ShouldReturnFalse_IfNoSuccessorsWereAdded_1()
    {
        var page1 = new Page(10);
        var page2 = new Page(20);

        var isSuccessor = page1.IsSuccessorOf(page2);

        Assert.False(isSuccessor);
    }
    [Fact]
    public void IsSuccessorOf_ShouldReturnFalse_IfNoSuccessorsWereAdded_2()
    {
        var page1 = new Page(10);
        var page2 = new Page(20);

        var isSuccessor = page2.IsSuccessorOf(page1);

        Assert.False(isSuccessor);
    }
}
