using Ardalis.Result;
using Ardalis.SharedKernel;

namespace CleanArchByArdalis.UseCases.Contributors.Update;

public record UpdateContributorCommand(int ContributorId, string NewName) : ICommand<Result<ContributorDTO>>;
