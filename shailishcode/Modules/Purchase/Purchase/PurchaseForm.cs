
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

    [FormScript("Purchase.Purchase")]
    [BasedOnRow(typeof(PurchaseRow), CheckNames = true)]
    public class PurchaseForm
    {
        [Category("General")]
        [OneThirdWidth(UntilNext = true)]
        public String InvoiceNo { get; set; }
        public Int32 PurchaseFromId { get; set; }
      //  [LookupEditor("Contacts.ContactPhoneLookup")]
        public String PurchaseFromPhone { get; set; }
        [DefaultValue("now")]
        public DateTime InvoiceDate { get; set; }
        [DefaultValue("1")
            ]
        public Int32 Status { get; set; }
        public Int32 BranchId { get; set; }
        [Category("Product Details")]
        [FullWidth]
        [PurchaseProductsEditor]
        public List<PurchaseProductsRow> Products { get; set; }
        [ReadOnly(true),HalfWidth]
        public Double Total { get; set; }
        [DecimalEditor(MinValue = "-999999999.99", MaxValue = "999999999.99"), HalfWidth]
        public Double Roundup { get; set; }
        [Category("Additional Details")]
        [OneThirdWidth(UntilNext = true)]
        public Int32 Type { get; set; }
        public Boolean ReverseCharge { get; set; }
        public Int32 InvoiceType { get; set; }
        public Int32 ITCEligibility { get; set; }
        [FullWidth]
        public String AdditionalInfo { get; set; }
        public String Attachments { get; set; }
        [Category("Representative")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
        [HalfWidth]
        public Int32 AssignedId { get; set; }
        public List<object> NoteList { get; set; }
    }
}