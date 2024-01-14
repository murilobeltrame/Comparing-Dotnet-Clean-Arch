using Ardalis.Specification;

namespace CleanArchByMe.Domain.TodoAggregate.Specifications;

public class GetTodoByIdSpecification : Specification<Todo>
{
    public GetTodoByIdSpecification(Guid id) => Query.Where(w => w.Id == id);
}