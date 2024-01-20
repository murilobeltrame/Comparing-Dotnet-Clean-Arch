using CleanArchByMe.Domain.TodoAggregate;
using CleanArchByMe.Domain.TodoAggregate.Specifications;

using FluentAssertions;

namespace CleanArchByMe.UnitTests.Domain.TodoAggregate.Specifications;

public class GetTodoByIdSpecificationTests
{
    private readonly IList<Todo> _todos = new List<Todo>()
    {
        new("Title A", "Description A"),
        new("Title B", "Description B"),
        new("Title C", "Description C"),
        new("Title D", "Description D"),
        new("Title E", "Description E"),
    };

    [Fact]
    public void ShouldReturnEntityIfFound()
    {
        var id = Guid.NewGuid();
        _todos.Add(new("Title", "Description") { Id = id });
        var specification = new GetTodoByIdSpecification(id);

        var result = specification.Evaluate(_todos).FirstOrDefault();

        result.Should().NotBeNull();
        result!.Id.Should().Be(id);
    }

    [Fact]
    public void ShouldReturnNullIfNotFound()
    {
        var specification = new GetTodoByIdSpecification(Guid.NewGuid());

        var result = specification.Evaluate(_todos).FirstOrDefault();

        result.Should().BeNull();
    }
}
