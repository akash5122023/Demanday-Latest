
namespace AdvanceCRM.Masters.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Masters.Stage")]
    [BasedOnRow(typeof(StageRow), CheckNames = true)]
    public class StageColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Stage { get; set; }
        public StageTypeMaster Type { get; set; }
    }
}