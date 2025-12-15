
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.RawTelecall")]
    [BasedOnRow(typeof(RawTelecallRow), CheckNames = true)]
    public class RawTelecallColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String CompanyName { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Details { get; set; }
        public String Feedback { get; set; }

        public String CreatedByUsername { get; set; }
        public String AssignedToUsername { get; set; }

        public Boolean IsMoved { get; set; }
    }
}