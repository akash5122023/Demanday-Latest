using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Payroll
{
    public partial class TabHolidaysEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Payroll.TabHolidaysEditor";

        public TabHolidaysEditorAttribute()
            : base(Key)
        {
        }
    }
}
