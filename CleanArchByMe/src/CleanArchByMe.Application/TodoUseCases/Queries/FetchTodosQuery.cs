using Ardalis.Specification;

using CleanArchByMe.Domain.TodoAggregate;
using CleanArchByMe.Domain.TodoAggregate.Specifications;

using MediatR;

namespace CleanArchByMe.Application.TodoUseCases.Queries;

public record FetchTodosQuery(string Title, string Description, DateTime? StartDate, DateTime? EndDate, bool? Complete) : IRequest<IEnumerable<TodoViewModel>>
{
    internal ISpecification<Todo, TodoViewModel> ToSpecification() => new FetchTodosSpecification<TodoViewModel>(0, 10, s => new TodoViewModel(s.Title, s.DueDate, s.Completed));
}

public record TodoViewModel(string Title, DateTime? DueDate, bool Complete);