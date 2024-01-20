using Ardalis.Specification;

using Bogus;

using CleanArchByMe.Domain.Shared.Abstracts;
using CleanArchByMe.Domain.Shared.Interfaces;
using CleanArchByMe.Infrastructure.Data;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchByMe.IntegrationTests.Infrastructure.Data;

public class RepositoryTests
{
    class Thing(string title, string description) : Entity
    {
        public string Title { get; private set; } = title;
        public string Description { get; private set; } = description;

        internal void UpdateDescription(string value) => Description = value;
    }

    record ThingView(string Title)
    {
        public static ThingView From(Thing thing) => new(thing.Title);
    }

    class TestingContext(DbContextOptions options) : ApplicationContext(options)
    {
        public DbSet<Thing> Things { get; set; }
    }

    class GetThingByIdSpecification : Specification<Thing>
    {
        public GetThingByIdSpecification(Guid id) => Query.Where(w => w.Id == id);
    }

    class GetThingViewByIdSpecification : Specification<Thing, ThingView>
    {
        public GetThingViewByIdSpecification(Guid id)
        {
            Query.Where(w => w.Id == id);
            Query.Select(s => ThingView.From(s));
        }
    }

    private readonly IRepository<Thing> _repository;
    private readonly TestingContext _context;

    public RepositoryTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder
            .UseInMemoryDatabase("TestingDb")
            .UseInternalServiceProvider(serviceProvider);

        var options = builder.Options;
        _context = new TestingContext(options);
        _repository = new Repository<Thing>(_context);
    }

    [Fact]
    public async Task ShouldBeAbleToRecordAnEntity()
    {
        var thing = new Thing("Foo", "Bar");

        var result = await _repository.CreateAsync(thing);

        result.Should().NotBeNull();
        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task ShouldBeAbleToUpdateATackedEntity()
    {
        var thing = new Thing("Thing", "To be updated");
        await _context.AddAsync(thing);
        await _context.SaveChangesAsync();
        var updatedDecription = "Description Updated";
        thing.UpdateDescription(updatedDecription);

        thing.Id.Should().NotBe(Guid.Empty);

        await _repository.UpdateAsync(thing);

        var updatedThing = await _context.Things.FirstOrDefaultAsync(w => w.Id == thing.Id);
        updatedThing.Should().NotBeNull();
        updatedThing!.Description.Should().Be(updatedDecription);
    }

    [Fact]
    public async Task ShouldBeAbleToDeleteATrackedEntity()
    {
        var thing = new Thing("Thing", "To be deleted");
        await _context.AddAsync(thing);
        await _context.SaveChangesAsync();

        thing.Id.Should().NotBe(Guid.Empty);

        await _repository.DeleteAsync(thing);

        var deletedThing = await _context.Things.FirstOrDefaultAsync(w => w.Id == thing.Id);
        deletedThing.Should().BeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToGetBySpecification()
    {
        var thing = new Thing("Thing", "To be queried");
        await _context.AddAsync(thing);
        await _context.SaveChangesAsync();

        thing.Id.Should().NotBe(Guid.Empty);

        var result = await _repository.GetAsync(new GetThingByIdSpecification(thing.Id));

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToGetAndProjectBySpecification()
    {
        var thing = new Thing("Thing", "To be queried");
        await _context.AddAsync(thing);
        await _context.SaveChangesAsync();

        thing.Id.Should().NotBe(Guid.Empty);

        var result = await _repository.GetAsync(new GetThingViewByIdSpecification(thing.Id));

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToFetchBySpecification()
    {
        var things = new Faker<Thing>()
            .CustomInstantiator(f => new Thing(f.Lorem.Word(), f.Lorem.Sentence()))
            .Generate(10);
        await _context.AddRangeAsync(things);
        await _context.SaveChangesAsync();
        var lastThing = await _context.Things.LastAsync();

        var result = await _repository.FetchAsync(new GetThingByIdSpecification(lastThing.Id));

        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task ShouldBeAbleToFetchAndProjectBySpecification()
    {
        var things = new Faker<Thing>()
                .CustomInstantiator(f => new Thing(f.Lorem.Word(), f.Lorem.Sentence()))
                .Generate(10);
        await _context.AddRangeAsync(things);
        await _context.SaveChangesAsync();
        var lastThing = await _context.Things.LastAsync();

        var result = await _repository.FetchAsync(new GetThingViewByIdSpecification(lastThing.Id));

        result.Should().HaveCount(1);
    }
}
