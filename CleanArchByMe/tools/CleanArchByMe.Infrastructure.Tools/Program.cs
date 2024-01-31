using CleanArchByMe.Infrastructure.Tools;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var app = Host.CreateDefaultBuilder()
    .ConfigureServices((context,  services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("DbConnection");
        services.AddDbContext<ApplicationMigrationContext>(options => options.UseSqlServer(connectionString));
    })
    .Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationMigrationContext>();
context.Database.Migrate();