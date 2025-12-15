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
    public class QLostController : ControllerBase
    {
        List<QLostModel> booker;
        private readonly string _connectionString;

        public QLostController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            booker = new List<QLostModel>();
        }

        [HttpGet]
        public IEnumerable<QLostModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Quotation where ClosingType=2";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QLostModel
                        {
                            LostQuotation = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }

        [HttpGet("{id}")]
        public IEnumerable<QLostModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Quotation where ClosingType=2 and AssignedId=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QLostModel
                        {
                            LostQuotation = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }
    }
}