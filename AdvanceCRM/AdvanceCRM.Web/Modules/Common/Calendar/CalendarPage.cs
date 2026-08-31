namespace AdvanceCRM.Common.Calendar
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Attendance;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class CalendarController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public CalendarController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Route("Calendar")]
        [Authorize]
        [HttpGet]
        public IActionResult Index(int? year, int? month, int? userId)
        {
            var now = DateTime.Now;
            int targetYear = year.HasValue && year.Value >= 2000 && year.Value <= 2100 ? year.Value : now.Year;
            int targetMonth = month.HasValue && month.Value >= 1 && month.Value <= 12 ? month.Value : now.Month;

            var model = new CalendarModel
            {
                Year = targetYear,
                Month = targetMonth,
                MonthName = new DateTime(targetYear, targetMonth, 1).ToString("MMMM yyyy", CultureInfo.InvariantCulture),
                TodayCheckIn = "-:-",
                TodayCheckOut = "Not yet",
                TodayWorkingHours = "0h 00m",
                TodayBreakTime = "0m"
            };

            var userIdString = Context.User.GetIdentifier();
            if (!int.TryParse(userIdString, out int loggedInUserId))
            {
                loggedInUserId = 0;
            }

            using (var connection = _connections.NewFor<AttendanceRow>())
            {
                connection.Open();

                var u = UserRow.Fields;

                // 1. Fetch User List for Admin/User selection dropdown
                var userList = connection.List<UserRow>(q => q
                    .SelectTableFields()
                    .Select(u.TeamsTeamName)
                    .Where(u.IsActive == 1)
                    .OrderBy(u.DisplayName));

                model.UserList = userList;
                model.MonthBirthdays = BuildMonthBirthdays(userList, targetYear, targetMonth, now);

                int targetUserId = (userId.HasValue && userId.Value > 0) ? userId.Value : loggedInUserId;
                model.TargetUserId = targetUserId;

                // 2. Fetch Selected User Info
                var targetUserRow = userList.FirstOrDefault(x => x.UserId == targetUserId);
                if (targetUserRow == null)
                {
                    targetUserRow = connection.TryFirst<UserRow>(q => q
                        .SelectTableFields()
                        .Select(u.TeamsTeamName)
                        .Where(u.UserId == targetUserId));
                }

                model.User = targetUserRow;

                string dept = "Sales";
                if (targetUserRow != null && !string.IsNullOrWhiteSpace(targetUserRow.TeamsTeamName))
                {
                    dept = targetUserRow.TeamsTeamName;
                }
                model.DepartmentName = dept;

                // 3. Fetch Attendance Records for Target User for target Month & Year
                var startDate = new DateTime(targetYear, targetMonth, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var a = AttendanceRow.Fields;
                var records = connection.List<AttendanceRow>(q => q
                    .SelectTableFields()
                    .Where(a.Name == targetUserId & a.DateNTime >= startDate & a.DateNTime <= endDate)
                    .OrderBy(a.DateNTime));

                model.AttendanceRecords = records;

                // Parse user shift start time (default 09:00 AM)
                TimeSpan shiftStart = new TimeSpan(9, 0, 0);
                if (!string.IsNullOrWhiteSpace(targetUserRow?.StartTime))
                {
                    if (TimeSpan.TryParse(targetUserRow.StartTime, out var parsedShift))
                    {
                        shiftStart = parsedShift;
                    }
                }

                int daysInMonth = DateTime.DaysInMonth(targetYear, targetMonth);
                double totalWorkingMinutesAcc = 0;
                double totalBreakMinutesAcc = 0;

                for (int day = 1; day <= daysInMonth; day++)
                {
                    var currentDate = new DateTime(targetYear, targetMonth, day);
                    bool isWeekend = currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday;
                    bool isPastOrToday = currentDate.Date <= now.Date;
                    bool isToday = currentDate.Date == now.Date;

                    var dayRecord = records.FirstOrDefault(r => r.DateNTime.HasValue && r.DateNTime.Value.Date == currentDate);

                    var dayDetail = new AttendanceDayDetail
                    {
                        Day = day,
                        Date = currentDate,
                        DateFormatted = currentDate.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture),
                        IsToday = isToday
                    };

                    if (isWeekend)
                    {
                        dayDetail.Status = "Weekend";
                        dayDetail.StatusCssClass = "weekend";
                        dayDetail.CheckIn = "--";
                        dayDetail.CheckOut = "--";
                        dayDetail.BreakTime = "0m";
                        dayDetail.WorkingHours = "--";
                        dayDetail.LateBy = "--";
                        dayDetail.EarlyExit = "--";
                    }
                    else
                    {
                        model.WorkingDaysCount++;

                        if (dayRecord != null && dayRecord.PunchIn.HasValue)
                        {
                            var punchIn = dayRecord.PunchIn.Value;
                            var punchOut = dayRecord.PunchOut;
                            int breakMins = dayRecord.BreakMinutes ?? 0;
                            totalBreakMinutesAcc += breakMins;

                            dayDetail.CheckIn = punchIn.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                            dayDetail.BreakTime = breakMins >= 60
                                ? $"{(breakMins / 60)}h {(breakMins % 60):D2}m"
                                : $"{breakMins}m";

                            if (punchOut.HasValue && punchOut.Value != punchIn)
                            {
                                dayDetail.CheckOut = punchOut.Value.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                                double grossMinutes = (punchOut.Value - punchIn).TotalMinutes;
                                double netMinutes = Math.Max(0, grossMinutes - breakMins);
                                totalWorkingMinutesAcc += netMinutes;

                                int hrs = (int)(netMinutes / 60);
                                int mins = (int)(netMinutes % 60);
                                dayDetail.WorkingHours = $"{hrs}h {mins:D2}m";
                            }
                            else
                            {
                                dayDetail.CheckOut = isToday ? "Not yet" : "--";
                                double grossMinutes = (now - punchIn).TotalMinutes;
                                double netMinutes = Math.Max(0, grossMinutes - breakMins);
                                totalWorkingMinutesAcc += netMinutes;

                                int hrs = (int)(netMinutes / 60);
                                int mins = (int)(netMinutes % 60);
                                dayDetail.WorkingHours = isToday ? $"{hrs}h {mins:D2}m" : "--";
                            }

                            // Check Late Arrival
                            if (punchIn.TimeOfDay > shiftStart.Add(TimeSpan.FromMinutes(15)))
                            {
                                int lateMins = (int)(punchIn.TimeOfDay - shiftStart).TotalMinutes;
                                dayDetail.Status = "Late";
                                dayDetail.StatusCssClass = "late";
                                dayDetail.LateBy = $"{lateMins} min";
                                model.LateCount++;
                                model.PresentCount++;
                            }
                            else
                            {
                                dayDetail.Status = "Present";
                                dayDetail.StatusCssClass = "present";
                                dayDetail.LateBy = "--";
                                model.PresentCount++;
                            }

                            dayDetail.EarlyExit = "--";
                        }
                        else if (isPastOrToday && !isToday)
                        {
                            dayDetail.Status = "Absent";
                            dayDetail.StatusCssClass = "absent";
                            dayDetail.CheckIn = "--";
                            dayDetail.CheckOut = "--";
                            dayDetail.BreakTime = "0m";
                            dayDetail.WorkingHours = "--";
                            dayDetail.LateBy = "--";
                            dayDetail.EarlyExit = "--";
                            model.AbsentCount++;

                            model.NeedsAttentionList.Add(new NeedsAttentionItem
                            {
                                DateText = currentDate.ToString("MMM d", CultureInfo.InvariantCulture),
                                Title = "Absent - no record",
                                Severity = "error"
                            });
                        }
                        else if (isToday)
                        {
                            dayDetail.Status = "No Record";
                            dayDetail.StatusCssClass = "absent";
                            dayDetail.CheckIn = "--";
                            dayDetail.CheckOut = "--";
                            dayDetail.BreakTime = "0m";
                            dayDetail.WorkingHours = "--";
                            dayDetail.LateBy = "--";
                            dayDetail.EarlyExit = "--";

                            model.NeedsAttentionList.Add(new NeedsAttentionItem
                            {
                                DateText = currentDate.ToString("MMM d", CultureInfo.InvariantCulture),
                                Title = "Missing check-in",
                                Severity = "warning"
                            });
                        }
                        else
                        {
                            dayDetail.Status = "None";
                            dayDetail.StatusCssClass = "none";
                            dayDetail.CheckIn = "--";
                            dayDetail.CheckOut = "--";
                            dayDetail.BreakTime = "0m";
                            dayDetail.WorkingHours = "--";
                            dayDetail.LateBy = "--";
                            dayDetail.EarlyExit = "--";
                        }
                    }

                    if (isToday)
                    {
                        model.TodayCheckIn = dayDetail.CheckIn;
                        model.TodayCheckOut = dayDetail.CheckOut;
                        model.TodayWorkingHours = dayDetail.WorkingHours;
                        model.TodayBreakTime = dayDetail.BreakTime;
                    }

                    model.DayDetails[day] = dayDetail;
                }

                // Overall Totals
                int totalHours = (int)(totalWorkingMinutesAcc / 60);
                int totalMins = (int)(totalWorkingMinutesAcc % 60);
                model.TotalWorkingHours = $"{totalHours}h {totalMins:D2}m";

                if (model.PresentCount > 0)
                {
                    double avgMins = totalWorkingMinutesAcc / model.PresentCount;
                    model.AvgWorkingHours = $"{(int)(avgMins / 60)}h {(int)(avgMins % 60):D2}m";
                }
                else
                {
                    model.AvgWorkingHours = "0h 00m";
                }

                int totalBreakHours = (int)(totalBreakMinutesAcc / 60);
                int totalBreakMins = (int)(totalBreakMinutesAcc % 60);
                model.TotalBreakHours = $"{totalBreakHours}h {totalBreakMins:D2}m";

                model.AttendancePercentage = model.WorkingDaysCount > 0
                    ? Math.Round(((double)model.PresentCount / model.WorkingDaysCount) * 100, 1)
                    : 100.0;
            }

            return View(MVC.Views.Common.Calendar.CalendarIndex, model);
        }

        /// <summary>
        /// Everyone from the same active-user list the page already loaded whose birthday falls
        /// somewhere within <paramref name="year"/>/<paramref name="month"/> - the month currently
        /// on screen, not necessarily the real "today". The birth year is used for the age, and is
        /// ignored when it is missing or in the future relative to the displayed year.
        /// </summary>
        private static List<BirthdayItem> BuildMonthBirthdays(List<UserRow> users, int year, int month, DateTime now)
        {
            var result = new List<BirthdayItem>();
            if (users == null)
                return result;

            int daysInMonth = DateTime.DaysInMonth(year, month);

            foreach (var user in users)
            {
                if (!user.DateOfBirth.HasValue)
                    continue;

                var birthDay = BirthdayDayInMonth(user.DateOfBirth.Value, month, daysInMonth);
                if (!birthDay.HasValue)
                    continue;

                var birthdayDate = new DateTime(year, month, birthDay.Value);

                int? age = null;
                if (user.DateOfBirth.Value.Year > 1 && user.DateOfBirth.Value.Year <= year)
                    age = year - user.DateOfBirth.Value.Year;

                var name = !string.IsNullOrWhiteSpace(user.DisplayName)
                    ? user.DisplayName.Trim()
                    : (user.Username ?? "").Trim();

                result.Add(new BirthdayItem
                {
                    UserId = user.UserId ?? 0,
                    Name = name,
                    Initials = InitialsOf(name),
                    Department = user.TeamsTeamName,
                    Age = age,
                    Day = birthDay.Value,
                    DateText = birthdayDate.ToString("MMM d", CultureInfo.InvariantCulture),
                    IsToday = birthdayDate.Date == now.Date
                });
            }

            return result
                .OrderBy(x => x.Day)
                .ThenBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        /// <summary>
        /// Which day of the given month this birth date lands on, or null when it doesn't occur in
        /// that month at all. Someone born on 29 February has no birthday in a non-leap February,
        /// so theirs is kept on the 28th rather than disappearing three years out of four.
        /// </summary>
        private static int? BirthdayDayInMonth(DateTime dateOfBirth, int month, int daysInMonth)
        {
            if (dateOfBirth.Month != month)
                return null;

            if (dateOfBirth.Day <= daysInMonth)
                return dateOfBirth.Day;

            if (dateOfBirth.Day == 29 && month == 2)
                return 28;

            return null;
        }

        /// <summary>Up to two letters for the avatar circle, e.g. "Akash Dudhe" -> "AD".</summary>
        private static string InitialsOf(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "?";

            var parts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
                return parts[0].Substring(0, 1).ToUpperInvariant();

            return (parts[0].Substring(0, 1) + parts[parts.Length - 1].Substring(0, 1)).ToUpperInvariant();
        }
    }
}