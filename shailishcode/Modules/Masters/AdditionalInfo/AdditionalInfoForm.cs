
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.AdditionalInfo")]
    [BasedOnRow(typeof(AdditionalInfoRow), CheckNames = true)]
    public class AdditionalInfoForm
    {
        public String AdditionalInfo { get; set; }

        public Int32 Type { get; set; }
    }
}