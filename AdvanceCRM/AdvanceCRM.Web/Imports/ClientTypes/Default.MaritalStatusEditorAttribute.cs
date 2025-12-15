using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class MaritalStatusEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Default.MaritalStatusEditor";

        public MaritalStatusEditorAttribute()
            : base(Key)
        {
        }
    }
}
