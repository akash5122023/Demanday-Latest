using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly List<AttendModel> _attend;

        public AttendController(IConfiguration config)
        {
            _connectionString = Startup.connectionString;
            _attend = new List<AttendModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Attendance";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int recordCount = ds.Tables[0].Rows.Count;

                    if (recordCount > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            _attend.Add(new AttendModel
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = Convert.ToInt32(dr["Name"]),
                                DateNTime = Convert.ToDateTime(dr["DateNTime"]),
                                Type = Convert.ToInt32(dr["Type"]),
                                Location = dr["Location"] == DBNull.Value ? string.Empty : dr["Location"].ToString(),
                                PunchIn = Convert.ToDateTime(dr["PunchIn"]),
                                PunchOut = Convert.ToDateTime(dr["PunchOut"]),
                                Distance = Convert.ToDouble(dr["Distance"])
                            });
                        }
                    }

                    // Paging logic
                    int currentPage = pagingparametermodel.pageNumber;
                    int pageSize = pagingparametermodel.pageSize;
                    int totalCount = recordCount;
                    int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                    var items = _attend
                        .Skip((currentPage - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    var previousPage = currentPage > 1 ? "Yes" : "No";
                    var nextPage = currentPage < totalPages ? "Yes" : "No";

                    var paginationMetadata = new
                    {
                        totalCount = totalCount,
                        pageSize = pageSize,
                        currentPage = currentPage,
                        totalPages = totalPages,
                        previousPage = previousPage,
                        nextPage = nextPage
                    };

                    Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

                    return Ok(items);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }
    }


}