using CleanArchByMe.Application.TodoUseCases.Queries;

using FluentAssertions;

namespace CleanArchByMe.UnitTests.Application.TodoUseCases.Queries;

public class FetchTodosQueryTests
{
    [Fact]
    public void ShouldBeAbleToCastSpecification()
    {
        var query = new FetchTodosQuery("title*", "*desciprition*", DateTime.Now, DateTime.Now, true);

        var action = () => query.ToSpecification();

        action.Should().NotThrow();
    }
}
