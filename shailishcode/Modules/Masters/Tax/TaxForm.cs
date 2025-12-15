
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Tax")]
    [BasedOnRow(typeof(TaxRow), CheckNames = true)]
    public class TaxForm
    {
        public String Name { get; set; }
        public String Type { get; set; }
        public Double Percentage { get; set; }
    }
}