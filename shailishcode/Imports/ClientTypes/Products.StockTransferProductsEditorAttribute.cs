using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Products
{
    public partial class StockTransferProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Products.StockTransferProductsEditor";

        public StockTransferProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
