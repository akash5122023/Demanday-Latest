using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using Serenity.Data;
using AdvanceCRM.ThirdParty;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class SulekhaController : ApiController
    {
        public string sulkharesult;
        private readonly ISqlConnections _connections;

        public SulekhaController(ISqlConnections connections)
        {
            _connections = connections;
        }
        [HttpPost]
        public IHttpActionResult SulekhaJSON([FromBody] sulekhadata data)
        {

            try
            {
                
                var dt = data.LeadDate.ToString("yyyy-MM-dd HH:mm:ss");

                //DateTime datetime = Convert.ToDateTime(dt);



                string str = "INSERT INTO SulekhaDetails ([UserName],[Mobile],[Email],[City] ,[Localities],[Comments],[Keywords],[DateTime]) VALUES " +
                "('" + data.UserName + "','" + data.UserMobile + "','" + data.UserEmail + "','" + data.UserCity + "','" + data.UserLocalities + "','"
                        + data.UserComments + "','" + data.Keywords + "','"+ dt + "')";

                using (var innerConnection = _connections.NewFor<SulekhaDetailsRow>())
                {
                    innerConnection.Execute(str);
                }
                // do something with the data
                sulkharesult = "Success";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var Sulekharesult1 = new { message = sulkharesult };
            return Ok(Sulekharesult1);
        }

        /// </summary>


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