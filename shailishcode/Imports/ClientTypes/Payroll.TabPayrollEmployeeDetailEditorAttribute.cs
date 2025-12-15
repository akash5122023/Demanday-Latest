using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Payroll
{
    public partial class TabPayrollEmployeeDetailEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Payroll.TabPayrollEmployeeDetailEditor";

        public TabPayrollEmployeeDetailEditorAttribute()
            : base(Key)
        {
        }
    }
}
