using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Payroll
{
    public partial class TabSalaryDetail1EditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Payroll.TabSalaryDetail1Editor";

        public TabSalaryDetail1EditorAttribute()
            : base(Key)
        {
        }
    }
}
