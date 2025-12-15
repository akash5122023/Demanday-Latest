
namespace AdvanceCRM.Products.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Products.StockTransfer")]
    [BasedOnRow(typeof(StockTransferRow), CheckNames = true)]
    public class StockTransferForm
    {
        [HalfWidth]
        [DefaultValue("now")]
        public DateTime Date { get; set; }
        [HalfWidth]
        public Int32 RepresentativeId { get; set; }
        [HalfWidth, LookupEditor("Administration.BranchLookup")]
        public Int32 FromBranchId { get; set; }
        [HalfWidth, LookupEditor("Administration.BranchLookup")]
        public Int32 ToBranchId { get; set; }
        
        [StockTransferProductsEditor]
        public List<StockTransferProductsRow> Products { get; set; }
        [ReadOnly(true), HalfWidth]
        public Double TotalQty { get; set; }
        [ReadOnly(true), HalfWidth]
        public Double Amount { get; set; }
        public String AdditionalInfo { get; set; }
    }
}