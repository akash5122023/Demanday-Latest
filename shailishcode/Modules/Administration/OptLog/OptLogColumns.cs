
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

    [ColumnsScript("Administration.OptLog")]
    [BasedOnRow(typeof(OptLogRow), CheckNames = true)]
    public class OptLogColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, QuickSearch]
        public String Phone { get; set; }
        public Double Opt { get; set; }
        public DateTime Validity { get; set; }
    }
}