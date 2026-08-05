using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace AdvanceCRM.Toolkit.Columns
{
    [ColumnsScript("Toolkit.ToolkitTMEnquiry")]
    [BasedOnRow(typeof(ToolkitTMEnquiryRow), CheckNames = true)]
    public class ToolkitTMEnquiryColumns
    {
        [EditLink, DisplayName("Sr No"), AlignRight]
        public Int32 SrNo { get; set; }
        [EditLink]
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String CompanyName { get; set; }
        [DisplayName("Campaign ID")]
        public String CampaignCampaignId { get; set; }
        public String MasterAccountAccountNumber { get; set; }
        [DisplayName("Team Leader")]
        public String TeamLeaderFirstName { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
