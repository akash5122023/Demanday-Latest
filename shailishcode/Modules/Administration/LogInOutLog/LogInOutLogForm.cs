
namespace AdvanceCRM.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Administration.LogInOutLog")]
    [BasedOnRow(typeof(LogInOutLogRow), CheckNames = true)]
    public class LogInOutLogForm
    {
        public DateTime Date { get; set; }
        public Int32 Type { get; set; }
        public Int32 UserId { get; set; }
    }
}