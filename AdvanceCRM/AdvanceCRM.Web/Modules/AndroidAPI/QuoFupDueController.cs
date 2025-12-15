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
    public class QuoFupDueController : ControllerBase
    {
        List<QuotationFollowupModel> Contact;
        private readonly string _connectionString;

        public QuoFupDueController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            Contact = new List<QuotationFollowupModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "Select en.Id,en.FollowUpDate,Status,en.FollowUpNote,en.Details,us.DisplayName from QuotationFollowUps en,Users us where en.RepresentativeId=us.UserId and en.FollowupDate < '" + dt + "'";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Contact.Add(new QuotationFollowupModel
                        {
                            id = (int)dr["Id"],
                            Date = (DateTime)dr["FollowUpDate"],
                            Status = (int)dr["Status"],
                            Note = (string)dr["FollowUpNote"],
                            Details = (string)dr["Details"],
                            Owner = (string)dr["DisplayName"]
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
                string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                string str = "Select cn.Name,cn.Phone,enq.QuotationN,enq.QuotationNo,enq.StageID,enq.SourceID,sr.Source,st.Stage,en.Id,en.QuotationId,en.FollowUpDate,en.Status,en.FollowUpNote,en.Details,us.DisplayName from QuotationFollowUps en, Quotation enq,Source sr, Stage st,Users us, Contacts cn where en.RepresentativeId = us.UserId and enq.ContactsId = cn.Id and en.EnquiryId = enq.Id and st.Id = enq.stageId and sr.Id = enq.SourceId and en.FollowupDate < '" + dt + "' and en.RepresentativeId =" + id + " ORDER BY en.Id DESC";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Contact.Add(new QuotationFollowupModel
                        {
                            id = (int)dr["Id"],
                            StatgeID = (int)dr["StageID"],
                            SourceID = (int)dr["SourceID"],
                            Name = (string)dr["Name"],
                            stage = (string)dr["Stage"],
                            QuotationID = (int)dr["QuotationId"],
                            source = (string)dr["Source"],
                            Phone = (string)dr["Phone"],
                            Date = (DateTime)dr["FollowUpDate"],
                            Status = (int)dr["Status"],
                            Note = (string)dr["FollowUpNote"],
                            Details = (string)dr["Details"],
                            Owner = (string)dr["DisplayName"],
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
    }
}