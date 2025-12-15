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
    public class QWonController : ControllerBase
    {
        List<QWonModel> booker;
        private readonly string _connectionString;

        public QWonController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            booker = new List<QWonModel>();
        }

        [HttpGet]
        public IEnumerable<QWonModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Quotation where ClosingType=1";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QWonModel
                        {
                            WonQuotation = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }

        [HttpGet("{id}")]
        public IEnumerable<QWonModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Enquiry where ClosingType=1 and AssignedId=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QWonModel
                        {
                            WonQuotation = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }
    }
}