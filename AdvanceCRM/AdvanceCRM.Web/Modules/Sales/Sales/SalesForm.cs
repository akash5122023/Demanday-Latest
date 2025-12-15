
namespace AdvanceCRM.Sales.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Sales.Sales")]
    [BasedOnRow(typeof(SalesRow), CheckNames = true)]
    public class SalesForm
    {
        [Category("Contact Details")]
        [HalfWidth]
        public Int32 ContactsId { get; set; }
        [Hidden]
        public Int32 ContactsContactType { get; set; }
        [Hidden]
        public String ContactsName { get; set; }
        [HalfWidth]
        //[LookupEditor("Contacts.ContactPhoneLookup")]
        public String ContactsPhone { get; set; }
        [Hidden]
        public String ContactsWhatsapp { get; set; }
        [ReadOnly(true)]
        public String ContactsAddress { get; set; }
        [HalfWidth, FormCssClass("line-break-sm")]
        public Int32 ContactPersonId { get; set; }
        [Hidden]
        public String ContactPersonName { get; set; }
        [ReadOnly(true), HalfWidth]
        public String ContactPersonPhone { get; set; }
        [Hidden]
        public String ContactPersonWhatsapp { get; set; }
        [ReadOnly(true), HalfWidth]
        public String ContactPersonProject { get; set; }
        [ReadOnly(true)]
        public String ContactPersonAddress { get; set; }
        public Int32 DealerId { get; set; }
        [Category("Product Details")]
        [SalesProductsEditor]
        public List<SalesProductsRow> Products { get; set; }
        [OneThirdWidth(UntilNext = true)]
        public Double PackagingCharges { get; set; }
        public Double FreightCharges { get; set; }
        public Double Advacne { get; set; }
        [ReadOnly(true)]
        public Double GrandTotal { get; set; }
        [DecimalEditor(MinValue = "-999999999.99", MaxValue = "999999999.99")]
        public Double Roundup { get; set; }
        public Boolean CurrencyConversion { get; set; }
        [DefaultValue(1.00)]
        public Double Conversion { get; set; }
        public Int32 FromCurrency { get; set; }
        public Int32 ToCurrency { get; set; }
        [FullWidth]
        public List<Int32> ChargesList { get; set; }
        [FullWidth]
        public List<Int32> ConcessionList { get; set; }
        [Category("Sales Details")]
        [DefaultValue("now"), OneThirdWidth(UntilNext = true)]
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        [DefaultValue("now")]
        public DateTime ClosingDate { get; set; }
        [DefaultValue("1")]
        public Int32 Status { get; set; }
        public Int32 Type { get; set; }
        public Int32 SourceId { get; set; }
        public Int32 StageId { get; set; }
        [Hidden]
        public Int32 InvoiceNo { get; set; }
        [ReadOnly(true), DisplayName("InvoiceNo")]
        public String InvoiceN { get; set; }
        public Int32 InvoiceType { get; set; }
        public Int32 TrasportationId { get; set; }
        
        public Int32 BranchId { get; set; }
        public String PurchaseOrderNo { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public Int32 EcomType { get; set; }
        public Boolean ReverseCharge { get; set; }
        public Boolean Taxable { get; set; }
        public Boolean BillingAddress { get; set; }
        public Boolean OtherAddress { get; set; }
        [FullWidth]
        public String ShippingAddress { get; set; }
        [Category("Additional Details")]
        public String DispatchDetails { get; set; }
        public String AdditionalInfo { get; set; }
        public String Attachments { get; set; }
        [Category("Printing Details")]
        [HalfWidth]
        public String Subject { get; set; }
        [HalfWidth]
        public String Reference { get; set; }
        public Int32 MessageId { get; set; }
        [DefaultValue(10)]
        public Int32 Lines { get; set; }
        [Category("Terms")]
        [FullWidth]
        public List<Int32> TermsList { get; set; }
        [Category("Representatives")]
        [HalfWidth, ReadOnly(true)]
        public Int32 OwnerId { get; set; }
        [HalfWidth]
        public Int32 AssignedId { get; set; }
        [ReadOnly(true), DefaultValue(1), Hidden]
        public Int32 CompanyId { get; set; }
        public List<object> NoteList { get; set; }
        public List<object> Timeline { get; set; }

    }
}