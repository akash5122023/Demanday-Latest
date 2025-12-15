
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

    [ColumnsScript("Sales.Invoice")]
    [BasedOnRow(typeof(InvoiceRow), CheckNames = true)]
    public class InvoiceColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int32 Id { get; set; }
        [EditLink, AlignRight, SortOrder(1, true)]
        public Int32 InvoiceNo { get; set; }

        public String InvoiceN { get; set; }

        [Hidden] public Int32 DealerId { get; set; }
        [Width(120), EditLink, QuickFilter, QuickSearch]
        public String ContactsName { get; set; }
        [QuickSearch]
        public String ContactsPhone { get; set; }
        [QuickSearch]
        public String ContactsEmail { get; set; }
        
        public String ContactsAddress { get; set; }
        [QuickFilter, QuickSearch]
        public String ContactPersonName { get; set; }
        [QuickSearch]
        public String ContactPersonPhone { get; set; }
        public String ContactPersonEmail { get; set; }
        [QuickSearch]
        public String ContactPersonProject { get; set; }
        public String ContactPersonAddress { get; set; }
       // public String ContactPersonProject { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter]
        public DateTime DueDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public Double GrandTotal { get; set; }
        [QuickFilter]
        public Masters.StatusMaster Status { get; set; }
        [QuickFilter]
        public Masters.EnquiryTypeMaster Type { get; set; }
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
        public String Subject { get; set; }
        [Hidden]
        public String Reference { get; set; }
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
        public Double PackagingCharges { get; set; }
        [Hidden]
        public Double FreightCharges { get; set; }

        [QuickFilter,Hidden]
        public Int32Field OwnerTeamsId { get; set; }
        [QuickFilter, Hidden]
        public Int32Field AssignedTeamsId { get; set; }
        [Hidden]
        public Double Advacne { get; set; }

        [QuickFilter]
        public String ApprovedByDisplayName { get; set; }
    }
}