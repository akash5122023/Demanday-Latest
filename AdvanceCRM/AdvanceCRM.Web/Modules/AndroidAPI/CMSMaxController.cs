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
    public class CMSMaxController : ControllerBase
    {
        List<CMSModel> Contact;
        private readonly string _connectionString;

        public CMSMaxController(IConfiguration config)
        {
            Contact = new List<CMSModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "SELECT MAX(CMSNo) as CMSNo FROM CMS";
                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Contact.Add(new CMSModel
                    {
                        CMSNo = dr["CMSNo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CMSNo"])
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
                string str = "SELECT MAX(CMSNo) as CMSNo FROM CMS en, Users u WHERE u.CompanyId = en.CompanyId AND u.CompanyId = @CompanyId";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@CompanyId", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Contact.Add(new CMSModel
                    {
                        CMSNo = dr["CMSNo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CMSNo"])
                    });
                }

                return Ok(Contact);
            }
        }
    }
}