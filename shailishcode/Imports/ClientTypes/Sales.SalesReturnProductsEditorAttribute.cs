using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Sales
{
    public partial class SalesReturnProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Sales.SalesReturnProductsEditor";

        public SalesReturnProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
