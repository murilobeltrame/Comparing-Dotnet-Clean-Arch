using CleanArchByMe.Domain.TodoAggregate;
using CleanArchByMe.Domain.TodoAggregate.Specifications;

using MediatR;

namespace CleanArchByMe.Application.TodoUseCases.Queries;

public record GetTodoByIdQuery(Guid Id) : IRequest<Todo>
{
    internal GetTodoByIdSpecification ToSpecification() => new(Id);
}
