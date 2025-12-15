using System.Security.Claims;

namespace AdvanceCRM
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Returns user identifier claim value.
        /// </summary>
        public static string GetIdentifier(this ClaimsPrincipal principal)
        {
            return principal?.FindFirst("UserId")?.Value;
        }
    }
}
