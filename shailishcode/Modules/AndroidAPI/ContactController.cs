using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class ContactController : ApiController
    {
        List<ContactModel> Contact;
        public ContactController()
        {
            Contact = new List<ContactModel>();
        }

        //public IEnumerable<ContactModel> AllContact()
        //{
        //    SqlConnection con = new SqlConnection(Startup.connectionString);
        //    //if (con.State == ConnectionState.Open)
        //    //{
        //    //    con.Close();
        //    //}
        //    //con.Open();
        //    string str = "Select Id,Name,Contacttype,Phone,Email,Address from Contacts";
        //    SqlDataAdapter sda = new SqlDataAdapter(str, con);
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);
        //    int count = ds.Tables[0].Rows.Count;
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        dynamic phone, name, contype, mail, address;//obj == DBNull.Value
        //        if (dr["Name"] == DBNull.Value)
        //            name = "";
        //        else
        //            name = (string)dr["Name"];

        //        if (dr["Contacttype"] == DBNull.Value)
        //            contype = 0;
        //        else
        //            contype = (int)dr["Contacttype"];

        //        if (dr["Phone"] == DBNull.Value)
        //            phone = "";
        //        else
        //            phone = (string)dr["Phone"];
        //        if (dr["Email"] == DBNull.Value)
        //            mail = "";
        //        else
        //            mail = (string)dr["Email"];
        //        if (dr["Address"] == DBNull.Value)
        //            address = "";
        //        else
        //            address = (string)dr["Address"];

        //        Contact.Add(new ContactModel
        //        {
        //            id = (int)dr["Id"],
        //            Name = name,
        //            ContactType = contype,
        //            Phone = phone,
        //            MailId = mail,
        //            Address = address
        //        });
        //    }

            
        //    return Contact;
        //}
       // public IEnumerable<ContactModel> Get([FromUri] EnquiryNModel pagingparametermodel)
             public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,Name,Contacttype,Phone,Email,Address from Contacts";
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

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = Contact.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            var abc = JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }

        //public IEnumerable<ContactModel> Get( string id, [FromUri] EnquiryNModel pagingparametermodel
            public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,Name,Contacttype,Phone,Email,Address from Contacts where OwnerID=" + id + " OR AssignedId=" + id+ " ORDER BY Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic phone, name, contype,mail,address;//obj == DBNull.Value
                if (dr["Name"] == DBNull.Value)
                    name = "";
                else
                    name= (string)dr["Name"];

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
                    MailId=mail,
                    Address=address
                //   Address= !Contact.ContainsKey("Address") ? "" : Convert.ToString(Contact["Address"]).Replace("'", "");
                //Address = !contact.dr["Address"]?"":(string)dr["Address"],
                //  //  "" : Convert.ToString(IndiaMartResponsObject["RN"]).Replace("'", "");
                //MailId = (string)dr["Email"]
                   });
            }
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = Contact.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            var abc = JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }
    }
}