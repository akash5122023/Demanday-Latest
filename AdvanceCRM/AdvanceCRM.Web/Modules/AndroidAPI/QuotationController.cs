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
    public class QuotationController : ControllerBase
    {
        List<QuotationModel> Contact;
        private readonly string _connectionString;

        public QuotationController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            Contact = new List<QuotationModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select en.Id,cn.Name,cn.Phone,cn.Email,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId,en.QuotationN,en.QuotationNo from Quotation en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic assign, asignid;

                        if (dr["AssignedId"] == DBNull.Value)
                            asignid = 0;
                        else
                            asignid = (int)dr["AssignedId"];

                        if (asignid > 0)
                        {
                            string strp = "Select DisplayName from Users where UserId=" + asignid;

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                            con.Open();

                            using (var com = new SqlCommand(strp, con))
                            {
                                assign = Convert.ToString(com.ExecuteScalar());
                            }
                        }
                        else
                        {
                            assign = "";
                        }

                        string emailid = dr["Email"] == DBNull.Value ? "" : (string)dr["Email"];

                        Contact.Add(new QuotationModel
                        {
                            id = (int)dr["Id"],
                            Name = (string)dr["Name"],
                            Date = (DateTime)dr["Date"],
                            Status = (int)dr["Status"],
                            Phone = (string)dr["Phone"],
                            Email = emailid,
                            source = (string)dr["Source"],
                            stage = (string)dr["Stage"],
                            Owner = (string)dr["DisplayName"],
                            Assignedid = (int)dr["AssignedId"],
                            Assign = assign,
                            QuotationN = (string)dr["QuotationN"],
                            QuotationNo = (int)dr["QuotationNo"]
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
                string str = "Select en.Id,cn.Name,cn.Phone,cn.Email,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId,en.QuotationN,en.QuotationNo from Quotation en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.OwnerId=" + id + " ORDER BY en.Id DESC";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic assign, asignid;

                        if (dr["AssignedId"] == DBNull.Value)
                            asignid = 0;
                        else
                            asignid = (int)dr["AssignedId"];

                        if (asignid > 0)
                        {
                            string strp = "Select DisplayName from Users where UserId=" + asignid;

                            using (var sda1 = new SqlDataAdapter(strp, con))
                            {
                                DataSet ds1 = new DataSet();
                                sda1.Fill(ds1);
                                assign = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            }
                        }
                        else
                        {
                            assign = "";
                        }

                        string emailid = dr["Email"] == DBNull.Value ? "" : (string)dr["Email"];

                        Contact.Add(new QuotationModel
                        {
                            id = (int)dr["Id"],
                            Name = (string)dr["Name"],
                            Phone = (string)dr["Phone"],
                            Email = emailid,
                            Date = (DateTime)dr["Date"],
                            Status = (int)dr["Status"],
                            source = (string)dr["Source"],
                            stage = (string)dr["Stage"],
                            Owner = (string)dr["DisplayName"],
                            Assignedid = (int)dr["AssignedId"],
                            QuotationN = (string)dr["QuotationN"],
                            Assign = assign,
                            QuotationNo = (int)dr["QuotationNo"]
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