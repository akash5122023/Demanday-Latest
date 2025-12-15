using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly string _connectionString;

        public ContactController(IConfiguration config)
        {
            _connectionString = Startup.connectionString;
        }

        // ------------------------------------------------------------
        // GET ALL CONTACTS
        // URL: /api/contact?pageNumber=1&pageSize=20
        // ------------------------------------------------------------
        [HttpGet]
        public IActionResult GetAll([FromQuery] EnquiryNModel paging)
        {
            var contacts = new List<ContactModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT Id, Name, Contacttype, Phone, Email, Address
                                 FROM Contacts
                                 ORDER BY Id DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    contacts.Add(new ContactModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString(),
                        ContactType = dr["Contacttype"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Contacttype"]),
                        Phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                        MailId = dr["Email"] == DBNull.Value ? "" : dr["Email"].ToString(),
                        Address = dr["Address"] == DBNull.Value ? "" : dr["Address"].ToString()
                    });
                }
            }

            return BuildPagedResponse(contacts, paging);
        }

        // ------------------------------------------------------------
        // GET CONTACT BY USER ID
        // URL: /api/contact/{userId}?pageNumber=1&pageSize=20
        // ------------------------------------------------------------
        [HttpGet("{id}")]
        public IActionResult GetByUser(string id, [FromQuery] EnquiryNModel paging)
        {
            var contacts = new List<ContactModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT Id, Name, Contacttype, Phone, Email, Address 
                                 FROM Contacts 
                                 WHERE OwnerID = @id OR AssignedId = @id
                                 ORDER BY Id DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    contacts.Add(new ContactModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString(),
                        ContactType = dr["Contacttype"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Contacttype"]),
                        Phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                        MailId = dr["Email"] == DBNull.Value ? "" : dr["Email"].ToString(),
                        Address = dr["Address"] == DBNull.Value ? "" : dr["Address"].ToString()
                    });
                }
            }

            return BuildPagedResponse(contacts, paging);
        }

        // ------------------------------------------------------------
        // PAGINATION BUILDER
        // ------------------------------------------------------------
        private IActionResult BuildPagedResponse(List<ContactModel> data, EnquiryNModel paging)
        {
            int currentPage = paging.pageNumber;
            int pageSize = paging.pageSize;

            int totalCount = data.Count;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = data.Skip((currentPage - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

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
