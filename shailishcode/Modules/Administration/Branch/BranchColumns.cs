
namespace AdvanceCRM.Administration.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Administration.Branch")]
    [BasedOnRow(typeof(BranchRow), CheckNames = true)]
    public class BranchColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Branch { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        [TextAreaEditor(Rows = 4)]
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Pin { get; set; }
        public Masters.CountryMaster Country { get; set; }
    }
}