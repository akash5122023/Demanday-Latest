using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class ChartofAccountBasedonEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Default.ChartofAccountBasedonEditor";

        public ChartofAccountBasedonEditorAttribute()
            : base(Key)
        {
        }
    }
}
