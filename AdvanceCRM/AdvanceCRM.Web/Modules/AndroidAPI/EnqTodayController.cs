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
    public class EnqTodayController : ControllerBase
    {
        List<EnquiryModel> Contact;
        private readonly string _connectionString;

        public EnqTodayController(IConfiguration config)
        {
            Contact = new List<EnquiryModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "SELECT en.Id, cn.Name, cn.Phone, en.Date, Status, sr.Source, st.Stage, us.DisplayName, en.AssignedId FROM Enquiry en, Source sr, Stage st, Contacts cn, Users us WHERE us.UserId = en.OwnerId AND en.ContactsId = cn.Id AND st.Id = en.stageId AND sr.Id = en.SourceId AND en.Date >= '" + dt + "' ORDER BY en.Id DESC";

                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Contact.Add(new EnquiryModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"]),
                        phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                        Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                        source = dr["Source"] == DBNull.Value ? "" : dr["Source"].ToString(),
                        stage = dr["Stage"] == DBNull.Value ? "" : dr["Stage"].ToString(),
                        Owner = dr["DisplayName"] == DBNull.Value ? "" : dr["DisplayName"].ToString(),
                        Assignedid = dr["AssignedId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AssignedId"])
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

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string dt1 = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000";
                // Fixed SQL injection vulnerability - parameterized query for id and dates
                string str = "SELECT en.Id, cn.Name, cn.Phone, cn.Address, en.Date, Status, sr.Source, st.Stage, us.DisplayName, en.AssignedId, en.EnquiryN, en.EnquiryNo FROM Enquiry en, Source sr, Stage st, Contacts cn, Users us WHERE us.UserId = en.OwnerId AND en.ContactsId = cn.Id AND st.Id = en.stageId AND sr.Id = en.SourceId AND en.AssignedId = @AssignedId AND en.Date >= @StartDate AND en.Date <= @EndDate ORDER BY en.Id DESC";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@AssignedId", id);
                cmd.Parameters.AddWithValue("@StartDate", DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@EndDate", DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000");

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic assign, asignid, address;

                    if (dr["AssignedId"] == DBNull.Value)
                        asignid = 0;
                    else
                        asignid = Convert.ToInt32(dr["AssignedId"]);

                    if (dr["Address"] == DBNull.Value)
                        address = "";
                    else
                        address = dr["Address"].ToString();

                    if (asignid > 0)
                    {
                        // Fixed SQL injection vulnerability in subquery
                        string strp = "SELECT DisplayName FROM Users WHERE UserId = @UserId";
                        SqlCommand cmd1 = new SqlCommand(strp, con);
                        cmd1.Parameters.AddWithValue("@UserId", asignid);

                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        sda1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            assign = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        }
                        else
                        {
                            assign = "";
                        }
                    }
                    else
                    {
                        assign = "";
                    }

                    Contact.Add(new EnquiryModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        Address = address,
                        Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"]),
                        Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                        phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                        source = dr["Source"] == DBNull.Value ? "" : dr["Source"].ToString(),
                        stage = dr["Stage"] == DBNull.Value ? "" : dr["Stage"].ToString(),
                        Owner = dr["DisplayName"] == DBNull.Value ? "" : dr["DisplayName"].ToString(),
                        Assignedid = asignid,
                        Assign = assign,
                        EnquiryN = dr["EnquiryN"] == DBNull.Value ? "" : dr["EnquiryN"].ToString(),
                        EnquiryNo = dr["EnquiryNo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EnquiryNo"])
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
}