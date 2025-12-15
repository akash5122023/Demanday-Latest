
namespace AdvanceCRM.Services.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Services.CMSProducts")]
    [BasedOnRow(typeof(CMSProductsRow), CheckNames = true)]
    public class CMSProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(150)]
        public String ProductsName { get; set; }
        public Double Price { get; set; }
        public Double Quantity { get; set; }
        [Width(100)]
        public Decimal LineTotal { get; set; }
        //public Int32 CMSId { get; set; }
    }
}