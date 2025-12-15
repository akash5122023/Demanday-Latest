
namespace AdvanceCRM.Attendance.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Attendance.Attendance")]
    [BasedOnRow(typeof(AttendanceRow), CheckNames = true)]
    public class AttendanceColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        public Int32 Id { get; set; }
        [EditLink, Width(140), QuickFilter, QuickSearch]
        public String NameDisplayName { get; set; }
        [EditLink, DateTimeEditor(IntervalMinutes = 1), SortOrder(1, true), QuickFilter]
        public DateTime DateNTime { get; set; }
        [QuickFilter]
        public Masters.AttendanceTypeMaster Type { get; set; }
        public String Location { get; set; }

        public String Coordinates { get; set; }
        [QuickFilter]
        public String ApprovedByDisplayName { get; set; }


        
        public TimeZone PunchIn { get; set; }
       
        public TimeZone PunchOut { get; set; }
        
        public Double Distance { get; set; }
    }
}