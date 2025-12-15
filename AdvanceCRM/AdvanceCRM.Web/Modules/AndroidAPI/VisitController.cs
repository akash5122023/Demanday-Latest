using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly string _connectionString;

        public VisitController(IConfiguration configuration)
        {
            //_connectionString = configuration.GetConnectionString("DefaultConnection");
            _connectionString = Startup.connectionString;

        }

        // --------------------- GET ALL VISITS -----------------------
        [HttpGet]
        public IActionResult GetAll([FromQuery] EnquiryNModel pagingparametermodel)
        {
            List<VisitModel> visit = new List<VisitModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT Id,CompanyName,Name,Address,Email,MobileNo,
                                 Location,DateNTime,Requirements,purpose 
                                 FROM Visit 
                                 ORDER BY Id DESC";

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    visit.Add(new VisitModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CompanyName = Convert.ToString(dr["CompanyName"]),
                        ContactPerson = Convert.ToString(dr["Name"]),
                        CompanyAddress = Convert.ToString(dr["Address"]),
                        EmailId = Convert.ToString(dr["Email"]),
                        MobileNumber = Convert.ToString(dr["MobileNo"]),
                        Location = Convert.ToString(dr["Location"]),
                        VisitDate = Convert.ToDateTime(dr["DateNTime"]),
                        Reason = Convert.ToString(dr["Requirements"]),
                        Purpose = Convert.ToString(dr["purpose"])
                    });
                }
            }

            return BuildPagedResponse(visit, pagingparametermodel);
        }

        // --------------------- GET VISITS BY USER -----------------------
        [HttpGet("{id}")]
        public IActionResult GetByUser(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            List<VisitModel> visit = new List<VisitModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT Id,CompanyName,Name,Address,Email,MobileNo,
                                 Location,DateNTime,Requirements,Feedback,purpose
                                 FROM Visit 
                                 WHERE CreatedBy = @id
                                 ORDER BY Id DESC";

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@id", id);

                DataSet ds = new DataSet();
                sda.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    visit.Add(new VisitModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CompanyName = Convert.ToString(dr["CompanyName"]),
                        ContactPerson = Convert.ToString(dr["Name"]),
                        CompanyAddress = dr["Address"] == DBNull.Value ? "" : dr["Address"].ToString(),
                        EmailId = dr["Email"] == DBNull.Value ? "" : dr["Email"].ToString(),
                        MobileNumber = dr["MobileNo"] == DBNull.Value ? "" : dr["MobileNo"].ToString(),
                        Location = dr["Location"] == DBNull.Value ? "" : dr["Location"].ToString(),
                        VisitDate = Convert.ToDateTime(dr["DateNTime"]),
                        Reason = dr["Requirements"] == DBNull.Value ? "" : dr["Requirements"].ToString(),
                        Purpose = dr["purpose"] == DBNull.Value ? "" : dr["purpose"].ToString(),
                        Feedback = dr["Feedback"] == DBNull.Value ? "" : dr["Feedback"].ToString()
                    });
                }
            }

            return BuildPagedResponse(visit, pagingparametermodel);
        }

        // --------------------- PAGING RESPONSE BUILDER -----------------------
        private IActionResult BuildPagedResponse(List<VisitModel> data, EnquiryNModel paging)
        {
            int currentPage = paging.pageNumber;
            int pageSize = paging.pageSize;

            int totalCount = data.Count;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var metadata = new
            {
                totalCount,
                pageSize,
                currentPage,
                totalPages,
                previousPage = currentPage > 1 ? "Yes" : "No",
                nextPage = currentPage < totalPages ? "Yes" : "No"
            };

            Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(metadata));
            return Ok(items);
        }
    }
}