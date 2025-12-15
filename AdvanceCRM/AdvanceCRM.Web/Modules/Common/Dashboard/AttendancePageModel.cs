
namespace AdvanceCRM.Common
{
    using Administration;
    using System.Collections.Generic;
    using System;
    
    using AdvanceCRM.Attendance;
    

    public class AttendancePageModel
    {
        public int TotalEmployee { get; set; }
        public int punchoutEmployee { get; set; }
        public int TotalPunchedIn { get; set; }
        public int TotalPunchedOut { get; set; }
        public int TotalInactive { get; set; }
        public List<UserRow> Coordinates { get;set; }
        public List<AttendanceRow> PunchOut { get; set; }
        public List<AttendanceRow> PunchIn { get; set; }
    }
}