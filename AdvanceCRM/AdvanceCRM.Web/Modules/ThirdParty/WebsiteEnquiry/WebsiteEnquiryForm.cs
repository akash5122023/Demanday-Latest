
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.WebsiteEnquiry")]
    [BasedOnRow(typeof(WebsiteEnquiryRow), CheckNames = true)]
    public class WebsiteEnquiryForm
    {
        [HalfWidth(UntilNext = true)]
        public String Name { get; set; }
        [DateTimeEditor]
        public DateTime DateTime { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        [TextAreaEditor(Rows = 4), FullWidth]
        public String Address { get; set; }
        [TextAreaEditor(Rows = 4)]
        public String Requirement { get; set; }
        [TextAreaEditor(Rows = 4), FullWidth]
        public String Feedback { get; set; }
        [Hidden]
        public Boolean IsMoved { get; set; }
    }
}