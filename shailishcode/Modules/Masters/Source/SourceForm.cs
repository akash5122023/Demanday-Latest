
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Source")]
    [BasedOnRow(typeof(SourceRow), CheckNames = true)]
    public class SourceForm
    {
        public String Source { get; set; }
    }
}