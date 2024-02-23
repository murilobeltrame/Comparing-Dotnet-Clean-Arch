using CleanArchByMe.Infrastructure.Data;
using CleanArchByMe.Infrastructure.Tools;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CleanArchByMe.FunctionalTests.Abstractions;

public class ApplicationFactory<T>: WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationContext>>();
            services.RemoveAll<DbContextOptions<ApplicationMigrationContext>>();

            // This should be set for each individual test run
            string inMemoryCollectionName = Guid.NewGuid().ToString();

            // Add ApplicationDbContext using an in-memory database for testing.
            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(inMemoryCollectionName));
        });
    }
}
