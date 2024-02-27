using System.Reflection;

using CleanArchByMe.Domain.Shared.Abstracts;

using FluentAssertions;

using Mono.Cecil;
using Mono.Cecil.Rocks;

using NetArchTest.Rules;

namespace CleanArchByMe.ArchTests.Domain;

public class EntitiesTests
{
    [Fact]
    public void Domain_Models_Shouldnt_Have_Public_Or_Protected_Default_Constructors()
    {
        var test = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(nameof(Domain))
            .And()
            .Inherit(typeof(Entity))
            .ShouldNot()
            .MeetCustomRule(new HaventPublicOrProtectedDefaultConstructors())
            .GetResult();
        test.IsSuccessful.Should().BeTrue();
    }

    private class HaventPublicOrProtectedDefaultConstructors : ICustomRule
    {
        public bool MeetsRule(TypeDefinition type)
        {
            return type.GetConstructors()
                .Any(ctor => !(ctor.IsPublic && ctor.Parameters.Count == 0));
        }
    }
}
