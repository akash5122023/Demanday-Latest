using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdvanceCRM.Modules.AndroidAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacebookController : ControllerBase
    {
        private readonly string _connectionString;

        public FacebookController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            try
            {
                var visit = new List<FacebookModel>();

                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    string query = "SELECT Id,Name,Phone,Email,Address,CompaignName,AdSetName,CreatedTime,LeadId,Campaignid,Company,AdId,AdName,AdSetId,AdditionalDetails,IsMoved,Feedback FROM FacebookDetails ORDER BY Id DESC";

                    using (var cmd = new SqlCommand(query, con))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var model = new FacebookModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "" : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "" : reader.GetString(reader.GetOrdinal("Address")),
                                CompaignName = reader.IsDBNull(reader.GetOrdinal("CompaignName")) ? "" : reader.GetString(reader.GetOrdinal("CompaignName")),
                                AdSetName = reader.IsDBNull(reader.GetOrdinal("AdSetName")) ? "" : reader.GetString(reader.GetOrdinal("AdSetName")),
                                LeadId = reader.IsDBNull(reader.GetOrdinal("LeadId")) ? "" : reader.GetString(reader.GetOrdinal("LeadId")),
                                Campaignid = reader.IsDBNull(reader.GetOrdinal("Campaignid")) ? "" : reader.GetString(reader.GetOrdinal("Campaignid")),
                                Company = reader.IsDBNull(reader.GetOrdinal("Company")) ? "" : reader.GetString(reader.GetOrdinal("Company")),
                                AdId = reader.IsDBNull(reader.GetOrdinal("AdId")) ? "" : reader.GetString(reader.GetOrdinal("AdId")),
                                AdName = reader.IsDBNull(reader.GetOrdinal("AdName")) ? "" : reader.GetString(reader.GetOrdinal("AdName")),
                                AdSetId = reader.IsDBNull(reader.GetOrdinal("AdSetId")) ? "" : reader.GetString(reader.GetOrdinal("AdSetId")),
                                AdditionalDetails = reader.IsDBNull(reader.GetOrdinal("AdditionalDetails")) ? "" : reader.GetString(reader.GetOrdinal("AdditionalDetails")),
                                IsMoved = reader.GetBoolean(reader.GetOrdinal("IsMoved")),
                                Feedback = reader.IsDBNull(reader.GetOrdinal("Feedback")) ? "" : reader.GetString(reader.GetOrdinal("Feedback"))
                            };

                            int createdTimeOrdinal = reader.GetOrdinal("CreatedTime");
                            if (!reader.IsDBNull(createdTimeOrdinal))
                            {
                                model.CreatedTime = reader.GetDateTime(createdTimeOrdinal);
                            }

                            visit.Add(model);
                        }
                    }
                }

                int currentPage = pagingparametermodel.pageNumber;
                int pageSize = pagingparametermodel.pageSize;
                int totalCount = visit.Count;
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                var items = visit
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var paginationMetadata = new
                {
                    totalCount,
                    pageSize,
                    currentPage,
                    totalPages,
                    previousPage = currentPage > 1 ? "Yes" : "No",
                    nextPage = currentPage < totalPages ? "Yes" : "No"
                };

                Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            try
            {
                if (!int.TryParse(id, out int facebookId))
                {
                    return BadRequest("Invalid ID format");
                }

                var visit = new List<FacebookModel>();

                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    string query = "SELECT Id,Name,Phone,Email,Address,CompaignName,AdSetName,CreatedTime,LeadId,Campaignid,Company,AdId,AdName,AdSetId,AdditionalDetails,IsMoved,Feedback FROM FacebookDetails WHERE Id = @Id ORDER BY Id DESC";

                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", facebookId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var model = new FacebookModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "" : reader.GetString(reader.GetOrdinal("Phone")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "" : reader.GetString(reader.GetOrdinal("Address")),
                                    CompaignName = reader.IsDBNull(reader.GetOrdinal("CompaignName")) ? "" : reader.GetString(reader.GetOrdinal("CompaignName")),
                                    AdSetName = reader.IsDBNull(reader.GetOrdinal("AdSetName")) ? "" : reader.GetString(reader.GetOrdinal("AdSetName")),
                                    LeadId = reader.IsDBNull(reader.GetOrdinal("LeadId")) ? "" : reader.GetString(reader.GetOrdinal("LeadId")),
                                    Campaignid = reader.IsDBNull(reader.GetOrdinal("Campaignid")) ? "" : reader.GetString(reader.GetOrdinal("Campaignid")),
                                    Company = reader.IsDBNull(reader.GetOrdinal("Company")) ? "" : reader.GetString(reader.GetOrdinal("Company")),
                                    AdId = reader.IsDBNull(reader.GetOrdinal("AdId")) ? "" : reader.GetString(reader.GetOrdinal("AdId")),
                                    AdName = reader.IsDBNull(reader.GetOrdinal("AdName")) ? "" : reader.GetString(reader.GetOrdinal("AdName")),
                                    AdSetId = reader.IsDBNull(reader.GetOrdinal("AdSetId")) ? "" : reader.GetString(reader.GetOrdinal("AdSetId")),
                                    AdditionalDetails = reader.IsDBNull(reader.GetOrdinal("AdditionalDetails")) ? "" : reader.GetString(reader.GetOrdinal("AdditionalDetails")),
                                    IsMoved = reader.GetBoolean(reader.GetOrdinal("IsMoved")),
                                    Feedback = reader.IsDBNull(reader.GetOrdinal("Feedback")) ? "" : reader.GetString(reader.GetOrdinal("Feedback"))
                                };

                                int createdTimeOrdinal = reader.GetOrdinal("CreatedTime");
                                if (!reader.IsDBNull(createdTimeOrdinal))
                                {
                                    model.CreatedTime = reader.GetDateTime(createdTimeOrdinal);
                                }

                                visit.Add(model);
                            }
                        }
                    }
                }

                // Note: For single record by ID, paging might not be necessary
                // But keeping it as per your original design
                int currentPage = pagingparametermodel.pageNumber;
                int pageSize = pagingparametermodel.pageSize;
                int totalCount = visit.Count;
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                var items = visit
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var paginationMetadata = new
                {
                    totalCount,
                    pageSize,
                    currentPage,
                    totalPages,
                    previousPage = currentPage > 1 ? "Yes" : "No",
                    nextPage = currentPage < totalPages ? "Yes" : "No"
                };

                Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}