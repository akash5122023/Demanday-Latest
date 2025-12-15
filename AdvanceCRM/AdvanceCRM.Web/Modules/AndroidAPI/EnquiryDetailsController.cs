using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnquiryDetailsController : ControllerBase
    {
        List<EnquiryModel> Contact;
        private readonly string _connectionString;

        public EnquiryDetailsController(IConfiguration config)
        {
            Contact = new List<EnquiryModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Fixed SQL injection vulnerability
                string str = "SELECT en.Id, en.ContactsId, en.AdditionalInfo, cn.Name, en.StageId, en.SourceId, en.OwnerId, en.AssignedId, cn.Phone, cn.Address, en.Date, Status, sr.Source, st.Stage, en.EnquiryN, en.EnquiryNo, us.DisplayName FROM Enquiry en, Source sr, Stage st, Contacts cn, Users us WHERE us.UserId = en.OwnerId AND en.ContactsId = cn.Id AND st.Id = en.stageId AND sr.Id = en.SourceId AND en.Id = @Id ORDER BY en.Id DESC";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int i = ds.Tables[0].Rows.Count;

                Contact.Clear();

                if (i > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic assign, asignid, address, additional;

                        if (dr["AssignedId"] == DBNull.Value)
                            asignid = 0;
                        else
                            asignid = Convert.ToInt32(dr["AssignedId"]);

                        if (dr["Address"] == DBNull.Value)
                            address = "";
                        else
                            address = dr["Address"].ToString();

                        if (dr["AdditionalInfo"] == DBNull.Value)
                            additional = "";
                        else
                            additional = dr["AdditionalInfo"].ToString();

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
                            ContactsId = dr["ContactsId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ContactsId"]),
                            AdditionalInfo = additional,
                            Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString(),
                            Date = dr["Date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["Date"]),
                            Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"]),
                            phone = dr["Phone"] == DBNull.Value ? "" : dr["Phone"].ToString(),
                            source = dr["Source"] == DBNull.Value ? "" : dr["Source"].ToString(),
                            stage = dr["Stage"] == DBNull.Value ? "" : dr["Stage"].ToString(),
                            Owner = dr["DisplayName"] == DBNull.Value ? "" : dr["DisplayName"].ToString(),
                            Assignedid = asignid,
                            EnquiryN = dr["EnquiryN"] == DBNull.Value ? "" : dr["EnquiryN"].ToString(),
                            EnquiryNo = dr["EnquiryNo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EnquiryNo"]),
                            Assign = assign,
                            Stageid = dr["StageId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StageId"]),
                            sourceid = dr["SourceId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SourceId"]),
                            Ownerid = dr["OwnerId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OwnerId"])
                        });
                    }
                }

                return Ok(Contact);
            }
        }
    }
}