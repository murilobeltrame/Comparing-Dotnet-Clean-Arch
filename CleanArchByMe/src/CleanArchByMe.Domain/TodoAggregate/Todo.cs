using Ardalis.GuardClauses;
using CleanArchByMe.Domain.Shared.Abstracts;

namespace CleanArchByMe.Domain.TodoAggregate;

public class Todo(string description, DateTime? dueDate, bool complete = false): Entity {
    public string Description {get; private set;} = Guard.Against.NullOrEmpty(description, nameof(description));
    public DateTime? DueDate{get;private set; } = dueDate;
    public bool Complete {get; private set;} = complete;
}