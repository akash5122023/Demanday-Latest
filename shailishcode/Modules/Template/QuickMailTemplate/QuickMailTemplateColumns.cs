
namespace AdvanceCRM.Template.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Template.QuickMailTemplate")]
    [BasedOnRow(typeof(QuickMailTemplateRow), CheckNames = true)]
    public class QuickMailTemplateColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
        public String Attachments { get; set; }
    }
}