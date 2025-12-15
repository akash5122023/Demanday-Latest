
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.Visit")]
    [BasedOnRow(typeof(VisitRow), CheckNames = true)]
    public class VisitColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String CompanyName { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String MobileNo { get; set; }
        public String Location { get; set; }
        [QuickFilter,DateTimeEditor, DateTimeFormatter(DisplayFormat = "dd/MM/yyyy HH:mm")]
        public DateTime DateNTime { get; set; }
        public String Requirements { get; set; }
        [QuickFilter]
        public String Purpose { get; set; }
        public String Attachments { get; set; }
        public Boolean IsMoved { get; set; }
        [QuickFilter, DisplayName("CreatedBy")]
        public String CreatedByUsername { get; set; }
        public String Feedback { get; set; }
    }
}