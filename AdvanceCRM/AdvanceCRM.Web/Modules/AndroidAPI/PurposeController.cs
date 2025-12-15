using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurposeController : ControllerBase
    {
        List<PurposeModel> attend;
        private readonly string _connectionString;

        public PurposeController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            attend = new List<PurposeModel>();
        }

        [HttpGet]
        public IEnumerable<PurposeModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select * from Purpose";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        attend.Add(new PurposeModel
                        {
                            id = (int)dr["Id"],
                            Purpose = (String)dr["Purpose"],
                        });
                    }
                }
            }
            return attend;
        }

        [HttpGet("{id}")]
        public IEnumerable<PurposeModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select * from Purpose where id=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        attend.Add(new PurposeModel
                        {
                            id = (int)dr["Id"],
                            Purpose = (String)dr["Purpose"],
                        });
                    }
                }
            }
            return attend;
        }
    }
}