using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Sales
{
    public partial class OutwardProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Sales.OutwardProductsEditor";

        public OutwardProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
