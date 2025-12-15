using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Sales
{
    public partial class SalesEditorAttribute : LookupEditorBaseAttribute
    {
        public const string Key = "AdvanceCRM.Sales.SalesEditor";

        public SalesEditorAttribute()
            : base(Key)
        {
        }
    }
}
