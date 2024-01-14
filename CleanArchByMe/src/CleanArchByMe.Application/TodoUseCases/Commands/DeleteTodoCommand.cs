using MediatR;

namespace CleanArchByMe.Application.TodoUseCases.Commands;

public record DeleteTodoCommand(Guid Id): IRequest;
