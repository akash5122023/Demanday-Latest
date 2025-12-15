
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Settings.Endpoints;
    using AdvanceCRM.ThirdParty;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Newtonsoft.Json;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    
    //using System.Web.Script.Serialization;
    using MyRepository = Repositories.IndiaMartDetailsRepository;
    using MyRow = IndiaMartDetailsRow;
    using System.Linq;

    [Route("Services/ThirdParty/IndiaMartDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class IndiaMartDetailsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public IndiaMartDetailsController(ISqlConnections connections, IWebHostEnvironment env)
        {
            _connections = connections;
            _env = env;
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }

        [HttpPost]
        public StandardResponse Sync2(IUnitOfWork uow)
        {
            var response = new StandardResponse();
            IndiaMartDetailsRow LastEnquiry;
            IndiaMartConfigurationRow Config;
            DateTime StartDate, EndDate;
            using (var connection = _connections.NewFor<IndiaMartConfigurationRow>())
            {
                var s = IndiaMartConfigurationRow.Fields;
                Config = connection.TryFirst<IndiaMartConfigurationRow>(q => q
                    .SelectTableFields()
                    .Select(s.MobileNumber)
                    .Select(s.ApiKey)
                    );

                var i = IndiaMartDetailsRow.Fields;
                LastEnquiry = connection.TryFirst<IndiaMartDetailsRow>(q => q
                .SelectTableFields()
                .Select(i.DateTimeRe)
                .OrderBy(i.DateTimeRe, true)
                );
            }

            //var m = new List<dynamic>();
            try
            {
                if (LastEnquiry == null)
                {
                    StartDate = DateTime.Now.AddDays(-7);
                }
                else
                {
                    StartDate = LastEnquiry.DateRe.Value;
                }

                EndDate = StartDate.AddDays(7);

                //  https://mapi.indiamart.com/wservce/crm/crmListing/v2/?glusr_crm_key=mR22G7pu5nfGSPes4naK7l2KoFPGnjU=&start_time=01-jul-2022&end_time=06-jul-2022

                string uri = "https://mapi.indiamart.com/wservce/crm/crmListing/v2/?glusr_crm_key=" + Config.ApiKey + "&start_time=" + StartDate.ToString("dd-MMM-yyyy") + "&end_time=" + EndDate.ToString("dd-MMM-yyyy");
               // string uri = "http://mapi.indiamart.com/wservce/enquiry/listing/GLUSR_MOBILE/" + Config.MobileNumber + "/GLUSR_MOBILE_KEY/" + Config.ApiKey + "/Start_Time/" + StartDate.ToString("dd-MMM-yyyy") + "/End_Time/" + EndDate.ToString("dd-MMM-yyyy") + "/";
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                //string status = string.Empty;
                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds. 
                myHttpWebRequest.Timeout = 15000;
                HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                var IndiaMartResponsObjects = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());
                if (IndiaMartResponsObjects != null)
                {
                    var count = IndiaMartResponsObjects["TOTAL_RECORDS"];
                    var result = IndiaMartResponsObjects["RESPONSE"];
                    var status = IndiaMartResponsObjects["STATUS"];
                    // Dictionary<string, object> result = IndiaMartResponsObjects[RESPONSE];
                    if (status!= "SUCCESS")
                    {
                        response.Status = Convert.ToString(IndiaMartResponsObjects["MESSAGE"]).Replace("'", "");
                        return response;
                    }

                    List<string> Records = new List<string>();
                    foreach (Dictionary<string, object> IndiaMartResponsObject in result)
                    {
                        IndiaMartDetail indiaMartDetail = new IndiaMartDetail();
                        indiaMartDetail.RN = !IndiaMartResponsObject.ContainsKey("RN") ? "" : Convert.ToString(IndiaMartResponsObject["RN"]).Replace("'", "");
                        indiaMartDetail.QUERY_ID = !IndiaMartResponsObject.ContainsKey("UNIQUE_QUERY_ID") ? "" : Convert.ToString(IndiaMartResponsObject["UNIQUE_QUERY_ID"]).Replace("'", "");
                        indiaMartDetail.QTYPE = !IndiaMartResponsObject.ContainsKey("QUERY_TYPE") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_TYPE"]).Replace("'", "");
                        indiaMartDetail.SENDERNAME = !IndiaMartResponsObject.ContainsKey("SENDER_NAME") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_NAME"]).Replace("'", "");
                        indiaMartDetail.SENDEREMAIL = !IndiaMartResponsObject.ContainsKey("SENDER_EMAIL") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_EMAIL"]).Replace("'", "");
                        indiaMartDetail.SUBJECT = !IndiaMartResponsObject.ContainsKey("SUBJECT") ? "" : Convert.ToString(IndiaMartResponsObject["SUBJECT"]).Replace("'", "");
                        indiaMartDetail.DATE_RE = !IndiaMartResponsObject.ContainsKey("QUERY_TIME") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_TIME"]).Replace("'", "");
                        indiaMartDetail.DATE_R = !IndiaMartResponsObject.ContainsKey("QUERY_TIME") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_TIME"]).Replace("'", "");
                        indiaMartDetail.DATE_TIME_RE = !IndiaMartResponsObject.ContainsKey("QUERY_TIME") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_TIME"]).Replace("'", "");
                        indiaMartDetail.GLUSR_USR_COMPANYNAME = !IndiaMartResponsObject.ContainsKey("GLUSR_USR_COMPANYNAME") ? "" : Convert.ToString(IndiaMartResponsObject["GLUSR_USR_COMPANYNAME"]).Replace("'", "");
                        indiaMartDetail.READ_STATUS = !IndiaMartResponsObject.ContainsKey("READ_STATUS") ? "" : Convert.ToString(IndiaMartResponsObject["READ_STATUS"]).Replace("'", "");
                        indiaMartDetail.SENDER_GLUSR_USR_ID = !IndiaMartResponsObject.ContainsKey("SENDER_GLUSR_USR_ID") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_GLUSR_USR_ID"]).Replace("'", "");
                        indiaMartDetail.MOB = !IndiaMartResponsObject.ContainsKey("SENDER_MOBILE") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_MOBILE"]).Replace("'", "");
                        indiaMartDetail.COUNTRY_FLAG = !IndiaMartResponsObject.ContainsKey("COUNTRY_FLAG") ? "" : Convert.ToString(IndiaMartResponsObject["COUNTRY_FLAG"]).Replace("'", "");
                        indiaMartDetail.QUERY_MODID = !IndiaMartResponsObject.ContainsKey("QUERY_MODID") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_MODID"]).Replace("'", "");
                        indiaMartDetail.LOG_TIME = !IndiaMartResponsObject.ContainsKey("LOG_TIME") ? "" : Convert.ToString(IndiaMartResponsObject["LOG_TIME"]).Replace("'", "");
                        indiaMartDetail.QUERY_MODREFID = !IndiaMartResponsObject.ContainsKey("QUERY_MODREFID") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_MODREFID"]).Replace("'", "");
                        indiaMartDetail.DIR_QUERY_MODREF_TYPE = !IndiaMartResponsObject.ContainsKey("DIR_QUERY_MODREF_TYPE") ? "" : Convert.ToString(IndiaMartResponsObject["DIR_QUERY_MODREF_TYPE"]).Replace("'", "");
                        indiaMartDetail.ORG_SENDER_GLUSR_ID = !IndiaMartResponsObject.ContainsKey("ORG_SENDER_GLUSR_ID") ? "" : Convert.ToString(IndiaMartResponsObject["ORG_SENDER_GLUSR_ID"]).Replace("'", "");
                        indiaMartDetail.ENQ_MESSAGE = !IndiaMartResponsObject.ContainsKey("QUERY_MESSAGE") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_MESSAGE"]).Replace("'", "");
                        indiaMartDetail.ENQ_ADDRESS = !IndiaMartResponsObject.ContainsKey("SENDER_ADDRESS") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_ADDRESS"]).Replace("'", "");
                        indiaMartDetail.ENQ_CALL_DURATION = !IndiaMartResponsObject.ContainsKey("ENQ_CALL_DURATION") ? "" : Convert.ToString(IndiaMartResponsObject["ENQ_CALL_DURATION"]).Replace("'", "");
                        indiaMartDetail.ENQ_RECEIVER_MOB = !IndiaMartResponsObject.ContainsKey("ENQ_RECEIVER_MOB") ? "" : Convert.ToString(IndiaMartResponsObject["ENQ_RECEIVER_MOB"]).Replace("'", "");
                        indiaMartDetail.ENQ_CITY = !IndiaMartResponsObject.ContainsKey("SENDER_CITY") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_CITY"]).Replace("'", "");
                        indiaMartDetail.ENQ_STATE = !IndiaMartResponsObject.ContainsKey("SENDER_STATE") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_STATE"]).Replace("'", "");
                        indiaMartDetail.PRODUCT_NAME = !IndiaMartResponsObject.ContainsKey("QUERY_PRODUCT_NAME") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_PRODUCT_NAME"]).Replace("'", "");
                        indiaMartDetail.COUNTRY_ISO = !IndiaMartResponsObject.ContainsKey("SENDER_COUNTRY_ISO") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_COUNTRY_ISO"]).Replace("'", "");
                        indiaMartDetail.EMAIL_ALT = !IndiaMartResponsObject.ContainsKey("SENDER_EMAIL_ALT") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_EMAIL_ALT"]).Replace("'", "");
                        indiaMartDetail.MOBILE_ALT = !IndiaMartResponsObject.ContainsKey("SENDER_MOBILE_ALT") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_MOBILE_ALT"]).Replace("'", "");
                        indiaMartDetail.PHONE = !IndiaMartResponsObject.ContainsKey("SENDER_MOBILE") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_MOBILE"]).Replace("'", "");
                        indiaMartDetail.PHONE_ALT = !IndiaMartResponsObject.ContainsKey("PHONE_ALT") ? "" : Convert.ToString(IndiaMartResponsObject["PHONE_ALT"]).Replace("'", @"\'");
                        indiaMartDetail.IM_MEMBER_SINCE = !IndiaMartResponsObject.ContainsKey("IM_MEMBER_SINCE") ? "" : Convert.ToString(IndiaMartResponsObject["IM_MEMBER_SINCE"]).Replace("'", "");
                        indiaMartDetail.TOTAL_COUNT = !IndiaMartResponsObject.ContainsKey("TOTAL_COUNT") ? "" : Convert.ToString(IndiaMartResponsObject["TOTAL_COUNT"]).Replace("'", "");

                        if (indiaMartDetail.READ_STATUS == "")
                        {
                            indiaMartDetail.READ_STATUS = "-1";
                        }

                        int source = 1;
                        if (indiaMartDetail.QTYPE == "B")
                        {
                            source = 2;
                        }
                        else if (indiaMartDetail.QTYPE == "P")
                        {
                            source = 3;
                        }

                        Records.Add("IF NOT EXISTS (SELECT * FROM IndiaMartDetails WHERE QueryId ='" + indiaMartDetail.QUERY_ID + "')" +
                            "INSERT INTO IndiaMartDetails ([Rn],[QueryId],[QueryType],[SenderName],[SenderEmail],[Subject],[DateRe],[DateR],[DateTimeRe],[GlUserCompanyName],[ReadStatus],[SenderGLUserId],[Mob],[CountryFlag],[QueryModId],[LogTime],[QueryModRefId],[DIRQueryModrefType],[ORGSenderGLUserId],[EnqMessage],[EnqAddress],[EnqCallDuration],[EnqReceiverMob],[EnqCity],[EnqState],[ProductName],[CountryISO],[EmailAlt],[MobileAlt],[Phone],[PhoneAlt],[ImmemberSince],[TotalCnt],[Source]) VALUES " +
                            "('" + indiaMartDetail.RN + "','" +
                                 indiaMartDetail.QUERY_ID + "','" +
                                 indiaMartDetail.QTYPE + "','" +
                                 indiaMartDetail.SENDERNAME + "','" +
                                 indiaMartDetail.SENDEREMAIL + "','" +
                                 indiaMartDetail.SUBJECT + "','" +
                                 Convert.ToDateTime(indiaMartDetail.DATE_RE).ToString("yyyy-MM-dd") + "','" +
                                 indiaMartDetail.DATE_R + "','" +
                                 Convert.ToDateTime(indiaMartDetail.DATE_TIME_RE).ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                                 indiaMartDetail.GLUSR_USR_COMPANYNAME + "','" +
                                 indiaMartDetail.READ_STATUS + "','" +
                                 indiaMartDetail.SENDER_GLUSR_USR_ID + "','" +
                                 indiaMartDetail.MOB + "','" +
                                 indiaMartDetail.COUNTRY_FLAG + "','" +
                                 indiaMartDetail.QUERY_MODID + "','" +
                                 indiaMartDetail.LOG_TIME + "','" +
                                 indiaMartDetail.QUERY_MODREFID + "','" +
                                 indiaMartDetail.DIR_QUERY_MODREF_TYPE + "','" +
                                 indiaMartDetail.ORG_SENDER_GLUSR_ID + "','" +
                                 indiaMartDetail.ENQ_MESSAGE + "','" +
                                 indiaMartDetail.ENQ_ADDRESS + "','" +
                                 indiaMartDetail.ENQ_CALL_DURATION + "','" +
                                 indiaMartDetail.ENQ_RECEIVER_MOB + "','" +
                                 indiaMartDetail.ENQ_CITY + "','" +
                                 indiaMartDetail.ENQ_STATE + "','" +
                                 indiaMartDetail.PRODUCT_NAME + "','" +
                                 indiaMartDetail.COUNTRY_ISO + "','" +
                                 indiaMartDetail.EMAIL_ALT + "','" +
                                 indiaMartDetail.MOBILE_ALT + "','" +
                                 indiaMartDetail.PHONE + "','" +
                                 indiaMartDetail.PHONE_ALT + "','" +
                                 indiaMartDetail.IM_MEMBER_SINCE + "','" +
                                 indiaMartDetail.TOTAL_COUNT + "'," +
                                 source + ")");

                    }
                    Records.Reverse();
                    if (Records.Count > 0)
                    {
                        using (var innerConnection = _connections.NewFor<IndiaMartDetailsRow>())
                        {
                            for (int ij = 0; ij < Records.Count; ij++)
                            {
                                try
                                {
                                    innerConnection.Execute(Records[ij]);
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                    }
                }
                response.Status = "Sync completed";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();
            IndiaMartDetailsRow LastEnquiry;
            IndiaMartConfigurationRow Config;
            DateTime StartDate, EndDate;
            using (var connection = _connections.NewFor<IndiaMartConfigurationRow>())
            {
                var s = IndiaMartConfigurationRow.Fields;
                Config = connection.TryFirst<IndiaMartConfigurationRow>(q => q
                    .SelectTableFields()
                    .Select(s.MobileNumber)
                    .Select(s.ApiKey)
                    );

                var i = IndiaMartDetailsRow.Fields;
                LastEnquiry = connection.TryFirst<IndiaMartDetailsRow>(q => q
                .SelectTableFields()
                .Select(i.DateTimeRe)
                .OrderBy(i.DateTimeRe, true)
                );
            }

            //var m = new List<dynamic>();
            try
            {
                if (LastEnquiry == null)
                {
                    StartDate = DateTime.Now.AddDays(-7);
                }
                else
                {
                    StartDate = LastEnquiry.DateRe.Value;
                }

               // EndDate = Convert.ToDateTime("06-Nov-2022");
               EndDate = StartDate.AddDays(7);
               // string uri = "http://mapi.indiamart.com/wservce/enquiry/listing/GLUSR_MOBILE/" + Config.MobileNumber + "/GLUSR_MOBILE_KEY/" + Config.ApiKey + "/Start_Time/" + StartDate.ToString("dd-MMM-yyyy") + "/End_Time/" + EndDate.ToString("dd-MMM-yyyy") + "/";

                string uri = "http://mapi.indiamart.com/wservce/enquiry/listing/GLUSR_MOBILE/" + Config.MobileNumber + "/GLUSR_MOBILE_KEY/" + Config.ApiKey + "/Start_Time/" + StartDate.ToString("dd-MMM-yyyy") + "/End_Time/" + EndDate.ToString("dd-MMM-yyyy") + "/";
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                //string status = string.Empty;
                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds. 
                myHttpWebRequest.Timeout = 15000;
                HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                var IndiaMartResponsObjects = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());
                if (IndiaMartResponsObjects != null)
                {
                    Dictionary<string, object> result = IndiaMartResponsObjects[0];
                    if (result.ContainsKey("Error_Message"))
                    {
                        response.Status = Convert.ToString(result["Error_Message"]).Replace("'", "");
                        return response;
                    }

                    List<string> Records = new List<string>();
                    foreach (Dictionary<string, object> IndiaMartResponsObject in IndiaMartResponsObjects)
                    {
                        IndiaMartDetail indiaMartDetail = new IndiaMartDetail();
                        indiaMartDetail.RN = !IndiaMartResponsObject.ContainsKey("RN") ? "" : Convert.ToString(IndiaMartResponsObject["RN"]).Replace("'", "");
                        indiaMartDetail.QUERY_ID = !IndiaMartResponsObject.ContainsKey("QUERY_ID") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_ID"]).Replace("'", "");
                        indiaMartDetail.QTYPE = !IndiaMartResponsObject.ContainsKey("QTYPE") ? "" : Convert.ToString(IndiaMartResponsObject["QTYPE"]).Replace("'", "");
                        indiaMartDetail.SENDERNAME = !IndiaMartResponsObject.ContainsKey("SENDERNAME") ? "" : Convert.ToString(IndiaMartResponsObject["SENDERNAME"]).Replace("'", "");
                        indiaMartDetail.SENDEREMAIL = !IndiaMartResponsObject.ContainsKey("SENDEREMAIL") ? "" : Convert.ToString(IndiaMartResponsObject["SENDEREMAIL"]).Replace("'", "");
                        indiaMartDetail.SUBJECT = !IndiaMartResponsObject.ContainsKey("SUBJECT") ? "" : Convert.ToString(IndiaMartResponsObject["SUBJECT"]).Replace("'", "");
                        indiaMartDetail.DATE_RE = !IndiaMartResponsObject.ContainsKey("DATE_RE") ? "" : Convert.ToString(IndiaMartResponsObject["DATE_RE"]).Replace("'", "");
                        indiaMartDetail.DATE_R = !IndiaMartResponsObject.ContainsKey("DATE_R") ? "" : Convert.ToString(IndiaMartResponsObject["DATE_R"]).Replace("'", "");
                        indiaMartDetail.DATE_TIME_RE = !IndiaMartResponsObject.ContainsKey("DATE_TIME_RE") ? "" : Convert.ToString(IndiaMartResponsObject["DATE_TIME_RE"]).Replace("'", "");
                        indiaMartDetail.GLUSR_USR_COMPANYNAME = !IndiaMartResponsObject.ContainsKey("GLUSR_USR_COMPANYNAME") ? "" : Convert.ToString(IndiaMartResponsObject["GLUSR_USR_COMPANYNAME"]).Replace("'", "");
                        indiaMartDetail.READ_STATUS = !IndiaMartResponsObject.ContainsKey("READ_STATUS") ? "" : Convert.ToString(IndiaMartResponsObject["READ_STATUS"]).Replace("'", "");
                        indiaMartDetail.SENDER_GLUSR_USR_ID = !IndiaMartResponsObject.ContainsKey("SENDER_GLUSR_USR_ID") ? "" : Convert.ToString(IndiaMartResponsObject["SENDER_GLUSR_USR_ID"]).Replace("'", "");
                        indiaMartDetail.MOB = !IndiaMartResponsObject.ContainsKey("MOB") ? "" : Convert.ToString(IndiaMartResponsObject["MOB"]).Replace("'", "");
                        indiaMartDetail.COUNTRY_FLAG = !IndiaMartResponsObject.ContainsKey("COUNTRY_FLAG") ? "" : Convert.ToString(IndiaMartResponsObject["COUNTRY_FLAG"]).Replace("'", "");
                        indiaMartDetail.QUERY_MODID = !IndiaMartResponsObject.ContainsKey("QUERY_MODID") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_MODID"]).Replace("'", "");
                        indiaMartDetail.LOG_TIME = !IndiaMartResponsObject.ContainsKey("LOG_TIME") ? "" : Convert.ToString(IndiaMartResponsObject["LOG_TIME"]).Replace("'", "");
                        indiaMartDetail.QUERY_MODREFID = !IndiaMartResponsObject.ContainsKey("QUERY_MODREFID") ? "" : Convert.ToString(IndiaMartResponsObject["QUERY_MODREFID"]).Replace("'", "");
                        indiaMartDetail.DIR_QUERY_MODREF_TYPE = !IndiaMartResponsObject.ContainsKey("DIR_QUERY_MODREF_TYPE") ? "" : Convert.ToString(IndiaMartResponsObject["DIR_QUERY_MODREF_TYPE"]).Replace("'", "");
                        indiaMartDetail.ORG_SENDER_GLUSR_ID = !IndiaMartResponsObject.ContainsKey("ORG_SENDER_GLUSR_ID") ? "" : Convert.ToString(IndiaMartResponsObject["ORG_SENDER_GLUSR_ID"]).Replace("'", "");
                        indiaMartDetail.ENQ_MESSAGE = !IndiaMartResponsObject.ContainsKey("ENQ_MESSAGE") ? "" : Convert.ToString(IndiaMartResponsObject["ENQ_MESSAGE"]).Replace("'", "");
                        indiaMartDetail.ENQ_ADDRESS = !IndiaMartResponsObject.ContainsKey("ENQ_ADDRESS") ? "" : Convert.ToString(IndiaMartResponsObject["ENQ_ADDRESS"]).Replace("'", "");
                        indiaMartDetail.ENQ_CALL_DURATION = !IndiaMartResponsObject.ContainsKey("ENQ_CALL_DURATION") ? "" : Convert.ToString(IndiaMartResponsObject["ENQ_CALL_DURATION"]).Replace("'", "");
                        indiaMartDetail.ENQ_RECEIVER_MOB = !IndiaMartResponsObject.ContainsKey("ENQ_RECEIVER_MOB") ? "" : Convert.ToString(IndiaMartResponsObject["ENQ_RECEIVER_MOB"]).Replace("'", "");
                        indiaMartDetail.ENQ_CITY = !IndiaMartResponsObject.ContainsKey("ENQ_CITY") ? "" : Convert.ToString(IndiaMartResponsObject["ENQ_CITY"]).Replace("'", "");
                        indiaMartDetail.ENQ_STATE = !IndiaMartResponsObject.ContainsKey("ENQ_STATE") ? "" : Convert.ToString(IndiaMartResponsObject["ENQ_STATE"]).Replace("'", "");
                        indiaMartDetail.PRODUCT_NAME = !IndiaMartResponsObject.ContainsKey("PRODUCT_NAME") ? "" : Convert.ToString(IndiaMartResponsObject["PRODUCT_NAME"]).Replace("'", "");
                        indiaMartDetail.COUNTRY_ISO = !IndiaMartResponsObject.ContainsKey("COUNTRY_ISO") ? "" : Convert.ToString(IndiaMartResponsObject["COUNTRY_ISO"]).Replace("'", "");
                        indiaMartDetail.EMAIL_ALT = !IndiaMartResponsObject.ContainsKey("EMAIL_ALT") ? "" : Convert.ToString(IndiaMartResponsObject["EMAIL_ALT"]).Replace("'", "");
                        indiaMartDetail.MOBILE_ALT = !IndiaMartResponsObject.ContainsKey("MOBILE_ALT") ? "" : Convert.ToString(IndiaMartResponsObject["MOBILE_ALT"]).Replace("'", "");
                        indiaMartDetail.PHONE = !IndiaMartResponsObject.ContainsKey("PHONE") ? "" : Convert.ToString(IndiaMartResponsObject["PHONE"]).Replace("'", "");
                        indiaMartDetail.PHONE_ALT = !IndiaMartResponsObject.ContainsKey("PHONE_ALT") ? "" : Convert.ToString(IndiaMartResponsObject["PHONE_ALT"]).Replace("'", @"\'");
                        indiaMartDetail.IM_MEMBER_SINCE = !IndiaMartResponsObject.ContainsKey("IM_MEMBER_SINCE") ? "" : Convert.ToString(IndiaMartResponsObject["IM_MEMBER_SINCE"]).Replace("'", "");
                        indiaMartDetail.TOTAL_COUNT = !IndiaMartResponsObject.ContainsKey("TOTAL_COUNT") ? "" : Convert.ToString(IndiaMartResponsObject["TOTAL_COUNT"]).Replace("'", "");

                        if (indiaMartDetail.READ_STATUS == "")
                        {
                            indiaMartDetail.READ_STATUS = "-1";
                        }

                        int source = 1;
                        if (indiaMartDetail.QTYPE == "B")
                        {
                            source = 2;
                        }
                        else if (indiaMartDetail.QTYPE == "P")
                        {
                            source = 3;
                        }

                        Records.Add("IF NOT EXISTS (SELECT * FROM IndiaMartDetails WHERE QueryId ='" + indiaMartDetail.QUERY_ID + "')" +
                            "INSERT INTO IndiaMartDetails ([Rn],[QueryId],[QueryType],[SenderName],[SenderEmail],[Subject],[DateRe],[DateR],[DateTimeRe],[GlUserCompanyName],[ReadStatus],[SenderGLUserId],[Mob],[CountryFlag],[QueryModId],[LogTime],[QueryModRefId],[DIRQueryModrefType],[ORGSenderGLUserId],[EnqMessage],[EnqAddress],[EnqCallDuration],[EnqReceiverMob],[EnqCity],[EnqState],[ProductName],[CountryISO],[EmailAlt],[MobileAlt],[Phone],[PhoneAlt],[ImmemberSince],[TotalCnt],[Source]) VALUES " +
                            "('" + indiaMartDetail.RN + "','" +
                                 indiaMartDetail.QUERY_ID + "','" +
                                 indiaMartDetail.QTYPE + "','" +
                                 indiaMartDetail.SENDERNAME + "','" +
                                 indiaMartDetail.SENDEREMAIL + "','" +
                                 indiaMartDetail.SUBJECT + "','" +
                                 Convert.ToDateTime(indiaMartDetail.DATE_RE).ToString("yyyy-MM-dd") + "','" +
                                 indiaMartDetail.DATE_R + "','" +
                                 Convert.ToDateTime(indiaMartDetail.DATE_TIME_RE).ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                                 indiaMartDetail.GLUSR_USR_COMPANYNAME + "','" +
                                 indiaMartDetail.READ_STATUS + "','" +
                                 indiaMartDetail.SENDER_GLUSR_USR_ID + "','" +
                                 indiaMartDetail.MOB + "','" +
                                 indiaMartDetail.COUNTRY_FLAG + "','" +
                                 indiaMartDetail.QUERY_MODID + "','" +
                                 indiaMartDetail.LOG_TIME + "','" +
                                 indiaMartDetail.QUERY_MODREFID + "','" +
                                 indiaMartDetail.DIR_QUERY_MODREF_TYPE + "','" +
                                 indiaMartDetail.ORG_SENDER_GLUSR_ID + "','" +
                                 indiaMartDetail.ENQ_MESSAGE + "','" +
                                 indiaMartDetail.ENQ_ADDRESS + "','" +
                                 indiaMartDetail.ENQ_CALL_DURATION + "','" +
                                 indiaMartDetail.ENQ_RECEIVER_MOB + "','" +
                                 indiaMartDetail.ENQ_CITY + "','" +
                                 indiaMartDetail.ENQ_STATE + "','" +
                                 indiaMartDetail.PRODUCT_NAME + "','" +
                                 indiaMartDetail.COUNTRY_ISO + "','" +
                                 indiaMartDetail.EMAIL_ALT + "','" +
                                 indiaMartDetail.MOBILE_ALT + "','" +
                                 indiaMartDetail.PHONE + "','" +
                                 indiaMartDetail.PHONE_ALT + "','" +
                                 indiaMartDetail.IM_MEMBER_SINCE + "'," +
                                 indiaMartDetail.TOTAL_COUNT + "," +
                                 source + ")");

                    }
                    Records.Reverse();
                    if (Records.Count > 0)
                    {
                        using (var innerConnection = _connections.NewFor<IndiaMartDetailsRow>())
                        {
                            for (int ij = 0; ij < Records.Count; ij++)
                            {
                                try
                                {
                                    innerConnection.Execute(Records[ij]);
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                    }
                }
                response.Status = "Sync completed";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        [ServiceAuthorize("IndiaMART:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.IndiaMartDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "IndiaMart_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }


        [HttpPost]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            var Contacttyp = 2;
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();

            var data = new IndiaMartDetailsRow();

            using (var connection = _connections.NewFor<IndiaMartDetailsRow>())
            {
                var ind = IndiaMartDetailsRow.Fields;
                data = connection.TryById<IndiaMartDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.SenderName)
                   .Select(ind.Subject)
                   .Select(ind.DateRe)
                   .Select(ind.Mob)
                   .Select(ind.EnqAddress)
                   .Select(ind.SenderEmail)
                   .Select(ind.GlUserCompanyName)
                   .Select(ind.EnqMessage)
                   );

                UData = connection.First<UserRow>(q => q
                .SelectTableFields()
                .Select(br.CompanyId)
                .Where(br.UserId == Context.User.GetIdentifier())
               );
            }


            if (data.EnqAddress != null)
            { data.EnqAddress = data.EnqAddress.Replace(",", " ");  }
            if (data.EnqMessage != null)
            { data.EnqMessage = Uri.UnescapeDataString(data.EnqMessage).Replace(",", ""); }
            if (data.Subject != null)
            { data.Subject = data.Subject.Replace(",", " ");          //  data.GlUserCompanyName = data.GlUserCompanyName.Replace(",", " ");
            }
            if (data.EnqAddress != null)
            {
                data.EnqAddress = data.EnqAddress.Replace("\'", "");
            }
            if (data.EnqMessage != null)
            {
                data.EnqMessage = data.EnqMessage.Replace("\'", "");
            }
            if (data.Subject != null)
            {
                data.Subject = data.Subject.Replace("\'", "");
            }
            if (data.EnqAddress != null)
            {

                data.EnqAddress = data.EnqAddress.Replace("\"", "");
            }
            if (data.EnqMessage != null)
            {
                data.EnqMessage = data.EnqMessage.Replace("\"", "");
            }
            if (data.Subject != null)
            {  data.Subject = data.Subject.Replace("\"", "");    }
            if (data.Mob != null)
            { data.Mob = data.Mob.Replace("+91-", "");  }
          

                try
            {
                using (var connection = _connections.NewFor<ContactsRow>())
                {
                    if (!(string.IsNullOrEmpty)(data.GlUserCompanyName))
                    {
                        Contacttyp = 2;
                    }
                    else
                    {
                        Contacttyp = 1;
                    }
                    // string AdditionalCon = data.GlUserCompanyName + ", Message:" + data.EnqMessage;
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId) VALUES('" + Contacttyp + "','81','1','" + data.SenderName + "','" + data.Mob + "','" + data.SenderEmail + "','" + data.EnqAddress + "','" + data.EnqMessage + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

                    connection.Execute(str);



                    var c = ContactsRow.Fields;
                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .OrderBy(c.Id, desc: true)
                        );



                    if (data.SenderName != LastContact.Name)
                    {
                        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    }


                    var s = SourceRow.Fields;
                    var sourceMaster = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "IndiaMART") || (s.Source == "IndiaMart") || (s.Source == "Indiamart") || (s.Source == "India MART") || (s.Source == "India Mart") || (s.Source == "India mart"))
                        );

                    if (sourceMaster == null)
                    {
                        response.Status = "Error: IndiaMART Source not found in Source master\nKindly add in masters and try again";

                        throw new Exception("IndiaMART Source not found in Source master\nKindly add in masters and try again");
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                        );

                    if (stageMaster == null)
                    {
                        response.Status = "Error: Enquiry Stage not found in Stage master\nKindly add in masters and try again";

                        throw new Exception(" Enquiry Stage not found in Stage master\nKindly add in masters and try again");
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                      string date = Convert.ToDateTime(data.DateRe).ToString("yyyy-MM-dd");
                    string feedback = data.EnqMessage + " Feedback-" + data.Feedback;
                  
                    //  string Additional = data.GlUserCompanyName + ", Message:" + data.EnqMessage;
                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + feedback + "','" + sourceMaster.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update IndiaMartDetails SET IsMoved = 1 WHERE Id = " + request.Id);

                 //   connection.Execute("Update Contacts SET Name='" + data.SenderName + "' WHERE Id=" + LastContact.Id);

                    //string stru = "Update Contacts Set Name='" + data.SenderName + "' where Id='" + LastContact.Id + "'";
                    //connection.Execute(stru);

                    var IndiaMartSettings = new IndiaMartConfigurationRow();

                    var i = IndiaMartConfigurationRow.Fields;
                    IndiaMartSettings = connection.First<IndiaMartConfigurationRow>(l => l
                    .SelectTableFields()
                        .Select(i.AutoEmail)
                        .Select(i.AutoSms)
                        .Select(i.Sender)
                             .Select(i.Subject)
                             .Select(i.SMSTemplate)
                             .Select(i.SmsTemplateId)
                             .Select(i.EmailTemplate)
                             .Select(i.Host)
                             .Select(i.Port)
                             .Select(i.SSL)
                             .Select(i.EmailId)
                             .Select(i.EmailPassword)
                    );

                    if (IndiaMartSettings.AutoEmail.Value == true && !data.SenderEmail.IsNullOrEmpty())
                    {
                        var u = UserRow.Fields;
                        var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));

                        try
                        {
                            if (IndiaMartSettings.Host != null)
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(IndiaMartSettings.EmailId, IndiaMartSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(data.SenderEmail);
                                mm.Subject = IndiaMartSettings.Subject;
                                var msg = IndiaMartSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", data.SenderName.IsNullOrEmpty() ? "Customer" : data.SenderName);
                                mm.Body = msg;

                                if (IndiaMartSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, IndiaMartSettings.EmailId, IndiaMartSettings.EmailPassword, (Boolean)IndiaMartSettings.SSL, IndiaMartSettings.Host, IndiaMartSettings.Port.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, IndiaMartSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(data.SenderEmail);
                                mm.Subject = IndiaMartSettings.Subject;
                                var msg = IndiaMartSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", data.SenderName.IsNullOrEmpty() ? "Customer" : data.SenderName);
                                mm.Body = msg;

                                if (IndiaMartSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message.ToString());
                        }
                    }

                    if (IndiaMartSettings.AutoSms.Value == true && !data.Mob.IsNullOrEmpty())
                    {
                        String msg = IndiaMartSettings.SMSTemplate;
                        String tempId = IndiaMartSettings.SmsTemplateId;
                        msg = msg.Replace("#customername", data.SenderName.IsNullOrEmpty() ? "Customer" : data.SenderName);
                        data.Mob = data.Mob.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(data.Mob, msg,tempId);
                    }
                }
                response.Id = LastEnquiry.Id.Value;
                response.Status = "Success";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = "Error\n" + ex.ToString();
            }

            return response;

        }

        [HttpPost]
        public BulkImportResponse BulkMoveToEnquiry(IUnitOfWork uow, BulkRequest request)
        {

            var response = new BulkImportResponse();
            var Contacttyp = 2;
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();

            List<IndiaMartDetailsRow> data1;
            var ind1 = IndiaMartDetailsRow.Fields;


           // var data = new IndiaMartDetailsRow();

            using (var connection = _connections.NewFor<IndiaMartDetailsRow>())
            {
                data1 = connection.List<IndiaMartDetailsRow>(q => q
                   .SelectTableFields()
                   .Select(ind1.SenderName)
                   .Select(ind1.Subject)
                   .Select(ind1.DateRe)
                   .Select(ind1.Mob)
                   .Select(ind1.EnqAddress)
                   .Select(ind1.SenderEmail)
                   .Select(ind1.GlUserCompanyName)
                   .Select(ind1.EnqMessage)
                   .Where(ind1.Id.In(request.EnqIds)) // Include only selected rows
                    .Where(ind1.IsMoved == "false")
                   );

                int NoOfEnquiries = data1.Count;
                int NoOfUsrs = request.Ids.Count();
                // int NoOfUsrs1 = request.EnqIds.Count();

                if (NoOfUsrs < 1)
                {
                    response.ErrorList.Add("No users selected");

                    return response;
                }

                int recordPerUser = NoOfEnquiries / NoOfUsrs;

                int counter = 0;
                int currRecCount = 1;
                string TuserId = request.Ids.ElementAt(counter);

                foreach (var item in data1)
                {
                    if (item.EnqAddress != null)
                    { item.EnqAddress = item.EnqAddress.Replace(",", " "); }
                    if (item.EnqMessage != null)
                    { item.EnqMessage = Uri.UnescapeDataString(item.EnqMessage).Replace(",", ""); }
                    if (item.Subject != null)
                    {
                        item.Subject = item.Subject.Replace(",", " ");          //  data.GlUserCompanyName = data.GlUserCompanyName.Replace(",", " ");
                    }
                    if (item.EnqAddress != null)
                    {
                        item.EnqAddress = item.EnqAddress.Replace("\'", "");
                    }
                    if (item.EnqMessage != null)
                    {
                        item.EnqMessage = item.EnqMessage.Replace("\'", "");
                    }
                    if (item.Subject != null)
                    {
                        item.Subject = item.Subject.Replace("\'", "");
                    }
                    if (item.EnqAddress != null)
                    {

                        item.EnqAddress = item.EnqAddress.Replace("\"", "");
                    }
                    if (item.EnqMessage != null)
                    {
                        item.EnqMessage = item.EnqMessage.Replace("\"", "");
                    }
                    if (item.Subject != null)
                    { item.Subject = item.Subject.Replace("\"", ""); }
                    if (item.Mob != null)
                    { item.Mob = item.Mob.Replace("+91-", ""); }


                    try
                    {
                        //using (var connection = _connections.NewFor<ContactsRow>())
                        //{
                        var UserId = TuserId;
                        if (!(string.IsNullOrEmpty)(item.GlUserCompanyName))
                        {
                            Contacttyp = 2;
                        }
                        else
                        {
                            Contacttyp = 1;
                        }
                        // string AdditionalCon = data.GlUserCompanyName + ", Message:" + data.EnqMessage;
                        string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId) VALUES('" + Contacttyp + "','81','1','" + item.SenderName + "','" + item.Mob + "','" + item.SenderEmail + "','" + item.EnqAddress + "','" + item.EnqMessage + "','" + UserId + "','" +UserId + "')";

                        connection.Execute(str);



                        var c = ContactsRow.Fields;
                        var LastContact = connection.First<ContactsRow>(l => l
                            .Select(c.Id)
                            .Select(c.Name)
                            .OrderBy(c.Id, desc: true)
                            );



                        if (item.SenderName != LastContact.Name)
                        {
                            response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                            throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                        }


                        var s = SourceRow.Fields;
                        var sourceMaster = connection.TryFirst<SourceRow>(l => l
                            .Select(s.Id)
                            .Select(s.Source)
                            .Where((s.Source == "IndiaMART") || (s.Source == "IndiaMart") || (s.Source == "Indiamart") || (s.Source == "India MART") || (s.Source == "India Mart") || (s.Source == "India mart"))
                            );

                        if (sourceMaster == null)
                        {
                            response.Status = "Error: IndiaMART Source not found in Source master\nKindly add in masters and try again";

                            throw new Exception("IndiaMART Source not found in Source master\nKindly add in masters and try again");
                        }

                        var st = StageRow.Fields;
                        var stageMaster = connection.TryFirst<StageRow>(l => l
                            .Select(st.Id)
                            .Select(st.Stage)
                            .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                            );

                        if (stageMaster == null)
                        {
                            response.Status = "Error: Enquiry Stage not found in Stage master\nKindly add in masters and try again";

                            throw new Exception(" Enquiry Stage not found in Stage master\nKindly add in masters and try again");
                        }

                        GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                        string date = Convert.ToDateTime(item.DateRe).ToString("yyyy-MM-dd");
                        string feedback = item.EnqMessage + " Feedback-" + item.Feedback;

                        //  string Additional = data.GlUserCompanyName + ", Message:" + data.EnqMessage;
                        var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + feedback + "','" + sourceMaster.Id + "','" + stageMaster.Id + "','" + UserId + "','" + UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1')";

                        connection.Execute(str1);

                        var e = EnquiryRow.Fields;
                        LastEnquiry = connection.First<EnquiryRow>(l => l
                            .Select(e.Id)
                            .OrderBy(c.Id, desc: true)
                            );

                        connection.Execute("Update IndiaMartDetails SET IsMoved = 1 WHERE Id = " + item.Id);



                        var IndiaMartSettings = new IndiaMartConfigurationRow();

                        var i = IndiaMartConfigurationRow.Fields;
                        IndiaMartSettings = connection.First<IndiaMartConfigurationRow>(l => l
                        .SelectTableFields()
                            .Select(i.AutoEmail)
                            .Select(i.AutoSms)
                            .Select(i.Sender)
                                 .Select(i.Subject)
                                 .Select(i.SMSTemplate)
                                 .Select(i.EmailTemplate)
                                 .Select(i.Host)
                                 .Select(i.Port)
                                 .Select(i.SSL)
                                 .Select(i.EmailId)
                                 .Select(i.EmailPassword)
                        );

                        if (IndiaMartSettings.AutoEmail.Value == true && !item.SenderEmail.IsNullOrEmpty())
                        {
                            var u = UserRow.Fields;
                            var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                                .SelectTableFields()
                                .Select(u.Host)
                                .Select(u.Port)
                                .Select(u.SSL)
                                .Select(u.EmailId)
                                .Select(u.EmailPassword));

                            try
                            {
                                if (IndiaMartSettings.Host != null)
                                {
                                    MailMessage mm = new MailMessage();
                                    var addr = new MailAddress(IndiaMartSettings.EmailId, IndiaMartSettings.Sender);

                                    mm.From = addr;
                                    mm.Sender = addr;
                                    mm.To.Add(item.SenderEmail);
                                    mm.Subject = IndiaMartSettings.Subject;
                                    var msg = IndiaMartSettings.EmailTemplate;
                                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                    msg = msg.Replace("#customername", item.SenderName.IsNullOrEmpty() ? "Customer" : item.SenderName);
                                    mm.Body = msg;

                                    if (IndiaMartSettings.Attachment != null)
                                    {
                                        JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                                        foreach (var f in att)
                                        {
                                            if (f["Filename"].HasValue())
                                            {
                                                var filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                                mm.Attachments.Add(new Attachment(filePath));
                                            }
                                        }
                                    }

                                    mm.IsBodyHtml = true;

                                    EmailHelper.Send(mm, IndiaMartSettings.EmailId, IndiaMartSettings.EmailPassword, (Boolean)IndiaMartSettings.SSL, IndiaMartSettings.Host, IndiaMartSettings.Port.Value);
                                }
                                else
                                {
                                    MailMessage mm = new MailMessage();
                                    var addr = new MailAddress(User.EmailId, IndiaMartSettings.Sender);

                                    mm.From = addr;
                                    mm.Sender = addr;
                                    mm.To.Add(item.SenderEmail);
                                    mm.Subject = IndiaMartSettings.Subject;
                                    var msg = IndiaMartSettings.EmailTemplate;
                                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                    msg = msg.Replace("#customername", item.SenderName.IsNullOrEmpty() ? "Customer" : item.SenderName);
                                    mm.Body = msg;

                                    if (IndiaMartSettings.Attachment != null)
                                    {
                                        JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                                        foreach (var f in att)
                                        {
                                            if (f["Filename"].HasValue())
                                            {
                                                var filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                                mm.Attachments.Add(new Attachment(filePath));
                                            }
                                        }
                                    }

                                    mm.IsBodyHtml = true;

                                    EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message.ToString());
                            }
                        }

                        if (IndiaMartSettings.AutoSms.Value == true && !item.Mob.IsNullOrEmpty())
                        {
                            String msg = IndiaMartSettings.SMSTemplate;
                            msg = msg.Replace("#customername", item.SenderName.IsNullOrEmpty() ? "Customer" : item.SenderName);
                            item.Mob = item.Mob.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                            SMSHelper.SendSMS(item.Mob, msg,msg);
                        }
                        if (currRecCount == recordPerUser)
                        {
                            if ((counter < NoOfUsrs) && (counter != (NoOfUsrs - 1)))
                            {
                                TuserId = request.Ids.ElementAt(counter + 1);
                                currRecCount = 0;
                            }
                            counter++;
                        }
                        currRecCount++;

                        response.Inserted = response.Inserted + 1;
                        //response.Inserted = LastEnquiry.Id.Value;
                        
                     }
                    catch (Exception ex)
                    {                        
                        response.Status = "Error\n" + ex.ToString();
                    }
                }
            }

            return response;

        }



        internal class IndiaMartDetail
        {
            public int Id { get; set; }
            public string RN { get; set; }
            public string QUERY_ID { get; set; }
            public string QTYPE { get; set; }
            public string SENDERNAME { get; set; }
            public string SENDEREMAIL { get; set; }
            public string SUBJECT { get; set; }
            public string DATE_RE { get; set; }
            public string DATE_R { get; set; }
            public string DATE_TIME_RE { get; set; }
            public string GLUSR_USR_COMPANYNAME { get; set; }
            public string READ_STATUS { get; set; }
            public string SENDER_GLUSR_USR_ID { get; set; }
            public string MOB { get; set; }
            public string COUNTRY_FLAG { get; set; }
            public string QUERY_MODID { get; set; }
            public string LOG_TIME { get; set; }
            public string QUERY_MODREFID { get; set; }
            public string DIR_QUERY_MODREF_TYPE { get; set; }
            public string REMOTE_IP { get; set; }
            public string ORG_SENDER_GLUSR_ID { get; set; }
            public string ENQ_MESSAGE { get; set; }
            public string ENQ_ADDRESS { get; set; }
            public string ENQ_CALL_DURATION { get; set; }
            public string ENQ_RECEIVER_MOB { get; set; }
            public string ENQ_CITY { get; set; }
            public string ENQ_STATE { get; set; }
            public string OTHER_FOLDER_CNT { get; set; }
            public string TOTAL_COUNT { get; set; }
            public string IM_MEMBER_SINCE { get; set; }
            public string QUERY_STAGE_MASTER_DESC { get; set; }
            public dynamic PRODUCT_NAME { get; internal set; }
            public dynamic COUNTRY_ISO { get; internal set; }
            public dynamic EMAIL_ALT { get; internal set; }
            public dynamic MOBILE_ALT { get; internal set; }
            public dynamic PHONE { get; internal set; }
            public dynamic PHONE_ALT { get; internal set; }
        }
    }
}
