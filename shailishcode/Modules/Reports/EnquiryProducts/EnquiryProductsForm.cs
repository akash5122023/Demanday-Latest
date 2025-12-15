
namespace AdvanceCRM.Reports.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Reports.EnquiryProducts")]
    [BasedOnRow(typeof(EnquiryProductsRow), CheckNames = true)]
    public class EnquiryProductsForm
    {
        public Int32 ProductsId { get; set; }
        public Double Quantity { get; set; }
        public Double Mrp { get; set; }
        public Double SellingPrice { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        public Int32 EnquiryId { get; set; }
        public String Description { get; set; }
        public String Capacity { get; set; }
    }
}