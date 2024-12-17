using System.Security.Claims;
using PurchaseOrder.Domain.Constants;
using PurchaseOrder.Domain.Enums;

namespace PurchaseOrder.Domain.Objects;

public class LoggedPerson
{
    public long? Id { get; init; }
    public string Name { get; init; }
    public IEnumerable<string> Roles { get; init; } = [];
    public bool IsAuthenticated => Id > 0;
    public bool IsAdministrator => Roles.Contains(RoleConstant.Administrator);
    public static LoggedPerson Anonymous() => new();
    public static LoggedPerson System() => new(){Name = UserConstant.System};

    public LoggedPerson(ClaimsPrincipal user) : this()
    {
        Name = user.Identity?.Name ?? UserConstant.Anonymous;
        Id = GetLoggedPersonId(user.Claims);
    }

    public LoggedPerson()
    {
        Name = UserConstant.Anonymous;
    }

    private static long GetLoggedPersonId(IEnumerable<Claim> claims) => claims
        .Where(claim => claim.Type == ClaimType.Id.ToString())
        .Select(claim => Convert.ToInt64(claim.Value)).FirstOrDefault();
}