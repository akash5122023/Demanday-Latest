using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Administration
{
    public partial class HierarchyEditorAttribute : LookupEditorBaseAttribute
    {
        public const string Key = "AdvanceCRM.Administration.HierarchyEditor";

        public HierarchyEditorAttribute()
            : base(Key)
        {
        }
    }
}
