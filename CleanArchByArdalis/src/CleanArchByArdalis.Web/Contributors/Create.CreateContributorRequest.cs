using System.ComponentModel.DataAnnotations;

namespace CleanArchByArdalis.Web.Endpoints.ContributorEndpoints;

public class CreateContributorRequest
{
  public const string Route = "/Contributors";

  [Required]
  public string? Name { get; set; }
}
