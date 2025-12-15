using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class LoyalityProgramDetailEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Default.LoyalityProgramDetailEditor";

        public LoyalityProgramDetailEditorAttribute()
            : base(Key)
        {
        }
    }
}
