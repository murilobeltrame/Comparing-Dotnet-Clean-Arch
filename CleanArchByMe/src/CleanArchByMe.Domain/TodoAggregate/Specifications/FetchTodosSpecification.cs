using System.Linq.Expressions;

using CleanArchByMe.Domain.Shared.Abstracts;

namespace CleanArchByMe.Domain.TodoAggregate.Specifications;

public class FetchTodosSpecification<TResult>(uint skip, ushort take, Expression<Func<Todo, TResult>> projection) : 
    PagedSpecification<Todo, TResult>(skip, take, projection) { }