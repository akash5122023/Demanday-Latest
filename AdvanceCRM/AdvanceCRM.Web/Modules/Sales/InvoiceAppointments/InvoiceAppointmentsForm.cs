
namespace AdvanceCRM.Sales.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Sales.InvoiceAppointments")]
    [BasedOnRow(typeof(InvoiceAppointmentsRow), CheckNames = true)]
    public class InvoiceAppointmentsForm
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
        public Int32 InvoiceId { get; set; }
    }
}