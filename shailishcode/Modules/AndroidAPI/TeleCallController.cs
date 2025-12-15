using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;
using System.Net;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class TeleCallController : ApiController
    {
        List<TeleCallModel> Contact;
        public TeleCallController()
        {
            Contact = new List<TeleCallModel>();
        }
        //public IEnumerable<TeleCallModel> Get([FromUri] EnquiryNModel pagingparametermodel)
                        public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select tl.Id,tl.CompanyName,tl.Name,tl.Phone,tl.Email,tl.Details,tl.CreatedBy,tl.AssignedTo,tl.IsMoved,us.DisplayName from RawTelecall tl,Users us where us.UserId=tl.AssignedTo ORDER BY tl.Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic Details, ComName, Name, Mail, phone;//obj == DBNull.Value
                if (dr["CompanyName"] == DBNull.Value)
                    ComName = "";
                else
                    ComName = (string)dr["CompanyName"];

                if (dr["Name"] == DBNull.Value)
                    Name = "";
                else
                    Name = (string)dr["Name"];

                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];

                if (dr["Email"] == DBNull.Value)
                    Mail = "";
                else
                    Mail = (string)dr["Email"];

                if (dr["Details"] == DBNull.Value)
                    Details = "";
                else
                    Details = (string)dr["Details"];

                Contact.Add(new TeleCallModel
                {
                    id = (int)dr["Id"],
                    CompanyName = ComName,
                    Name = Name,
                    Email = Mail,
                    Phone = phone,
                    Details = Details,
                   // created = (string)dr["DisplayName"],
                    asssign = (string)dr["DisplayName"],
                    AssignedTo = (int)dr["AssignedTo"],
                    CreatedBy = (int)dr["CreatedBy"],
                    IsMoved = (bool)dr["IsMoved"]
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
       // public IEnumerable<TeleCallModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
                        public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select tl.Id,tl.CompanyName,tl.Name,tl.Phone,tl.Email,tl.Details,tl.CreatedBy,tl.AssignedTo,tl.Feedback,tl.IsMoved,us.DisplayName from RawTelecall tl,Users us where us.UserId=tl.AssignedTo and tl.AssignedTo=" + id + " ORDER BY tl.Id DESC";
            
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
           DataSet ds = new DataSet();
           sda.Fill(ds);int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic Details, ComName,Name,Mail,phone, feedback;//obj == DBNull.Value
                if (dr["CompanyName"] == DBNull.Value)
                    ComName = "";
                else
                    ComName = (string)dr["CompanyName"];

                if (dr["Name"] == DBNull.Value)
                    Name = "";
                else
                    Name = (string)dr["Name"];

                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];

                if (dr["Email"] == DBNull.Value)
                    Mail = "";
                else
                    Mail = (string)dr["Email"];

                if (dr["Details"] == DBNull.Value)
                    Details = "";
                else
                    Details = (string)dr["Details"];
                if (dr["Feedback"] == DBNull.Value)
                    feedback = "";
                else
                    feedback = (string)dr["Feedback"];

                Contact.Add(new TeleCallModel
                {
                    id = (int) dr["Id"],
                    CompanyName = ComName,
                    Name =Name,
                    Email = Mail,
                    Phone = phone,
                    Feedback= feedback,
                    Details = Details,
                   // created = (string)dr["DisplayName"],
                    asssign = (string)dr["DisplayName"],
                    AssignedTo=(int)dr["AssignedTo"],
                    CreatedBy = (int)dr["CreatedBy"],
                    IsMoved =(bool)dr["IsMoved"]
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
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata)); // Returing List of Customers Collections  
            return response;
        }

        public IEnumerable<TeleCallModel> Get(int TeleCallId)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);

            string str = "Select tl.Id,tl.CompanyName,tl.Name,tl.Phone,tl.Email,tl.Details,tl.CreatedBy,tl.Feedback,tl.AssignedTo,tl.IsMoved,us.DisplayName from RawTelecall tl,Users us where us.UserId=tl.AssignedTo and tl.Id=" + TeleCallId + " ORDER BY tl.Id DESC";

            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds); int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic Details, ComName, Name, Mail, phone, feedback;//obj == DBNull.Value
                if (dr["CompanyName"] == DBNull.Value)
                    ComName = "";
                else
                    ComName = (string)dr["CompanyName"];

                if (dr["Name"] == DBNull.Value)
                    Name = "";
                else
                    Name = (string)dr["Name"];

                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];

                if (dr["Email"] == DBNull.Value)
                    Mail = "";
                else
                    Mail = (string)dr["Email"];

                if (dr["Details"] == DBNull.Value)
                    Details = "";
                else
                    Details = (string)dr["Details"];
                if (dr["Feedback"] == DBNull.Value)
                    feedback = "";
                else
                    feedback = (string)dr["Feedback"];


                Contact.Add(new TeleCallModel
                {
                    id = (int)dr["Id"],
                    CompanyName = ComName,
                    Name = Name,
                    Email = Mail,
                    Phone = phone,
                    Details = Details,
                    Feedback=feedback,
                    // created = (string)dr["DisplayName"],
                    asssign = (string)dr["DisplayName"],
                    AssignedTo = (int)dr["AssignedTo"],
                    CreatedBy = (int)dr["CreatedBy"],
                    IsMoved = (bool)dr["IsMoved"]
                });
            }
       
            return Contact;
        }
    }
}