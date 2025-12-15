
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

    [ColumnsScript("Purchase.Purchase")]
    [BasedOnRow(typeof(PurchaseRow), CheckNames = true)]
    public class PurchaseColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, Width(80), QuickSearch]
        public String InvoiceNo { get; set; }
        [SortOrder(1, true), QuickFilter]
        public DateTime InvoiceDate { get; set; }
        [EditLink, Width(120), QuickFilter, QuickSearch]
        public String PurchaseFromName { get; set; }
        public String PurchaseFromGSTIN { get; set; }
        public Double Total { get; set; }
        [QuickFilter]
        public Masters.StatusMaster Status { get; set; }
        [QuickFilter]
        public Masters.InvoiceTypeMaster Type { get; set; }
        public String AdditionalInfo { get; set; }
        public String Branch { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        [QuickFilter]
        public String AssignedUsername { get; set; }
        [Hidden]
        public Boolean ReverseCharge { get; set; }
        [Hidden]
        public Masters.InvoiceTypeMaster InvoiceType { get; set; }
        [QuickFilter,Hidden]
        public Int32Field OwnerTeamsId { get; set; }
        [QuickFilter, Hidden]
        public Int32Field AssignedTeamsId { get; set; }

        [QuickFilter]
        public String ApprovedByDisplayName { get; set; }

        [Hidden]
        public Int32Field CompanyId { get; set; }

        [Hidden]
        public Masters.GSTITCEligibilityTypeMaster ITCEligibility { get; set; }
    }
}