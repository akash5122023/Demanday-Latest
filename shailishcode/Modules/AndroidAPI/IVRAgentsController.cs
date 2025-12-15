using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Web;
using System.IO;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class IVRAgentsController : ApiController
    {
        List<IVRAgentsModel> attend;
        public IVRAgentsController()
        {
            attend = new List<IVRAgentsModel>();
        }
       // public IEnumerable<IVRAgentsModel> Get([FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select * from KnowlarityAgents where KnowlarityId=1";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);            
            int i = ds.Tables[0].Rows.Count;
            if ( i > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    attend.Add(new IVRAgentsModel
                    {
                        Id = (int)dr["Id"],
                        IVRId=(int)dr["KnowlarityId"],
                        Name = (string)dr["Name"],
                        Number = (string)dr["Number"]
                       
                       
                    });
                }
            }// Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = i;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(i / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = attend.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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