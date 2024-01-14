using CleanArchByMe.Domain.Shared.Exceptions;

namespace CleanArchByMe.UnitTests.Domain.Shared.Exceptions;

public class EntityNotFoundExceptionTests
{
    [Fact]
    public void ShouldInstantiateWithCorrectMessage()
    {
        var ex = new EntityNotFoundException("potato");

        Assert.Equal("Cannot find potato", ex.Message);
    }
}
