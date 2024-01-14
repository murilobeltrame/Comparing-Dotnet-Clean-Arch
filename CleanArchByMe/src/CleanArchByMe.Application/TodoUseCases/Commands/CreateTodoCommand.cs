using CleanArchByMe.Domain.TodoAggregate;

using MediatR;

namespace CleanArchByMe.Application.TodoUseCases.Commands;

public record CreateTodoCommand(string Title, string Description, DateTime? DueDate, DateTime? CompletionDateTime) : IRequest<Todo>
{
    internal Todo ToEntity() => new(Title, Description, DueDate, CompletionDateTime);
}
