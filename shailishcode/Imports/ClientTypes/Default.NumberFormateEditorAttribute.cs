using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class NumberFormateEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Default.NumberFormateEditor";

        public NumberFormateEditorAttribute()
            : base(Key)
        {
        }
    }
}
