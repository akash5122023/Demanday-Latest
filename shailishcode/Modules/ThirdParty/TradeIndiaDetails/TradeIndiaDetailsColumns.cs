
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.TradeIndiaDetails")]
    [BasedOnRow(typeof(TradeIndiaDetailsRow), CheckNames = true)]
    public class TradeIndiaDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public Int32 RfiId { get; set; }
        [EditLink]
        [QuickFilter]
        public Boolean IsMoved { get; set; }
        public String Source { get; set; }
        public String ProductSource { get; set; }
        [QuickFilter]
        public DateTime GeneratedDateTime { get; set; }
        public String InquiryType { get; set; }
        public String Subject { get; set; }
        [QuickFilter]
        public String ProductName { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 OrderValueMin { get; set; }
        public String Message { get; set; }
        [QuickFilter]
        public String SenderCo { get; set; }
        public String SenderName { get; set; }
        public String SenderMobile { get; set; }
        public String SenderEmail { get; set; }
        public String SenderAddress { get; set; }
        public String SenderCity { get; set; }
        public String SenderState { get; set; }
        public String SenderCountry { get; set; }
        public String MonthSlot { get; set; }
        public String LandlineNumber { get; set; }
        public String PrefSuppLocation { get; set; }
        public String Feedback { get; set; }
    }
}