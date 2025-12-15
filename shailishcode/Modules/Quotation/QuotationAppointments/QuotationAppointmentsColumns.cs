
namespace AdvanceCRM.Quotation.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Quotation.QuotationAppointments")]
    [BasedOnRow(typeof(QuotationAppointmentsRow), CheckNames = true)]
    public class QuotationAppointmentsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(140)]
        public String Details { get; set; }
        [Hidden]
        public String ContactName { get; set; }
        [Hidden]
        public String ContactPhone { get; set; }
        [Hidden]
        public String ContactAddress { get; set; }
        [DateTimeEditor, EditLink]
        public DateTime AppointmentDate { get; set; }
        [QuickFilter]
        public Masters.StatusMaster Status { get; set; }
        public String MinutesOfMeeting { get; set; }
        public String Reason { get; set; }
        [QuickFilter]
        public String RepresentativeDisplayName { get; set; }
    }
}