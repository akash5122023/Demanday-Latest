
namespace AdvanceCRM.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Administration.AppointmentSmsLog")]
    [BasedOnRow(typeof(AppointmentSmsLogRow), CheckNames = true)]
    public class AppointmentSmsLogForm
    {
        public DateTime Date { get; set; }
        public String Log { get; set; }
    }
}