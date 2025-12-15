
namespace AdvanceCRM.Services.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Services.TeleCalling")]
    [BasedOnRow(typeof(TeleCallingRow), CheckNames = true)]
    public class TeleCallingForm
    {
        [Category("General")]
        [HalfWidth(UntilNext = true)]
        public Int32 ContactsId { get; set; }

        [Hidden]
        public String ContactsName { get; set; }

        [Hidden]
        public String ContactsWhatsapp { get; set; }

        [ReadOnly(true)]
        public String ContactsPhone { get; set; }
        [DefaultValue("now")]
        public DateTime Date { get; set; }
        [ReadOnly(true)]
        public DateTime AppointmentDate { get; set; }
        public Int32 ProductsId { get; set; }
        [Category("Additional Details")]
        [DefaultValue("1")]
        public Int32 Status { get; set; }
        public Int32 SourceId { get; set; }
        public Int32 StageId { get; set; }
        public Int32 BranchId { get; set; }
        [FullWidth]
        public String Details { get; set; }
        public String Feedback { get; set; }
        [Category("Representatives")]
        [HalfWidth(UntilNext = true), ReadOnly(true)]
        public Int32 RepresentativeId { get; set; }
        public Int32 AssignedTo { get; set; }
        public List<object> NoteList { get; set; }
        public List<object> Timeline { get; set; }
    }
}