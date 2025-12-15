using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        List<CompanyModel> Company;
        private readonly string _connectionString;

        public CompanyController(IConfiguration config)
        {
            Company = new List<CompanyModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "SELECT Id,Name,Phone,EnquiryPrefix,EnquirySuffix,QuotationSuffix,QuotationPrefix,YearInPrefix,CmSprefix,CmsSuffix FROM CompanyDetails";
                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                Company.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic phone, epre, enesuf, quopre, quosuf, year, cmssuf, cmspre;

                    if (dr["EnquiryPrefix"] == DBNull.Value)
                        epre = "";
                    else
                        epre = dr["EnquiryPrefix"].ToString();

                    if (dr["YearInPrefix"] == DBNull.Value)
                        year = 0;
                    else
                        year = Convert.ToInt32(dr["YearInPrefix"]);

                    if (dr["EnquirySuffix"] == DBNull.Value)
                        enesuf = "";
                    else
                        enesuf = dr["EnquirySuffix"].ToString();

                    if (dr["QuotationPrefix"] == DBNull.Value)
                        quopre = "";
                    else
                        quopre = dr["QuotationPrefix"].ToString();

                    if (dr["QuotationSuffix"] == DBNull.Value)
                        quosuf = "";
                    else
                        quosuf = dr["QuotationSuffix"].ToString();

                    if (dr["CmSprefix"] == DBNull.Value)
                        cmspre = "";
                    else
                        cmspre = dr["CmSprefix"].ToString();

                    if (dr["CmsSuffix"] == DBNull.Value)
                        cmssuf = "";
                    else
                        cmssuf = dr["CmsSuffix"].ToString();

                    if (dr["Phone"] == DBNull.Value)
                        phone = "";
                    else
                        phone = dr["Phone"].ToString();

                    Company.Add(new CompanyModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Phone = phone,
                        EnquiryPrefix = epre,
                        EnquirySuffix = enesuf,
                        QuotationPrefix = quopre,
                        QuotationSuffix = quosuf,
                        Yearinprefix = year,
                        CMSPrefix = cmspre,
                        CMSSuffix = cmssuf
                    });
                }

                return Ok(Company);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Fixed SQL injection vulnerability
                string str = "SELECT Id,Name,Phone,EnquiryPrefix,EnquirySuffix,QuotationSuffix,QuotationPrefix,YearInPrefix,CmSprefix,CmsSuffix FROM CompanyDetails WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int i = ds.Tables[0].Rows.Count;

                Company.Clear();

                if (i > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic phone, epre, enesuf, quopre, quosuf, year, cmssuf, cmspre;

                        if (dr["EnquiryPrefix"] == DBNull.Value)
                            epre = "";
                        else
                            epre = dr["EnquiryPrefix"].ToString();

                        if (dr["YearInPrefix"] == DBNull.Value)
                            year = 0;
                        else
                            year = Convert.ToInt32(dr["YearInPrefix"]);

                        if (dr["EnquirySuffix"] == DBNull.Value)
                            enesuf = "";
                        else
                            enesuf = dr["EnquirySuffix"].ToString();

                        if (dr["QuotationPrefix"] == DBNull.Value)
                            quopre = "";
                        else
                            quopre = dr["QuotationPrefix"].ToString();

                        if (dr["QuotationSuffix"] == DBNull.Value)
                            quosuf = "";
                        else
                            quosuf = dr["QuotationSuffix"].ToString();

                        if (dr["CmSprefix"] == DBNull.Value)
                            cmspre = "";
                        else
                            cmspre = dr["CmSprefix"].ToString();

                        if (dr["CmsSuffix"] == DBNull.Value)
                            cmssuf = "";
                        else
                            cmssuf = dr["CmsSuffix"].ToString();

                        if (dr["Phone"] == DBNull.Value)
                            phone = "";
                        else
                            phone = dr["Phone"].ToString();

                        Company.Add(new CompanyModel
                        {
                            id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                            Phone = phone,
                            EnquiryPrefix = epre,
                            EnquirySuffix = enesuf,
                            QuotationPrefix = quopre,
                            QuotationSuffix = quosuf,
                            Yearinprefix = year,
                            CMSPrefix = cmspre,
                            CMSSuffix = cmssuf
                        });
                    }
                }

                int CurrentPage = pagingparametermodel.pageNumber;
                int PageSize = pagingparametermodel.pageSize;
                int TotalCount = i;
                int TotalPages = (int)Math.Ceiling(i / (double)PageSize);
                var items = Company.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                var previousPage = CurrentPage > 1 ? "Yes" : "No";
                var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                var paginationMetadata = new
                {
                    totalCount = TotalCount,
                    pageSize = PageSize,
                    currentPage = CurrentPage,
                    totalPages = TotalPages,
                    previousPage,
                    nextPage
                };

                // Fixed: Use Append() instead of Add() for headers in .NET Core
                Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                return Ok(items);
            }
        }
    }
}