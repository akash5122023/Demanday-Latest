using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class AttendanceReasonAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Default.AttendanceReason";

        public AttendanceReasonAttribute()
            : base(Key)
        {
        }
    }
}
