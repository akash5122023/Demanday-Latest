using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class IVRAgentsController : ControllerBase
    {
        List<IVRAgentsModel> attend;
        private readonly string _connectionString;

        public IVRAgentsController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            attend = new List<IVRAgentsModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select * from KnowlarityAgents where KnowlarityId=1";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int i = ds.Tables[0].Rows.Count;

                    if (i > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            attend.Add(new IVRAgentsModel
                            {
                                Id = (int)dr["Id"],
                                IVRId = (int)dr["KnowlarityId"],
                                Name = (string)dr["Name"],
                                Number = (string)dr["Number"]
                            });
                        }
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = i;
                    int TotalPages = (int)Math.Ceiling(i / (double)PageSize);

                    var items = attend.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                    var previousPage = CurrentPage > 1 ? "Yes" : "No";
                    var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                    var paginationMetadata = new
                    {
                        totalCount = TotalCount,
                        pageSize = PageSize,
                        currentPage = CurrentPage,
                        totalPages = TotalPages,
                        previousPage,
                        nextPage
                    };

                    Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    var abc = JsonConvert.SerializeObject(paginationMetadata);

                    return Ok(items);
                }
            }
        }
    }
}