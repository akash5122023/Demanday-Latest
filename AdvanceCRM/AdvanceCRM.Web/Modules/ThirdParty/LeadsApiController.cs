using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using AdvanceCRM.Attendance;
using System;
using System.Data;
using System.Globalization;

using Microsoft.Data.SqlClient;


namespace AdvanceCRM.Modules.ThirdParty
{
    [Route("api/Leads")]
    [ApiController]
    public class LeadsApiController : ControllerBase
    {
        private readonly ISqlConnections _connections;

        public LeadsApiController(ISqlConnections connections)
        {
            _connections = connections;
        }

        [HttpGet("CheckPunchStatus")]
        public string CheckPunchStatus(int UserId)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");

            using (var connection = _connections.NewFor<AttendanceRow>())
            {

                const string query = @"SELECT TOP 1 PunchIn, PunchOut FROM Attendance
                         WHERE Name = @UserId AND CAST(DateNTime AS DATE) = @Date
                         ORDER BY DateNTime DESC";


                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@UserId", UserId));
                    command.Parameters.Add(new SqlParameter("@Date", date));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var punchIn = reader["PunchIn"] as DateTime?;
                            if (punchIn != null)
                                return "PunchedIn";
                        }
                    }
                }
            }
            return "NoPunch";
        }

        [HttpPost("PunchIn")]
        public string PunchIn(int UserId, string Location, string Coordinates)
        {
            try
            {
                if (UserId <= 0)
                    return "Unable to determine the current user.";

                var now = DateTime.Now;
                var resolvedLocation = string.IsNullOrWhiteSpace(Location) ? "Unknown" : Location.Trim();
                var resolvedCoordinates = string.IsNullOrWhiteSpace(Coordinates) ? string.Empty : Coordinates.Trim();

                using var connection = _connections.NewFor<AttendanceRow>();
                connection.Open();

                // Prevent a duplicate punch-in for the same day.
                using (var checkCmd = connection.CreateCommand())
                {
                    checkCmd.CommandText = @"SELECT COUNT(Id) FROM Attendance
                        WHERE Name = @UserId AND CAST(DateNTime AS DATE) = CAST(@Date AS DATE)";
                    checkCmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                    checkCmd.Parameters.Add(new SqlParameter("@Date", now));
                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        return "You have already punched in today.";
                }

                using (var insertCmd = connection.CreateCommand())
                {
                    insertCmd.CommandText = @"
INSERT INTO Attendance([Name], [UserId], [DateNTime], [Location], [Coordinates], [PunchIn], [PunchOut], [Type])
VALUES (@Name, @UserId, @Date, @Location, @Coordinates, @Punch, @Punch, 3)";
                    insertCmd.Parameters.Add(new SqlParameter("@Name", UserId));
                    insertCmd.Parameters.Add(new SqlParameter("@UserId", UserId.ToString(CultureInfo.InvariantCulture)));
                    insertCmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime) { Value = now });
                    insertCmd.Parameters.Add(new SqlParameter("@Location", resolvedLocation));
                    insertCmd.Parameters.Add(new SqlParameter("@Coordinates", resolvedCoordinates));
                    insertCmd.Parameters.Add(new SqlParameter("@Punch", SqlDbType.DateTime) { Value = now });
                    insertCmd.ExecuteNonQuery();
                }

                return "Punch-in recorded at " + now.ToString("hh:mm tt", CultureInfo.CurrentUICulture) + ".";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [HttpPost("PunchOut")]
        public string PunchOut(int UserId, string Coordinates = null)
        {
            try
            {
                if (UserId <= 0)
                    return "Unable to determine the current user.";

                var now = DateTime.Now;

                using var connection = _connections.NewFor<AttendanceRow>();
                connection.Open();

                DateTime punchInTime;
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT TOP 1 PunchIn FROM Attendance
                        WHERE Name = @UserId AND CAST(DateNTime AS DATE) = CAST(@Date AS DATE)
                        ORDER BY DateNTime DESC";
                    cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                    cmd.Parameters.Add(new SqlParameter("@Date", now));
                    var obj = cmd.ExecuteScalar();
                    if (obj == null || obj == DBNull.Value || !DateTime.TryParse(obj.ToString(), out punchInTime))
                        return "Punch-in record not found for today.";
                }

                var hoursDiff = (now - punchInTime).TotalHours;
                var type = hoursDiff > 4 ? 1 : 3;

                using (var updateCmd = connection.CreateCommand())
                {
                    updateCmd.CommandText = @"UPDATE Attendance SET PunchOut = @PunchOut, Type = @Type
                        WHERE Name = @UserId AND CAST(DateNTime AS DATE) = CAST(@Date AS DATE)";
                    updateCmd.Parameters.Add(new SqlParameter("@PunchOut", SqlDbType.DateTime) { Value = now });
                    updateCmd.Parameters.Add(new SqlParameter("@Type", type));
                    updateCmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                    updateCmd.Parameters.Add(new SqlParameter("@Date", now));
                    updateCmd.ExecuteNonQuery();
                }

                return "Punch-out recorded at " + now.ToString("hh:mm tt", CultureInfo.CurrentUICulture) + ".";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
