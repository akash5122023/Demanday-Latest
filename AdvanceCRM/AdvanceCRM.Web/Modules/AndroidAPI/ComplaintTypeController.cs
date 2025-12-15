using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintTypeController : ControllerBase
    {
        List<ComplaintTypeModel> attend;
        private readonly string _connectionString;

        public ComplaintTypeController(IConfiguration config)
        {
            attend = new List<ComplaintTypeModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "SELECT * FROM ComplaintType";
                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                attend.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    attend.Add(new ComplaintTypeModel
                    {
                        id = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]),
                        ComplaintType = dr["ComplaintType"] == DBNull.Value ? "" : dr["ComplaintType"].ToString()
                    });
                }

                return Ok(attend);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Fixed SQL injection vulnerability
                string str = "SELECT * FROM ComplaintType WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                attend.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    attend.Add(new ComplaintTypeModel
                    {
                        id = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]),
                        ComplaintType = dr["ComplaintType"] == DBNull.Value ? "" : dr["ComplaintType"].ToString()
                    });
                }

                return Ok(attend);
            }
        }
    }
}