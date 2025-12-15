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
    public class VisitCController : ControllerBase
    {
        List<VisitCModel> booker;
        private readonly string _connectionString;

        public VisitCController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            booker = new List<VisitCModel>();
        }

        [HttpGet]
        public IEnumerable<VisitCModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Count(Id) as no from Visit";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new VisitCModel
                        {
                            TodaysVisit = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }

        [HttpGet("{id}")]
        public IEnumerable<VisitCModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-yy");
                string str = "Select Count(Id) as no from Visit where CreatedBy=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        booker.Add(new VisitCModel
                        {
                            TodaysVisit = (int)dr["no"],
                        });
                    }
                }
            }
            return booker;
        }
    }
}