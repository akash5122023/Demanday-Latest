
namespace AdvanceCRM.Administration.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Administration.DailyWishesLog")]
    [BasedOnRow(typeof(DailyWishesLogRow), CheckNames = true)]
    public class DailyWishesLogColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [SortOrder(1, true), Width(100), QuickFilter]
        public DateTime Date { get; set; }
        [Width(500), EditLink]
        public String Log { get; set; }
    }
}