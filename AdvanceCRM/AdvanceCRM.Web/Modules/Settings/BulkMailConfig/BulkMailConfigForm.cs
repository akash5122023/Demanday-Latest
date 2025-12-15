
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.BulkMailConfig")]
    [BasedOnRow(typeof(BulkMailConfigRow), CheckNames = true)]
    public class BulkMailConfigForm
    {
        public String Host { get; set; }
        public Int32 Port { get; set; }
        public Boolean Ssl { get; set; }
        public String EmailId { get; set; }
        public String EmailPassword { get; set; }
    }
}