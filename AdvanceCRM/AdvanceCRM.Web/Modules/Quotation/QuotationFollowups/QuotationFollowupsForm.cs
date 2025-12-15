
namespace AdvanceCRM.Quotation.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [FormScript("Quotation.QuotationFollowups")]
    [BasedOnRow(typeof(QuotationFollowupsRow), CheckNames = true)]
    public class QuotationFollowupsForm
    {
        [Tab("General")]
        public String FollowupNote { get; set; }
        public String Details { get; set; }
        [DateTimeEditor]
        public DateTime FollowupDate { get; set; }
        [DefaultValue("1")]
        [HalfWidth]
        public Int32 Status { get; set; }
        [HalfWidth]
        public Int32 RepresentativeId { get; set; }
        [DateTimeEditor]
        public DateTime ClosingDate { get; set; }
        [Tab("Notes")]
        public List<object> NoteList { get; set; }
        [Hidden]
        public Int32 QuotationId { get; set; }
        [Hidden]
        public String ContactPhone { get; set; }
        [Hidden]
        public String ContactEmail { get; set; }
        [Hidden]
        public String ContactPersonPhone { get; set; }
        [Hidden]
        public String ContactPersonEmail { get; set; }
    }
}