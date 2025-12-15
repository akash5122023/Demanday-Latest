
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.TicketWebDetails")]
    [BasedOnRow(typeof(TicketWebDetailsRow), CheckNames = true)]
    public class TicketWebDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String ProductName { get; set; }
        public String Requirement { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime PurchaseDate { get; set; }
        public String ComplaintDetails { get; set; }
        public String Attachment { get; set; }
        public Boolean IsMoved { get; set; }
    }
}