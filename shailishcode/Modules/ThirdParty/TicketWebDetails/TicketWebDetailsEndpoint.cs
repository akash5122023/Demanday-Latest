
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Serenity.Services;
    using System.Data;
    using System.IO;
    
    using Newtonsoft.Json.Linq;   
    using Serenity.Reporting;
    using System.Net.Mail;
    
    using Serenity.Web;
    using System;

    using AdvanceCRM.Settings;
    using AdvanceCRM.ThirdParty;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Services;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Administration;
 
  
    using MyRepository = Repositories.TicketWebDetailsRepository;
    using MyRow = TicketWebDetailsRow;
    using AdvanceCRM.Common;

    [Route("Services/ThirdParty/TicketWebDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class TicketWebDetailsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public TicketWebDetailsController(ISqlConnections connections, IWebHostEnvironment env)
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

        [ServiceAuthorize("TicketWebDetails:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.TicketWebDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "TicketWebDetails_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public StandardResponse MoveToTicket(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            var Contacttyp = 2;
            TicketRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();

            var data = new TicketWebDetailsRow();

            using (var connection = _connections.NewFor<TicketWebDetailsRow>())
            {
                var ind = TicketWebDetailsRow.Fields;
                data = connection.TryById<TicketWebDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Name)
                   .Select(ind.Phone)
                   .Select(ind.Email)
                   .Select(ind.DateTime)
                   .Select(ind.Address)
                   .Select(ind.ProductName)
                   .Select(ind.Requirement)
                   .Select(ind.PurchaseDate)
                   .Select(ind.ComplaintDetails)
                   .Select(ind.Attachment)
                   );
                UData = connection.First<UserRow>(q => q
              .SelectTableFields()
              .Select(br.CompanyId)
              .Where(br.UserId == Context.User.GetIdentifier())
             );
            }

            //if (data.EnqAddress != null)
            //{ data.EnqAddress = data.EnqAddress.Replace(",", " "); }
            //if (data.EnqMessage != null)
            //{ data.EnqMessage = Uri.UnescapeDataString(data.EnqMessage).Replace(",", ""); }
            //if (data.Subject != null)
            //{
            //    data.Subject = data.Subject.Replace(",", " ");          //  data.GlUserCompanyName = data.GlUserCompanyName.Replace(",", " ");
            //}
            //if (data.EnqAddress != null)
            //{
            //    data.EnqAddress = data.EnqAddress.Replace("\'", "");
            //}
            //if (data.EnqMessage != null)
            //{
            //    data.EnqMessage = data.EnqMessage.Replace("\'", "");
            //}
            //if (data.Subject != null)
            //{
            //    data.Subject = data.Subject.Replace("\'", "");
            //}
            //if (data.EnqAddress != null)
            //{

            //    data.EnqAddress = data.EnqAddress.Replace("\"", "");
            //}
            //if (data.EnqMessage != null)
            //{
            //    data.EnqMessage = data.EnqMessage.Replace("\"", "");
            //}
            //if (data.Subject != null)
            //{ data.Subject = data.Subject.Replace("\"", ""); }
            //if (data.Mob != null)
            //{ data.Mob = data.Mob.Replace("+91-", ""); }


            try
            {
                using (var connection = _connections.NewFor<ContactsRow>())
                {

                    var c = ContactsRow.Fields;

                    //var LastContactc = connection.Count<ContactsRow>(c.Phone == data.Phone);
                   
                    //Contacttyp = 1;
                    //if (LastContactc == 0)
                    //{

                    //    string AdditionalCon1 = data.ComplaintDetails + ", Purchase Date:" + data.PurchaseDate;
                    //    string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId) VALUES('" + Contacttyp + "','1','" + data.Name + "','" + data.Phone + "','" + data.Email + "','" + data.Address + "','" + AdditionalCon1 + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

                    //    connection.Execute(str);
                    //}
                    //    //var c = ContactsRow.Fields;
                    //    var LastContact = connection.First<ContactsRow>(l => l
                    //        .Select(c.Id)
                    //        .Select(c.Name)
                    //        .OrderBy(c.Id, desc: true)
                    //        );

                    //    if (data.Name != LastContact.Name)
                    //    {
                    //        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                    //        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    //    }
                   
                    string date = Convert.ToDateTime(data.DateTime).ToString("yyyy-MM-dd");
                    string AdditionalCon = data.ComplaintDetails + ", Purchase Date:" + data.PurchaseDate;
                    var str1 = "INSERT INTO Ticket(Name,Phone,ProductsId,Priority,AdditionalDetails,ComplaintDetails,AssignedId) VALUES('" + data.Name + "','"+data.Phone+"','1','1','" + data.Requirement + "','" + AdditionalCon + "','" + Context.User.GetIdentifier() + "')";

                    connection.Execute(str1);

                    var e = TicketRow.Fields;
                    LastEnquiry = connection.First<TicketRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update TicketWebDetails SET IsMoved = 1 WHERE Id = " + request.Id);

                    var IndiaMartSettings = new TicketWebRow();

                    var i = TicketWebRow.Fields;
                    IndiaMartSettings = connection.First<TicketWebRow>(l => l
                    .SelectTableFields()
                        .Select(i.AutoEmail)
                        .Select(i.AutoSms)
                        .Select(i.Sender)
                             .Select(i.Subject)
                             .Select(i.SmsTemplate)
                             //.Select(i.templateID)
                             .Select(i.EmailTemplate)
                             .Select(i.Host)
                             .Select(i.Port)
                             .Select(i.Ssl)
                             .Select(i.EmailId)
                             .Select(i.EmailPassword)
                    );

                    if (IndiaMartSettings.AutoEmail.Value == true && !data.Email.IsNullOrEmpty())
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
                                mm.To.Add(data.Email);
                                mm.Subject = IndiaMartSettings.Subject;
                                var msg = IndiaMartSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
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

                                EmailHelper.Send(mm, IndiaMartSettings.EmailId, IndiaMartSettings.EmailPassword, (Boolean)IndiaMartSettings.Ssl, IndiaMartSettings.Host, IndiaMartSettings.Port.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, IndiaMartSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(data.Email);
                                mm.Subject = IndiaMartSettings.Subject;
                                var msg = IndiaMartSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
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

                    if (IndiaMartSettings.AutoSms.Value == true && !data.Phone.IsNullOrEmpty())
                    {
                        String msg = IndiaMartSettings.SmsTemplate;
                        String tempId = IndiaMartSettings.TemplateId;
                        msg = msg.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
                        data.Phone = data.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(data.Phone, msg, msg);
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
    }
}
