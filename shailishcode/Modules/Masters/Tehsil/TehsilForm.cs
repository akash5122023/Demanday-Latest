
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Tehsil")]
    [BasedOnRow(typeof(TehsilRow), CheckNames = true)]
    public class TehsilForm
    {
        public String Tehsil { get; set; }
        public Int32 StateId { get; set; }
        public Int32 CityId { get; set; }
    }
}