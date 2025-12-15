
namespace AdvanceCRM.Quotation.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Quotation.Quotation")]
    [BasedOnRow(typeof(QuotationRow), CheckNames = true)]
    public class QuotationForm
    {
        [Category("Contact Details")]
        [HalfWidth]
        public Int32 ContactsId { get; set; }
        [Hidden]
        public Int32 ContactsContactType { get; set; }
        [Hidden]
        public String ContactsName { get; set; }
        [HalfWidth,ReadOnly(true)]
     //   [LookupEditor("Contacts.ContactPhoneLookup")]
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
        [Visible(false)]
        public Int32 ProjectId { get; set; }
        [HalfWidth]
        public Int32 DealerId { get; set; }

        [Category("Product Details")]
        public List<QuotationProductsRow> Products { get; set; }
        [OneThirdWidth(UntilNext = true)]
        [ReadOnly(true)]
        public Double Total { get; set; }
        [ReadOnly(true)]
        public Double GrandTotal { get; set; }
        public Double PerDiscount { get; set; }
        public Double DiscountAmt { get; set; }
       
        [ReadOnly(true),DisplayName("GrandTotal")]
        public Double DisGrandTotal { get; set; }

        [DecimalEditor(MinValue = "-999999999.99", MaxValue = "999999999.99")]
        public Double Roundup { get; set; }
        public Boolean CurrencyConversion { get; set; }
        [DefaultValue(1)]
        public Double Conversion { get; set; }
        public Int32 FromCurrency { get; set; }
        public Int32 ToCurrency { get; set; }
        [FullWidth]
        public List<Int32> ChargesList { get; set; }
        [FullWidth]
        public List<Int32> ConcessionList { get; set; }
        [Category("Quotation Details")]
        [OneThirdWidth,Hidden]
        public Int32 QuotationNo { get; set; }
        [OneThirdWidth,ReadOnly(true)]
        public string QuotationN { get; set; }

        [DefaultValue("now"), OneThirdWidth]
        public DateTime Date { get; set; }

        [OneThirdWidth]
        public DateTime ExpectedClosingDate { get; set; }

        [DefaultValue("1"), OneThirdWidth]
        public Int32 Status { get; set; }
        [DefaultValue("now"),OneThirdWidth ]
        public DateTime ClosingDate { get; set; }
        [OneThirdWidth]
        public Int32 ClosingType { get; set; }
        [OneThirdWidth]
        public String LostReason { get; set; }
        [OneThirdWidth,DefaultValue(1)]
        public Int32 SourceId { get; set; }
        [OneThirdWidth,DefaultValue(1)]
        public Int32 StageId { get; set; }
        [OneThirdWidth,DefaultValue(1)]
        public Int32 Type { get; set; }
        [OneThirdWidth]
        public Int32 BranchId { get; set; }
        [OneThirdWidth]
        public Int32 WinPercentage { get; set; }
        [OneThirdWidth]
        public Boolean Taxable { get; set; }
        [OneThirdWidth]
        public String ReferenceName { get; set; }
        [OneThirdWidth, MaskedEditor(Mask = "9999999999")]
        public String ReferencePhone { get; set; }
        public String AdditionalInfo { get; set; }
        public String AdditionalInfo2 { get; set; }

        public List<Int32> QuotationAddinfoList { get; set; }
        public String Attachment { get; set; }
        [Category("Print Details")]
        [HalfWidth]
        public String Subject { get; set; }
        [HalfWidth]
        public String Reference { get; set; }
        public Int32 MessageId { get; set; }
        [DefaultValue(10), HalfWidth]
        public Int32 Lines { get; set; }
        [Category("Terms")]
        public List<Int32> TermsList { get; set; }
        [Category("Representatives")]
        [OneThirdWidth, ReadOnly(true)]
        public Int32 OwnerId { get; set; }
        [OneThirdWidth]
        public Int32 AssignedId { get; set; }
        [OneThirdWidth]
        public List<Int32> MultiAssignList { get; set; }
        [ReadOnly(true), DefaultValue(1), Hidden]
        public Int32 CompanyId { get; set; }
        public List<object> NoteList { get; set; }
        public List<object> Timeline { get; set; }
    }
}