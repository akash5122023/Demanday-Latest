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

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class WebsiteController : ApiController
    {
        List<WebsiteModel> visit;
        public WebsiteController()
        {
            visit = new List<WebsiteModel>();
        }
       // public IEnumerable<WebsiteModel> Get([FromUri] EnquiryNModel pagingparametermodel)
                        public HttpResponseMessage Get([FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select Id,Name,Phone,Email,Address,Requirement,DateTime,IsMoved,Feedback from WebsiteEnquiryDetails ORDER BY Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic mob, mail, address, req, feedback;

                if (dr["Phone"] == DBNull.Value)  mob = "";  else  mob = (string)dr["Phone"];

                if (dr["Email"] == DBNull.Value) mail = "";  else  mail = (string)dr["Email"];

                if (dr["Address"] == DBNull.Value)  address = "";  else address = (string)dr["Address"];
                             
                if (dr["Requirements"] == DBNull.Value) req = "";  else  req = (string)dr["Requirements"];

                if (dr["Feedback"] == DBNull.Value) feedback = ""; else  feedback = (string)dr["Feedback"];

                visit.Add(new WebsiteModel
                { Id = (int)dr["Id"],                    
                    Name = (string)dr["Name"],
                    Address = address,
                    Email = mail,
                    Phone = mob,
                    Requirement = req,
                    DateTime = (DateTime)dr["DateTime"],
                    IsMoved = (bool)dr["IsMoved"], 
                    Feedback = feedback
                  
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
        //public IEnumerable<WebsiteModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
                        public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select Id,Name,Phone,Email,Address,Requirement,DateTime,IsMoved,Feedback from WebsiteEnquiryDetails where Id=" + id + " ORDER BY Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic mob, mail, address, req, feedback;

                if (dr["Phone"] == DBNull.Value) mob = ""; else mob = (string)dr["Phone"];

                if (dr["Email"] == DBNull.Value) mail = ""; else mail = (string)dr["Email"];

                if (dr["Address"] == DBNull.Value) address = ""; else address = (string)dr["Address"];

                if (dr["Requirements"] == DBNull.Value) req = ""; else req = (string)dr["Requirements"];

                if (dr["Feedback"] == DBNull.Value) feedback = ""; else feedback = (string)dr["Feedback"];

                visit.Add(new WebsiteModel
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Address = address,
                    Email = mail,
                    Phone = mob,
                    Requirement = req,
                    DateTime = (DateTime)dr["DateTime"],
                    IsMoved = (bool)dr["IsMoved"],
                    Feedback = feedback

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