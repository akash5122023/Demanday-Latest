using AdvanceCRM.Administration;
using AdvanceCRM.Modules.Administration.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        List<UserModel> User;
        private readonly ISqlConnections _connections;
        private readonly string _connectionString;

        public UserController(ISqlConnections connections, IConfiguration configuration)
        {
            _connections = connections;
            _connectionString = Startup.connectionString;
            User = new List<UserModel>();
        }

        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select UserId,Username,Phone from Users where IsActive=1";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic phone;

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
                }
            }
            return User;
        }

        [HttpGet("{id}")]
        public IEnumerable<UserModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select UserId,Username,Phone from Users where IsActive=1 and Phone=" + id;
                using (var sda1 = new SqlDataAdapter(str, con))
                {
                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1);

                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        dynamic phone;

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
                }
            }
            return User;
        }

        [HttpPost]
        public string UserLocation(string Location, string Coordinates, int UserId)
        {
            string str = "Update Users set [Coordinates]='" + Coordinates + "', [Location]='" + Location + "' where UserId=" + UserId;

            using (var innerConnection = _connections.NewFor<UserRow>())
            {
                innerConnection.Execute(str);
            }

            return "Successfully Updated User Location";
        }
    }
}