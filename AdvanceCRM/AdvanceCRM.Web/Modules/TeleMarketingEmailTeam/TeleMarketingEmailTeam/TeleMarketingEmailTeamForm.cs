using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.TeleMarketingEmailTeam.Forms
{
    [FormScript("TeleMarketingEmailTeam.TeleMarketingEmailTeam")]
    [BasedOnRow(typeof(TeleMarketingEmailTeamRow), CheckNames = true)]
    public class TeleMarketingEmailTeamForm
    {
        [Category("TM Email Team")]
        [HalfWidth, LookupEditor(typeof(Masters.DemandayMasterAccountRow))]
        public Int32 MasterAccountId { get; set; }
        [HalfWidth, LookupEditor(typeof(Masters.DemandayCampaignIdRow))]
        public Int32 CampaignId { get; set; }
        [HalfWidth]
        public String FirstName { get; set; }
        [HalfWidth]
        public String LastName { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        // Saving a new value here also writes it to DemandayTeleMarketingQualilty.EmailTeamStatus
        // for the linked TM Quality record (TeleMarketingEmailTeamSaveHandler).
        [FullWidth]
        public TeleMarketingEmailTeamStatus Status { get; set; }
        // "Created By" – auto-stamped, read-only.
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}
