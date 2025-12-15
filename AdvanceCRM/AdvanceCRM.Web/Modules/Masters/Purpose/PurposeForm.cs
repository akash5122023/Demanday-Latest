
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Purpose")]
    [BasedOnRow(typeof(PurposeRow), CheckNames = true)]
    public class PurposeForm
    {
        public String Purpose { get; set; }
    }
}