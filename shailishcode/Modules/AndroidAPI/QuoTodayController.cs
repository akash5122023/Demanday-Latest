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
    public class QuoTodayController : ApiController
    {
        List<QuotationModel> Contact;
        public QuoTodayController()
        {
            Contact = new List<QuotationModel>();
        }
        //public IEnumerable<QuotationModel> Get([FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
          
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId from Quotation en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.Date >= '" + dt + "' ORDER BY en.Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Contact.Add(new QuotationModel
                {
                    id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Date = (DateTime)dr["Date"],
                    Phone = (string)dr["Phone"],
                    Status = (int)dr["Status"],
                    source= (string)dr["Source"],
                    stage= (string)dr["Stage"],
                    Owner= (string)dr["DisplayName"],
                    Assignedid= (int)dr["AssignedId"]
                }) ;
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
        //public IEnumerable<QuotationModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select en.Id,cn.Name,en.Date,cn.Phone,Status,sr.Source,en.QuotationN,en.QuotationNo,st.Stage,us.DisplayName,en.AssignedId from Quotation en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.OwnerId=" + id+ " and en.Date >= '" + dt + "' ORDER BY en.Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic assign, asignid;
                if (dr["AssignedId"] == DBNull.Value)
                    asignid = 0;
                else
                    asignid = (int)dr["AssignedId"];
                if (asignid > 0)
                {
                    string strp = "Select DisplayName from Users where UserId=" + asignid;
                    SqlDataAdapter sda1 = new SqlDataAdapter(strp, con);
                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1);
                    assign = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else
                {
                    assign = "";
                }
                Contact.Add(new QuotationModel
                {
                    id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Date = (DateTime)dr["Date"],
                    Status = (int)dr["Status"],
                    Phone = (string)dr["Phone"],
                    source = (string)dr["Source"],
                    stage = (string)dr["Stage"],
                    Owner = (string)dr["DisplayName"],
                    Assignedid = (int)dr["AssignedId"],
                    Assign=assign,
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

            // Setting Header  
            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            var abc = JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }

        
    }
}