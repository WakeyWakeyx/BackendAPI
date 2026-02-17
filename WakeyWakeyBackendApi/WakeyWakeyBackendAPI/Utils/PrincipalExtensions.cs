using System.Security.Claims;

namespace WakeyWakeyBackendAPI.Utils;

/// <summary>Helper extensions for working with ClaimsPrincipal objects.</summary>
public static class PrincipalExtensions
{
    /// <summary>Obtains the unique id of a user from a ClaimsPrincipal.</summary>
    /// <param name="principal">The ClaimsPrincipal receiver object.</param>
    /// <returns>The integer id of the user or null if none could be obtained</returns>
    public static int? GetUserId(this ClaimsPrincipal principal)
    {
        var userIdText = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdText != null && int.TryParse(userIdText, out var userId))
            return userId;
        return null;
    }
}