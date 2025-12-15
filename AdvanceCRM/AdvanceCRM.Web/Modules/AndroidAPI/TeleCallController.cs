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
    public class TeleCallController : ControllerBase
    {
        List<TeleCallModel> Contact;
        private readonly string _connectionString;

        public TeleCallController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            Contact = new List<TeleCallModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select tl.Id,tl.CompanyName,tl.Name,tl.Phone,tl.Email,tl.Details,tl.CreatedBy,tl.AssignedTo,tl.IsMoved,us.DisplayName from RawTelecall tl,Users us where us.UserId=tl.AssignedTo ORDER BY tl.Id DESC";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic Details, ComName, Name, Mail, phone;

                        if (dr["CompanyName"] == DBNull.Value)
                            ComName = "";
                        else
                            ComName = (string)dr["CompanyName"];

                        if (dr["Name"] == DBNull.Value)
                            Name = "";
                        else
                            Name = (string)dr["Name"];

                        if (dr["Phone"] == DBNull.Value)
                            phone = "";
                        else
                            phone = (string)dr["Phone"];

                        if (dr["Email"] == DBNull.Value)
                            Mail = "";
                        else
                            Mail = (string)dr["Email"];

                        if (dr["Details"] == DBNull.Value)
                            Details = "";
                        else
                            Details = (string)dr["Details"];

                        Contact.Add(new TeleCallModel
                        {
                            id = (int)dr["Id"],
                            CompanyName = ComName,
                            Name = Name,
                            Email = Mail,
                            Phone = phone,
                            Details = Details,
                            asssign = (string)dr["DisplayName"],
                            AssignedTo = (int)dr["AssignedTo"],
                            CreatedBy = (int)dr["CreatedBy"],
                            IsMoved = (bool)dr["IsMoved"]
                        });
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = count;
                    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                    var items = Contact.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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
                string str = "Select tl.Id,tl.CompanyName,tl.Name,tl.Phone,tl.Email,tl.Details,tl.CreatedBy,tl.AssignedTo,tl.Feedback,tl.IsMoved,us.DisplayName from RawTelecall tl,Users us where us.UserId=tl.AssignedTo and tl.AssignedTo=" + id + " ORDER BY tl.Id DESC";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic Details, ComName, Name, Mail, phone, feedback;

                        if (dr["CompanyName"] == DBNull.Value)
                            ComName = "";
                        else
                            ComName = (string)dr["CompanyName"];

                        if (dr["Name"] == DBNull.Value)
                            Name = "";
                        else
                            Name = (string)dr["Name"];

                        if (dr["Phone"] == DBNull.Value)
                            phone = "";
                        else
                            phone = (string)dr["Phone"];

                        if (dr["Email"] == DBNull.Value)
                            Mail = "";
                        else
                            Mail = (string)dr["Email"];

                        if (dr["Details"] == DBNull.Value)
                            Details = "";
                        else
                            Details = (string)dr["Details"];
                        if (dr["Feedback"] == DBNull.Value)
                            feedback = "";
                        else
                            feedback = (string)dr["Feedback"];

                        Contact.Add(new TeleCallModel
                        {
                            id = (int)dr["Id"],
                            CompanyName = ComName,
                            Name = Name,
                            Email = Mail,
                            Phone = phone,
                            Feedback = feedback,
                            Details = Details,
                            asssign = (string)dr["DisplayName"],
                            AssignedTo = (int)dr["AssignedTo"],
                            CreatedBy = (int)dr["CreatedBy"],
                            IsMoved = (bool)dr["IsMoved"]
                        });
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = count;
                    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                    var items = Contact.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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

                    return Ok(items);
                }
            }
        }

        [HttpGet("byid/{TeleCallId}")]
        public IEnumerable<TeleCallModel> Get(int TeleCallId)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select tl.Id,tl.CompanyName,tl.Name,tl.Phone,tl.Email,tl.Details,tl.CreatedBy,tl.Feedback,tl.AssignedTo,tl.IsMoved,us.DisplayName from RawTelecall tl,Users us where us.UserId=tl.AssignedTo and tl.Id=" + TeleCallId + " ORDER BY tl.Id DESC";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic Details, ComName, Name, Mail, phone, feedback;

                        if (dr["CompanyName"] == DBNull.Value)
                            ComName = "";
                        else
                            ComName = (string)dr["CompanyName"];

                        if (dr["Name"] == DBNull.Value)
                            Name = "";
                        else
                            Name = (string)dr["Name"];

                        if (dr["Phone"] == DBNull.Value)
                            phone = "";
                        else
                            phone = (string)dr["Phone"];

                        if (dr["Email"] == DBNull.Value)
                            Mail = "";
                        else
                            Mail = (string)dr["Email"];

                        if (dr["Details"] == DBNull.Value)
                            Details = "";
                        else
                            Details = (string)dr["Details"];
                        if (dr["Feedback"] == DBNull.Value)
                            feedback = "";
                        else
                            feedback = (string)dr["Feedback"];

                        Contact.Add(new TeleCallModel
                        {
                            id = (int)dr["Id"],
                            CompanyName = ComName,
                            Name = Name,
                            Email = Mail,
                            Phone = phone,
                            Details = Details,
                            Feedback = feedback,
                            asssign = (string)dr["DisplayName"],
                            AssignedTo = (int)dr["AssignedTo"],
                            CreatedBy = (int)dr["CreatedBy"],
                            IsMoved = (bool)dr["IsMoved"]
                        });
                    }
                }
            }
            return Contact;
        }
    }
}