using Serenity;
using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.VerifySheet.Forms
{
    [FormScript("VerifySheet.TMEnquiry")]
    [BasedOnRow(typeof(TMEnquiryRow), CheckNames = true)]
    public class TMEnquiryForm
    {
        [HalfWidth]
        public Int32 MasterAccountId { get; set; }

        [HalfWidth]
        public String CampaignId { get; set; }

        [HalfWidth]
        public String FirstName { get; set; }

        [HalfWidth]
        public String LastName { get; set; }

        [HalfWidth]
        public String Email { get; set; }

        [HalfWidth]
        public String CompanyName { get; set; }

        [HalfWidth]
        public DateTime Timestamp { get; set; }

        [DisplayName("Created On"), ReadOnly(true)]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Created By"), ReadOnly(true)]
        public String CreatedBy { get; set; }

        [DisplayName("Updated On"), ReadOnly(true)]
        public DateTime UpdatedOn { get; set; }

        [DisplayName("Updated By"), ReadOnly(true)]
        public String UpdatedBy { get; set; }
    }
}
