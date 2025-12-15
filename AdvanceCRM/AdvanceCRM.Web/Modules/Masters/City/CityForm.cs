
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.City")]
    [BasedOnRow(typeof(CityRow), CheckNames = true)]
    public class CityForm
    {
        public String City { get; set; }
        public Int32 StateId { get; set; }
    }
}