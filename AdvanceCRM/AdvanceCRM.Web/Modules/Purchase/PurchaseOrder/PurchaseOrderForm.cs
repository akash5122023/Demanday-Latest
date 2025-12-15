
namespace AdvanceCRM.Purchase.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using AdvanceCRM.Purchase;

    [FormScript("Purchase.PurchaseOrder")]
    [BasedOnRow(typeof(PurchaseOrderRow), CheckNames = true)]
    public class PurchaseOrderForm
    {
        [Category("General")]
        [OneThirdWidth(UntilNext = true)]
        public Int32 PurchaseOrderNo { get; set; }
        public Int32 ContactsId { get; set; }
        [Hidden]
        public String ContactsName { get; set; }
       // [LookupEditor("Contacts.ContactPhoneLookup")]
        public String ContactsPhone { get; set; }
        [Hidden]
        public String ContactsWhatsapp { get; set; }
        [DefaultValue("now")]
        public DateTime Date { get; set; }
        public Int32 Status { get; set; }
        [DisplayName("Valid Date")]
        public DateTime DueDate { get; set; }
        public Int32 BranchId { get; set; }
        [FullWidth]
        public String Description { get; set; }
        [HalfWidth]
        public String ShippingAddress { get; set; }
        [HalfWidth]
        public String AdditionalInfo { get; set; }
        public String Attachments { get; set; }
        [Category("Product Details")]
        [PurchaseOrderProductsEditor]
        public List<PurchaseOrderProductsRow> Products { get; set; }
        [ReadOnly(true),OneThirdWidth(UntilNext = true)]
        public Double Total { get; set; }
        [DecimalEditor(MinValue = "-999999999.99", MaxValue = "999999999.99")]
        public Double Roundup { get; set; }

        public Boolean CurrencyConversion { get; set; }
        [DefaultValue(1.00)]
        public Double Conversion { get; set; }
        public Int32 FromCurrency { get; set; }
        public Int32 ToCurrency { get; set; }
        //public Int32 SourceId { get; set; }
        //[Category("Terms")]
        [DefaultValue(10), FullWidth]
        public Int32 Lines { get; set; }

        [Category("Terms")]
        [FullWidth]
        public List<Int32> TermsList { get; set; }
        //public String Terms { get; set; }
        [Category("Representative")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
        [HalfWidth]
        public Int32 AssignedId { get; set; }
        public List<object> NoteList { get; set; }
    }
}