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
    public class EnquiryNController : ControllerBase
    {
        List<EnquiryModel> Contact;
        private readonly string _connectionString;

        public EnquiryNController(IConfiguration config)
        {
            Contact = new List<EnquiryModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "SELECT en.Id, cn.Name, cn.Phone, cn.Address, en.Date, Status, sr.Source, st.Stage, en.EnquiryN, en.EnquiryNo, us.DisplayName, en.AssignedId FROM Enquiry en, Source sr, Stage st, Contacts cn, Users us WHERE us.UserId = en.OwnerId AND en.ContactsId = cn.Id AND st.Id = en.stageId AND sr.Id = en.SourceId";

                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic typ;

                    if (dr["Address"] == DBNull.Value)
                        typ = "null";
                    else
                        typ = dr["Address"].ToString();

                    Contact.Add(new EnquiryModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"]),
                        phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                        Address = typ,
                        Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                        source = dr["Source"] == DBNull.Value ? "" : dr["Source"].ToString(),
                        stage = dr["Stage"] == DBNull.Value ? "" : dr["Stage"].ToString(),
                        Owner = dr["DisplayName"] == DBNull.Value ? "" : dr["DisplayName"].ToString(),
                        Assignedid = dr["AssignedId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AssignedId"]),
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

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Fixed SQL injection vulnerability
                string str = "SELECT en.Id, cn.Name, cn.Phone, en.Date, Status, sr.Source, st.Stage, en.EnquiryN, en.EnquiryNo, us.DisplayName, en.AssignedId FROM Enquiry en, Source sr, Stage st, Contacts cn, Users us WHERE us.UserId = en.OwnerId AND en.ContactsId = cn.Id AND st.Id = en.stageId AND sr.Id = en.SourceId AND (en.OwnerId = @UserId OR en.AssignedId = @UserId) ORDER BY en.Id DESC";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@UserId", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
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
                        Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                        phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                        source = dr["Source"] == DBNull.Value ? "" : dr["Source"].ToString(),
                        stage = dr["Stage"] == DBNull.Value ? "" : dr["Stage"].ToString(),
                        Owner = dr["DisplayName"] == DBNull.Value ? "" : dr["DisplayName"].ToString(),
                        Assignedid = dr["AssignedId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AssignedId"]),
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

        [HttpPost]
        public IActionResult Enquiry([FromBody] EnquiryRequest request)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    // Fixed SQL injection vulnerability using parameterized query
                    string str = @"INSERT INTO Enquiry ([ContactsId], [Date], [Status], [SourceId], [StageId], [OwnerId], [AssignedId], [AdditionalInfo], [EnquiryNo], [EnquiryN], [CompanyId]) 
                                   VALUES (@ContactId, @Date, @Status, @Source, @Stage, @Owner, @Assign, @Description, @EnquiryNo, @EnquiryN, @CompanyId)";

                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.Parameters.AddWithValue("@ContactId", request.ContactId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Status", request.status);
                    cmd.Parameters.AddWithValue("@Source", request.Source);
                    cmd.Parameters.AddWithValue("@Stage", request.Stage);
                    cmd.Parameters.AddWithValue("@Owner", request.Owner);
                    cmd.Parameters.AddWithValue("@Assign", request.Assign);
                    cmd.Parameters.AddWithValue("@Description", request.Description ?? "");
                    cmd.Parameters.AddWithValue("@EnquiryNo", request.EnquiryNo);
                    cmd.Parameters.AddWithValue("@EnquiryN", request.EnquiryN ?? "");
                    cmd.Parameters.AddWithValue("@CompanyId", 1);

                    cmd.ExecuteNonQuery();
                }

                return Ok("Enquiry added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding enquiry: {ex.Message}");
            }
        }
    }

    // Request model for the Enquiry POST method
    public class EnquiryRequest
    {
        public int ContactId { get; set; }
        public int Source { get; set; }
        public int status { get; set; }
        public int Stage { get; set; }
        public int Owner { get; set; }
        public int Assign { get; set; }
        public string Description { get; set; }
        public int EnquiryNo { get; set; }
        public string EnquiryN { get; set; }
    }
}