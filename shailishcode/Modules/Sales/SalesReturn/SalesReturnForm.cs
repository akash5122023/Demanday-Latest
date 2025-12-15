
namespace AdvanceCRM.Sales.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Sales.SalesReturn")]
    [BasedOnRow(typeof(SalesReturnRow), CheckNames = true)]
    public class SalesReturnForm
    {
        [Category("General")]
        [OneThirdWidth(UntilNext = true)]
        
        public Int32 ContactsId { get; set; }
        public DateTime Date { get; set; }
        public String InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Int32 BranchId { get; set; }
        [FullWidth]
        public String AdditionalInfo { get; set; }
        [Category("Product Details")]
        [SalesReturnProductsEditor]
        public List<SalesReturnProductsRow> Products { get; set; }
        [OneThirdWidth(UntilNext = true)]
        [DecimalEditor(MinValue = "-999999999.99", MaxValue = "999999999.99")]
        public Double Roundup { get; set; }
        [ReadOnly(true)]
        public Double Amount { get; set; }
        [DefaultValue("10")]
        public Int32 Lines { get; set; }
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
        [HalfWidth]
        public Int32 AssignedId { get; set; }
        public List<object> NoteList { get; set; }
    }
}