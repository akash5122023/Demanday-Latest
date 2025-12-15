using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Sales.Columns
{
    [ColumnsScript("Sales.IndentProducts")]
    [BasedOnRow(typeof(IndentProductsRow), CheckNames = true)]
    public class IndentProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink]
        public String ProductsName { get; set; }
       // public String IndentAdditionalInfo { get; set; }
        public Double Quantity { get; set; }
        [EditLink]
        public String Description { get; set; }
    }
}