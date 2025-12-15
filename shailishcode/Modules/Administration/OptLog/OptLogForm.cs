
namespace AdvanceCRM.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Administration.OptLog")]
    [BasedOnRow(typeof(OptLogRow), CheckNames = true)]
    public class OptLogForm
    {
        public String Phone { get; set; }
        public Double Opt { get; set; }
        public DateTime Validity { get; set; }
    }
}