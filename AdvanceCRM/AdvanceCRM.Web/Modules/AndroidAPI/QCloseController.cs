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
    public class QCloseController : ControllerBase
    {
        List<QCloseModel> booker;
        private readonly string _connectionString;

        public QCloseController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            booker = new List<QCloseModel>();
        }

        [HttpGet]
        public IEnumerable<QCloseModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Quotation where Status=2";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QCloseModel
                        {
                            CloseQuotation = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }

        [HttpGet("{id}")]
        public IEnumerable<QCloseModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Quotation where Status=2 and AssignedId=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QCloseModel
                        {
                            CloseQuotation = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }
    }
}