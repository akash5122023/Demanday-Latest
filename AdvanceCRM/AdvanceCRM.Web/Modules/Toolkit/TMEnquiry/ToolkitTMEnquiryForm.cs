using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace AdvanceCRM.Toolkit.Forms
{
    [FormScript("Toolkit.ToolkitTMEnquiry")]
    [BasedOnRow(typeof(ToolkitTMEnquiryRow), CheckNames = true)]
    public class ToolkitTMEnquiryForm
    {
        [HalfWidth, DisplayName("Sr No")]
        public Int32 SrNo { get; set; }
        [HalfWidth]
        public Int32 MasterAccountId { get; set; }
        [HalfWidth]
        [LookupEditor(typeof(Masters.DemandayCampaignIdRow), CascadeFrom = "MasterAccountId", CascadeField = "DemandayMasterAccountId")]
        public Int32 CampaignId { get; set; }
        [HalfWidth]
        public String FirstName { get; set; }
        [HalfWidth]
        public String LastName { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        [HalfWidth]
        public String CompanyName { get; set; }
        [HalfWidth]
        public DateTime? Timestamp { get; set; }
        [HalfWidth]
        public Int32 TeamLeaderId { get; set; }
    }
}
