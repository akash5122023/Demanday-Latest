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
    public class WebsiteController : ControllerBase
    {
        List<WebsiteModel> visit;
        private readonly string _connectionString;

        public WebsiteController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            visit = new List<WebsiteModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,Name,Phone,Email,Address,Requirement,DateTime,IsMoved,Feedback from WebsiteEnquiryDetails ORDER BY Id DESC";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic mob, mail, address, req, feedback;

                        if (dr["Phone"] == DBNull.Value) mob = ""; else mob = (string)dr["Phone"];
                        if (dr["Email"] == DBNull.Value) mail = ""; else mail = (string)dr["Email"];
                        if (dr["Address"] == DBNull.Value) address = ""; else address = (string)dr["Address"];
                        if (dr["Requirements"] == DBNull.Value) req = ""; else req = (string)dr["Requirements"];
                        if (dr["Feedback"] == DBNull.Value) feedback = ""; else feedback = (string)dr["Feedback"];

                        visit.Add(new WebsiteModel
                        {
                            Id = (int)dr["Id"],
                            Name = (string)dr["Name"],
                            Address = address,
                            Email = mail,
                            Phone = mob,
                            Requirement = req,
                            DateTime = (DateTime)dr["DateTime"],
                            IsMoved = (bool)dr["IsMoved"],
                            Feedback = feedback
                        });
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = count;
                    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                    var items = visit.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,Name,Phone,Email,Address,Requirement,DateTime,IsMoved,Feedback from WebsiteEnquiryDetails where Id=" + id + " ORDER BY Id DESC";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic mob, mail, address, req, feedback;

                        if (dr["Phone"] == DBNull.Value) mob = ""; else mob = (string)dr["Phone"];
                        if (dr["Email"] == DBNull.Value) mail = ""; else mail = (string)dr["Email"];
                        if (dr["Address"] == DBNull.Value) address = ""; else address = (string)dr["Address"];
                        if (dr["Requirements"] == DBNull.Value) req = ""; else req = (string)dr["Requirements"];
                        if (dr["Feedback"] == DBNull.Value) feedback = ""; else feedback = (string)dr["Feedback"];

                        visit.Add(new WebsiteModel
                        {
                            Id = (int)dr["Id"],
                            Name = (string)dr["Name"],
                            Address = address,
                            Email = mail,
                            Phone = mob,
                            Requirement = req,
                            DateTime = (DateTime)dr["DateTime"],
                            IsMoved = (bool)dr["IsMoved"],
                            Feedback = feedback
                        });
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = count;
                    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                    var items = visit.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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