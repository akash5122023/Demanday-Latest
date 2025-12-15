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
    public class EnqMaxController : ControllerBase
    {
        List<EnquiryModel> Contact;
        private readonly string _connectionString;

        public EnqMaxController(IConfiguration config)
        {
            Contact = new List<EnquiryModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "SELECT MAX(EnquiryNo) as EnqNo FROM Enquiry";
                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Contact.Add(new EnquiryModel
                    {
                        EnquiryNo = dr["EnqNo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EnqNo"])
                    });
                }

                return Ok(Contact);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Fixed SQL injection vulnerability
                string str = "SELECT MAX(EnquiryNo) as EnqNo FROM Enquiry en, Users u WHERE u.CompanyId = en.CompanyId AND u.CompanyId = @CompanyId";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@CompanyId", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Contact.Add(new EnquiryModel
                    {
                        EnquiryNo = dr["EnqNo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EnqNo"])
                    });
                }

                return Ok(Contact);
            }
        }
    }
}