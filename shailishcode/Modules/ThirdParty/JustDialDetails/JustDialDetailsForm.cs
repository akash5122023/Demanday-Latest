
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.JustDialDetails")]
    [BasedOnRow(typeof(JustDialDetailsRow), CheckNames = true)]
    public class JustDialDetailsForm
    {
        [HalfWidth(UntilNext = true)]
        public String LeadId { get; set; }
        public String LeadType { get; set; }
        public String Prefix { get; set; }
        public String Name { get; set; }
        public String Mobile { get; set; }
        public String Landline { get; set; }
        public String Email { get; set; }
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
        public String ParentId { get; set; }
        [TextAreaEditor(Rows = 4), FullWidth]
        public String Feedback { get; set; }
        [Hidden]
        public Boolean IsMoved { get; set; }
    }
}