using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Purchase
{
    public partial class PurchaseReturnProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Purchase.PurchaseReturnProductsEditor";

        public PurchaseReturnProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
