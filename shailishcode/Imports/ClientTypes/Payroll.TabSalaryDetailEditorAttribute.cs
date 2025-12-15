using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Payroll
{
    public partial class TabSalaryDetailEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Payroll.TabSalaryDetailEditor";

        public TabSalaryDetailEditorAttribute()
            : base(Key)
        {
        }
    }
}
