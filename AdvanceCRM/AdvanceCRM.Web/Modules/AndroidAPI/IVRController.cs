using AdvanceCRM.ThirdParty;
using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using System;
using System.Globalization;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class IVRController : ControllerBase
    {
        private string result1;
        private readonly ISqlConnections _connections;

        public IVRController(ISqlConnections connections)
        {
            _connections = connections;
        }

        [HttpPost]
        public IActionResult IVRJSON([FromBody] YOCC data)
        {
            try
            {
                var datetimestr = data.CallDate + " " + data.StartTime;
                var enddate = data.CallDate + " " + data.EndTime;
                DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime datetime1 = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                string str = "INSERT INTO KnowlarityDetails ([Name],[CustomerNumber],[EmployeeNumber],[Type],[Recording],[DateTime]) VALUES " +
                "('Unknown','"
                 + data.CallerNo + "','"
                 + data.AgentNo + "','"
                 + data.CallStatus + "','"
                 + data.recordingurl + "',"
                 + datetime.ToSql() + ")";

                using (var innerConnection = _connections.NewFor<KnowlarityDetailsRow>())
                {
                    innerConnection.Execute(str);
                }
                result1 = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var result = new { message = result1 };
            return Ok(result);
        }

        public class YOCC
        {
            public string CallerNo { get; set; }
            public string CallDate { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string AgentNo { get; set; }
            public string CallStatus { get; set; }
            public string recordingurl { get; set; }
            public string CallType { get; set; }
        }
    }
}