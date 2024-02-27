using Ardalis.Specification;

using FluentAssertions;

using NetArchTest.Rules;

namespace CleanArchByMe.ArchTests.Domain;

public class SpecificationsTests
{
    [Fact]
    public void Specifications_Shouldnt_Be_Placed_Elsewhere_Than_Domain()
    {
        var test = Types
            .InCurrentDomain()
            .That()
            .Inherit(typeof(Specification<>))
            .Or()
            .Inherit(typeof(Specification<,>))
            .Should()
            .HaveNameEndingWith("Specification")
            .And()
            .ResideInNamespace(nameof(Domain))
            .And()
            .ResideInNamespaceEndingWith("Specifications");
    }
}
