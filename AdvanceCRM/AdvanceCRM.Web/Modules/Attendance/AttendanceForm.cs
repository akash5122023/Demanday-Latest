
namespace AdvanceCRM.Attendance.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Attendance.Attendance")]
    [BasedOnRow(typeof(AttendanceRow), CheckNames = true)]
    public class AttendanceForm
    {
        [HalfWidth]
        public Int32 Name { get; set; }
        [HalfWidth]
        public Int32 Type { get; set; }
        [HalfWidth]
         public DateTime DateNTime { get; set; }
        [HalfWidth]
        public String Coordinates { get; set; }
        public String Location { get; set; }
        [OneThirdWidth]
        [DateTimeEditor(IntervalMinutes = 1)]
        public DateTime PunchIn { get; set; }
        [OneThirdWidth]
        [DateTimeEditor(IntervalMinutes = 1)]
        public DateTime PunchOut { get; set; }
        [OneThirdWidth]
       
        public Double Distance { get; set; }

        //public Int32 ApprovedBy { get; set; }
    }
}