using CleanArchByMe.Api.Shared.Middlewares;
using CleanArchByMe.Application;
using CleanArchByMe.Domain.Shared.Interfaces;
using CleanArchByMe.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options => options.AddServerHeader = false);
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<IApplication>());
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddControllers();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationContext>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();

public partial class Program { }