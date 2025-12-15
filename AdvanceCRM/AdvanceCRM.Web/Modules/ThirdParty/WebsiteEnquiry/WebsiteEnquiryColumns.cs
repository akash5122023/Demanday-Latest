
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.WebsiteEnquiry")]
    [BasedOnRow(typeof(WebsiteEnquiryRow), CheckNames = true)]
    public class WebsiteEnquiryColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, QuickFilter]
        public String Name { get; set; }
        [QuickFilter]
        public DateTime DateTime { get; set; }
        [QuickFilter]
        public String Phone { get; set; }
        public String Email { get; set; }
        //[DisplayName("City")]
        [DisplayName("Additional Info")]
        public String Address { get; set; }
        //[DisplayName("Scheme")]
        [DisplayName("Company")]    
        public String Requirement { get; set; }
        public String Feedback { get; set; }
        [QuickFilter]
        public Boolean IsMoved { get; set; }
    }
}