
namespace AdvanceCRM.Services.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Services.AMCProducts")]
    [BasedOnRow(typeof(AMCProductsRow), CheckNames = true)]
    public class AMCProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(150)]
        public String ProductsName { get; set; }
        [EditLink, Width(120)]
        public String SerialNo { get; set; }
        public Double Rate { get; set; }
        [Width(120)]
        public Masters.AMCTypeMaster Type { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 Visits { get; set; }
        public Double Discount { get; set; }
        public Double DiscountAmount { get; set; }
        [Width(100)]
        public String TaxType1 { get; set; }
        public Double Percentage1 { get; set; }
        //public Decimal TAX1Amount { get; set; }
        [Width(100)]
        public String TaxType2 { get; set; }
        public Double Percentage2 { get; set; }
        //public Decimal TAX2Amount { get; set; }
        public Decimal LineTotal { get; set; }
        public Decimal GrandTotal { get; set; }
    }
}