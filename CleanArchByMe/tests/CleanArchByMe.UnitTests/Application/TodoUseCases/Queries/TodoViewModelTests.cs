using CleanArchByMe.Application.TodoUseCases.Queries;
using CleanArchByMe.Domain.TodoAggregate;

using FluentAssertions;

namespace CleanArchByMe.UnitTests.Application.TodoUseCases.Queries;

public class TodoViewModelTests
{
    [Fact]
    public void ShouldBeInstantiatedFromTodo()
    {
        var todo = new Todo("Title", "Description", DateTime.Now);
        
        var viewModel = TodoViewModel.FromTodo(todo);

        viewModel.Title.Should().Be(todo.Title);
        viewModel.DueDate.Should().Be(todo.DueDate);
        viewModel.Complete.Should().Be(todo.Completed);
    }
}
