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
    public class PunchInController : ControllerBase
    {
        List<PunchInModel> PunchIn;
        private readonly string _connectionString;

        public PunchInController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            PunchIn = new List<PunchInModel>();
        }

        [HttpGet("{id}")]
        public IEnumerable<PunchInModel> Get(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                string str = "Select PunchIn from Attendance where DateNTime='" + dt + "' and Name=" + id;

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int i = ds.Tables[0].Rows.Count;

                    if (i == 1)
                    {
                        string st = "Select PunchOut from Attendance where DateNTime='" + dt + "' and Name=" + id;

                        using (var sda1 = new SqlDataAdapter(st, con))
                        {
                            DataSet ds1 = new DataSet();
                            sda1.Fill(ds1);
                            int j = ds1.Tables[0].Rows.Count;

                            if (j == 1)
                            {
                                if (ds.Tables[0].Rows[0].ItemArray[0].ToString() == ds1.Tables[0].Rows[0].ItemArray[0].ToString())
                                {
                                    PunchIn.Add(new PunchInModel
                                    {
                                        PunchIn = "Yes",
                                        PunchOut = "No"
                                    });
                                }
                                else
                                {
                                    PunchIn.Add(new PunchInModel
                                    {
                                        PunchIn = "Yes",
                                        PunchOut = "Yes"
                                    });
                                }
                            }
                            else
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    PunchIn.Add(new PunchInModel
                                    {
                                        PunchIn = "Yes",
                                        PunchOut = "Yes"
                                    });
                                }
                            }
                        }
                    }
                    else if (i == 0)
                    {
                        DataTable dt1 = new DataTable("MyTable");
                        dt1.Columns.Add(new DataColumn("id", typeof(int)));
                        DataRow dr1 = dt1.NewRow();
                        dr1["id"] = 123;
                        dt1.Rows.Add(dr1);

                        foreach (DataRow dr in dt1.Rows)
                        {
                            PunchIn.Add(new PunchInModel
                            {
                                PunchIn = "No",
                                PunchOut = "No"
                            });
                        }
                    }
                }
            }
            return PunchIn;
        }
    }
}