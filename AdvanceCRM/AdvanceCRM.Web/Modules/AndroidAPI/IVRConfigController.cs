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
    public class IVRConfigController : ControllerBase
    {
        List<IVRConfigModel> attend;
        private readonly string _connectionString;

        public IVRConfigController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            attend = new List<IVRConfigModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select * from IVRConfiguration where Id=1";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int i = ds.Tables[0].Rows.Count;

                    if (i > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dynamic IVRNumber, Plan, ApiKey;
                            if (dr["IVRNumber"] == DBNull.Value)
                                IVRNumber = "";
                            else
                                IVRNumber = (string)dr["IVRNumber"];
                            if (dr["Plan"] == DBNull.Value)
                                Plan = "";
                            else
                                Plan = (string)dr["Plan"];
                            if (dr["ApiKey"] == DBNull.Value)
                                ApiKey = "";
                            else
                                ApiKey = (string)dr["ApiKey"];

                            attend.Add(new IVRConfigModel
                            {
                                Id = (int)dr["Id"],
                                IVRType = (int)dr["IVRType"],
                                IVRNumber = IVRNumber,
                                ApiKey = ApiKey,
                                Plan = Plan,
                                AppId = (string)dr["AppId"],
                                AppSecret = (string)dr["AppSecret"]
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