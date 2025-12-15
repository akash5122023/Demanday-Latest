
namespace AdvanceCRM.Enquiry.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Enquiry.EnquiryFollowups")]
    [BasedOnRow(typeof(EnquiryFollowupsRow), CheckNames = true)]
    public class EnquiryFollowupsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(120)]
        public String FollowupNote { get; set; }
        [Hidden, QuickSearch]
        public String ContactName { get; set; }
        [Hidden, QuickSearch]
        public String ContactPhone { get; set; }
        [Hidden, QuickSearch]
        public String ContactEmail { get; set; }
        [Hidden]
        public String ContactAddress { get; set; }
        public String Details { get; set; }
        [QuickFilter]
        public DateTime FollowupDate { get; set; }
        [QuickFilter]
        public Masters.StatusMaster Status { get; set; }
        [DisplayName("Followup Done By"), QuickFilter]
        public String RepresentativeDisplayName { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}