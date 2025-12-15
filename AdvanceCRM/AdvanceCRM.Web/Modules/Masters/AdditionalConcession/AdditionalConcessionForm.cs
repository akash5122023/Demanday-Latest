
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.AdditionalConcession")]
    [BasedOnRow(typeof(AdditionalConcessionRow), CheckNames = true)]
    public class AdditionalConcessionForm
    {
        public String Name { get; set; }
        public Double Percentage { get; set; }
        public Double Amount { get; set; }
    }
}