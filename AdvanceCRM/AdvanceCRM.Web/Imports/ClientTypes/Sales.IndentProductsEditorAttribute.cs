using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Sales
{
    public partial class IndentProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Sales.IndentProductsEditor";

        public IndentProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
