using CleanArchByArdalis.Web.ContributorEndpoints;

namespace CleanArchByArdalis.Web.Endpoints.ContributorEndpoints;

public class ContributorListResponse
{
  public List<ContributorRecord> Contributors { get; set; } = new();
}
