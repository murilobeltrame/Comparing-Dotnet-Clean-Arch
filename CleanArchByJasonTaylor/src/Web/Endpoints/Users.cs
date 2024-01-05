using CleanArchByJasonTaylor.Infrastructure.Identity;

namespace CleanArchByJasonTaylor.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
