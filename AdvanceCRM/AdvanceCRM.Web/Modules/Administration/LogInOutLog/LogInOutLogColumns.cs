
namespace AdvanceCRM.Administration.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Administration.LogInOutLog")]
    [BasedOnRow(typeof(LogInOutLogRow), CheckNames = true)]
    public class LogInOutLogColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, QuickFilter, DateTimeEditor]
        public DateTime Date { get; set; }
        [QuickFilter]
        public Masters.AttendanceTypeMaster Type { get; set; }
        [QuickSearch]
        public String UserDisplayName { get; set; }
    }
}