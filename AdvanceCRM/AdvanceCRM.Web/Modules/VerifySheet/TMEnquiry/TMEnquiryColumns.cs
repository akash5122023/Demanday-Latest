using Serenity;
using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.VerifySheet.Columns
{
    [ColumnsScript("VerifySheet.TMEnquiry")]
    [BasedOnRow(typeof(TMEnquiryRow), CheckNames = true)]
    public class TMEnquiryColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }

        [EditLink]
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String CompanyName { get; set; }

        public String CampaignId { get; set; }

        [DisplayName("Timestamp")]
        public DateTime Timestamp { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Created By")]
        public String CreatedBy { get; set; }
    }
}
