
namespace AdvanceCRM.Quotation.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Quotation.QuotationAppointments")]
    [BasedOnRow(typeof(QuotationAppointmentsRow), CheckNames = true)]
    public class QuotationAppointmentsForm
    {
        [Tab("General")]
        public String Details { get; set; }
        [DateTimeEditor]
        public DateTime AppointmentDate { get; set; }
        [HalfWidth]
        public Int32 Status { get; set; }
        [HalfWidth]
        public Int32 RepresentativeId { get; set; }
        public String MinutesOfMeeting { get; set; }
        public String Reason { get; set; }
        [Tab("Notes")]
        public List<object> NoteList { get; set; }
        [Hidden]
        public Int32 QuotationId { get; set; }
    }
}