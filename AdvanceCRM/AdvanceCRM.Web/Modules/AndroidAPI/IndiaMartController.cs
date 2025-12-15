using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdvanceCRM.Modules.AndroidAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndiaMartController : ControllerBase
    {
        List<IndiaMartModel> visit;

        private readonly string _connectionString;

        public IndiaMartController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            visit = new List<IndiaMartModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,Rn,QueryId,QueryType,SenderName,SenderEmail,Subject,DateRe,DateR,DateTimeRe,GlUserCompanyName,ReadStatus,SenderGLUserId,Mob,CountryFlag,QueryModId,LogTime,QueryModRefId,DIRQueryModrefType,ORGSenderGLUserId,EnqMessage,EnqAddress,EnqCallDuration,EnqReceiverMob,EnqCity,EnqState,ProductName,CountryISO,EmailAlt,MobileAlt,Phone,PhoneAlt,ImmemberSince,TotalCnt,Source,IsMoved,Feedback from IndiaMartDetails ORDER BY Id DESC";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic Rn, QueryId, QueryType, SenderName, SenderEmail, Subject, DateR, GlUserCompanyName, ReadStatus, SenderGLUserId, Mob, CountryFlag, QueryModId, LogTime, QueryModRefId, DIRQueryModrefType, ORGSenderGLUserId, EnqMessage, EnqAddress, EnqCallDuration, EnqReceiverMob, EnqCity, EnqState, ProductName, CountryISO, EmailAlt, MobileAlt, Phone, PhoneAlt, ImmemberSince, TotalCnt, Source, Feedback;

                        if (dr["Rn"] == DBNull.Value) Rn = 0; else Rn = (int)dr["Rn"];
                        if (dr["QueryId"] == DBNull.Value) QueryId = ""; else QueryId = (string)dr["QueryId"];
                        if (dr["QueryType"] == DBNull.Value) QueryType = ""; else QueryType = (string)dr["QueryType"];
                        if (dr["SenderName"] == DBNull.Value) SenderName = ""; else SenderName = (string)dr["SenderName"];
                        if (dr["SenderEmail"] == DBNull.Value) SenderEmail = ""; else SenderEmail = (string)dr["SenderEmail"];
                        if (dr["Subject"] == DBNull.Value) Subject = ""; else Subject = (string)dr["Subject"];
                        if (dr["DateR"] == DBNull.Value) DateR = ""; else DateR = (string)dr["DateR"];
                        if (dr["GlUserCompanyName"] == DBNull.Value) GlUserCompanyName = ""; else GlUserCompanyName = (string)dr["GlUserCompanyName"];
                        if (dr["ReadStatus"] == DBNull.Value) ReadStatus = 0; else ReadStatus = (int)dr["ReadStatus"];
                        if (dr["SenderGLUserId"] == DBNull.Value) SenderGLUserId = ""; else SenderGLUserId = (string)dr["SenderGLUserId"];
                        if (dr["Mob"] == DBNull.Value) Mob = ""; else Mob = (string)dr["Mob"];
                        if (dr["CountryFlag"] == DBNull.Value) CountryFlag = ""; else CountryFlag = (string)dr["CountryFlag"];
                        if (dr["QueryModId"] == DBNull.Value) QueryModId = ""; else QueryModId = (string)dr["QueryModId"];
                        if (dr["LogTime"] == DBNull.Value) LogTime = ""; else LogTime = (string)dr["LogTime"];
                        if (dr["QueryModRefId"] == DBNull.Value) QueryModRefId = ""; else QueryModRefId = (string)dr["QueryModRefId"];
                        if (dr["ORGSenderGLUserId"] == DBNull.Value) ORGSenderGLUserId = ""; else ORGSenderGLUserId = (string)dr["ORGSenderGLUserId"];
                        if (dr["EnqMessage"] == DBNull.Value) EnqMessage = ""; else EnqMessage = (string)dr["EnqMessage"];
                        if (dr["CountryISO"] == DBNull.Value) CountryISO = ""; else CountryISO = (string)dr["CountryISO"];
                        if (dr["EnqCallDuration"] == DBNull.Value) EnqCallDuration = ""; else EnqCallDuration = (string)dr["EnqCallDuration"];
                        if (dr["EnqReceiverMob"] == DBNull.Value) EnqReceiverMob = ""; else EnqReceiverMob = (string)dr["EnqReceiverMob"];
                        if (dr["EnqCity"] == DBNull.Value) EnqCity = ""; else EnqCity = (string)dr["EnqCity"];
                        if (dr["EnqState"] == DBNull.Value) EnqState = ""; else EnqState = (string)dr["EnqState"];
                        if (dr["ProductName"] == DBNull.Value) ProductName = ""; else ProductName = (string)dr["ProductName"];
                        if (dr["EnqAddress"] == DBNull.Value) EnqAddress = ""; else EnqAddress = (string)dr["EnqAddress"];
                        if (dr["EmailAlt"] == DBNull.Value) EmailAlt = ""; else EmailAlt = (string)dr["EmailAlt"];
                        if (dr["MobileAlt"] == DBNull.Value) MobileAlt = ""; else MobileAlt = (string)dr["MobileAlt"];
                        if (dr["Phone"] == DBNull.Value) Phone = ""; else Phone = (string)dr["Phone"];
                        if (dr["PhoneAlt"] == DBNull.Value) PhoneAlt = ""; else PhoneAlt = (string)dr["PhoneAlt"];
                        if (dr["ImmemberSince"] == DBNull.Value) ImmemberSince = ""; else ImmemberSince = (string)dr["ImmemberSince"];
                        if (dr["Source"] == DBNull.Value) Source = 0; else Source = (int)dr["Source"];
                        if (dr["TotalCnt"] == DBNull.Value) TotalCnt = 0; else TotalCnt = (int)dr["TotalCnt"];
                        if (dr["Feedback"] == DBNull.Value) Feedback = ""; else Feedback = (string)dr["Feedback"];

                        visit.Add(new IndiaMartModel
                        {
                            Id = (int)dr["Id"],
                            Rn = Rn,
                            SenderName = SenderName,
                            SenderEmail = SenderEmail,
                            Source = Source,
                            Subject = Subject,
                            IsMoved = (bool)dr["IsMoved"],
                            DateTimeRe = (DateTime)dr["DateTimeRe"],
                            GlUserCompanyName = GlUserCompanyName,
                            Mob = Mob,
                            CountryFlag = CountryFlag,
                            EnqMessage = EnqMessage,
                            EnqAddress = EnqAddress,
                            EnqCallDuration = EnqCallDuration,
                            EnqReceiverMob = EnqReceiverMob,
                            EnqCity = EnqCity,
                            EnqState = EnqState,
                            EmailAlt = EmailAlt,
                            MobileAlt = MobileAlt,
                            Phone = Phone,
                            PhoneAlt = PhoneAlt,
                            Feedback = Feedback,
                            QueryId = QueryId,
                            QueryType = QueryType,
                            ReadStatus = ReadStatus,
                            SenderGlUserId = SenderGLUserId,
                            QueryModId = QueryModId,
                            LogTime = LogTime,
                            QueryModRefId = QueryModRefId,
                            OrgSenderGlUserId = ORGSenderGLUserId,
                            ProductName = ProductName,
                            CountryIso = CountryISO,
                            ImmemberSince = ImmemberSince,
                            TotalCnt = TotalCnt
                        });
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = count;
                    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                    var items = visit.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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

                    Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    var abc = JsonConvert.SerializeObject(paginationMetadata);

                    return Ok(items);
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,Rn,QueryId,QueryType,SenderName,SenderEmail,Subject,DateRe,DateR,DateTimeRe,GlUserCompanyName,ReadStatus,SenderGLUserId,Mob,CountryFlag,QueryModId,LogTime,QueryModRefId,DIRQueryModrefType,ORGSenderGLUserId,EnqMessage,EnqAddress,EnqCallDuration,EnqReceiverMob,EnqCity,EnqState,ProductName,CountryISO,EmailAlt,MobileAlt,Phone,PhoneAlt,ImmemberSince,TotalCnt,Source,IsMoved,Feedback from IndiaMartDetails where Id=" + id + " ORDER BY Id DESC";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic Rn, QueryId, QueryType, SenderName, SenderEmail, Subject, DateR, GlUserCompanyName, ReadStatus, SenderGLUserId, Mob, CountryFlag, QueryModId, LogTime, QueryModRefId, DIRQueryModrefType, ORGSenderGLUserId, EnqMessage, EnqAddress, EnqCallDuration, EnqReceiverMob, EnqCity, EnqState, ProductName, CountryISO, EmailAlt, MobileAlt, Phone, PhoneAlt, ImmemberSince, TotalCnt, Source, Feedback;

                        if (dr["Rn"] == DBNull.Value) Rn = 0; else Rn = (int)dr["Rn"];
                        if (dr["QueryId"] == DBNull.Value) QueryId = ""; else QueryId = (string)dr["QueryId"];
                        if (dr["QueryType"] == DBNull.Value) QueryType = ""; else QueryType = (string)dr["QueryType"];
                        if (dr["SenderName"] == DBNull.Value) SenderName = ""; else SenderName = (string)dr["SenderName"];
                        if (dr["SenderEmail"] == DBNull.Value) SenderEmail = ""; else SenderEmail = (string)dr["SenderEmail"];
                        if (dr["Subject"] == DBNull.Value) Subject = ""; else Subject = (string)dr["Subject"];
                        if (dr["DateR"] == DBNull.Value) DateR = ""; else DateR = (string)dr["DateR"];
                        if (dr["GlUserCompanyName"] == DBNull.Value) GlUserCompanyName = ""; else GlUserCompanyName = (string)dr["GlUserCompanyName"];
                        if (dr["ReadStatus"] == DBNull.Value) ReadStatus = 0; else ReadStatus = (int)dr["ReadStatus"];
                        if (dr["SenderGLUserId"] == DBNull.Value) SenderGLUserId = ""; else SenderGLUserId = (string)dr["SenderGLUserId"];
                        if (dr["Mob"] == DBNull.Value) Mob = ""; else Mob = (string)dr["Mob"];
                        if (dr["CountryFlag"] == DBNull.Value) CountryFlag = ""; else CountryFlag = (string)dr["CountryFlag"];
                        if (dr["QueryModId"] == DBNull.Value) QueryModId = ""; else QueryModId = (string)dr["QueryModId"];
                        if (dr["LogTime"] == DBNull.Value) LogTime = ""; else LogTime = (string)dr["LogTime"];
                        if (dr["QueryModRefId"] == DBNull.Value) QueryModRefId = ""; else QueryModRefId = (string)dr["QueryModRefId"];
                        if (dr["ORGSenderGLUserId"] == DBNull.Value) ORGSenderGLUserId = ""; else ORGSenderGLUserId = (string)dr["ORGSenderGLUserId"];
                        if (dr["EnqMessage"] == DBNull.Value) EnqMessage = ""; else EnqMessage = (string)dr["EnqMessage"];
                        if (dr["CountryISO"] == DBNull.Value) CountryISO = ""; else CountryISO = (string)dr["CountryISO"];
                        if (dr["EnqCallDuration"] == DBNull.Value) EnqCallDuration = ""; else EnqCallDuration = (string)dr["EnqCallDuration"];
                        if (dr["EnqReceiverMob"] == DBNull.Value) EnqReceiverMob = ""; else EnqReceiverMob = (string)dr["EnqReceiverMob"];
                        if (dr["EnqCity"] == DBNull.Value) EnqCity = ""; else EnqCity = (string)dr["EnqCity"];
                        if (dr["EnqState"] == DBNull.Value) EnqState = ""; else EnqState = (string)dr["EnqState"];
                        if (dr["ProductName"] == DBNull.Value) ProductName = ""; else ProductName = (string)dr["ProductName"];
                        if (dr["EnqAddress"] == DBNull.Value) EnqAddress = ""; else EnqAddress = (string)dr["EnqAddress"];
                        if (dr["EmailAlt"] == DBNull.Value) EmailAlt = ""; else EmailAlt = (string)dr["EmailAlt"];
                        if (dr["MobileAlt"] == DBNull.Value) MobileAlt = ""; else MobileAlt = (string)dr["MobileAlt"];
                        if (dr["Phone"] == DBNull.Value) Phone = ""; else Phone = (string)dr["Phone"];
                        if (dr["PhoneAlt"] == DBNull.Value) PhoneAlt = ""; else PhoneAlt = (string)dr["PhoneAlt"];
                        if (dr["ImmemberSince"] == DBNull.Value) ImmemberSince = ""; else ImmemberSince = (string)dr["ImmemberSince"];
                        if (dr["Source"] == DBNull.Value) Source = 0; else Source = (int)dr["Source"];
                        if (dr["TotalCnt"] == DBNull.Value) TotalCnt = 0; else TotalCnt = (int)dr["TotalCnt"];
                        if (dr["Feedback"] == DBNull.Value) Feedback = ""; else Feedback = (string)dr["Feedback"];

                        visit.Add(new IndiaMartModel
                        {
                            Id = (int)dr["Id"],
                            Rn = Rn,
                            SenderName = SenderName,
                            SenderEmail = SenderEmail,
                            Source = Source,
                            Subject = Subject,
                            IsMoved = (bool)dr["IsMoved"],
                            DateTimeRe = (DateTime)dr["DateTimeRe"],
                            GlUserCompanyName = GlUserCompanyName,
                            Mob = Mob,
                            CountryFlag = CountryFlag,
                            EnqMessage = EnqMessage,
                            EnqAddress = EnqAddress,
                            EnqCallDuration = EnqCallDuration,
                            EnqReceiverMob = EnqReceiverMob,
                            EnqCity = EnqCity,
                            EnqState = EnqState,
                            EmailAlt = EmailAlt,
                            MobileAlt = MobileAlt,
                            Phone = Phone,
                            PhoneAlt = PhoneAlt,
                            Feedback = Feedback,
                            QueryId = QueryId,
                            QueryType = QueryType,
                            ReadStatus = ReadStatus,
                            SenderGlUserId = SenderGLUserId,
                            QueryModId = QueryModId,
                            LogTime = LogTime,
                            QueryModRefId = QueryModRefId,
                            OrgSenderGlUserId = ORGSenderGLUserId,
                            ProductName = ProductName,
                            CountryIso = CountryISO,
                            ImmemberSince = ImmemberSince,
                            TotalCnt = TotalCnt
                        });
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = count;
                    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                    var items = visit.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

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

                    Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    var abc = JsonConvert.SerializeObject(paginationMetadata);

                    return Ok(items);
                }
            }
        }
    }
}