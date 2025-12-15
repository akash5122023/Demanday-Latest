using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Payroll
{
    public partial class TabModeofPaymentAccountEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Payroll.TabModeofPaymentAccountEditor";

        public TabModeofPaymentAccountEditorAttribute()
            : base(Key)
        {
        }
    }
}
