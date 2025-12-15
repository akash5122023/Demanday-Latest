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
using System.Web;
using Newtonsoft.Json;
using System.IO;
////using System.Web.Script.Serialization;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class EnquiryController : ApiController
    {
        List<EnquiryModel> Contact;
        public EnquiryController()
        {
            Contact = new List<EnquiryModel>();
        }
        public IEnumerable<EnquiryModel> Get([FromBody] Paginator filter)

        {
            var paginator = new Paginator(filter.per_page, filter.current_page);
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                    Contact.Add(new EnquiryModel
                    {
                        id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Date = (DateTime)dr["Date"],
                        phone = (string)dr["Phone"],
                        // Address=(string)dr["Address"],
                        Status = (int)dr["Status"],
                        source = (string)dr["Source"],
                        stage = (string)dr["Stage"],
                        Owner = (string)dr["DisplayName"],
                        Assignedid = (int)dr["AssignedId"],
                        EnquiryN = (string)dr["EnquiryN"],
                        EnquiryNo = (int)dr["EnquiryNo"]
                    }) ;
            }
            return Contact.Skip((paginator.current_page - 1) * paginator.per_page).Take(paginator.per_page).ToArray();
           //return JavaScriptSerializer.Deserialize<dynamic>(Contact).Skip((paginator.current_page - 1) * paginator.per_page).Take(paginator.per_page).ToArray();
        }
        //public IEnumerable<EnquiryModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            
            //con.Open();
            string str = "Select en.Id,cn.Name,cn.Phone,cn.Address,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.AssignedId =" + id + " ORDER BY en.Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int i = ds.Tables[0].Rows.Count;
            if (i > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic assign, asignid,Adress;
                    if (dr["AssignedId"] == DBNull.Value)
                        asignid = 0;
                    else
                        asignid = (int)dr["AssignedId"];
                    if (dr["Address"] == DBNull.Value)
                        Adress = "";
                    else
                        Adress = (string)dr["Address"];
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
                    Contact.Add(new EnquiryModel
                    {
                        id = (int)dr["Id"],
                        Address=Adress,
                        Name = (string)dr["Name"],
                        Date = (DateTime)dr["Date"],
                        Status = (int)dr["Status"],
                        phone = (string)dr["Phone"],
                        source = (string)dr["Source"],
                        stage = (string)dr["Stage"],
                        Owner = (string)dr["DisplayName"],
                        Assignedid = (int)dr["AssignedId"],
                        EnquiryN = (string)dr["EnquiryN"],
                        EnquiryNo = (int)dr["EnquiryNo"],
                        Assign=assign
                    });
                }
            }
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = i;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(i / (double)PageSize);

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