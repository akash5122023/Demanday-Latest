
namespace AdvanceCRM.Purchase.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Purchase.Rorder")]
    [BasedOnRow(typeof(RorderRow), CheckNames = true)]
    public class RorderForm
    {
        public string Name { get; set; }
        public Double MinimumStock { get; set; }
        public Double MaximumStock { get; set; }
        //public Double Quantity { get; set; }
        public Double SellingPrice { get; set; }
    }
}