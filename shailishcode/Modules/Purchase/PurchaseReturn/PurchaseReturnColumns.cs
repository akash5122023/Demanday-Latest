
namespace AdvanceCRM.Purchase.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Purchase.PurchaseReturn")]
    [BasedOnRow(typeof(PurchaseReturnRow), CheckNames = true)]
    public class PurchaseReturnColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, Width(120), QuickFilter, QuickSearch]
        public String ContactsName { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickSearch]
        public String InvoiceNo { get; set; }
        [QuickFilter]
        public DateTime InvoiceDate { get; set; }
        public Double Amount { get; set; }
        public Double Roundup { get; set; }
        public String AdditionalInfo { get; set; }
        public String Branch { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        [QuickFilter]
        public String AssignedUsername { get; set; }
        
    }
}