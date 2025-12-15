using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdvanceCRM.Modules.Administration.User;
using AdvanceCRM.Administration.Endpoints;
using AdvanceCRM.Administration;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Serenity.Data;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class UserController : ApiController
    {
        List<UserModel> User;
        private readonly ISqlConnections _connections;

        public UserController(ISqlConnections connections)
        {
            _connections = connections;
        }
        public UserController()
        {
            User = new List<UserModel>();
        }
        public IEnumerable<UserModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select UserId,Username,Phone from Users where IsActive=1";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic phone;//obj == DBNull.Value
                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];

                User.Add(new UserModel
                {
                    Id = (int)dr["UserId"],
                    Name = (string)dr["Username"],
                    MobileNo = phone
                });
            }
              
            return User;
        }

        // GET api/<controller>/5
        public IEnumerable<UserModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select UserId,Username,Phone from Users where IsActive=1 and Phone=" + id;
            SqlDataAdapter sda1 = new SqlDataAdapter(str, con);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
          
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                dynamic phone;//obj == DBNull.Value
                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];

                User.Add(new UserModel
                {
                    Id = (int)dr["UserId"],
                    Name = (string)dr["Username"],
                    MobileNo = phone
                });
            }
            return User;
        }


        //Update USers Location
        [HttpPost]
        public string UserLocation( String Location, String Coordinates, int UserId)
        {
            // try
            {
                //var datetimestr = date + " " + time;
                // DateTime datetime = DateTime.ParseExact(VisitDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                string str = "Update Users set [Coordinates]='" + Coordinates + "', [Location]='" + Location  + "' where UserId=" + UserId;

                using (var innerConnection = _connections.NewFor<UserRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    return "Error While Inserting Visit Details";
            //}

            return "Successfully Updated User Location";
        }


    }
}