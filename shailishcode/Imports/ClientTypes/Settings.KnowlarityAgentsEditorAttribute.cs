using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Settings
{
    public partial class KnowlarityAgentsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Settings.KnowlarityAgentsEditor";

        public KnowlarityAgentsEditorAttribute()
            : base(Key)
        {
        }
    }
}
