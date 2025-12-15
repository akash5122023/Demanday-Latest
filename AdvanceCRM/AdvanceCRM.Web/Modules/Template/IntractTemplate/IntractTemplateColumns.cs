
namespace AdvanceCRM.Template.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Template.IntractTemplate")]
    [BasedOnRow(typeof(IntractTemplateRow), CheckNames = true)]
    public class IntractTemplateColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [Hidden]
        public String CreatedAtUtc { get; set; }
        public String IntractId { get; set; }
        [EditLink]
        public String Name { get; set; }
        
        public String ApprovalStatus { get; set; }

        public String Category { get; set; }
        public String Language { get; set; }

        public String WaTemplateId { get; set; }
        [Hidden]
        public String TemplateCategoryLabel { get; set; }
        [Hidden]
        public String HeaderFormat { get; set; }
        [Hidden]
        public String Header { get; set; }
        [Hidden]
        public String Body { get; set; }
        [Hidden]
        public String Footer { get; set; }
        [Hidden]
        public String Buttons { get; set; }
        [Hidden]
        public String AutosubmittedFor { get; set; }
        [Hidden]
        public String DisplayName { get; set; }
        
        [Hidden]
        public String VariablePresent { get; set; }

        public String header_handle_file_url { get; set; }

    }
}