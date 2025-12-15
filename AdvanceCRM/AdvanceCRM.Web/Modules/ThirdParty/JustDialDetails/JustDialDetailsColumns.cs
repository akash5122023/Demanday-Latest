
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.JustDialDetails")]
    [BasedOnRow(typeof(JustDialDetailsRow), CheckNames = true)]
    public class JustDialDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String LeadId { get; set; }
        public String LeadType { get; set; }
        [Width(80), QuickFilter]
        public Boolean IsMoved { get; set; }
        public String Prefix { get; set; }
        public String Name { get; set; }
        public String Mobile { get; set; }
        public String Landline { get; set; }
        public String Email { get; set; }
        [QuickFilter, DateTimeEditor, DateTimeFormatter(DisplayFormat = "dd/MM/yyyy HH:mm")]
        public DateTime DateTime { get; set; }
        public String Category { get; set; }
        public String City { get; set; }
        public String Area { get; set; }
        public String BranchArea { get; set; }
        public Boolean DcnMobile { get; set; }
        public Boolean DcnPhone { get; set; }
        public String Company { get; set; }
        public String Pin { get; set; }
        public String BranhPin { get; set; }
        public String Feedback { get; set; }
    }
}