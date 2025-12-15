
namespace AdvanceCRM.Reports.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Reports.QuotationProducts")]
    [BasedOnRow(typeof(QuotationProductsRow), CheckNames = true)]
    public class QuotationProductsForm
    {
        public Int32 ProductsId { get; set; }
        public Double Quantity { get; set; }
        public Double Mrp { get; set; }
        public Double SellingPrice { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        public String TaxType1 { get; set; }
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        public Double Percentage2 { get; set; }
        public Int32 QuotationId { get; set; }
        public Double DiscountAmount { get; set; }
        public String Description { get; set; }
        public String Unit { get; set; }
        public String Capacity { get; set; }
        public String ProductsDivision { get; set; }
    }
}