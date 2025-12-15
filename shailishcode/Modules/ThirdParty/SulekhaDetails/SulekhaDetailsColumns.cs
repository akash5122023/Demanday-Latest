
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.SulekhaDetails")]
    [BasedOnRow(typeof(SulekhaDetailsRow), CheckNames = true)]
    public class SulekhaDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink,QuickFilter]
        public String UserName { get; set; }       
        public String Mobile { get; set; }
        public String Email { get; set; }
        public String City { get; set; }
        public String Localities { get; set; }
        public String Comments { get; set; }
        public String Keywords { get; set; }
        public String Feedback { get; set; }
        [QuickFilter, DateTimeEditor, DateTimeFormatter(DisplayFormat = "dd/MM/yyyy HH:mm")]
        public DateTime DateTime { get; set; }
        [Width(80), QuickFilter]
        public Boolean IsMoved { get; set; }
    }
}