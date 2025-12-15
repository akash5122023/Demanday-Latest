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
    public class QFupOpenController : ControllerBase
    {
        List<QFupOpenModel> booker;
        private readonly string _connectionString;

        public QFupOpenController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            booker = new List<QFupOpenModel>();
        }

        [HttpGet]
        public IEnumerable<QFupOpenModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "Select  Count(Id) as no from QuotationFollowUps where Status=1 and FollowupDate >= '" + dt + "'";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QFupOpenModel
                        {
                            OpenQuotationFollowup = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }

        [HttpGet("{id}")]
        public IEnumerable<QFupOpenModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "Select  Count(Id) as no from QuotationFollowUps where Status=1 and FollowupDate >= '" + dt + "' and RepresentativeId=" + id;

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QFupOpenModel
                        {
                            OpenQuotationFollowup = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }
    }
}