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
    public class EnqDueController : ControllerBase
    {
        List<EnquiryModel> Contact;
        private readonly string _connectionString;

        public EnqDueController(IConfiguration config)
        {
            Contact = new List<EnquiryModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "SELECT en.Id, cn.Name, cn.Phone, en.Date, Status, sr.Source, st.Stage, us.DisplayName, en.AssignedId FROM Enquiry en, Source sr, Stage st, Contacts cn, Users us WHERE us.UserId = en.OwnerId AND en.ContactsId = cn.Id AND st.Id = en.stageId AND sr.Id = en.SourceId AND en.Date < '" + dt + "' ORDER BY en.Id DESC";

                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

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

                return Ok(Contact);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                // Fixed SQL injection vulnerability - parameterized query for both id and date
                string str = "SELECT en.Id, cn.Name, en.Date, cn.Phone, Status, sr.Source, st.Stage, us.DisplayName, en.AssignedId FROM Enquiry en, Source sr, Stage st, Contacts cn, Users us WHERE us.UserId = en.OwnerId AND en.ContactsId = cn.Id AND st.Id = en.stageId AND sr.Id = en.SourceId AND en.AssignedId = @AssignedId AND en.Date < @Date ORDER BY en.Id DESC";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@AssignedId", id);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int i = ds.Tables[0].Rows.Count;

                Contact.Clear();

                if (i > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic assign, asignid;

                        if (dr["AssignedId"] == DBNull.Value)
                            asignid = 0;
                        else
                            asignid = Convert.ToInt32(dr["AssignedId"]);

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
                            Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString(),
                            Date = Convert.ToDateTime(dr["Date"]),
                            Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                            phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                            source = dr["Source"] == DBNull.Value ? "" : dr["Source"].ToString(),
                            stage = dr["Stage"] == DBNull.Value ? "" : dr["Stage"].ToString(),
                            Owner = dr["DisplayName"] == DBNull.Value ? "" : dr["DisplayName"].ToString(),
                            Assign = assign,
                            Assignedid = asignid
                        });
                    }
                }

                int CurrentPage = pagingparametermodel.pageNumber;
                int PageSize = pagingparametermodel.pageSize;
                int TotalCount = i;
                int TotalPages = (int)Math.Ceiling(i / (double)PageSize);
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