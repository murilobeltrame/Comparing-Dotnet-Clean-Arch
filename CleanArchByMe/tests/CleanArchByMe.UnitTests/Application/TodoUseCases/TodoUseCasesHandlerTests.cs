using Ardalis.Specification;

using Bogus;

using CleanArchByMe.Application.TodoUseCases;
using CleanArchByMe.Application.TodoUseCases.Commands;
using CleanArchByMe.Application.TodoUseCases.Queries;
using CleanArchByMe.Domain.Shared.Exceptions;
using CleanArchByMe.Domain.Shared.Interfaces;
using CleanArchByMe.Domain.TodoAggregate;

using FluentAssertions;

using Moq;

namespace CleanArchByMe.UnitTests.Application.TodoUseCases;

public class TodoUseCasesHandlerTests
{
    private readonly Mock<IRepository<Todo>> _mockedRepository;

    public TodoUseCasesHandlerTests()
    {
        _mockedRepository = new Mock<IRepository<Todo>>();
    }

    // GET

    [Fact]
    public async Task WhenGetWithIdShouldReturnEntityIfExists()
    {
        var resultingTodo = new Todo("Title", "Description");
        var repository = new Mock<IRepository<Todo>>();
        repository
            .Setup(s => s.GetAsync(It.IsAny<ISpecification<Todo>>(), CancellationToken.None))
            .Returns(Task.FromResult<Todo?>(resultingTodo));
        var handler = new TodoUseCasesHandler(repository.Object);

        var result = await handler.Handle(new GetTodoByIdQuery(Guid.NewGuid()), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(resultingTodo);
    }

    [Fact]
    public async Task WhenGetWithIdShouldThrowEntityNotFoundIfRecordDoesntExists()
    {
        _mockedRepository
            .Setup(s => s.GetAsync(It.IsAny<ISpecification<Todo>>(), CancellationToken.None))
            .Returns(Task.FromResult<Todo?>(null));
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var action = async () => await handler.Handle(new GetTodoByIdQuery(Guid.NewGuid()), CancellationToken.None);

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    // FETCH

    [Fact]
    public async Task WhenFetchShouldReturnEntitiesWithGivenSpecification()
    {
        var todos = new Faker<Todo>()
            .CustomInstantiator(f => new Todo(f.Lorem.Word(), f.Lorem.Paragraph()))
            .Generate(15)
            .Select(TodoViewModel.FromTodo);
        _mockedRepository
            .Setup(s => s.FetchAsync(It.IsAny<ISpecification<Todo, TodoViewModel>>(), CancellationToken.None))
            .Returns(Task.FromResult(todos));
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var result = await handler.Handle(new FetchTodosQuery(), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(todos);
    }

    // CREATE

    [Fact]
    public async Task WhenCreatingShouldBeAbleToDoSo()
    {
        _mockedRepository
            .Setup(s => s.CreateAsync(It.IsAny<Todo>(), CancellationToken.None))
            .Returns(Task.FromResult(new Todo("Title", "Description")));
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var result = await handler.Handle(new CreateTodoCommand("Title", "Description"), CancellationToken.None);

        result.Should().NotBeNull();
    }

    // UPDATE

    [Fact]
    public async Task WhenUpdatingShouldBeAbleToDoSo()
    {
        var todo = new Todo("Title", "Description");
        _mockedRepository
            .Setup(s => s.GetAsync(It.IsAny<ISpecification<Todo>>(), CancellationToken.None))
            .Returns(Task.FromResult<Todo?>(todo));
        _mockedRepository
            .Setup(s => s.UpdateAsync(It.IsAny<Todo>(), CancellationToken.None))
            .Returns(Task.FromResult(todo));
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var action = async () => await handler.Handle(new UpdateTodoCommand(Guid.NewGuid(), "Title", "Description"), CancellationToken.None);

        await action.Should().NotThrowAsync();
        _mockedRepository.Verify(v => v.UpdateAsync(It.IsAny<Todo>(), CancellationToken.None));
    }

    [Fact]
    public async Task WhenUpdatingUnexistingEntityShouldThowEntityNotFound()
    {
        _mockedRepository
            .Setup(s => s.GetAsync(It.IsAny<ISpecification<Todo>>(), CancellationToken.None))
            .Returns(Task.FromResult<Todo?>(null));
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var action = async () => await handler.Handle(new UpdateTodoCommand(Guid.NewGuid(), "Title", "Description"), CancellationToken.None);

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task WhenUpdatingWithoutIdShouldThowArgumentException()
    {
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var action = async () => await handler.Handle(new UpdateTodoCommand(Guid.Empty, "Title", "Description"), CancellationToken.None);

        await action.Should().ThrowAsync<ArgumentException>();
    }

    // DELETE

    [Fact]
    public async Task WhenDeletingShouldBeAbleToDoSo()
    {
        _mockedRepository
            .Setup(s => s.GetAsync(It.IsAny<ISpecification<Todo>>(), CancellationToken.None))
            .Returns(Task.FromResult<Todo?>(new Todo("Title", "Description")));
        _mockedRepository
            .Setup(s => s.DeleteAsync(It.IsAny<Todo>(), CancellationToken.None))
            .Returns(Task.CompletedTask);
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var action = async () => await handler.Handle(new DeleteTodoCommand(Guid.NewGuid()), CancellationToken.None);

        await action.Should().NotThrowAsync();
        _mockedRepository.Verify(v => v.DeleteAsync(It.IsAny<Todo>(), CancellationToken.None));
    }

    [Fact]
    public async Task WhenDeletingUnexistingEntityShouldThowEntityNotFound()
    {
        _mockedRepository
            .Setup(s => s.GetAsync(It.IsAny<ISpecification<Todo>>(), CancellationToken.None))
            .Returns(Task.FromResult<Todo?>(null));
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var action = async () => await handler.Handle(new DeleteTodoCommand(Guid.NewGuid()), CancellationToken.None);

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task WhenDeletingWithoutIdShouldThowArgumentException()
    {
        var handler = new TodoUseCasesHandler(_mockedRepository.Object);

        var action = async () => await handler.Handle(new DeleteTodoCommand(Guid.Empty), CancellationToken.None);

        await action.Should().ThrowAsync<ArgumentException>();
    }
}
