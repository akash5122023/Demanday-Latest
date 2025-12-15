
namespace AdvanceCRM.Services.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Services.TeleCallingAppointments")]
    [BasedOnRow(typeof(TeleCallingAppointmentsRow), CheckNames = true)]
    public class TeleCallingAppointmentsForm
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
        public Int32 TeleCallingId { get; set; }
    }
}