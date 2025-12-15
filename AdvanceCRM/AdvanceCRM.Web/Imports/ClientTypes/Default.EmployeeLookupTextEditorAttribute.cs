using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class EmployeeLookupTextEditorAttribute : LookupEditorBaseAttribute
    {
        public const string Key = "AdvanceCRM.Default.EmployeeLookupTextEditor";

        public EmployeeLookupTextEditorAttribute()
            : base(Key)
        {
        }
    }
}
