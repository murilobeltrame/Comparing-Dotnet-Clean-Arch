using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

using CleanArchByMe.Domain.Shared.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace CleanArchByMe.Infrastructure.Data;

public class Repository<T>(ApplicationContext context) : IRepository<T> where T : class, IEntity
{
    private readonly ApplicationContext _context = context;

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<IEnumerable<T>> FetchAsync(ISpecification<T> specification, CancellationToken cancellationToken = default) =>
        await _context.Set<T>()
            .WithSpecification(specification)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<TResult>> FetchAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) =>
       await _context.Set<T>()
           .WithSpecification(specification)
           .ToListAsync(cancellationToken);

    public async Task<T?> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default) =>
        await _context.Set<T>()
            .WithSpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<TResult?> GetAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) => 
        await _context.Set<T>()
            .WithSpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);
}
