using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

namespace CleanArchByMe.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
