using System.Linq.Expressions;

using Bogus;

using CleanArchByMe.Domain.Shared.Abstracts;
using CleanArchByMe.Domain.Shared.Interfaces;

using FluentAssertions;

namespace CleanArchByMe.UnitTests.Domain.Abstracts;

public class PagedSpecificationTests
{
    record Thing(string Title, string Description) : IEntity { public Guid Id { get; init; } }

    record ThingView(string Title)
    {
        public static ThingView From(Thing thing) => new(thing.Title);
    }

    class FetchPagedThings(uint skip, ushort take) : PagedSpecification<Thing>(skip, take);

    class FetchPagedProjectedThings<ThingView>(uint skip, ushort take, Expression<Func<Thing, ThingView>> projection): 
        PagedSpecification<Thing, ThingView>(skip, take, projection);

    private readonly IEnumerable<Thing> _things = new Faker<Thing>()
        .CustomInstantiator(f => new Thing(f.Lorem.Word(), f.Lorem.Paragraph()))
        .Generate(50);

    [Fact]
    public void ShouldMatchPaginationSpecification()
    {
        var specification = new FetchPagedThings(10, 10);

        var result = specification.Evaluate(_things);

        result.Should().HaveCount(10);
    }

    [Fact]
    public void ShouldMatchProjectionSpecification()
    {
        var specification = new FetchPagedProjectedThings<ThingView>(10, 10, thing => ThingView.From(thing));

        var result = specification.Evaluate(_things);

        result.Should().HaveCount(10);
    }
}
