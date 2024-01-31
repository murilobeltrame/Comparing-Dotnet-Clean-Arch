using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

using CleanArchByMe.Infrastructure.Data.Configurations;

namespace CleanArchByMe.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
