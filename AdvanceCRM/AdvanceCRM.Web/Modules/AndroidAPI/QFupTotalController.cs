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
    public class QFupTotalController : ControllerBase
    {
        List<QFupTotalModel> booker;
        private readonly string _connectionString;

        public QFupTotalController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            booker = new List<QFupTotalModel>();
        }

        [HttpGet]
        public IEnumerable<QFupTotalModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "Select  Count(Id) as no from QuotationFollowUps whereFollowupDate >= '" + dt + "'";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QFupTotalModel
                        {
                            TotalQuotationFollowup = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }

        [HttpGet("{id}")]
        public IEnumerable<QFupTotalModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "Select  Count(Id) as no from QuotationFollowUps where FollowupDate >= '" + dt + "' and RepresentativeId=" + id;

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new QFupTotalModel
                        {
                            TotalQuotationFollowup = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }
    }
}