using Ardalis.Result;
using Ardalis.SharedKernel;

namespace CleanArchByArdalis.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
