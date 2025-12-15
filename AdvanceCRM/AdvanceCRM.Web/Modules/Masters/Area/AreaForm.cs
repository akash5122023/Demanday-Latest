
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Area")]
    [BasedOnRow(typeof(AreaRow), CheckNames = true)]
    public class AreaForm
    {
        public String Area { get; set; }
    }
}