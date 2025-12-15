
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.AdditionalCharges")]
    [BasedOnRow(typeof(AdditionalChargesRow), CheckNames = true)]
    public class AdditionalChargesForm
    {
        public String Name { get; set; }
        public Double Percentage { get; set; }
    }
}