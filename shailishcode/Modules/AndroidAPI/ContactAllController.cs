using System;
using System.Collections.Generic;
using System.Web.Http;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class ContactAllController : ApiController
    {
        List<ContactModel> Contact;
        public ContactAllController()
        {
            Contact = new List<ContactModel>();
        }
        public IEnumerable<ContactModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,Name,Contacttype,Phone,Email,Address from Contacts ORDER BY Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
           // int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic phone, name, contype, mail, address;//obj == DBNull.Value
                if (dr["Name"] == DBNull.Value)
                    name = "";
                else
                    name = (string)dr["Name"];

                if (dr["Contacttype"] == DBNull.Value)
                    contype = 0;
                else
                    contype = (int)dr["Contacttype"];

                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];
                if (dr["Email"] == DBNull.Value)
                    mail = "";
                else
                    mail = (string)dr["Email"];
                if (dr["Address"] == DBNull.Value)
                    address = "";
                else
                    address = (string)dr["Address"];
                Contact.Add(new ContactModel
                {
                    id = (int)dr["Id"],
                    Name = name,
                    ContactType = contype,
                    Phone = phone,
                    MailId = mail,
                    Address = address
                });
            }           
            return Contact;
        }

        public IEnumerable<ContactModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,Name,Contacttype,Phone,Email,Address from Contacts where Id=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic phone, name, contype, mail, address;//obj == DBNull.Value
                if (dr["Name"] == DBNull.Value)
                    name = "";
                else
                    name = (string)dr["Name"];

                if (dr["Contacttype"] == DBNull.Value)
                    contype = 0;
                else
                    contype = (int)dr["Contacttype"];

                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];
                if (dr["Email"] == DBNull.Value)
                    mail = "";
                else
                    mail = (string)dr["Email"];
                if (dr["Address"] == DBNull.Value)
                    address = "";
                else
                    address = (string)dr["Address"];
                Contact.Add(new ContactModel
                {
                    id = (int)dr["Id"],
                    Name = name,
                    ContactType = contype,
                    Phone = phone,
                    MailId = mail,
                    Address = address
                });
            }
             
            return Contact;
        }
    }
}