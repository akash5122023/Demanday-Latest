using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EmailTeam.Forms
{
    [FormScript("EmailTeam.EmailTeam")]
    [BasedOnRow(typeof(EmailTeamRow), CheckNames = true)]
    public class EmailTeamForm
    {
        [Category("Email Team")]
        [HalfWidth, LookupEditor(typeof(Masters.DemandayMasterAccountRow))]
        public Int32 MasterAccountId { get; set; }
        // Campaign list is limited to the campaigns of the selected Master Account.
        [HalfWidth, LookupEditor(typeof(Masters.DemandayCampaignIdRow), CascadeFrom = "MasterAccountId", CascadeField = "DemandayMasterAccountId")]
        public Int32 CampaignId { get; set; }
        [HalfWidth]
        public String FirstName { get; set; }
        [HalfWidth]
        public String LastName { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        // Saving a new value here also writes it to DemandayQuality.EmailTeamStatus
        // for the linked Quality record (EmailTeamSaveHandler).
        [FullWidth]
        public EmailTeamStatus Status { get; set; }
        // "Created By" – auto-stamped, read-only.
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}
