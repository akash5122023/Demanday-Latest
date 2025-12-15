using System;
using Serenity.Data;
using AdvanceCRM.ThirdParty;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class SulekhaController : ControllerBase
    {
        public string sulkharesult;
        private readonly ISqlConnections _connections;

        public SulekhaController(ISqlConnections connections)
        {
            _connections = connections;
        }

        [HttpPost]
        public IActionResult SulekhaJSON([FromBody] sulekhadata data)
        {
            try
            {
                var dt = data.LeadDate.ToString("yyyy-MM-dd HH:mm:ss");

                string str = "INSERT INTO SulekhaDetails ([UserName],[Mobile],[Email],[City] ,[Localities],[Comments],[Keywords],[DateTime]) VALUES " +
                "('" + data.UserName + "','" + data.UserMobile + "','" + data.UserEmail + "','" + data.UserCity + "','" + data.UserLocalities + "','"
                        + data.UserComments + "','" + data.Keywords + "','" + dt + "')";

                using (var innerConnection = _connections.NewFor<SulekhaDetailsRow>())
                {
                    innerConnection.Execute(str);
                }

                sulkharesult = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var Sulekharesult1 = new { message = sulkharesult };
            return Ok(Sulekharesult1);
        }

        public class sulekhadata
        {
            public string UserName { get; set; }
            public string UserMobile { get; set; }
            public string UserEmail { get; set; }
            public string UserCity { get; set; }
            public string UserLocalities { get; set; }
            public string UserComments { get; set; }
            public string Keywords { get; set; }
            public DateTime LeadDate { get; set; }
        }
    }
}