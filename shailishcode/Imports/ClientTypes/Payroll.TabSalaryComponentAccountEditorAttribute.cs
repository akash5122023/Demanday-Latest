using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Payroll
{
    public partial class TabSalaryComponentAccountEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Payroll.TabSalaryComponentAccountEditor";

        public TabSalaryComponentAccountEditorAttribute()
            : base(Key)
        {
        }
    }
}
