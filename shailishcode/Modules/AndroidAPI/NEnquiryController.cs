using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace AdvanceCRM.Modules.AndroidAPI
{
    public class NEnquiryController : ApiController
    {
        List<EnquiryModel> Contact;
        public NEnquiryController()
        {
            Contact = new List<EnquiryModel>();
        }



        //public Object Get(int page = 0, int pageSize = 10)
        //{
        //    IQueryable<EnquiryModel> query;

        //    query = EnquiryRepository.().OrderBy(c => c.CourseSubject.Id);
        //    var totalCount = query.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        //    var urlHelper = new UrlHelper(Request);
        //    var prevLink = page > 0 ? urlHelper.Link("Courses", new { page = page - 1 }) : "";
        //    var nextLink = page < totalPages - 1 ? urlHelper.Link("Courses", new { page = page + 1 }) : "";

        //    var results = query
        //    .Skip(pageSize * page)
        //    .Take(pageSize)
        //    .ToList()
        //    .Select(s => TheModelFactory.Create(s));

        //    return new
        //    {
        //        TotalCount = totalCount,
        //        TotalPages = totalPages,
        //        PrevPageLink = prevLink,
        //        NextPageLink = nextLink,
        //        Results = results
        //    };

        //}

        // GET api/<controller>
        //public static async Task<DataTable> Get([FromUri] EnquiryNModel pagingparametermodel)
        //{
        //    //try
        //    {
        //        SqlConnection con = new SqlConnection(Startup.connectionString);
        //        string st = string.Empty;
        //        //if (mode == 0)
        //        //{
        //        //    st = "Select TOP 10 en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.Id<'"+ LastID + "'";
        //        //}
        //        //else if (mode == 1)
        //        {
        //            st = "Select TOP 10 en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId";
        //        }

        //        SqlDataAdapter da = new SqlDataAdapter(st,con);
        //        DataSet dt = new DataSet();
        //        da.Fill(dt);
        //        int count=dt.Tables[0].Rows.Count;

        //        List<string> rows = new List<string>();

        //        //foreach (DataRow dr in dt.Tables[0].Rows)
        //        //{
        //        //    row = new Dictionary<string, object>();
        //        //    foreach (DataColumn col in dt.Tables[0].Columns)
        //        //    {
        //        //        row.Add(col.ColumnName, dr[col]);
        //        //    }
        //        //    rows.Add(row);
        //        //}

        //        // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
        //        int CurrentPage = pagingparametermodel.pageNumber;

        //        // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
        //        int PageSize = pagingparametermodel.pageSize;

        //        // Display TotalCount to Records to User  
        //        int TotalCount = count;

        //        // Calculating Totalpage by Dividing (No of Records / Pagesize)  
        //        int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

        //        // Returns List of Customer after applying Paging   
        //        var items = st.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

        //        // if CurrentPage is greater than 1 means it has previousPage  
        //        var previousPage = CurrentPage > 1 ? "Yes" : "No";

        //        // if TotalPages is greater than CurrentPage means it has nextPage  
        //        var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

        //        // Object which we are going to send in header   
        //        var paginationMetadata = new
        //        {
        //            totalCount = TotalCount,
        //            pageSize = PageSize,
        //            currentPage = CurrentPage,
        //            totalPages = TotalPages,
        //            previousPage,
        //            nextPage
        //        };

        //        // Setting Header  
        //        HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

        //        var abc = JsonConvert.SerializeObject(paginationMetadata);
        //        // Returing List of Customers Collections  
        //        return items;

        //        //DataTable responseObj = new DataTable();
        //        //var abc = JsonConvert.SerializeObject(dt);
        //        ////JObject json = JObject.Parse(abc);
        //        ////foreach (var e in json)
        //        ////{
        //        ////    abcd = Convert.ToString(e);
        //        ////}

        //        //responseObj= JsonConvert.DeserializeObject<DataTable>(abc);
        //        //return responseObj;

        //        //int no = dt.Tables[0].Rows.Count;
        //        //if (dt.Tables[0].Rows.Count > 0)                   
        //        //{
        //        //    var abc = JsonConvert.SerializeObject(dt);
        //        //    var IndiaMartResponsObjects = JavaScriptSerializer.Deserialize<dynamic>(abc.);


        //        //    return JsonConvert.SerializeObject(dt);
        //        //}
        //        //else
        //        //{
        //        //    return "No Record Found";
        //        //}                

        //    }
        // //   return new string[] { "value1", "value2" };
        //}

        public IEnumerable<EnquiryModel> Get(string id, int LastID, int mode)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);

            string st = string.Empty;
            if (mode == 0)
            {
                st = "Select TOP 10 en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.Id<'" + LastID + "' and en.AssignedId ='" + id + "'";
            }
            else if (mode == 1)
            {
                st = "Select TOP 10 en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.Id>'" + LastID + "' and en.AssignedId ='" + id + "'";
            }

            // string str = "Select en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.OwnerId =" + id;
            SqlDataAdapter sda = new SqlDataAdapter(st, con);
            DataSet dt = new DataSet();
            sda.Fill(dt);
            int no = dt.Tables[0].Rows.Count;
            if (dt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Tables[0].Rows)
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
                    });
                }

            }
            return Contact;
        }

        //public string Get(int FirstID)
        //{
        //    //try
        //    {
        //        int currentIndex = 1;
        //        int pageSize = 20;
        //        //var paginator = new Paginator(filter.per_page, filter.current_page);
        //        SqlDataAdapter da = new SqlDataAdapter("Select TOP 10 en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.Id <" + FirstID, con);
        //        DataSet dt = new DataSet();
        //        da.Fill(dt, currentIndex, pageSize, "Enquiry");
        //        int no = dt.Tables[0].Rows.Count;
        //        if (dt.Tables[0].Rows.Count > 0)
        //        {
        //            //var obj= JsonConvert.SerializeObject(dt);
        //            //List<EnquiryDetail> Records = new List<EnquiryDetail>();
        //            //foreach (var objec in obj)
        //            //{
        //            //    EnquiryDetail EnquiryDetails = new EnquiryDetail();
        //            //    //EnquiryDetails.id = objec.id;
        //            //    //EnquiryDetails.Name = objec.name;
        //            //    //EnquiryDetails.Date = Fbdata[i].created_time;
        //            //    //EnquiryDetails.Status = Fbdata[i].ad_id;
        //            //    //EnquiryDetails.stage = Fbdata[i].ad_name;
        //            //    //EnquiryDetails.source = Fbdata[i].adset_id;
        //            //    //EnquiryDetails.EnquiryN = Fbdata[i].adset_name;
        //            //    //EnquiryDetails.EnquiryNo = Fbdata[i].id;
        //            //}
        //            //IPagedList<EnquiryDetail> Record = new StaticPagedList<EnquiryDetail>(Records, 1, 10, no);

        //            return JsonConvert.SerializeObject(dt.Tables[0]);
        //        }
        //        else
        //        {
        //            return "No data Found";
        //        }
        //    }
        //    //catch(Exception ex)
        //    //{

        //    //}

        //    // return new string[] { "value1", "value2" };
        //}

       // GET api/<controller>/5
        public string Get()
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        internal class EnquiryDetail
        {
            public int id { get; set; }
            //  public int ContactsId { get; set; }
            public string Name { get; set; }

            public DateTime Date { get; set; }

            public int Status { get; set; }

            // public int Stageid { get; set; }

            public string stage { get; set; }
            public string phone { get; set; }
            public string Address { get; set; }
            public string source { get; set; }
            public string Owner { get; set; }
            public int EnquiryNo { get; set; }
            public string EnquiryN { get; set; }
            public int sourceid { get; set; }
            public int Ownerid { get; set; }
            public int Assignedid { get; set; }
        }
    }

}