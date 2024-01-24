using System.Linq.Expressions;

using Ardalis.Specification;

using CleanArchByMe.Domain.Shared.Abstracts;

namespace CleanArchByMe.Domain.TodoAggregate.Specifications;

public class FetchTodosSpecification<TResult> :
    PagedSpecification<Todo, TResult>
{
    public FetchTodosSpecification(
    string? title,
    string? description,
    DateTime? startDate,
    DateTime? endDate,
    bool? complete,
    uint skip,
    ushort take,
    Expression<Func<Todo, TResult>> projection) : base(skip, take, projection)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            Query.Search(s => s.Title, title.Replace("*", "%"));
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            Query.Search(s => s.Description, description.Replace("*", "%"));
        }

        if (startDate.HasValue)
        {
            Query.Where(w => w.DueDate >=  startDate.Value);
        }

        if (endDate.HasValue)
        {
            Query.Where(w => w.DueDate <= endDate.Value);
        }

        if (complete.HasValue)
        {
            Query.Where(w => complete.Value == w.CompletionDateTime.HasValue);
        }
    }
}