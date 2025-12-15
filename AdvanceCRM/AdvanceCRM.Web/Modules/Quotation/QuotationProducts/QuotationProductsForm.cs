
namespace AdvanceCRM.Quotation.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Quotation.QuotationProducts")]
    [BasedOnRow(typeof(QuotationProductsRow), CheckNames = true)]
    public class QuotationProductsForm
    {
        [Category("General")]
        [HalfWidth]
        public Int32 ProductsId { get; set; }
        [HalfWidth]
        public String Capacity { get; set; }
        [HalfWidth]
        public String Code { get; set; }
        [DefaultValue("1"), OneThirdWidth]
        public Double Quantity { get; set; }
        [ReadOnly(true), OneThirdWidth]
        public Double Mrp { get; set; }
        [ReadOnly(true), OneThirdWidth]
        public String Unit { get; set; }
        [ReadOnly(true), OneThirdWidth]
        public Double SellingPrice { get; set; }
        [OneThirdWidth]
        public Double Price { get; set; }
        [Hidden]
        public String ProductsDivision { get; set; }
        [OneThirdWidth]
        public Boolean Inclusive { get; set; }
        [DefaultValue("0"), DecimalEditor(MaxValue = "100"), OneThirdWidth]
        public Double Discount { get; set; }
        [DefaultValue("0"), OneThirdWidth]
        public Double DiscountAmount { get; set; }
        public String FileAttachment { get; set; }
        //public String ImageAttachment { get; set; }
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

       
        [Category("TAX")]
        [HalfWidth]
        public String TaxType1 { get; set; }
        [HalfWidth]
        public Double Percentage1 { get; set; }
        [HalfWidth]
        public String TaxType2 { get; set; }
        [HalfWidth]
        public Double Percentage2 { get; set; }
    }
}