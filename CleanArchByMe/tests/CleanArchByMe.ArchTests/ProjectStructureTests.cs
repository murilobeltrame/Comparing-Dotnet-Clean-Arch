using System.Reflection;

using FluentAssertions;

using NetArchTest.Rules;

namespace CleanArchByMe.ArchTests;

public class ProjectStructureTests
{
    [Fact]
    public void Domain_Should_Not_Be_Dependent_of_Anything()
    {
        var test = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(nameof(Domain))
            .ShouldNot()
            .HaveDependencyOn(nameof(Api))
            .And()
            .HaveDependencyOn(nameof(Application))
            .And()
            .HaveDependencyOn(nameof(Infrastructure))
            .GetResult();
        test.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Be_Dependent_by_Domain()
    {
        var test = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(nameof(Application))
            .Should()
            .HaveDependencyOn(nameof(Domain))
            .GetResult();
        test.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_Be_Dependent_by_Api_and_Infrastructure()
    {
        var test = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(nameof(Application))
            .ShouldNot()
            .HaveDependencyOn(nameof(Api))
            .And()
            .HaveDependencyOn(nameof(Infrastructure))
            .GetResult();
        test.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Be_Dependent_by_Domain()
    {
        var test = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(nameof(Infrastructure))
            .Should()
            .HaveDependencyOn(nameof(Domain))
            .GetResult();
        test.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_Be_Dependent_by_Api_and_Application()
    {
        var test = Types
            .InCurrentDomain()
            .That()
            .ResideInNamespace(nameof(Infrastructure))
            .ShouldNot()
            .HaveDependencyOn(nameof(Api))
            .And()
            .HaveDependencyOn(nameof(Application))
            .GetResult();
        test.IsSuccessful.Should().BeTrue();
    }

}