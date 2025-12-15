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
    public class ContactAllController : ControllerBase
    {
        List<ContactModel> Contact;
        private readonly string _connectionString;

        public ContactAllController(IConfiguration config)
        {
            Contact = new List<ContactModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "SELECT Id, Name, Contacttype, Phone, Email, Address FROM Contacts ORDER BY Id DESC";
                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic phone, name, contype, mail, address;

                    if (dr["Name"] == DBNull.Value)
                        name = "";
                    else
                        name = dr["Name"].ToString();

                    if (dr["Contacttype"] == DBNull.Value)
                        contype = 0;
                    else
                        contype = Convert.ToInt32(dr["Contacttype"]);

                    if (dr["Phone"] == DBNull.Value)
                        phone = "";
                    else
                        phone = dr["Phone"].ToString();

                    if (dr["Email"] == DBNull.Value)
                        mail = "";
                    else
                        mail = dr["Email"].ToString();

                    if (dr["Address"] == DBNull.Value)
                        address = "";
                    else
                        address = dr["Address"].ToString();

                    Contact.Add(new ContactModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        Name = name,
                        ContactType = contype,
                        Phone = phone,
                        MailId = mail,
                        Address = address
                    });
                }

                return Ok(Contact);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Fixed SQL injection vulnerability
                string str = "SELECT Id, Name, Contacttype, Phone, Email, Address FROM Contacts WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic phone, name, contype, mail, address;

                    if (dr["Name"] == DBNull.Value)
                        name = "";
                    else
                        name = dr["Name"].ToString();

                    if (dr["Contacttype"] == DBNull.Value)
                        contype = 0;
                    else
                        contype = Convert.ToInt32(dr["Contacttype"]);

                    if (dr["Phone"] == DBNull.Value)
                        phone = "";
                    else
                        phone = dr["Phone"].ToString();

                    if (dr["Email"] == DBNull.Value)
                        mail = "";
                    else
                        mail = dr["Email"].ToString();

                    if (dr["Address"] == DBNull.Value)
                        address = "";
                    else
                        address = dr["Address"].ToString();

                    Contact.Add(new ContactModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        Name = name,
                        ContactType = contype,
                        Phone = phone,
                        MailId = mail,
                        Address = address
                    });
                }

                return Ok(Contact);
            }
        }
    }
}