
namespace AdvanceCRM.Products.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Products.Products")]
    [BasedOnRow(typeof(ProductsRow), CheckNames = true)]
    public class ProductsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, Width(120), QuickFilter, QuickSearch]
        public String Name { get; set; }
        [QuickSearch]
        public String Code { get; set; }
        public Int32? CompanyId { get; set; }
        [QuickSearch]
        public String HSN { get; set; }
        [QuickFilter]
        public String DivisionProductsDivision { get; set; }
        public String UnitProductsUnit { get; set; }
        [QuickFilter]
        public String GroupProductsGroup { get; set; }
        public Double SellingPrice { get; set; }
        public Double Mrp { get; set; }
        public Double PurchasePrice { get; set; }
        public Double OpeningStock { get; set; }
        public String Description { get; set; }
        public String TechSpecs { get; set; }
        public String TaxId1Name { get; set; }
        public String TaxId2Name { get; set; }
        public Double ChannelCustomerPrice { get; set; }
        public Double ResellerPrice { get; set; }
        public Double WholesalerPrice { get; set; }
        public Double DealerPrice { get; set; }
        public Double DistributorPrice { get; set; }
        public Double StockiestPrice { get; set; }
        public Double NationalDistributorPrice { get; set; }
        [Hidden]
        public Boolean RawMaterial { get; set; }
        [Hidden]
        public Double MinimumStock { get; set; }
        [Hidden]
        public Double MaximumStock { get; set; }

        public String From { get; set; }
        public String To { get; set; }
        public DateTime? Date { get; set; }
        public String Destination { get; set; }
        public String Nights { get; set; }
        public String Adults { get; set; }
        public String Childrens { get; set; }
        public String HotelName { get; set; }
        public String MealPlan { get; set; }
        //public String FileAttachment { get; set; }
        //public String ImageAttachment { get; set; }
    }
}