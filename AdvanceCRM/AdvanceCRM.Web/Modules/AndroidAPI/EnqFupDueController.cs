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
    public class EnqFupDueController : ControllerBase
    {
        List<EnquiryFollowupModel> Contact;
        private readonly string _connectionString;

        public EnqFupDueController(IConfiguration config)
        {
            Contact = new List<EnquiryFollowupModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "SELECT en.Id, en.FollowUpDate, Status, en.FollowUpNote, en.Details, us.DisplayName FROM EnquiryFollowUps en, Users us WHERE en.RepresentativeId = us.UserId AND en.FollowupDate < '" + dt + "'";

                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Contact.Add(new EnquiryFollowupModel
                    {
                        id = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]),
                        Date = dr["FollowUpDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FollowUpDate"]),
                        Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                        Note = dr["FollowUpNote"] == DBNull.Value ? "" : dr["FollowUpNote"].ToString(),
                        Details = dr["Details"] == DBNull.Value ? "" : dr["Details"].ToString(),
                        Owner = dr["DisplayName"] == DBNull.Value ? "" : dr["DisplayName"].ToString()
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
                // Fixed SQL injection vulnerability - parameterized query for both id and date
                string str = "SELECT cn.Name, cn.Phone, enq.EnquiryN, enq.EnquiryNo, enq.StageID, enq.SourceID, sr.Source, st.Stage, en.Id, en.EnquiryId, en.FollowUpDate, en.Status, en.FollowUpNote, en.Details, us.DisplayName FROM EnquiryFollowUps en, Enquiry enq, Source sr, Stage st, Users us, Contacts cn WHERE en.RepresentativeId = us.UserId AND enq.ContactsId = cn.Id AND en.EnquiryId = enq.Id AND st.Id = enq.stageId AND sr.Id = enq.SourceId AND en.FollowupDate < @Date AND en.RepresentativeId = @RepresentativeId ORDER BY en.Id DESC";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@RepresentativeId", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Contact.Add(new EnquiryFollowupModel
                    {
                        id = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]),
                        StatgeID = dr["StageID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StageID"]),
                        SourceID = dr["SourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SourceID"]),
                        Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString(),
                        stage = dr["Stage"] == DBNull.Value ? "" : dr["Stage"].ToString(),
                        source = dr["Source"] == DBNull.Value ? "" : dr["Source"].ToString(),
                        EnquiryID = dr["EnquiryId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EnquiryId"]),
                        Phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                        Date = dr["FollowUpDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FollowUpDate"]),
                        Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                        Note = dr["FollowUpNote"] == DBNull.Value ? "" : dr["FollowUpNote"].ToString(),
                        Details = dr["Details"] == DBNull.Value ? "" : dr["Details"].ToString(),
                        Owner = dr["DisplayName"] == DBNull.Value ? "" : dr["DisplayName"].ToString(),
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