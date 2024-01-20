using Ardalis.Specification;

using CleanArchByMe.Domain.TodoAggregate;
using CleanArchByMe.Domain.TodoAggregate.Specifications;

using MediatR;

namespace CleanArchByMe.Application.TodoUseCases.Queries;

public record FetchTodosQuery(
    string? Title = null, 
    string? Description = null, 
    DateTime? StartDate = null, 
    DateTime? EndDate = null, 
    bool? Complete = null) : IRequest<IEnumerable<TodoViewModel>>
{
    public ISpecification<Todo, TodoViewModel> ToSpecification() => new FetchTodosSpecification<TodoViewModel>(0, 10, todo => TodoViewModel.FromTodo(todo));
}

public record TodoViewModel(string Title, DateTime? DueDate, bool Complete)
{
    public static TodoViewModel FromTodo(Todo todo) => new(todo.Title, todo.DueDate, todo.Completed);
}