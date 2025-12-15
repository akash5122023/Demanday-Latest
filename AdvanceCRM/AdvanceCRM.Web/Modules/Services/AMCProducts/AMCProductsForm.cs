
namespace AdvanceCRM.Services.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Services.AMCProducts")]
    [BasedOnRow(typeof(AMCProductsRow), CheckNames = true)]
    public class AMCProductsForm
    {
        public Int32 ProductsId { get; set; }
        //[_Ext.AutoCompleteEditor(LookupKey = "Products.ProductCodeLookup")]
        [LookupEditor("Products.ProductCodeLookup")]
        public String Code { get; set; }
        public String SerialNo { get; set; }
        public Double Rate { get; set; }
        public Boolean Inclusive { get; set; }
        public Int32 Type { get; set; }
        [DefaultValue("1")]
        public Int32 Quantity { get; set; }
        [DefaultValue("0")]
        public Int32 Visits { get; set; }
        [DefaultValue("0"), DecimalEditor(MaxValue = "100")]
        public Double Discount { get; set; }
        [DefaultValue("0")]
        public Double DiscountAmount { get; set; }
        public String TaxType1 { get; set; }
        [DefaultValue("0")]
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        [DefaultValue("0")]
        public Double Percentage2 { get; set; }
        //public Int32 AMCId { get; set; }
    }
}