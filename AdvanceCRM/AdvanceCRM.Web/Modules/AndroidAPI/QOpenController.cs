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
    public class QOpenController : ControllerBase
    {
        List<QOpenModel> booker;
        private readonly string _connectionString;

        public QOpenController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            booker = new List<QOpenModel>();
        }

        [HttpGet]
        public IEnumerable<QOpenModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Quotation where Status=1";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QOpenModel
                        {
                            OpenQuotation = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }

        [HttpGet("{id}")]
        public IEnumerable<QOpenModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Quotation where Status=1 and AssignedId=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QOpenModel
                        {
                            OpenQuotation = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }
    }
}