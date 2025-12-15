using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class ChangeLookupTextEditorAttribute : LookupEditorBaseAttribute
    {
        public const string Key = "AdvanceCRM.Default.ChangeLookupTextEditor";

        public ChangeLookupTextEditorAttribute()
            : base(Key)
        {
        }
    }
}
