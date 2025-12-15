
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.Visit")]
    [BasedOnRow(typeof(VisitRow), CheckNames = true)]
    public class VisitForm
    {
        [HalfWidth(UntilNext =true)]
        public String CompanyName { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String MobileNo { get; set; }
        public String Location { get; set; }
        [DateTimeEditor]
        public DateTime DateNTime { get; set; }
        public String Requirements { get; set; }       
        public String Purpose { get; set; }
        [FullWidth]
        public String Attachments { get; set; }
        [TextAreaEditor(Rows = 4), FullWidth]
        public String Feedback { get; set; }
        [HalfWidth(UntilNext = true)]
        public Boolean IsMoved { get; set; }
        public Int32 CreatedBy { get; set; }
    }
}