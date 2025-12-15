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
    public class SubContactController : ControllerBase
    {
        List<SubContactModel> Contact;
        private readonly string _connectionString;

        public SubContactController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            Contact = new List<SubContactModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,Name,Contacttype,Phone,Email,Address from SubContacts";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic phone, name, contype, mail, address;

                        if (dr["Name"] == DBNull.Value)
                            name = "";
                        else
                            name = (string)dr["Name"];

                        if (dr["Phone"] == DBNull.Value)
                            phone = "";
                        else
                            phone = (string)dr["Phone"];
                        if (dr["Email"] == DBNull.Value)
                            mail = "";
                        else
                            mail = (string)dr["Email"];
                        if (dr["Address"] == DBNull.Value)
                            address = "";
                        else
                            address = (string)dr["Address"];

                        Contact.Add(new SubContactModel
                        {
                            id = (int)dr["Id"],
                            Name = name,
                            Phone = phone,
                            MailId = mail,
                            Address = address
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
                string str = "Select Id,Name,Phone,Email,Address from SubContacts where ContactsId=" + id;
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic phone, name, contype, mail, address;

                        if (dr["Name"] == DBNull.Value)
                            name = "";
                        else
                            name = (string)dr["Name"];

                        if (dr["Phone"] == DBNull.Value)
                            phone = "";
                        else
                            phone = (string)dr["Phone"];
                        if (dr["Email"] == DBNull.Value)
                            mail = "";
                        else
                            mail = (string)dr["Email"];
                        if (dr["Address"] == DBNull.Value)
                            address = "";
                        else
                            address = (string)dr["Address"];

                        Contact.Add(new SubContactModel
                        {
                            id = (int)dr["Id"],
                            Name = name,
                            Phone = phone,
                            MailId = mail,
                            Address = address
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
    }
}