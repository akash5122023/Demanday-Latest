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


namespace AdvanceCRM.Modules.AndroidAPI
{
    public class CompanyController : ApiController
    {
        List<CompanyModel> Company;
        public CompanyController()
        {
            Company = new List<CompanyModel>();
        }
        public IEnumerable<CompanyModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,Name,Phone,EnquiryPrefix,EnquirySuffix,QuotationSuffix,QuotationPrefix,YearInPrefix,CmSprefix,CmsSuffix from CompanyDetails";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic phone, epre, enesuf, quopre, quosuf,year,cmssuf,cmspre;//obj == DBNull.Value
                if (dr["EnquiryPrefix"] == DBNull.Value)
                    epre = "";
                else
                    epre = (string)dr["EnquiryPrefix"];

                if (dr["YearInPrefix"] == DBNull.Value)
                    year = 0;
                else
                    year = (int)dr["YearInPrefix"];

                if (dr["EnquirySuffix"] == DBNull.Value)
                    enesuf = "";
                else
                    enesuf = (string)dr["EnquirySuffix"];
                if (dr["QuotationPrefix"] == DBNull.Value)
                    quopre = "";
                else
                    quopre = (string)dr["QuotationPrefix"];
                if (dr["QuotationSuffix"] == DBNull.Value)
                    quosuf = "";
                else
                    quosuf = (string)dr["QuotationSuffix"];

                if (dr["CmSprefix"] == DBNull.Value)
                    cmspre = "";
                else
                    cmspre = (string)dr["CmSprefix"];
                if (dr["CmsSuffix"] == DBNull.Value)
                    cmssuf = "";
                else
                    cmssuf = (string)dr["CmsSuffix"];
                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];
                Company.Add(new CompanyModel
                {
                    id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Phone = phone,
                    EnquiryPrefix = epre,
                    EnquirySuffix = enesuf,
                    QuotationPrefix = quopre,
                    QuotationSuffix = quosuf,
                    Yearinprefix=year,
                    CMSPrefix=cmspre,
                    CMSSuffix=cmssuf
                });
            }
            return Company;
        }

       // public IEnumerable<CompanyModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
             public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,Name,Phone,EnquiryPrefix,EnquirySuffix,QuotationSuffix,QuotationPrefix,YearInPrefix,CmSprefix,CmsSuffix from CompanyDetails where Id=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int i = ds.Tables[0].Rows.Count;
            if (i > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic phone,epre,enesuf,quopre,quosuf,year, cmssuf, cmspre; ;//obj == DBNull.Value
                    if (dr["EnquiryPrefix"] == DBNull.Value)
                        epre = "";
                    else
                        epre = (string)dr["EnquiryPrefix"];

                    if (dr["YearInPrefix"] == DBNull.Value)
                        year = 0;
                    else
                        year = (int)dr["YearInPrefix"];

                    if (dr["EnquirySuffix"] == DBNull.Value)
                        enesuf = "";
                    else
                        enesuf = (string)dr["EnquirySuffix"];
                    if (dr["QuotationPrefix"] == DBNull.Value)
                        quopre = "";
                    else
                        quopre = (string)dr["QuotationPrefix"];
                    if (dr["QuotationSuffix"] == DBNull.Value)
                        quosuf = "";
                    else
                        quosuf = (string)dr["QuotationSuffix"];
                    if (dr["CmSprefix"] == DBNull.Value)
                        cmspre = "";
                    else
                        cmspre = (string)dr["CmSprefix"];
                    if (dr["CmsSuffix"] == DBNull.Value)
                        cmssuf = "";
                    else
                        cmssuf = (string)dr["CmsSuffix"];
                    if (dr["Phone"] == DBNull.Value)
                        phone = "";
                    else
                        phone = (string)dr["Phone"];
                    Company.Add(new CompanyModel
                    {
                        id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Phone = phone,
                        EnquiryPrefix =epre,
                        EnquirySuffix = enesuf,
                        QuotationPrefix =quopre,
                        QuotationSuffix = quosuf,
                        Yearinprefix = year,
                        CMSPrefix = cmspre,
                        CMSSuffix = cmssuf
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
            var items = Company.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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