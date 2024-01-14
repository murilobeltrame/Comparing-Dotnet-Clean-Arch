using Microsoft.EntityFrameworkCore;

namespace CleanArchByMe.Infrastructure.Data;

public class ApplicationContext: DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options) { }
}
