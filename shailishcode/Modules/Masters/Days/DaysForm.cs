
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Days")]
    [BasedOnRow(typeof(DaysRow), CheckNames = true)]
    public class DaysForm
    {
        public String Title { get; set; }
        public String Heading { get; set; }
        [TextAreaEditor(Rows = 5)]
        public String Description { get; set; }
        public String FileAttachments { get; set; }
    }
}