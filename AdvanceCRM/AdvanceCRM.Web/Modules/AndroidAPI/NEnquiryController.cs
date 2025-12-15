using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class NEnquiryController : ControllerBase
    {
        List<EnquiryModel> Contact;
        private readonly string _connectionString;

        public NEnquiryController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            Contact = new List<EnquiryModel>();
        }

        [HttpGet("{id}")]
        public IEnumerable<EnquiryModel> Get(string id, int LastID, int mode)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string st = string.Empty;
                if (mode == 0)
                {
                    st = "Select TOP 10 en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.Id<'" + LastID + "' and en.AssignedId ='" + id + "'";
                }
                else if (mode == 1)
                {
                    st = "Select TOP 10 en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.Id>'" + LastID + "' and en.AssignedId ='" + id + "'";
                }

                using (var sda = new SqlDataAdapter(st, con))
                {
                    DataSet dt = new DataSet();
                    sda.Fill(dt);
                    int no = dt.Tables[0].Rows.Count;
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Tables[0].Rows)
                        {
                            Contact.Add(new EnquiryModel
                            {
                                id = (int)dr["Id"],
                                Name = (string)dr["Name"],
                                Date = (DateTime)dr["Date"],
                                phone = (string)dr["Phone"],
                                Status = (int)dr["Status"],
                                source = (string)dr["Source"],
                                stage = (string)dr["Stage"],
                                Owner = (string)dr["DisplayName"],
                                Assignedid = (int)dr["AssignedId"],
                                EnquiryN = (string)dr["EnquiryN"],
                                EnquiryNo = (int)dr["EnquiryNo"]
                            });
                        }
                    }
                }
            }
            return Contact;
        }

        [HttpGet]
        public string Get()
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        internal class EnquiryDetail
        {
            public int id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public int Status { get; set; }
            public string stage { get; set; }
            public string phone { get; set; }
            public string Address { get; set; }
            public string source { get; set; }
            public string Owner { get; set; }
            public int EnquiryNo { get; set; }
            public string EnquiryN { get; set; }
            public int sourceid { get; set; }
            public int Ownerid { get; set; }
            public int Assignedid { get; set; }
        }
    }
}