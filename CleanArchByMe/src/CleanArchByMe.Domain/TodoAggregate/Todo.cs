using Ardalis.GuardClauses;
using CleanArchByMe.Domain.Shared.Abstracts;

namespace CleanArchByMe.Domain.TodoAggregate;

public class Todo(string title, string description, DateTime? dueDate = null, DateTime? completionDateTime = null) : Entity
{
    public string Title { get; private set; } = Guard.Against.NullOrWhiteSpace(title);
    public string Description { get; private set; } = Guard.Against.NullOrWhiteSpace(description);
    public DateTime? DueDate { get; private set; } = dueDate;
    public DateTime? CompletionDateTime { get; private set; } = completionDateTime;
    public bool Completed { get { return CompletionDateTime.HasValue; } } 

    public void Complete(DateTime datetime) => CompletionDateTime = datetime;

    public Todo Update(string title, string description, DateTime? dueDate, DateTime? completionDateTime)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        CompletionDateTime = completionDateTime;
        return this;
    }
}