using CleanArchByMe.Domain.Shared.Abstracts;

using FluentAssertions;

namespace CleanArchByMe.UnitTests.Domain.Abstracts;

public class EntityTests
{
    [Fact]
    public void ShouldBeInstantiated()
    {
        var id = Guid.NewGuid();
        var entity = new EntityWrap { Id = id };

        entity.Id.Should().Be(id);
    }

    class EntityWrap : Entity { }
}
