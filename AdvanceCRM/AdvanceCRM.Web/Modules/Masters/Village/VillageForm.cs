
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Village")]
    [BasedOnRow(typeof(VillageRow), CheckNames = true)]
    public class VillageForm
    {
        public String Village { get; set; }
        public String PIN { get; set; }
        public Int32 StateId { get; set; }
        public Int32 CityId { get; set; }
        public Int32 TehsilId { get; set; }
    }
}