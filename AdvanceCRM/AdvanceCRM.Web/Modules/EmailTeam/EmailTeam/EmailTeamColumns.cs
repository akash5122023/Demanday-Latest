using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EmailTeam.Columns
{
    [ColumnsScript("EmailTeam.EmailTeam")]
    [BasedOnRow(typeof(EmailTeamRow), CheckNames = true)]
    public class EmailTeamColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [DisplayName("Master Account")]
        public String MasterAccountNumber { get; set; }
        [DisplayName("Campaign Id")]
        public String CampaignCode { get; set; }
        [EditLink]
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public EmailTeamStatus Status { get; set; }
        [DisplayName("Created By")]
        public String OwnerUsername { get; set; }
    }
}
