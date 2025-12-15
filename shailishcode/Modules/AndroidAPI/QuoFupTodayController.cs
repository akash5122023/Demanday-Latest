using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class QuoFupTodayController : ApiController
    {
        List<QuotationFollowupModel> Contact;
        public QuoFupTodayController()
        {
            Contact = new List<QuotationFollowupModel>();
        }
        //public IEnumerable<QuotationFollowupModel> Get([FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get([FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
          
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select en.Id,en.FollowUpDate,Status,en.FollowUpNote,en.Details,us.DisplayName from QuotationFollowUps en,Users us where en.RepresentativeId=us.UserId and en.FollowupDate >= '" + dt + "'";
            //   string str = "Select en.Id,cn.Name,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId from Quotation en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
               
                    Contact.Add(new QuotationFollowupModel
                    {
                        id = (int)dr["Id"],
                        Date = (DateTime)dr["FollowUpDate"],
                        Status = (int)dr["Status"],
                        Note = (string)dr["FollowUpNote"],
                        Details = (string)dr["Details"],
                        Owner = (string)dr["DisplayName"]

                    }

                     ) ;
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
       // public IEnumerable<QuotationFollowupModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);

            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string dt1 = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000";
            string str = "Select cn.Name,cn.Phone,enq.QuotationN,enq.QuotationNo,enq.StageID,enq.SourceID,sr.Source,st.Stage,en.Id,en.FollowUpDate,en.Status,en.FollowUpNote,en.QuotationId,en.Details,us.DisplayName from QuotationFollowUps en,Quotation enq,Source sr,Stage st,Users us,Contacts cn where en.RepresentativeId=us.UserId and enq.ContactsId=cn.Id and en.QuotationId=enq.Id and st.Id=enq.stageId and sr.Id=enq.SourceId and en.FollowupDate >= '" + dt + "' and en.FollowupDate <= '" + dt1 + "' and en.RepresentativeId =" + id + " ORDER BY en.Id DESC";


            //string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            //string str = "Select en.Id,en.FollowUpDate,Status,en.FollowUpNote,en.Details,us.DisplayName from QuotationFollowUps en,Users us where en.RepresentativeId=us.UserId and en.FollowupDate >= '" + dt + "' and en.RepresentativeId =" + id + " ORDER BY en.Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Contact.Add(new QuotationFollowupModel
                {
                    id = (int)dr["Id"],
                    StatgeID = (int)dr["StageID"],
                    SourceID = (int)dr["SourceID"],
                    Name = (string)dr["Name"],
                    stage = (string)dr["Stage"],
                    QuotationID = (int)dr["QuotationId"],
                    source = (string)dr["Source"],
                    Phone = (string)dr["Phone"],
                    Date = (DateTime)dr["FollowUpDate"],
                    Status = (int)dr["Status"],
                    Note = (string)dr["FollowUpNote"],
                    Details = (string)dr["Details"],
                    Owner = (string)dr["DisplayName"],
                    QuotationN = (string)dr["QuotationN"],
                    QuotationNo = (int)dr["QuotationNo"]

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

            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            var abc = JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }
    }
}