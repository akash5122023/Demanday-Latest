
namespace AdvanceCRM.Sales.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Sales.InvoiceFollowups")]
    [BasedOnRow(typeof(InvoiceFollowupsRow), CheckNames = true)]
    public class InvoiceFollowupsForm
    {
        [Tab("General")]
        public String FollowupNote { get; set; }
        public String Details { get; set; }
        [DateTimeEditor]
        public DateTime FollowupDate { get; set; }
        [HalfWidth]
        [DefaultValue("1")]
        public Int32 Status { get; set; }
        [HalfWidth]
        public Int32 RepresentativeId { get; set; }
        [DateTimeEditor]
        public DateTime ClosingDate { get; set; }
        [Tab("Notes")]
        public List<object> NoteList { get; set; }
        [Hidden]
        public Int32 InvoiceId { get; set; }
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