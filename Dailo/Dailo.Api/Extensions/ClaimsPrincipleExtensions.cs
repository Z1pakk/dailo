using System.Security.Claims;

namespace Dailo.Api.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string? GetUserIdentityId(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
