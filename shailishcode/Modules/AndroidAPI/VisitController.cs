using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdvanceCRM.Modules.AndroidAPI;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace AdvanceCRM.Modules
{
    public class VisitController : ApiController
    {
        List<VisitModel> visit;
        public VisitController()
        {
            visit = new List<VisitModel>();
        }
       // public IEnumerable<VisitModel> Get([FromUri] EnquiryNModel pagingparametermodel)
                        public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,CompanyName,Name,Address,Email,MobileNo,Location,DateNTime,Requirements,purpose from Visit ORDER BY Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                visit.Add(new VisitModel
                { Id = (int)dr["Id"],
                    CompanyName = (string)dr["CompanyName"],
                    ContactPerson = (string)dr["Name"],
                    CompanyAddress = (string)dr["Address"],
                    EmailId = (string)dr["Email"],
                    MobileNumber = (string)dr["MobileNo"],
                    Location = (string)dr["Location"],
                    VisitDate = (DateTime)dr["DateNTime"],
                    Reason = (string)dr["Requirements"], Purpose = (string)dr["purpose"]
                  //  FileName = (string)dr["Attachments"]
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
            var items = visit.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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
        // GET api/<controller>/5
       // public IEnumerable<VisitModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
                        public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,CompanyName,Name,Address,Email,MobileNo,Location,DateNTime,Requirements,Feedback,purpose from Visit where CreatedBy=" + id+ " ORDER BY Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic mob,mail,address,location,req,Purpose,feedback;

                if (dr["MobileNo"] == DBNull.Value)
                    mob = "";
                else
                    mob = (string)dr["MobileNo"];
                if (dr["Address"] == DBNull.Value)
                    address = "";
                else
                    address = (string)dr["Address"];

                if (dr["Email"] == DBNull.Value)
                    mail = "";
                else
                    mail = (string)dr["Email"];
                if (dr["Location"] == DBNull.Value)
                    location = "";
                else
                    location = (string)dr["Location"];
                if (dr["Requirements"] == DBNull.Value)
                    req = "";
                else
                    req = (string)dr["Requirements"];

                if (dr["purpose"] == DBNull.Value)
                    Purpose = "";
                else
                    Purpose = (string)dr["purpose"];
                if (dr["Feedback"] == DBNull.Value)
                    feedback = "";
                else
                    feedback = (string)dr["Feedback"];

                visit.Add(new VisitModel { Id = (int)dr["Id"],
                    CompanyName = (string)dr["CompanyName"],
                    ContactPerson = (string)dr["Name"],
                    CompanyAddress = address,
                    EmailId = mail,
                    MobileNumber = mob,
                    Location = location, 
                    VisitDate = (DateTime)dr["DateNTime"], 
                    Reason = req, 
                    Purpose = Purpose,
                    Feedback= feedback
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
            var items = visit.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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