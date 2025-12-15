using System.Collections.Generic;

namespace AdvanceCRM.Administration
{
    public class ScriptUserDefinition
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }
        public string UpperLevel { get; set; }
        public int? BranchId { get; set; }
        public int? CompanyId { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }
    }
}
