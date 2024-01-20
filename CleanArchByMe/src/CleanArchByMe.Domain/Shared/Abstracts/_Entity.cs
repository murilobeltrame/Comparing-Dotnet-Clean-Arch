using CleanArchByMe.Domain.Shared.Interfaces;

namespace CleanArchByMe.Domain.Shared.Abstracts;

public abstract class Entity : IEntity
{
    public Guid Id {get; init;}
}