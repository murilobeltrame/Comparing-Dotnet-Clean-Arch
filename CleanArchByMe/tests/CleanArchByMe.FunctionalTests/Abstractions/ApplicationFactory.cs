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

            services.AddDbContext<ApplicationContext>(options => options
                .UseSqlite("DataSource=:memory:", o => 
                    o.MigrationsAssembly(typeof(ApplicationMigrationContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging());

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            context.Database.Migrate();
        });
        builder.UseEnvironment("Development");
    }
}
