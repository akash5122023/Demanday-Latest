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
    public class SourceController : ControllerBase
    {
        List<SourceModel> Source;
        private readonly string _connectionString;

        public SourceController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            Source = new List<SourceModel>();
        }

        [HttpGet]
        public IEnumerable<SourceModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select * from Source";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Source.Add(new SourceModel
                        {
                            id = (int)dr["Id"],
                            Source = (String)dr["Source"],
                        });
                    }
                }
            }
            return Source;
        }

        [HttpGet("{id}")]
        public IEnumerable<SourceModel> Get(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select * from Source where Id=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Source.Add(new SourceModel
                        {
                            id = (int)dr["Id"],
                            Source = (String)dr["Source"],
                        });
                    }
                }
            }
            return Source;
        }
    }
}