
namespace AdvanceCRM.Sales.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Sales.Challan")]
    [BasedOnRow(typeof(ChallanRow), CheckNames = true)]
    public class ChallanColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int32 Id { get; set; }
        [EditLink, AlignRight, SortOrder(1, true)]
        public Int32 ChallanNo { get; set; }
        [Width(120), EditLink, QuickFilter, QuickSearch]
        public String ContactsName { get; set; }
        [QuickSearch]
        public String ContactsPhone { get; set; }
        public String ContactsAddress { get; set; }
        [QuickFilter, QuickSearch]
        public String ContactPersonName { get; set; }
        [QuickSearch]
        public String ContactPersonPhone { get; set; }
        public String ContactPersonEmail { get; set; }
        [QuickSearch]
        public String ContactPersonProject { get; set; }
        public String ContactPersonAddress { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter]
        public DateTime DueDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public Boolean InvoiceMade { get; set; }
        [QuickFilter]
        public Masters.StatusMaster Status { get; set; }
        [QuickFilter]
        public Masters.InvoiceTypeMaster Type { get; set; }
        public String AdditionalInfo { get; set; }
        [QuickFilter]
        public String Source { get; set; }
        [QuickFilter]
        public String Stage { get; set; }
        public String Branch { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        [QuickFilter]
        public String AssignedUsername { get; set; }
        [Hidden]
        public Boolean OtherAddress { get; set; }
        [Hidden]
        public String ShippingAddress { get; set; }
        [Hidden]
        public String DispatchDetails { get; set; }
        [Hidden]
        public Double PackagingCharges { get; set; }
        [Hidden]
        public Double FreightCharges { get; set; }
        [Hidden]
        public Double Advacne { get; set; }

        [QuickFilter]
        public String ApprovedByDisplayName { get; set; }

    }
}