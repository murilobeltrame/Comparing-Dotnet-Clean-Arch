using CleanArchByMe.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace CleanArchByMe.Infrastructure.Tools;

public class ApplicationMigrationContext(DbContextOptions options) : ApplicationContext(options) { }