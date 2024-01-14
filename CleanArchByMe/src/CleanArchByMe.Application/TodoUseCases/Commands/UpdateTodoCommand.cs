using CleanArchByMe.Domain.TodoAggregate;

using MediatR;

namespace CleanArchByMe.Application.TodoUseCases.Commands;

public record UpdateTodoCommand(Guid Id, string Title, string Description, DateTime? DueDate, DateTime? CompletionDateTime) : IRequest
{
    internal Todo Update(Todo todo) => todo.Update(Title, Description, DueDate, CompletionDateTime);
}
