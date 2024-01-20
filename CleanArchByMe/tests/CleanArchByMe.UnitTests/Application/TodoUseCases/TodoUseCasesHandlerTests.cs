using Ardalis.Specification;

using CleanArchByMe.Application.TodoUseCases;
using CleanArchByMe.Application.TodoUseCases.Queries;
using CleanArchByMe.Domain.Shared.Interfaces;
using CleanArchByMe.Domain.TodoAggregate;

using FluentAssertions;

using Moq;

namespace CleanArchByMe.UnitTests.Application.TodoUseCases;

public class TodoUseCasesHandlerTests
{
    private readonly IRepository<Todo> _repository = Mock.Of<IRepository<Todo>>();
    private readonly TodoUseCasesHandler _handler;

    public TodoUseCasesHandlerTests()
    {
        _handler = new TodoUseCasesHandler(_repository);
    }

    [Fact]
    public async Task ShouldGetEntityWithGivenIdIfExists()
    {
        var todo = new Todo("title", "description") { Id = Guid.NewGuid() };
        var todos = new List<Todo>() { todo };
        var repository = new Mock<IRepository<Todo>>();
        //repository
        //    .Setup(s => s.GetAsync(It.IsAny<ISpecification<Todo>>(), CancellationToken.None))
        //    .Returns()

        //var result = await _handler.Handle(new GetTodoByIdQuery(todo.Id), CancellationToken.None);
        //result.Should().NotBeNull();
    }

    public void WhenGetShouldThrowNotFoundIfGivenIdDoesntExists()
    {

    }
}
