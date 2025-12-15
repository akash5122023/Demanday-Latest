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
    public class EFupCloseController : ControllerBase
    {
        List<EFupCloseModel> booker;
        private readonly string _connectionString;

        public EFupCloseController(IConfiguration config)
        {
            booker = new List<EFupCloseModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "SELECT COUNT(Id) as no FROM EnquiryFollowUps WHERE Status = 2 AND FollowupDate >= '" + dt + "'";

                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                booker.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    booker.Add(new EFupCloseModel
                    {
                        CloseEnquiryFollowup = dr["no"] == DBNull.Value ? 0 : Convert.ToInt32(dr["no"])
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
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                // Fixed SQL injection vulnerability - parameterized query for date too
                string str = "SELECT COUNT(Id) as no FROM EnquiryFollowUps WHERE Status = 2 AND FollowupDate >= @Date AND RepresentativeId = @RepresentativeId";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@RepresentativeId", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                booker.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    booker.Add(new EFupCloseModel
                    {
                        CloseEnquiryFollowup = dr["no"] == DBNull.Value ? 0 : Convert.ToInt32(dr["no"])
                    });
                }

                return Ok(booker);
            }
        }
    }
}