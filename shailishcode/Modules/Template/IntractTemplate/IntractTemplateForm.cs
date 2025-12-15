
namespace AdvanceCRM.Template.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Template.IntractTemplate")]
    [BasedOnRow(typeof(IntractTemplateRow), CheckNames = true)]
    public class IntractTemplateForm
    {
        public String CreatedAtUtc { get; set; }
        public String Name { get; set; }
        public String IntractId { get; set; }

        public String Language { get; set; }
        public String Category { get; set; }
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
        public String DisplayName { get; set; }
        public String ApprovalStatus { get; set; }
        
        public String WaTemplateId { get; set; }
        [Hidden]
        public String VariablePresent { get; set; }
        public String header_handle_file_url { get; set; }
    }
}