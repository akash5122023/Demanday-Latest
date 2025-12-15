
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

    [ColumnsScript("Purchase.PurchaseOrder")]
    [BasedOnRow(typeof(PurchaseOrderRow), CheckNames = true)]
    public class PurchaseOrderColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int32 Id { get; set; }
        [EditLink, AlignRight, SortOrder(1, true)]
        public Int32 PurchaseOrderNo { get; set; }
        [EditLink, Width(120), QuickFilter, QuickSearch]
        public String ContactsName { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter]
        public DateTime DueDate { get; set; }
        [QuickFilter]
        public Masters.StatusMaster Status { get; set; }
        [QuickFilter]
        public String Source { get; set; }
        public Double Total { get; set; }
        public String Description { get; set; }
        public String AdditionalInfo { get; set; }
        public String Branch { get; set; }

        [Hidden]
        public String ShippingAddress { get; set; }
        public String Terms { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        [QuickFilter]
        public String AssignedUsername { get; set; }
        
    }
}