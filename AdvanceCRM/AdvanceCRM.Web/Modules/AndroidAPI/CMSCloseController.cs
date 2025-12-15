using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class CMSCloseController : ControllerBase
    {
        private readonly string _connectionString;

        public CMSCloseController(IConfiguration config)
        {
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var booker = new List<CMSCloseModel>();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "SELECT COUNT(Id) as no FROM CMS WHERE Status = 2";

                    using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                booker.Add(new CMSCloseModel
                                {
                                    ClosedCMS = Convert.ToInt32(dr["no"])
                                });
                            }
                        }
                    }
                }

                return Ok(booker);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var booker = new List<CMSCloseModel>();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Parameterized query to prevent SQL injection
                    string query = "SELECT COUNT(Id) as no FROM CMS WHERE Status = 2 AND AssignedTo = @AssignedTo";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AssignedTo", id);

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            sda.Fill(ds);

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    booker.Add(new CMSCloseModel
                                    {
                                        ClosedCMS = Convert.ToInt32(dr["no"])
                                    });
                                }
                            }
                        }
                    }
                }

                return Ok(booker);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}