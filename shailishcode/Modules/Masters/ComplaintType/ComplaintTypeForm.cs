
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.ComplaintType")]
    [BasedOnRow(typeof(ComplaintTypeRow), CheckNames = true)]
    public class ComplaintTypeForm
    {
        public String ComplaintType { get; set; }
    }
}