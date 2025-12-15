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
    public class PunchOutController : ControllerBase
    {
        List<PunchInModel> PunchIn;
        private readonly string _connectionString;

        public PunchOutController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            PunchIn = new List<PunchInModel>();
        }

        [HttpGet("{id}")]
        public IEnumerable<PunchInModel> Get(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select PunchOut from Attendance where DateNTime='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and Name=" + id;

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int i = ds.Tables[0].Rows.Count;

                    if (i == 1)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PunchIn.Add(new PunchInModel
                            {
                                PunchOut = "Yes",
                                PunchIn = "No"
                            });
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PunchIn.Add(new PunchInModel
                            {
                                PunchOut = "No",
                                PunchIn = "No"
                            });
                        }
                    }
                }
            }
            return PunchIn;
        }
    }
}