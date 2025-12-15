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
    public class EnquiryfollowupController : ApiController
    {
        List<EnquiryFollowupModel> Contact;
        public EnquiryfollowupController()
        {
            Contact = new List<EnquiryFollowupModel>();
        }
        //public IEnumerable<EnquiryFollowupModel> Get([FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select en.Id,en.FollowUpDate,Status,en.FollowUpNote,en.Details,us.DisplayName from EnquiryFollowUps en,Users uswhere en.RepresentativeId=us.UserId";
            //   string str = "Select en.Id,cn.Name,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
               
                    Contact.Add(new EnquiryFollowupModel
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
       // public IEnumerable<EnquiryFollowupModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
        //  string str = "Select en.Id,en.FollowUpDate,en.EnquiryId,Status,en.FollowUpNote,en.Details,us.DisplayName from EnquiryFollowUps en,Users us where en.RepresentativeId=us.UserId and  en.EnquiryId =" + id + " ORDER BY en.Id DESC";
            string str = "Select cn.Name,cn.Phone,enq.EnquiryN,enq.EnquiryNo,enq.StageID,enq.SourceID,sr.Source,st.Stage,en.Id,en.FollowUpDate,en.Status,en.FollowUpNote,en.EnquiryId,en.Details,us.DisplayName from EnquiryFollowUps en,Enquiry enq,Source sr,Stage st,Users us,Contacts cn where en.RepresentativeId=us.UserId and enq.ContactsId=cn.Id and en.EnquiryId=enq.Id and st.Id=enq.stageId and sr.Id=enq.SourceId and  en.EnquiryId =" + id + " ORDER BY en.Id DESC";

            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Contact.Add(new EnquiryFollowupModel
                {
                    id = (int)dr["Id"],
                    StatgeID = (int)dr["StageID"],
                    SourceID = (int)dr["SourceID"],
                    Name = (string)dr["Name"],
                    stage = (string)dr["Stage"],
                    EnquiryID = (int)dr["EnquiryId"],
                    source = (string)dr["Source"],
                    Phone = (string)dr["Phone"],
                    Date = (DateTime)dr["FollowUpDate"],
                    Status = (int)dr["Status"],
                    Note = (string)dr["FollowUpNote"],
                    Details = (string)dr["Details"],
                    Owner = (string)dr["DisplayName"],
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
            var abc = JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }
    }
}