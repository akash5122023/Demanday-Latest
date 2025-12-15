using AdvanceCRM.Modules.Administration.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        List<UserPModel> User;
        private readonly string _connectionString;

        public ProfileController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            User = new List<UserPModel>();
        }

        [HttpGet]
        public IEnumerable<UserPModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select UserId,Username,Phone,NonOperational,IsActive from Users";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic phone, nonop, active, activestat;

                        if (dr["Phone"] == DBNull.Value)
                            phone = "";
                        else
                            phone = (string)dr["Phone"];

                        if (dr["NonOperational"] == DBNull.Value)
                            nonop = "";
                        else
                            nonop = (bool)dr["NonOperational"];

                        if (dr["IsActive"] == DBNull.Value)
                            active = "";
                        else
                            active = dr["IsActive"];

                        if (active == 1)
                        {
                            activestat = "True";
                        }
                        else
                        {
                            activestat = "False";
                        }

                        User.Add(new UserPModel
                        {
                            Id = (int)dr["UserId"],
                            Name = (string)dr["Username"],
                            MobileNo = phone,
                            NonOperational = nonop,
                            IsActive = activestat
                        });
                    }
                }
            }
            return User;
        }

        [HttpGet("{id}")]
        public IEnumerable<UserPModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select UserId,Username,Phone,NonOperational,IsActive from Users where IsActive=1 and Phone=" + id;
                using (var sda1 = new SqlDataAdapter(str, con))
                {
                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1);

                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        dynamic phone, nonop, active, activestat;

                        if (dr["Phone"] == DBNull.Value)
                            phone = "";
                        else
                            phone = (string)dr["Phone"];

                        if (dr["NonOperational"] == DBNull.Value)
                            nonop = "";
                        else
                            nonop = (bool)dr["NonOperational"];

                        if (dr["IsActive"] == DBNull.Value)
                            active = "";
                        else
                            active = dr["IsActive"];

                        if (active == 1)
                        {
                            activestat = "True";
                        }
                        else
                        {
                            activestat = "False";
                        }

                        User.Add(new UserPModel
                        {
                            Id = (int)dr["UserId"],
                            Name = (string)dr["Username"],
                            MobileNo = phone,
                            NonOperational = nonop,
                            IsActive = activestat
                        });
                    }
                }
            }
            return User;
        }
    }
}