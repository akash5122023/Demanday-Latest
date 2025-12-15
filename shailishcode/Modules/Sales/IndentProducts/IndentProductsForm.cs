using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Sales.Forms
{
    [FormScript("Sales.IndentProducts")]
    [BasedOnRow(typeof(IndentProductsRow), CheckNames = true)]
    public class IndentProductsForm
    {
        public Int32 ProductsId { get; set; }
        //public Int32 IndentId { get; set; }
        public Double Quantity { get; set; }
        public String Description { get; set; }
    }
}