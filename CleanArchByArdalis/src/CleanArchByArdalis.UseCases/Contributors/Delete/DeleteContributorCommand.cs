using Ardalis.Result;
using Ardalis.SharedKernel;

namespace CleanArchByArdalis.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
