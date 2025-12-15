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
    public class QuoMaxController : ControllerBase
    {
        List<QuotationModel> Contact;
        private readonly string _connectionString;

        public QuoMaxController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            Contact = new List<QuotationModel>();
        }

        [HttpGet]
        public IEnumerable<QuotationModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select MAX(QuotationNo) as QuoNo from Quotation";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Contact.Add(new QuotationModel
                        {
                            QuotationNo = (int)dr["QuoNo"]
                        });
                    }
                }
            }
            return Contact;
        }

        [HttpGet("{id}")]
        public IEnumerable<QuotationModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select MAX(QuotationNo) as QuoNo from Quotation en,Users u where u.CompanyId=en.CompanyId and u.CompanyId=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Contact.Add(new QuotationModel
                        {
                            QuotationNo = (int)dr["QuoNo"]
                        });
                    }
                }
            }
            return Contact;
        }
    }
}