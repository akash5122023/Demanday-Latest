using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Quotation
{
    public partial class QuotationProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Quotation.QuotationProductsEditor";

        public QuotationProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
