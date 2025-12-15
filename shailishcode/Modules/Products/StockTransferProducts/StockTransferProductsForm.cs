
namespace AdvanceCRM.Products.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Products.StockTransferProducts")]
    [BasedOnRow(typeof(StockTransferProductsRow), CheckNames = true)]
    public class StockTransferProductsForm
    {
        public Int32 ProductsId { get; set; }
        [DefaultValue("1")]
        public Double Quantity { get; set; }
        public Double TransferPrice { get; set; }
        public String TaxType1 { get; set; }
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        public Double Percentage2 { get; set; }
        //public Int32 StockTransferId { get; set; }
    }
}