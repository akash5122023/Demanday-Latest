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
    public class EWonController : ControllerBase
    {
        List<EWonModel> booker;
        private readonly string _connectionString;

        public EWonController(IConfiguration config)
        {
            booker = new List<EWonModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "SELECT COUNT(Id) as no FROM Enquiry WHERE ClosingType = 1";
                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                booker.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    booker.Add(new EWonModel
                    {
                        WonEnquiry = dr["no"] == DBNull.Value ? 0 : Convert.ToInt32(dr["no"])
                    });
                }

                return Ok(booker);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Fixed SQL injection vulnerability
                string str = "SELECT COUNT(Id) as no FROM Enquiry WHERE ClosingType = 1 AND AssignedId = @AssignedId";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@AssignedId", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                booker.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    booker.Add(new EWonModel
                    {
                        WonEnquiry = dr["no"] == DBNull.Value ? 0 : Convert.ToInt32(dr["no"])
                    });
                }

                return Ok(booker);
            }
        }
    }
}