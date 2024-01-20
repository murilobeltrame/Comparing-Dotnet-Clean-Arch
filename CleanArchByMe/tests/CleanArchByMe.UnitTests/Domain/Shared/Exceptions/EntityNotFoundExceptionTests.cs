using CleanArchByMe.Domain.Shared.Exceptions;

using FluentAssertions;

namespace CleanArchByMe.UnitTests.Domain.Shared.Exceptions;

public class EntityNotFoundExceptionTests
{
    [Fact]
    public void ShouldInstantiateWithCorrectMessage()
    {
        var ex = new EntityNotFoundException("potato");
        ex.Message.Should().Be("Cannot find potato");
    }
}
