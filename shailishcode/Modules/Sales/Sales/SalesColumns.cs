
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

    [ColumnsScript("Sales.Sales")]
    [BasedOnRow(typeof(SalesRow), CheckNames = true)]
    public class SalesColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int32 Id { get; set; }
        [EditLink, AlignRight, SortOrder(1, true)]
        public Int32 InvoiceNo { get; set; }
        [Hidden] public Int32 DealerId { get; set; }
        [EditLink, QuickFilter, QuickSearch]
        public String ContactsName { get; set; }
        [QuickSearch]
        public String ContactsPhone { get; set; }

        [QuickSearch]
        public String ContactsEmail { get; set; }
        public String ContactsGstin { get; set; }
        [Hidden]
        public String Subject { get; set; }
        [Hidden, QuickFilter]
        public String Reference { get; set; }      
        [QuickFilter, QuickSearch]
        public String ContactPersonName { get; set; }
        [QuickSearch]
        public String ContactPersonPhone { get; set; }
        public String ContactPersonProject { get; set; }
        public String ContactPersonAddress { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter]
        public DateTime DueDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public Double GrandTotal { get; set; }
        [QuickFilter, QuickFilterOption("multiple", true)]
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
        [QuickSearch]
        public Int32 PurchaseOrderNo { get; set; }
        [QuickSearch]
        public Int32 QuotationNo { get; set; }
        public Int32 QuotationDate { get; set; }
        [Hidden]
        public Boolean CurrencyConversion { get; set; }
        [Hidden]
        public Boolean Taxable { get; set; }
        [Hidden]
        public Boolean OtherAddress { get; set; }
        [Hidden]
        public String ShippingAddress { get; set; }
        [Hidden]
        public String DispatchDetails { get; set; }
        [Hidden]
        public String TrasportationName { get; set; }
        [Hidden]
        public Double PackagingCharges { get; set; }
        [Hidden]
        public Double FreightCharges { get; set; }
        [Hidden]
        public Double Advacne { get; set; }
        [Hidden]
        public Boolean ReverseCharge { get; set; }
        [QuickFilter, Hidden]
        public Int32Field OwnerTeamsId { get; set; }
        [QuickFilter,Hidden]
        public Int32Field AssignedTeamsId { get; set; }

        [QuickFilter]
        public String ApprovedByDisplayName { get; set; }

        [Hidden]
        public Masters.GSTEcomTypeMaster EcomType { get; set; }
        [Hidden]
        public Masters.GSTInvoiceTypeMaster InvoiceType { get; set; }
    }
}