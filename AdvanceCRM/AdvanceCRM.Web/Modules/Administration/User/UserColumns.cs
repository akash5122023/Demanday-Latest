
namespace AdvanceCRM.Administration.Columns
{
    using Serenity.ComponentModel;
    using System;

    [ColumnsScript("Administration.User")]
    [BasedOnRow(typeof(UserRow))]
    public class UserColumns
    {
        [EditLink, AlignRight, Width(55)]
        public String UserId { get; set; }
        [EditLink, Width(150)]
        public String Username { get; set; }
        [Width(150), QuickFilter]
        public String DisplayName { get; set; }
        [Width(100), QuickFilter]
        public String Phone { get; set; }
        [Width(250)]
        public String Email { get; set; }
        [Width(120), QuickFilter]
        public String Branch { get; set; }
        [Width(120), QuickFilter]
        public String TenantName { get; set; }
        [Width(200)]
        public String Url { get; set; }
        [Width(80)]
        public String TeamsTeam { get; set; }
        [Width(80)]
        public Boolean IsActive { get; set; }
        [Width(80)]
        public String UpperLevelName { get; set; }
        [Width(80)]
        public String UpperLevelName2 { get; set; }
        [Width(80)]
        public String UpperLevelName3 { get; set; }
        [Width(80)]
        public String UpperLevelName4 { get; set; }
        [Width(80)]
        public String UpperLevelName5 { get; set; }
    }
}
