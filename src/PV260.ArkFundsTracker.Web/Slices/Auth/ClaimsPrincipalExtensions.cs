using System.Globalization;
using System.Security.Claims;

namespace PV260.ArkFundsTracker.Web.Slices.Auth;

public static class ClaimsPrincipalExtensions
{
    public static int? GetUserId(this ClaimsPrincipal user)
    {
        var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userIdString, CultureInfo.InvariantCulture, out var userId)) return userId;

        return null;
    }
}