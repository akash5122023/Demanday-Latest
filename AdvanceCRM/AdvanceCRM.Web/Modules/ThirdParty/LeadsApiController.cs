using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using AdvanceCRM.Attendance;
using System;

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
    }
}
