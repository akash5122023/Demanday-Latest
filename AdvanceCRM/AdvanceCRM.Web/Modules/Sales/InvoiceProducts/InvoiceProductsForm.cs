
namespace AdvanceCRM.Sales.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Sales.InvoiceProducts")]
    [BasedOnRow(typeof(InvoiceProductsRow), CheckNames = true)]
    public class InvoiceProductsForm
    {
        [Category("General")]
        [HalfWidth(UntilNext = true)]
        public Int32 ProductsId { get; set; }
        //[_Ext.AutoCompleteEditor(LookupKey = "Products.ProductCodeLookup")]
        [LookupEditor("Products.ProductCodeLookup")]
        public String Code { get; set; }
        [OneThirdWidth(UntilNext = true)]
        [DefaultValue("1")]
        public Double Quantity { get; set; }
        [ReadOnly(true)]
        public Double Mrp { get; set; }
        [ReadOnly(true)]
        public String Unit { get; set; }
        [ReadOnly(true)]
        public Double SellingPrice { get; set; }
        public Double Price { get; set; }
        public Boolean Inclusive { get; set; }
        [FullWidth]
        public String Description { get; set; }
        [Category("Travels")]
        [HalfWidth]
        public String From { get; set; }
        [HalfWidth]
        public String To { get; set; }
        [HalfWidth]
        public DateTime? Date { get; set; }
        [Category("Hotels")]
        [HalfWidth]
        public String Destination { get; set; }
        [HalfWidth]
        public String HotelName { get; set; }
        [HalfWidth]
        public String Nights { get; set; }
        [HalfWidth]
        public String Adults { get; set; }
        [HalfWidth]
        public String Childrens { get; set; }
       
        [HalfWidth]
        public String MealPlan { get; set; }

        [Category("Discount")]
        [HalfWidth(UntilNext = true)]
        [DefaultValue("0"), DecimalEditor(MaxValue = "100")]
        public Double Discount { get; set; }
        [DefaultValue("0")]
        public Double DiscountAmount { get; set; }
        [Category("TAX")]
        public String TaxType1 { get; set; }
        [DefaultValue("0")]
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        [DefaultValue("0")]
        public Double Percentage2 { get; set; }
        [Category("Warranty")]
        public DateTime WarrantyStart { get; set; }
        public DateTime WarrantyEnd { get; set; }
    }
}