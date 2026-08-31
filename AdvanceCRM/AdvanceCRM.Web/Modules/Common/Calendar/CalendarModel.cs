namespace AdvanceCRM.Common.Calendar
{
    using System;
    using System.Collections.Generic;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Attendance;

    public class AttendanceDayDetail
    {
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public string DateFormatted { get; set; } // e.g. "July 9, 2026"
        public string Status { get; set; } // "Present", "Late", "Absent", "Leave", "Weekend", "None"
        public string StatusCssClass { get; set; } // "present", "late", "absent", "leave", "weekend"
        public string CheckIn { get; set; } // e.g. "09:35 AM" or "--"
        public string CheckOut { get; set; } // e.g. "06:00 PM" or "Not yet" or "--"
        public string BreakTime { get; set; } // e.g. "1h 00m" or "0m"
        public string WorkingHours { get; set; } // e.g. "8h 25m" or "--"
        public string LateBy { get; set; } // e.g. "35 min" or "--"
        public string EarlyExit { get; set; } // e.g. "--" or "15 min"
        public bool IsToday { get; set; }
    }

    public class NeedsAttentionItem
    {
        public string DateText { get; set; } // e.g. "Jul 14"
        public string Title { get; set; } // e.g. "Absent - no record"
        public string Severity { get; set; } // "error", "warning"
    }

    /// <summary>One person whose birthday falls within the month the calendar is currently showing.</summary>
    public class BirthdayItem
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        /// <summary>One or two letters for the avatar circle.</summary>
        public string Initials { get; set; }
        /// <summary>Their team, when they have one - the same label the header shows.</summary>
        public string Department { get; set; }
        /// <summary>The age they turn that year, or null when the birth year is not known.</summary>
        public int? Age { get; set; }
        /// <summary>Day of the displayed month (1-31) this birthday lands on.</summary>
        public int Day { get; set; }
        /// <summary>Formatted date within the displayed month, e.g. "Sep 1".</summary>
        public string DateText { get; set; }
        /// <summary>Whether this lands on the real, current calendar date (not just the month/day being browsed).</summary>
        public bool IsToday { get; set; }
    }

    public class CalendarModel
    {
        public UserRow User { get; set; }
        public string DepartmentName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }

        // Monthly Stats
        public int WorkingDaysCount { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public int LeaveCount { get; set; }
        public double AttendancePercentage { get; set; }

        public string TotalWorkingHours { get; set; }
        public string AvgWorkingHours { get; set; }
        public string TotalBreakHours { get; set; }

        // Live Banner
        public string TodayCheckIn { get; set; }
        public string TodayCheckOut { get; set; }
        public string TodayWorkingHours { get; set; }
        public string TodayBreakTime { get; set; }
        public string TodayPunchStatus { get; set; }

        public Dictionary<int, AttendanceDayDetail> DayDetails { get; set; }
        public List<NeedsAttentionItem> NeedsAttentionList { get; set; }
        public List<AttendanceRow> AttendanceRecords { get; set; }

        public int TargetUserId { get; set; }
        public List<UserRow> UserList { get; set; }

        /// <summary>
        /// Everyone whose birthday falls within the month currently on screen (Year/Month), not
        /// just today - so browsing to September still surfaces whoever's birthday lands there,
        /// whichever user's attendance is on screen (the card is about the team, not the person).
        /// </summary>
        public List<BirthdayItem> MonthBirthdays { get; set; }

        public CalendarModel()
        {
            UserList = new List<UserRow>();
            DayDetails = new Dictionary<int, AttendanceDayDetail>();
            NeedsAttentionList = new List<NeedsAttentionItem>();
            AttendanceRecords = new List<AttendanceRow>();
            MonthBirthdays = new List<BirthdayItem>();
        }
    }
}