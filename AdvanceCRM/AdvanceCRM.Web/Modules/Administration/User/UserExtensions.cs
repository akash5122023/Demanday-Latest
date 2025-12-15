using System.Security.Claims;

namespace AdvanceCRM
{
    public static class UserExtensions
    {
        public static UserDefinition ToUserDefinition(this ClaimsPrincipal principal)
        {
            if (principal == null || !principal.Identity.IsAuthenticated)
                return null;

            return new UserDefinition
            {
                UserId = int.TryParse(principal.FindFirst("UserId")?.Value, out var uid) ? uid : 0,
                Username = principal.Identity?.Name,
                CompanyId = int.TryParse(principal.FindFirst("CompanyId")?.Value, out var cid) ? cid : 0,
                DisplayName = principal.FindFirst("DisplayName")?.Value,
                BranchId = int.TryParse(principal.FindFirst("BranchId")?.Value, out var bid) ? (int?)bid : null,
                // convert boolean flag to int for UpperLevel property
                UpperLevel = bool.TryParse(principal.FindFirst("UpperLevel")?.Value, out var upper) && upper ? 1 : 0
            };
        }
    }
}
