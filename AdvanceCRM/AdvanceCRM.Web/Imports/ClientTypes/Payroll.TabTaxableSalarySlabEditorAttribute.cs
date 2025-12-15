using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Payroll
{
    public partial class TabTaxableSalarySlabEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Payroll.TabTaxableSalarySlabEditor";

        public TabTaxableSalarySlabEditorAttribute()
            : base(Key)
        {
        }
    }
}
