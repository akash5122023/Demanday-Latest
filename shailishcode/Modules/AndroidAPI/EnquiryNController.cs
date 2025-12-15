using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using Newtonsoft.Json;

using System.Web;


namespace AdvanceCRM.Modules.AndroidAPI
{
    public class EnquiryNController : ApiController
    {
        List<EnquiryModel> Contact;
        public EnquiryNController()
        {
            Contact = new List<EnquiryModel>();
        }
       // public IEnumerable<EnquiryModel> Get([FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)

        {

            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select en.Id,cn.Name,cn.Phone,cn.Address,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic typ, brnh, subcontact;//obj == DBNull.Value
                if (dr["Address"] == DBNull.Value)
                    typ = "null";
                else
                    typ = (string)dr["Address"];

                Contact.Add(new EnquiryModel
                {
                    id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Date = (DateTime)dr["Date"],
                    phone = (string)dr["Phone"],
                    Address = typ,
                    Status = (int)dr["Status"],
                    source = (string)dr["Source"],
                    stage = (string)dr["Stage"],
                    Owner = (string)dr["DisplayName"],
                    Assignedid = (int)dr["AssignedId"],
                    EnquiryN = (string)dr["EnquiryN"],
                    EnquiryNo = (int)dr["EnquiryNo"]
                }); ;
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

            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

            // Returing List of Customers Collections  
            return response;

            //return Contact.Skip((paginator.current_page - 1) * paginator.per_page).Take(paginator.per_page).ToArray();
            //return JavaScriptSerializer.Deserialize<dynamic>(Contact).Skip((paginator.current_page - 1) * paginator.per_page).Take(paginator.per_page).ToArray();
        }
       // public IEnumerable<EnquiryModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.OwnerId =" + id+ " OR en.AssignedId="+id + " ORDER BY en.Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Contact.Add(new EnquiryModel
                {

                    id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Date = (DateTime)dr["Date"],
                    Status = (int)dr["Status"],
                    phone = (string)dr["Phone"],
                    source = (string)dr["Source"],
                    stage = (string)dr["Stage"],
                    Owner = (string)dr["DisplayName"],
                    Assignedid = (int)dr["AssignedId"],
                    EnquiryN = (string)dr["EnquiryN"],
                    EnquiryNo = (int)dr["EnquiryNo"]
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
            var abc= JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }

        [HttpPost]
        public string Enquiry(int ContactId, int Source, int status, int Stage, int Owner, int Assign, string Description, int EnquiryNo, String EnquiryN)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //  try
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            {
                var datetime = DateTime.Now.ToString("yyyy-MM-dd");
                string str = "Insert into Enquiry([ContactsId],[Date],[Status],[SourceId],[StageId],[OwnerId],[AssignedId],[AdditionalInfo],[EnquiryNo],[EnquiryN],[CompanyId]) values " +
                    "('" + ContactId + "','" + datetime + "','" + status + "','" + Source + "','" + Stage + "','" + Owner + "','" + Assign + "','" + Description + "','" + EnquiryNo + "','" + EnquiryN + "','1')";
               
                SqlCommand com = new SqlCommand(str, con);
                com.ExecuteNonQuery();
            }
           

            return "Enquiry added Successfully";
        }



    }
}