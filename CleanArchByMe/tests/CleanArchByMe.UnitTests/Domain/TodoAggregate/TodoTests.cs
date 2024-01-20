using CleanArchByMe.Domain.TodoAggregate;

using FluentAssertions;

namespace CleanArchByMe.UnitTests.Domain.TodoAggregate;

public class TodoTests
{
    [Fact]
    public void ShouldBeInstantiated()
    {
        var title = "Some Title";
        var description = "A short taks description";
        var dueDate = DateTime.Now;
        var completionDateTime = DateTime.Now;
        var todo = new Todo(title, description, dueDate, completionDateTime);

        todo.Title.Should().Be(title);
        todo.Description.Should().Be(description);
        todo.DueDate.Should().Be(dueDate);
        todo.CompletionDateTime.Should().Be(completionDateTime);
        todo.Completed.Should().BeTrue();
    }

    [Fact]
    public void ShoulbBeAbleToComplete()
    {
        var completionDateTime = DateTime.Now;
        var todo = new Todo("A title", "A description");

        todo.Completed.Should().BeFalse();

        todo.Complete(completionDateTime);

        todo.Completed.Should().BeTrue();
        todo.CompletionDateTime.Should().Be(completionDateTime);
    }

    [Fact]
    public void ShouldBeAbleToUpdate()
    {
        var updatedTitle = "UpdatedTitle";
        var updatedDescription = "UpdatedDescription";
        var updatedDueDate = DateTime.Now.AddDays(1);
        var updatedCompletionDateTime = DateTime.Now.AddDays(1);
        var todo = new Todo("Title", "Description", DateTime.Now);

        var updatedTodo = todo.Update(updatedTitle, updatedDescription, updatedDueDate, updatedCompletionDateTime);

        todo.Title.Should().Be(updatedTitle);
        todo.Description.Should().Be(updatedDescription);
        todo.DueDate.Should().Be(updatedDueDate);
        todo.CompletionDateTime.Should().Be(updatedCompletionDateTime);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldThrowArgumentExceptionIfTitleInstGiven(string title)
    {
        var act = () => new Todo(title, "A description");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionIfTitleIsNull()
    {
        var act = () => new Todo(null, "A description");
        act.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldThrowArgumentExceptionIfDescriptionInstGiven(string description)
    {
        var act = () => new Todo("A title", description);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionIfDescriptionIsNull()
    {
        var act = () => new Todo("A title", null);
        act.Should().Throw<ArgumentNullException>();
    }
}
