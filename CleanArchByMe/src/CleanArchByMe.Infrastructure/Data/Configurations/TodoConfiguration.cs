using CleanArchByMe.Domain.TodoAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchByMe.Infrastructure.Data.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(255);
    }
}
