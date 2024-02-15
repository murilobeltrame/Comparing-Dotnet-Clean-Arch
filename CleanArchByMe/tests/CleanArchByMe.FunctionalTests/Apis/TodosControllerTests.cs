using CleanArchByMe.FunctionalTests.Abstractions;

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
    public async Task GetShouldReturnOk()
    {
        var response = await _client.GetAsync("/todos");

        response.EnsureSuccessStatusCode();
    }
}
