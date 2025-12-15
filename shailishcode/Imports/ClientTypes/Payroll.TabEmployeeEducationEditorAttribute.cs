using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Payroll
{
    public partial class TabEmployeeEducationEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Payroll.TabEmployeeEducationEditor";

        public TabEmployeeEducationEditorAttribute()
            : base(Key)
        {
        }
    }
}
