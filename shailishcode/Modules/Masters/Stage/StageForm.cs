
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Stage")]
    [BasedOnRow(typeof(StageRow), CheckNames = true)]
    public class StageForm
    {
        public String Stage { get; set; }
        public Int32 Type { get; set; }
    }
}