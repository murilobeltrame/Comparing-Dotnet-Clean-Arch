using Microsoft.AspNetCore.Mvc.Testing;

namespace CleanArchByMe.FunctionalTests.Apis;

public class TodosControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<Program>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task GetShouldReturnOk()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/todos");

        response.EnsureSuccessStatusCode();
    }
}
