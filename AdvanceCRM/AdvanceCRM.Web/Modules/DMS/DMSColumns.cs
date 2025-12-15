
namespace AdvanceCRM.DMS.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("DMS.DMS")]
    [BasedOnRow(typeof(DMSRow), CheckNames = true)]
    public class DMSColumns
    {
        [EditLink, Width(300)]
        public String Title { get; set; }
    }
}