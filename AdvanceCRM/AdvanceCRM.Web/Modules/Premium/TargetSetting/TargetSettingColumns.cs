
namespace AdvanceCRM.Premium.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Premium.TargetSetting")]
    [BasedOnRow(typeof(TargetSettingRow), CheckNames = true)]
    public class TargetSettingColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(80)]
        public Int32 Type { get; set; }
        [EditLink, Width(120), AlignRight]
        public Int32 MonthlyTarget { get; set; }
        [EditLink, Width(160), AlignRight]
        public Double MonthlyTargetAmount { get; set; }
        [EditLink, Width(120), AlignCenter]
        public Int32 RepresentativeUsername { get; set; }
    }
}