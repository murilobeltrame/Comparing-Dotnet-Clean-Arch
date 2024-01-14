using Ardalis.Specification;

namespace CleanArchByMe.Domain.Shared.Interfaces;

public interface IRepository<T> where T : IEntity
{
    Task<T?> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    Task<TResult?> GetAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> FetchAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    Task<IEnumerable<TResult>> FetchAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}