using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Purchase.Columns
{
    [ColumnsScript("Purchase.GrnProductsTwo")]
    [BasedOnRow(typeof(GrnProductsTwoRow), CheckNames = true)]
    public class GrnProductsTwoColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink]
        public String ProductsName { get; set; }
        public String Code { get; set; }
        //public String Branch { get; set; }
        public Int32 Price { get; set; }
        public Double OrderQuantity { get; set; }
        public Double ReceivedQuantity { get; set; }
        public Double ExtraQuantity { get; set; }
        public Double RejectedQuantity { get; set; }
        //public String Description { get; set; }
        //public String GrnPo { get; set; }
    }
}