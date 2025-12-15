
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.InstamojoDetails")]
    [BasedOnRow(typeof(InstamojoDetailsRow), CheckNames = true)]
    public class InstamojoDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String Purpose { get; set; }
        public String PaymentMode { get; set; }
        public String Status { get; set; }
        public DateTime PayoutDate { get; set; }
        public Boolean IsMoved { get; set; }
    }
}