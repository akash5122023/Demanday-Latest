using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Purchase
{
    public partial class PurchaseProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Purchase.PurchaseProductsEditor";

        public PurchaseProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
