
namespace AdvanceCRM.Purchase.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Purchase.Rorder")]
    [BasedOnRow(typeof(RorderRow), CheckNames = true)]
    public class RorderColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }

        [EditLink]
        public String Name { get; set; }
        public Double MinimumStock { get; set; }
        public Double MaximumStock { get; set; }
        //public Double Quantity { get; set; }
        public Double SellingPrice { get; set; }
    }
}
