
namespace AdvanceCRM.Products.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Products.Products")]
    [BasedOnRow(typeof(ProductsRow), CheckNames = true)]
    public class ProductsForm
    {
        [Tab("General")]
        public String Name { get; set; }
        [HalfWidth]
        public String Code { get; set; }
        [HalfWidth]
        public String HSN { get; set; }
        [HalfWidth]
        public Int32 DivisionId { get; set; }
        [HalfWidth]
        public Int32 UnitId { get; set; }
        [HalfWidth]
        public Int32 GroupId { get; set; }
        [HalfWidth]
        public Double OpeningStock { get; set; }
        public Boolean RawMaterial { get; set; }
        //public String FileAttachment { get; set; }
        //public String ImageAttachment { get; set; }
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
        public String Nights { get; set; }
        [HalfWidth]
        public String Adults { get; set; }
        [HalfWidth]
        public String Childrens { get; set; }
        [HalfWidth]
        public String HotelName { get; set; }
        [HalfWidth]
        public String MealPlan { get; set; }
        public String Description { get; set; }
        [Category("Pricing")]
        [OneThirdWidth,DefaultValue("0")]
        public Double SellingPrice { get; set; }
        [OneThirdWidth, DefaultValue("0")]
        public Double Mrp { get; set; }
        [OneThirdWidth, DefaultValue("0")]
        public Double PurchasePrice { get; set; }
        [Category("Tax")]
        [HalfWidth]
        public Int32 TaxId1 { get; set; }
        [HalfWidth]
        public Int32 TaxId2 { get; set; }
        [Tab("Additional Details")]
        [Category("Channel Pricing")]
        [HalfWidth]
        public Double ChannelCustomerPrice { get; set; }
        [HalfWidth]
        public Double ResellerPrice { get; set; }
        [HalfWidth]
        public Double WholesalerPrice { get; set; }
        [HalfWidth]
        public Double DealerPrice { get; set; }
        [HalfWidth]
        public Double DistributorPrice { get; set; }
        [HalfWidth]
        public Double StockiestPrice { get; set; }
        [HalfWidth]
        public Double NationalDistributorPrice { get; set; }
        [Category("Reorder")]
        [HalfWidth]
        public Double MinimumStock { get; set; }
        [HalfWidth]
        public Double MaximumStock { get; set; }
         [Category("Tech Specs")]
        public String TechSpecs { get; set; }
        [Category("Images")]
        public String Image { get; set; }
    }
}