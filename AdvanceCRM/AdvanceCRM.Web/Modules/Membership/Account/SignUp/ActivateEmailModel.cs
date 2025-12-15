
namespace AdvanceCRM.Membership
{
    public class ActivateEmailModel
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string TenantUrl { get; set; }
        public string LoginUrl { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
    }
}