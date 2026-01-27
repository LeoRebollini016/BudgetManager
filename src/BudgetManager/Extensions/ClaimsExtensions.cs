using System.Security.Claims;

namespace BudgetManager.Extensions;

public static class ClaimsExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(value))
            throw new UnauthorizedAccessException("Usuario no autenticado.");

        return Guid.Parse(value);
    }
}
