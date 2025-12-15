using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Project
{
    public partial class TabTimeSheetDetailEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Project.TabTimeSheetDetailEditor";

        public TabTimeSheetDetailEditorAttribute()
            : base(Key)
        {
        }
    }
}
