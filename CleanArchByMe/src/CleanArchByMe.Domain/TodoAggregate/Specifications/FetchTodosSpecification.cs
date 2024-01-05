using CleanArchByMe.Domain.Shared.Abstracts;

namespace CleanArchByMe.Domain.TodoAggregate.Specifications;

public class FetchTodosSpecification(uint skip, ushort take) : PagedSpecification<Todo>(skip, take) { }