using System;
using System.Web.Http;
using Serenity.Data;
using System.Globalization;
using AdvanceCRM.ThirdParty;
using Microsoft.AspNetCore.Mvc;


namespace AdvanceCRM.Modules.AndroidAPI
{
    public class IVRController : ApiController
    {
        private string result1;
        private readonly ISqlConnections _connections;

        public IVRController(ISqlConnections connections)
        {
            _connections = connections;
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult IVRJSON([System.Web.Http.FromBody] YOCC data)
        {

            try
            {
                var datetimestr = data.CallDate + " " + data.StartTime;
                var enddate = data.CallDate + " " + data.EndTime;
                //  int sec = int.Parse(DateTime.Parse(enddate.ToSql()) -DateTime.Parse(datetimestr.ToSql()));
                DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime datetime1 = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                //    DateTime sec =datetime1 - datetime;

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
                // do something with the data
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