using Ardalis.Specification;
using CleanArchByMe.Domain.Shared.Interfaces;

namespace CleanArchByMe.Domain.Shared.Abstracts;

public abstract class PagedSpecification<T>: Specification<T> where T: IEntity {
    protected PagedSpecification(uint skip, ushort take) => Query
        .Skip((int)skip)
        .Take(take);
}