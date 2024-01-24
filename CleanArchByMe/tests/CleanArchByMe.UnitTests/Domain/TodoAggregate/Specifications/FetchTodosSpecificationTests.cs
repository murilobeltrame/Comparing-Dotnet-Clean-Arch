using Bogus;

using CleanArchByMe.Domain.TodoAggregate;
using CleanArchByMe.Domain.TodoAggregate.Specifications;

using FluentAssertions;

namespace CleanArchByMe.UnitTests.Domain.TodoAggregate.Specifications;

public class FetchTodosSpecificationTests
{
    private readonly IEnumerable<Todo> _todos = new Faker<Todo>()
        .CustomInstantiator(f => new Todo(f.Lorem.Word(), f.Lorem.Paragraph()))
        .Generate(50);

    [Fact]
    public void ShouldMatchSpecification()
    {
        var specification = new FetchTodosSpecification<View>(
            string.Empty,
            string.Empty,
            null, null,
            null,
            10, 
            10, 
            todo => View.FromTodo(todo));
        var result = specification.Evaluate(_todos);

        result.Should().HaveCount(10);
    }

    record View(string Title)
    {
        public static View FromTodo(Todo todo) => new (todo.Title);
    }
}
