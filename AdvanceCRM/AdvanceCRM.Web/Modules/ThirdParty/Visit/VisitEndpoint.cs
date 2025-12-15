
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Masters;
    using AdvanceCRM.ThirdParty;
    using AdvanceCRM.Settings;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    using System.Net.Mail;
    
    using MyRepository = Repositories.VisitRepository;
    using MyRow = VisitRow;
    using System.Collections.Generic;
    using System.Linq;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/ThirdParty/Visit/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class VisitController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public VisitController(ISqlConnections connections)
        {
            _connections = connections;
        }

        public VisitController()
            : this(Dependency.Resolve<ISqlConnections>())
        {
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

        [ServiceAuthorize("Visit:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.VisitColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "VisitEnquiries_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();

            var model = new MyRow();

            using (var connection = _connections.NewFor<MyRow>())
            {
                var ind = MyRow.Fields;
                model = connection.TryById<MyRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Name)
                   .Select(ind.MobileNo)
                   .Select(ind.Email)
                   .Select(ind.DateNTime)
                   .Select(ind.Address)
                   .Select(ind.Requirements)
                   .Select(ind.Feedback)

                   );


                UData = connection.First<UserRow>(q => q
                .SelectTableFields()
                .Select(br.CompanyId)
                .Where(br.UserId == Context.User.GetIdentifier())
               );
            }

            try
            {
                using (var connection = _connections.NewFor<ContactsRow>())
                {

                    var c = ContactsRow.Fields;
                    var LastContact = connection.TryFirst<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .Where(c.Phone == model.MobileNo)
                        );

                    if (LastContact == null)
                    {
                        string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,OwnerId,AssignedId) VALUES('1','1'," + "'" + model.Name + "','" + model.MobileNo + "','" + model.Email + "','" + model.Address + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

                        connection.Execute(str);

                        LastContact = connection.First<ContactsRow>(l => l
                            .Select(c.Id)
                            .Select(c.Name)
                            .OrderBy(c.Id, desc: true)
                            );

                        if (model.Name != LastContact.Name)
                        {
                            response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                            throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                        }
                    }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "Visit") || (s.Source == "VISIT") || (s.Source == "visit"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('Visit')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "visit")
                        );
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                        );

                    if (stageMaster == null)
                    {
                        string str2 = "INSERT INTO Stage(Stage, Type) VALUES('Initial', 1)";
                        connection.Execute(str2);

                        stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Visit")
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(model.DateNTime).ToString("yyyy-MM-dd");
                    string feedback = model.Requirements + "" + model.Feedback;

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','"+date+"','1','" + feedback + "','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update Visit SET IsMoved = 1 WHERE Id = " + request.Id);


                    //var WebsiteEnquirySettings = new WebsiteEnquiryConfigurationRow();

                    //var i = WebsiteEnquiryConfigurationRow.Fields;
                    //WebsiteEnquirySettings = connection.First<WebsiteEnquiryConfigurationRow>(l => l
                    //.SelectTableFields()
                    //    .Select(i.AutoEmail)
                    //    .Select(i.AutoSms)
                    //    .Select(i.Sender)
                    //         .Select(i.Subject)
                    //         .Select(i.SMSTemplate)
                    //         .Select(i.EmailTemplate)
                    //         .Select(i.Host)
                    //         .Select(i.Port)
                    //         .Select(i.SSL)
                    //         .Select(i.EmailId)
                    //         .Select(i.EmailPassword)
                    //);

                    //if (WebsiteEnquirySettings.AutoEmail.Value == true && !model.Email.IsNullOrEmpty())
                    //{
                    //    var u = UserRow.Fields;
                    //    var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    //        .SelectTableFields()
                    //        .Select(u.Host)
                    //        .Select(u.Port)
                    //        .Select(u.SSL)
                    //        .Select(u.EmailId)
                    //        .Select(u.EmailPassword));

                    //    try
                    //    {
                    //        if (WebsiteEnquirySettings.Host != null)
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(WebsiteEnquirySettings.EmailId, WebsiteEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = WebsiteEnquirySettings.Subject;
                    //            var msg = WebsiteEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //            mm.Body = msg;

                    //            if (WebsiteEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(WebsiteEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }

                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, WebsiteEnquirySettings.EmailId, WebsiteEnquirySettings.EmailPassword, (Boolean)WebsiteEnquirySettings.SSL, WebsiteEnquirySettings.Host, WebsiteEnquirySettings.Port.Value);
                    //        }
                    //        else
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(User.EmailId, WebsiteEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = WebsiteEnquirySettings.Subject;
                    //            var msg = WebsiteEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //            mm.Body = msg;

                    //            if (WebsiteEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(WebsiteEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }
                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                    //        }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            throw new Exception(ex.Message.ToString());
                    //        }
                    //    }

                    //    if (WebsiteEnquirySettings.AutoSms.Value == true && !model.Phone.IsNullOrEmpty())
                    //    {
                    //        String msg = WebsiteEnquirySettings.SMSTemplate;
                    //        msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //        model.Phone = model.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    //        SMSHelper.SendSMS(model.Phone, msg);
                    //    }
                    //}
                    response.Id = LastEnquiry.Id.Value;
                    response.Status = "Success";
                }
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
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();

            List<VisitRow> data1;
            var ind1 = VisitRow.Fields;

            using (var connection = _connections.NewFor<VisitRow>())
            {

                data1 = connection.List<VisitRow>(q => q
                  .SelectTableFields()
                  .Select(ind1.Name)
                  .Select(ind1.MobileNo)
                  .Select(ind1.Email)
                  .Select(ind1.DateNTime)
                  .Select(ind1.Address)
                  .Select(ind1.Requirements)
                  .Select(ind1.Feedback)
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
                    var c = ContactsRow.Fields;
                    var LastContact = connection.TryFirst<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .Where(c.Phone == item.MobileNo)
                        );

                    if (LastContact == null)
                    {
                        string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,OwnerId,AssignedId) VALUES('1','1'," + "'" + item.Name + "','" + item.MobileNo + "','" + item.Email + "','" + item.Address + "','" + UserId + "','" + UserId + "')";

                        connection.Execute(str);

                        LastContact = connection.First<ContactsRow>(l => l
                            .Select(c.Id)
                            .Select(c.Name)
                            .OrderBy(c.Id, desc: true)
                            );

                        if (item.Name != LastContact.Name)
                        {
                            response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                            throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                        }
                    }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "Visit") || (s.Source == "VISIT") || (s.Source == "visit"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('Visit')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "visit")
                        );
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                        );

                    if (stageMaster == null)
                    {
                        string str2 = "INSERT INTO Stage(Stage, Type) VALUES('Initial', 1)";
                        connection.Execute(str2);

                        stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Visit")
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(item.DateNTime).ToString("yyyy-MM-dd");
                    string feedback = item.Requirements + "" + item.Feedback;

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + feedback + "','" + Source.Id + "','" + stageMaster.Id + "','" + UserId + "','" + UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1')";

                    connection.Execute(str1);

                    var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update Visit SET IsMoved = 1 WHERE Id = " + item.Id);


                    //var WebsiteEnquirySettings = new WebsiteEnquiryConfigurationRow();

                    //var i = WebsiteEnquiryConfigurationRow.Fields;
                    //WebsiteEnquirySettings = connection.First<WebsiteEnquiryConfigurationRow>(l => l
                    //.SelectTableFields()
                    //    .Select(i.AutoEmail)
                    //    .Select(i.AutoSms)
                    //    .Select(i.Sender)
                    //         .Select(i.Subject)
                    //         .Select(i.SMSTemplate)
                    //         .Select(i.EmailTemplate)
                    //         .Select(i.Host)
                    //         .Select(i.Port)
                    //         .Select(i.SSL)
                    //         .Select(i.EmailId)
                    //         .Select(i.EmailPassword)
                    //);

                    //if (WebsiteEnquirySettings.AutoEmail.Value == true && !model.Email.IsNullOrEmpty())
                    //{
                    //    var u = UserRow.Fields;
                    //    var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    //        .SelectTableFields()
                    //        .Select(u.Host)
                    //        .Select(u.Port)
                    //        .Select(u.SSL)
                    //        .Select(u.EmailId)
                    //        .Select(u.EmailPassword));

                    //    try
                    //    {
                    //        if (WebsiteEnquirySettings.Host != null)
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(WebsiteEnquirySettings.EmailId, WebsiteEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = WebsiteEnquirySettings.Subject;
                    //            var msg = WebsiteEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //            mm.Body = msg;

                    //            if (WebsiteEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(WebsiteEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }

                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, WebsiteEnquirySettings.EmailId, WebsiteEnquirySettings.EmailPassword, (Boolean)WebsiteEnquirySettings.SSL, WebsiteEnquirySettings.Host, WebsiteEnquirySettings.Port.Value);
                    //        }
                    //        else
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(User.EmailId, WebsiteEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = WebsiteEnquirySettings.Subject;
                    //            var msg = WebsiteEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //            mm.Body = msg;

                    //            if (WebsiteEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(WebsiteEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }
                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                    //        }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            throw new Exception(ex.Message.ToString());
                    //        }
                    //    }

                    //    if (WebsiteEnquirySettings.AutoSms.Value == true && !model.Phone.IsNullOrEmpty())
                    //    {
                    //        String msg = WebsiteEnquirySettings.SMSTemplate;
                    //        msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //        model.Phone = model.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    //        SMSHelper.SendSMS(model.Phone, msg);
                    //    }
                    //}
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



    }
}
