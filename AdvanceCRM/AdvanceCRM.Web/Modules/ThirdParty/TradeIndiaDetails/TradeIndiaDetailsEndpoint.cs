
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using AdvanceCRM.ThirdParty;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Text.Json;

    //using System.Web.Script.Serialization;
    using MyRepository = Repositories.TradeIndiaDetailsRepository;
    using MyRow = TradeIndiaDetailsRow;
    using System.Linq;

    [Route("Services/ThirdParty/TradeIndiaDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class TradeIndiaDetailsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public TradeIndiaDetailsController(ISqlConnections connections)
        {
            _connections = connections;
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
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.TradeIndiaDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "TradeIndia_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }
        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }
        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();
            MyRow LastEnquiry;
            TradeIndiaConfigurationRow Config;
            DateTime StartDate, EndDate;
            using (var connection = _connections.NewFor<TradeIndiaConfigurationRow>())
            {
                var s = TradeIndiaConfigurationRow.Fields;
                Config = connection.TryFirst<TradeIndiaConfigurationRow>(q => q
                    .SelectTableFields()
                    .Select(s.Userid)
                    .Select(s.Profileid)
                    .Select(s.ApiKey)
                    );

                var i = MyRow.Fields;
                LastEnquiry = connection.TryFirst<MyRow>(q => q
                .SelectTableFields()
                .Select(i.GeneratedDateTime)
                .OrderBy(i.GeneratedDateTime, true)
                );
            }


           // try
            {
                if (LastEnquiry == null)
                {
                    StartDate = DateTime.Now.AddDays(-360);
                }
                else
                {
                    StartDate = Convert.ToDateTime(LastEnquiry.GeneratedDateTime.ToString());
                }
                //EndDate = StartDate.AddDay;

                EndDate = DateTime.Now;

                int days = Convert.ToInt32((EndDate - StartDate).TotalDays);
                for (int i = 0; i <= days; i++)
                {
                    DateTime daterange = StartDate.AddDays(+i);
                    // string uri= "https://www.tradeindia.com/utils/my_inquiry.html?userid=15260823&profile_id=32402784&key=9ca3e85e27673db94685766ed5b3488c&from_date=2020-03-18&to_date=2021-03-19&limit=500";
                    //// string uri = "http://mapi.indiamart.com/wservce/enquiry/listing/GLUSR_MOBILE/" + Config.MobileNumber + "/GLUSR_MOBILE_KEY/" + Config.ApiKey + "/Start_Time/" + StartDate.ToString("dd-MMM-yyyy") + "/End_Time/" + EndDate.ToString("dd-MMM-yyyy") + "/";
                    string uri = "https://www.tradeindia.com/utils/my_inquiry.html?userid=" + Config.Userid + "&profile_id=" + Config.Profileid + "&key=" + Config.ApiKey + "&from_date=" + daterange.ToString("yyyy-MM-dd") + "&to_date=" + daterange.ToString("yyyy-MM-dd") + "&limit=500";
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                    myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";

                    myHttpWebRequest.Timeout = 15000;
                    HttpWebResponse myHttpWebResponse;
                    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    var jsonResponse = reader.ReadToEnd();
                    var TradeIndiaResponseObjects = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonResponse);
                    if (TradeIndiaResponseObjects == null || TradeIndiaResponseObjects.Count == 0)
                    {
                        response.Status = jsonResponse;
                        return response;
                    }

                        List<string> Records = new List<string>();
                        foreach (Dictionary<string, object> TradeIndiaResponseObject in TradeIndiaResponseObjects)
                        {
                            TradeIndiadetails tradeIndiaDetails = new TradeIndiadetails();
                            tradeIndiaDetails.rfi_id = !TradeIndiaResponseObject.ContainsKey("rfi_id") ? "" : Convert.ToString(TradeIndiaResponseObject["rfi_id"]).Replace("'", "");
                            tradeIndiaDetails.source = !TradeIndiaResponseObject.ContainsKey("source") ? "" : Convert.ToString(TradeIndiaResponseObject["source"]).Replace("'", "");
                            tradeIndiaDetails.product_source = !TradeIndiaResponseObject.ContainsKey("product_source") ? "" : Convert.ToString(TradeIndiaResponseObject["product_source"]).Replace("'", "");
                            tradeIndiaDetails.generated_date = !TradeIndiaResponseObject.ContainsKey("generated_date") ? "" : Convert.ToString(TradeIndiaResponseObject["generated_date"]).Replace("'", "");
                            tradeIndiaDetails.generated_time = !TradeIndiaResponseObject.ContainsKey("generated_time") ? "" : Convert.ToString(TradeIndiaResponseObject["generated_time"]).Replace("'", "");
                            tradeIndiaDetails.inquiry_type = !TradeIndiaResponseObject.ContainsKey("inquiry_type") ? "" : Convert.ToString(TradeIndiaResponseObject["inquiry_type"]).Replace("'", "");
                            tradeIndiaDetails.subject = !TradeIndiaResponseObject.ContainsKey("subject") ? "" : Convert.ToString(TradeIndiaResponseObject["subject"]).Replace("'", "");
                            tradeIndiaDetails.product_name = !TradeIndiaResponseObject.ContainsKey("product_name") ? "" : Convert.ToString(TradeIndiaResponseObject["product_name"]).Replace("'", "");
                            tradeIndiaDetails.quantity = !TradeIndiaResponseObject.ContainsKey("quantity") ? "" : Convert.ToString(TradeIndiaResponseObject["quantity"]).Replace("'", "");
                            tradeIndiaDetails.order_value_min = !TradeIndiaResponseObject.ContainsKey("order_value_min") ? "" : Convert.ToString(TradeIndiaResponseObject["order_value_min"]).Replace("'", "");
                            tradeIndiaDetails.message = !TradeIndiaResponseObject.ContainsKey("message") ? "" : Convert.ToString(TradeIndiaResponseObject["message"]).Replace("'", "");
                            tradeIndiaDetails.sender_co = !TradeIndiaResponseObject.ContainsKey("sender_co") ? "" : Convert.ToString(TradeIndiaResponseObject["sender_co"]).Replace("'", "");
                            tradeIndiaDetails.sender_name = !TradeIndiaResponseObject.ContainsKey("sender_name") ? "" : Convert.ToString(TradeIndiaResponseObject["sender_name"]).Replace("'", "");
                            tradeIndiaDetails.sender_mobile = !TradeIndiaResponseObject.ContainsKey("sender_mobile") ? "" : Convert.ToString(TradeIndiaResponseObject["sender_mobile"]).Replace("'", "");
                            tradeIndiaDetails.sender_email = !TradeIndiaResponseObject.ContainsKey("sender_email") ? "" : Convert.ToString(TradeIndiaResponseObject["sender_email"]).Replace("'", "");
                            tradeIndiaDetails.sender_city = !TradeIndiaResponseObject.ContainsKey("sender_city") ? "" : Convert.ToString(TradeIndiaResponseObject["sender_city"]).Replace("'", "");
                            tradeIndiaDetails.sender_state = !TradeIndiaResponseObject.ContainsKey("sender_state") ? "" : Convert.ToString(TradeIndiaResponseObject["sender_state"]).Replace("'", "");
                            tradeIndiaDetails.sender_country = !TradeIndiaResponseObject.ContainsKey("sender_country") ? "" : Convert.ToString(TradeIndiaResponseObject["sender_country"]).Replace("'", "");
                            tradeIndiaDetails.month_slot = !TradeIndiaResponseObject.ContainsKey("month_slot") ? "" : Convert.ToString(TradeIndiaResponseObject["month_slot"]).Replace("'", "");
                            tradeIndiaDetails.landline_number = !TradeIndiaResponseObject.ContainsKey("landline_number") ? "" : Convert.ToString(TradeIndiaResponseObject["landline_number"]).Replace("'", "");
                            tradeIndiaDetails.pref_supp_location = !TradeIndiaResponseObject.ContainsKey("pref_supp_location") ? "" : Convert.ToString(TradeIndiaResponseObject["pref_supp_location"]).Replace("'", "");

                            DateTime generated_date_time = DateTime.Parse(tradeIndiaDetails.generated_date + " " + tradeIndiaDetails.generated_time);
                            Records.Add("IF NOT EXISTS (SELECT * FROM TradeIndiadetails WHERE RfiId ='" + tradeIndiaDetails.rfi_id + "')" +
                                "INSERT INTO TradeIndiaDetails ([RfiId], [Source], [ProductSource], [GeneratedDateTime], [InquiryType], [Subject], [ProductName], [Quantity], [OrderValueMin], [Message], [SenderCo], [SenderName], [SenderMobile], [SenderEmail], [SenderCity], [SenderState], [SenderCountry], [MonthSlot], [LandlineNumber], [PrefSuppLocation]) VALUES " +
                                "('" + tradeIndiaDetails.rfi_id + "','" +
                                    tradeIndiaDetails.source + "','" +
                                    tradeIndiaDetails.product_source + "','" +
                                    generated_date_time.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" +
                                    tradeIndiaDetails.inquiry_type + "','" +
                                    tradeIndiaDetails.subject + "','" +
                                    tradeIndiaDetails.product_name + "','" +
                                    tradeIndiaDetails.quantity + "','" +
                                    tradeIndiaDetails.order_value_min + "','" +
                                    tradeIndiaDetails.message + "','" +
                                    tradeIndiaDetails.sender_co + "','" +
                                    tradeIndiaDetails.sender_name + "','" +
                                    tradeIndiaDetails.sender_mobile + "','" +
                                    tradeIndiaDetails.sender_email + "','" +
                                    tradeIndiaDetails.sender_city + "','" +
                                    tradeIndiaDetails.sender_state + "','" +
                                    tradeIndiaDetails.sender_country + "','" +
                                    tradeIndiaDetails.month_slot + "','" +
                                    tradeIndiaDetails.landline_number + "','" +
                                    tradeIndiaDetails.pref_supp_location + "')");

                        }
                        Records.Reverse();
                        if (Records.Count > 0)
                        {
                            using (var innerConnection = _connections.NewFor<MyRow>())
                            {
                                for (int ij = 0; ij < Records.Count; ij++)
                                {
                                    try
                                    {
                                        innerConnection.Execute(Records[ij]);
                                    }
                                    catch (Exception ex)
                                    {
                                        response.Status = ex.Message.ToString();
                                    }

                                }
                            }
                        }
                    }
                }
                response.Status = "Sync completed";
            
            //catch (Exception ex)
            //{
            //    response.Status = ex.Message.ToString();
            //}

            return response;
        }

        [HttpPost]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {

            var response = new StandardResponse();
            var Contacttyp = 2;
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();

            var data = new MyRow();

            using (var connection = _connections.NewFor<MyRow>())
            {
                var ind = MyRow.Fields;
                data = connection.TryById<MyRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Id)
                    .Select(ind.GeneratedDateTime)
                    .Select(ind.Subject)
                    .Select(ind.Message)
                    .Select(ind.SenderCo)
                    .Select(ind.SenderName)
                    .Select(ind.SenderMobile)
                    .Select(ind.SenderEmail)
                    .Select(ind.SenderAddress)
                    .Select(ind.SenderCity)
                    .Select(ind.SenderState)
                    .Select(ind.SenderCountry)
                    .Select(ind.Feedback)
                   );

                UData = connection.First<UserRow>(q => q
               .SelectTableFields()
               .Select(br.CompanyId)
               .Where(br.UserId == Context.User.GetIdentifier())
              );
            }
            data.Subject = data.Subject.Replace("\"", "");

            data.SenderMobile = data.SenderMobile.Replace("+91-", "").Replace("+91", "");

            try
            {
                using (var connection = _connections.NewFor<ContactsRow>())
                {
                    if (data.SenderCo.IsEmptyOrNull())
                    {
                        Contacttyp = 1;
                    }
                    else
                    {
                        Contacttyp = 2;
                    }
                    string address = data.SenderAddress + (data.SenderCity.IsEmptyOrNull() ? "" : " , " + data.SenderCity)
                        + (data.SenderState.IsEmptyOrNull() ? "" : " , " + data.SenderState)
                        + (data.SenderCountry.IsEmptyOrNull() ? "" : " , " + data.SenderCountry);

                    string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId) VALUES('" + Contacttyp + "','1','" + data.SenderName + "','" + data.SenderMobile + "','" + data.SenderEmail + "','" + address + "','" + data.SenderCo + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

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
                        .Where((s.Source == "Trade India") || (s.Source == "TradeIndia"))
                        );

                    if (sourceMaster == null)
                    {
                        response.Status = "Error: TradeIndia Source not found in Source master\nKindly add in masters and try again";

                        throw new Exception("TradeIndia Source not found in Source master\nKindly add in masters and try again");
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

                    string date = Convert.ToDateTime(data.GeneratedDateTime).ToString("yyyy-MM-dd");
                     string feedback = data.Message + "" + data.Feedback;
                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" +feedback+ "','" + sourceMaster.Id + "','"+ stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update TradeIndiaDetails SET IsMoved = 1 WHERE Id = " + request.Id);

                    var TradeIndiaSettings = new TradeIndiaConfigurationRow();

                    var i = TradeIndiaConfigurationRow.Fields;
                    TradeIndiaSettings = connection.First<TradeIndiaConfigurationRow>(l => l
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

                    if (TradeIndiaSettings.AutoEmail.Value == true && !data.SenderEmail.IsNullOrEmpty())
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
                            if (TradeIndiaSettings.Host != null)
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(TradeIndiaSettings.EmailId, TradeIndiaSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(data.SenderEmail);
                                mm.Subject = TradeIndiaSettings.Subject;
                                var msg = TradeIndiaSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", data.SenderName.IsNullOrEmpty() ? "Customer" : data.SenderName);
                                mm.Body = msg;

                                if (TradeIndiaSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(TradeIndiaSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, TradeIndiaSettings.EmailId, TradeIndiaSettings.EmailPassword, (Boolean)TradeIndiaSettings.SSL, TradeIndiaSettings.Host, TradeIndiaSettings.Port.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, TradeIndiaSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(data.SenderEmail);
                                mm.Subject = TradeIndiaSettings.Subject;
                                var msg = TradeIndiaSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", data.SenderName.IsNullOrEmpty() ? "Customer" : data.SenderName);
                                mm.Body = msg;

                                if (TradeIndiaSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(TradeIndiaSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "upload", f["Filename"].ToString());
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

                    if (TradeIndiaSettings.AutoSms.Value == true && !data.SenderMobile.IsNullOrEmpty())
                    {
                        String msg = TradeIndiaSettings.SMSTemplate;
                        String tempId = TradeIndiaSettings.SmsTemplateId;
                        msg = msg.Replace("#customername", data.SenderName.IsNullOrEmpty() ? "Customer" : data.SenderName);
                        data.SenderMobile = data.SenderMobile.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(data.SenderMobile, msg,tempId);
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

            List<TradeIndiaDetailsRow> data1;
            var ind1 = TradeIndiaDetailsRow.Fields;

            using (var connection = _connections.NewFor<TradeIndiaDetailsRow>())
            {

                data1 = connection.List<TradeIndiaDetailsRow>(q => q
                  .SelectTableFields()
                  .Select(ind1.Id)
                   .Select(ind1.GeneratedDateTime)
                   .Select(ind1.Subject)
                   .Select(ind1.Message)
                   .Select(ind1.SenderCo)
                   .Select(ind1.SenderName)
                   .Select(ind1.SenderMobile)
                   .Select(ind1.SenderEmail)
                   .Select(ind1.SenderAddress)
                   .Select(ind1.SenderCity)
                   .Select(ind1.SenderState)
                   .Select(ind1.SenderCountry)
                   .Select(ind1.Feedback)
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
                    var UserId = TuserId;

                    item.Subject = item.Subject.Replace("\"", "");

                    item.SenderMobile = item.SenderMobile.Replace("+91-", "").Replace("+91", "");


                    if (item.SenderCo.IsEmptyOrNull())
                    {
                        Contacttyp = 1;
                    }
                    else
                    {
                        Contacttyp = 2;
                    }
                    string address = item.SenderAddress + (item.SenderCity.IsEmptyOrNull() ? "" : " , " + item.SenderCity)
                        + (item.SenderState.IsEmptyOrNull() ? "" : " , " + item.SenderState)
                        + (item.SenderCountry.IsEmptyOrNull() ? "" : " , " + item.SenderCountry);

                    string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId) VALUES('" + Contacttyp + "','1','" + item.SenderName + "','" + item.SenderMobile + "','" + item.SenderEmail + "','" + address + "','" + item.SenderCo + "','" + UserId + "','" + UserId + "')";

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
                        .Where((s.Source == "Trade India") || (s.Source == "TradeIndia"))
                        );

                    if (sourceMaster == null)
                    {
                        response.Status = "Error: TradeIndia Source not found in Source master\nKindly add in masters and try again";

                        throw new Exception("TradeIndia Source not found in Source master\nKindly add in masters and try again");
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

                    string date = Convert.ToDateTime(item.GeneratedDateTime).ToString("yyyy-MM-dd");
                    string feedback = item.Message + "" + item.Feedback;
                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + feedback + "','" + sourceMaster.Id + "','" + stageMaster.Id + "','" + UserId + "','" + UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1')";

                    connection.Execute(str1);

                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update TradeIndiaDetails SET IsMoved = 1 WHERE Id = " + item.Id);

                    var TradeIndiaSettings = new TradeIndiaConfigurationRow();

                    var i = TradeIndiaConfigurationRow.Fields;
                    TradeIndiaSettings = connection.First<TradeIndiaConfigurationRow>(l => l
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

                    if (TradeIndiaSettings.AutoEmail.Value == true && !item.SenderEmail.IsNullOrEmpty())
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
                            if (TradeIndiaSettings.Host != null)
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(TradeIndiaSettings.EmailId, TradeIndiaSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(item.SenderEmail);
                                mm.Subject = TradeIndiaSettings.Subject;
                                var msg = TradeIndiaSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", item.SenderName.IsNullOrEmpty() ? "Customer" : item.SenderName);
                                mm.Body = msg;

                                if (TradeIndiaSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(TradeIndiaSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, TradeIndiaSettings.EmailId, TradeIndiaSettings.EmailPassword, (Boolean)TradeIndiaSettings.SSL, TradeIndiaSettings.Host, TradeIndiaSettings.Port.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, TradeIndiaSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(item.SenderEmail);
                                mm.Subject = TradeIndiaSettings.Subject;
                                var msg = TradeIndiaSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", item.SenderName.IsNullOrEmpty() ? "Customer" : item.SenderName);
                                mm.Body = msg;

                                if (TradeIndiaSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(TradeIndiaSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "upload", f["Filename"].ToString());
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

                    if (TradeIndiaSettings.AutoSms.Value == true && !item.SenderMobile.IsNullOrEmpty())
                    {
                        String msg = TradeIndiaSettings.SMSTemplate;
                        //String msg = TradeIndiaSettings.te
                        msg = msg.Replace("#customername", item.SenderName.IsNullOrEmpty() ? "Customer" : item.SenderName);
                        item.SenderMobile = item.SenderMobile.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(item.SenderMobile, msg,msg);
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
                }
            }
           

            return response;

        }

        internal class TradeIndiadetails
        {
            public string rfi_id { get; set; }
            public string source { get; set; }
            public string product_source { get; set; }
            public string generated_date { get; set; }
            public string generated_time { get; set; }
            public string inquiry_type { get; set; }
            public string subject { get; set; }
            public string product_name { get; set; }
            public string quantity { get; set; }
            public string order_value_min { get; set; }
            public string message { get; set; }
            public string sender_co { get; set; }
            public string sender_name { get; set; }
            public string sender_mobile { get; set; }
            public string sender_email { get; set; }
            public string sender_city { get; set; }
            public string sender_state { get; set; }
            public string sender_country { get; set; }
            public string month_slot { get; set; }
            public string landline_number { get; set; }
            public string pref_supp_location { get; set; }
        }
    }
}
