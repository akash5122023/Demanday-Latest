
namespace AdvanceCRM.Enquiry.Columns
{
    using Serenity;
    using AdvanceCRM.Masters;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Enquiry.EnquiryProducts")]
    [BasedOnRow(typeof(EnquiryProductsRow), CheckNames = true)]
    public class EnquiryProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(120)]
        public String ProductsName { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public DateTime? Date { get; set; }
        public String Destination { get; set; }
        public String Nights { get; set; }
        public String Adults { get; set; }
        public String Childrens { get; set; }
        public String HotelName { get; set; }
        public String MealPlan { get; set; }

        public StringField FileAttachment;
        public String Capacity { get; set; }
        public Double Quantity { get; set; }
        public Double Mrp { get; set; }
        public List<ProductsUnitRow> Unit { get; set; }
        public Double SellingPrice { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        [Width(100)]
        public Decimal LineTotal { get; set; }
    }
}