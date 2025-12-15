
namespace AdvanceCRM.Services.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Services.AMCVisitPlanner")]
    [BasedOnRow(typeof(AMCVisitPlannerRow), CheckNames = true)]
    public class AMCVisitPlannerForm
    {
        [Tab("General")]
        [Category("Visit")]
        [HalfWidth(UntilNext = true)]
        public DateTime VisitDate { get; set; }
        [DefaultValue("1")]
        public Int32 Status { get; set; }
        public String Serial { get; set; }
        [DefaultValue("now")]
        public DateTime CompletionDate { get; set; }
        [FullWidth]
        public String VisitDetails { get; set; }
        public String Attachment { get; set; }
        [Category("Representative")]
        [HalfWidth]
        public Int32 AssignedTo { get; set; }
        [HalfWidth]
        public Int32 RepresentativeId { get; set; }
        [Tab("Notes")]
        [Category("Notes")]
        public List<object> NoteList { get; set; }
        [Hidden]
        public Int32 AMCId { get; set; }
        [Hidden]
        public String ContactPhone { get; set; }
        [Hidden]
        public String ContactEmail { get; set; }
    }
}