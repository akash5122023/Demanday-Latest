
namespace AdvanceCRM.Masters.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Masters.Tehsil")]
    [BasedOnRow(typeof(TehsilRow), CheckNames = true)]
    public class TehsilColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [Width(120), QuickFilter]
        public String State { get; set; }
        [Width(120), QuickFilter]
        public String City { get; set; }
        [EditLink, Width(120), QuickFilter]
        public String Tehsil { get; set; }
    }
}