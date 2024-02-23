using System.Net;
using System.Text;

using CleanArchByMe.Application.TodoUseCases.Queries;
using CleanArchByMe.Domain.TodoAggregate;
using CleanArchByMe.FunctionalTests.Abstractions;
using CleanArchByMe.FunctionalTests.Extensions;

using FluentAssertions;

namespace CleanArchByMe.FunctionalTests.Apis;

public class TodosControllerTests: IClassFixture<ApplicationFactory<Program>>
{
    private readonly ApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public TodosControllerTests(ApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task PostShouldReturnCreated()
    {
        var title = "A title";
        var description = "A description";
        var todo = new Todo(title, description);

        var createdTodo = await CreateTodo(todo);

        createdTodo.Should().NotBeNull();
        createdTodo!.Title.Should().Be(title);
        createdTodo!.Description.Should().Be(description);
        createdTodo!.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetShouldReturnOk()
    {
        var title = "A title";
        var description = "A description";
        var createdTodo = await CreateTodo(new Todo(title, description));

        var response = await _client.GetAsync($"/todos/{createdTodo!.Id}");

        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var parsedResponse = await response.Content.Deserialize<Todo>();
        parsedResponse.Should().NotBeNull();
        parsedResponse!.Title.Should().Be(title);
        parsedResponse!.Description.Should().Be(description);
        parsedResponse!.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task FetchShouldReturnOk()
    {
        await CreateTodo(new Todo("A title", "A description"));

        var response = await _client.GetAsync("/todos");

        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var parsedResponse = await response.Content.Deserialize<IEnumerable<TodoViewModel>>();
        parsedResponse.Should().NotBeNull();
        parsedResponse.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task UpdateShouldReturnSuccess()
    {
        var createdTodo = await CreateTodo(new Todo("A title", "A description"));
        var updatingTodo = createdTodo!.Complete(DateTime.Now);

        var response = await _client.PutAsync($"/todos/{createdTodo.Id}", updatingTodo.ToJsonStringContent());

        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        (await response.Content.ReadAsStringAsync()).Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteShouldReturnSuccess()
    {
        var createdTodo = await CreateTodo(new Todo("A title", "A description"));

        var response = await _client.DeleteAsync($"/todos/{createdTodo!.Id}");

        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        (await response.Content.ReadAsStringAsync()).Should().BeEmpty();
    }

    private async Task<Todo?> CreateTodo(Todo todo)
    {
        var response = await _client.PostAsync("/todos", todo.ToJsonStringContent());

        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        return await response.Content.Deserialize<Todo>();
    }
}
