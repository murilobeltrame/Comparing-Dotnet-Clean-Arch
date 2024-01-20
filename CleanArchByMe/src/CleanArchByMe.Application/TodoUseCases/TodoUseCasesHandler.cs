using Ardalis.GuardClauses;

using CleanArchByMe.Application.TodoUseCases.Commands;
using CleanArchByMe.Application.TodoUseCases.Queries;
using CleanArchByMe.Domain.Shared.Exceptions;
using CleanArchByMe.Domain.Shared.Interfaces;
using CleanArchByMe.Domain.TodoAggregate;
using CleanArchByMe.Domain.TodoAggregate.Specifications;

using MediatR;

namespace CleanArchByMe.Application.TodoUseCases;

public class TodoUseCasesHandler(IRepository<Todo> repository) : 
    IRequestHandler<CreateTodoCommand, Todo>,
    IRequestHandler<UpdateTodoCommand>,
    IRequestHandler<DeleteTodoCommand>,
    IRequestHandler<GetTodoByIdQuery, Todo>,
    IRequestHandler<FetchTodosQuery, IEnumerable<TodoViewModel>>
{
    private readonly IRepository<Todo> _repository = repository;

    public async Task<Todo> Handle(CreateTodoCommand request, CancellationToken cancellationToken) =>
        await _repository.CreateAsync(request.ToEntity(), cancellationToken);

    public async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var existingTodo = await GetEntityAsync(request.Id, cancellationToken);
        var updatedTodo = request.Update(existingTodo);
        await _repository.UpdateAsync(updatedTodo, cancellationToken);
    }

    public async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var existingTodo = await GetEntityAsync(request.Id, cancellationToken);
        await _repository.DeleteAsync(existingTodo, cancellationToken);
    }

    public async Task<Todo> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken) => 
        await _repository.GetAsync(request.ToSpecification(), cancellationToken) ?? throw new EntityNotFoundException(nameof(Todo));

    public async Task<IEnumerable<TodoViewModel>> Handle(FetchTodosQuery request, CancellationToken cancellationToken) =>
        await _repository.FetchAsync(request.ToSpecification(), cancellationToken);

    private async Task<Todo> GetEntityAsync(Guid id, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(id, nameof(id));
        return await _repository.GetAsync(new GetTodoByIdSpecification(id), cancellationToken) ??
            throw new EntityNotFoundException(nameof(Todo));
    }
}
