using Serenity.Services;

namespace AdvanceCRM.Administration
{
    public class UserRoleListRequest : ServiceRequest
    {
        public int? UserID { get; set; }
    }
}