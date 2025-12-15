using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Attendance
{
    public partial class TabEmployeeAttendanceDetailEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Attendance.TabEmployeeAttendanceDetailEditor";

        public TabEmployeeAttendanceDetailEditorAttribute()
            : base(Key)
        {
        }
    }
}
