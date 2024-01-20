using CleanArchByMe.Domain.TodoAggregate;

using MediatR;

namespace CleanArchByMe.Application.TodoUseCases.Commands;

public record UpdateTodoCommand(Guid Id, string Title, string Description, DateTime? DueDate = null, DateTime? CompletionDateTime = null) : IRequest
{
    internal Todo Update(Todo todo) => todo.Update(Title, Description, DueDate, CompletionDateTime);
}
