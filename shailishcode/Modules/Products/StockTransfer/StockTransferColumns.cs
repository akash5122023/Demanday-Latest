
namespace AdvanceCRM.Products.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Products.StockTransfer")]
    [BasedOnRow(typeof(StockTransferRow), CheckNames = true)]
    public class StockTransferColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        public Int32 Id { get; set; }
        [EditLink, QuickFilter]
        public DateTime Date { get; set; }
        [EditLink, DisplayName("From"), QuickFilter]
        public String FromBranchBranch { get; set; }
        [EditLink, DisplayName("To"), QuickFilter]
        public String ToBranchBranch { get; set; }
        public Double TotalQty { get; set; }
        public Double Amount { get; set; }
        public String AdditionalInfo { get; set; }
        [QuickFilter]
        public String RepresentativeUsername { get; set; }
        
    }
}