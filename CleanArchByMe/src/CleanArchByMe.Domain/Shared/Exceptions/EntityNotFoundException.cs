namespace CleanArchByMe.Domain.Shared.Exceptions;

public class EntityNotFoundException(string entityName): Exception($"Cannot find {entityName}");
