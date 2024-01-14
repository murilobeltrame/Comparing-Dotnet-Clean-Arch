using CleanArchByMe.Domain.TodoAggregate;

namespace CleanArchByMe.UnitTests.Domain.TodoAggregate;

public class TodoTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldThrowArgumentExceptionIfTitleInstGiven(string title)
    {
        var act = () => new Todo(title, "A description");
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionIfTitleIsNull()
    {
        var act = () => new Todo(null, "A description");
        Assert.Throws<ArgumentNullException>(act);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldThrowArgumentExceptionIfDescriptionInstGiven(string description)
    {
        var act = () => new Todo("A title", description);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionIfDescriptionIsNull()
    {
        var act = () => new Todo("A title", null);
        Assert.Throws<ArgumentNullException>(act);
    }
}
