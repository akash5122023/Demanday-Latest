
namespace AdvanceCRM.Products.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Products.StockTransferProducts")]
    [BasedOnRow(typeof(StockTransferProductsRow), CheckNames = true)]
    public class StockTransferProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink]
        public String ProductsName { get; set; }
        public Double Quantity { get; set; }
        [DisplayName("Price")]
        public Double TransferPrice { get; set; }
        [EditLink]
        public String TaxType1 { get; set; }
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        public Double Percentage2 { get; set; }
        [Width(100)]
        public Decimal LineTotal { get; set; }
        [Width(100)]
        public Decimal GrandTotal { get; set; }
        //public Int32 StockTransferId { get; set; }
    }
}