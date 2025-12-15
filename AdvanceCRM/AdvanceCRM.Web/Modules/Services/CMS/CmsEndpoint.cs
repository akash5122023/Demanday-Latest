
namespace AdvanceCRM.Services.Endpoints
{
    using Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Quotation.Endpoints;
    using AdvanceCRM.Quotation;
  
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    
    using Template;
    using MyRepository = Repositories.CMSRepository;
    using MyRow = CMSRow;

    [Route("Services/Services/CMS/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class CMSController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public CMSController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Create(uow, request);
        }

        [HttpPost]
        public SendMailResponse SendMail(IUnitOfWork uow, SendMailRequest request)
        {
            var response = new SendMailResponse();

            var data = new CMSData();

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                var c = CMSRow.Fields;

                data.CMS = connection.TryById<CMSRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.ContactsName)
                     .Select(c.ContactsPhone)
                     .Select(c.ContactsEmail)
                     .Select(c.ContactsAddress)
                     .Select(c.ProductsName)
                     .Select(c.CompletionDate)
                     .Select(c.ExpectedCompletion)
                     .Select(c.AssignedToDisplayName)
                     .Select(c.AssignedToEmail)
                     .Select(c.ComplaintComplaintType)
                     .Select(c.Instructions)
                     );

                var ct = CmsTemplateRow.Fields;
                data.Template = connection.TryFirst<CmsTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(ct.SMSTemplate)
                   .Select(ct.CCEmails)
                  .Where(ct.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));
            }

            try
            {
                List<string> emails;

                emails = null;

                if (data.Template.CCEmails != null)
                {
                    emails = data.Template.CCEmails.Split(',').ToList<string>();
                }

                //Configuration
                if (data.Template.Host != null)
                {
                    MailMessage mm = new MailMessage();
                    var addr = new MailAddress(data.Template.EmailId, data.Template.Sender);

                    mm.From = addr;
                    mm.Sender = addr;

                    if (request.MailType == "Customer")
                    {
                        mm.To.Add(data.CMS.ContactsEmail);
                    }
                    else if (request.MailType == "Engineer")
                    {
                        mm.To.Add(data.CMS.AssignedToEmail);
                    }
                    else if (request.MailType == "ClosedMail")
                    {
                        mm.To.Add(data.CMS.ContactsEmail);
                    }


                    if (emails != null)
                    {
                        for (int i = 0; i < emails.Count; i++)
                        {
                            mm.CC.Add(emails.ElementAt(i));
                        }
                    }

                    mm.Subject = data.Template.Subject;

                    String msg = null;
                    if (request.MailType == "Customer")
                    {
                        msg = data.Template.EmailTemplate;
                    }
                    else if (request.MailType == "Engineer")
                    {
                        msg = data.Template.EngineerEmailTemplate;
                    }
                    else if (request.MailType == "ClosedMail")
                    {
                        msg = data.Template.ClosedEmailTemplate;
                    }

                    msg = msg.Replace("#customername", data.CMS.ContactsName);
                    msg = msg.Replace("#product", data.CMS.ProductsName);
                    msg = msg.Replace("#complaintdate", data.CMS.Date.Value.ToShortDateString());
                    if (data.CMS.CompletionDate.HasValue)
                    {
                        msg = msg.Replace("#completiondate", data.CMS.CompletionDate.Value.ToShortDateString());
                    }
                    msg = msg.Replace("#complaintno", data.CMS.Id.Value.ToString());
                    msg = msg.Replace("#expecteddate", data.CMS.ExpectedCompletion.Value.ToShortDateString());
                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    msg = msg.Replace("#representative", data.CMS.AssignedToDisplayName);
                    msg = msg.Replace("#complaintdetails", data.CMS.ComplaintComplaintType);
                    msg = msg.Replace("#instructions", data.CMS.Instructions);
                    msg = msg.Replace("#phone", data.CMS.ContactsPhone);
                    msg = msg.Replace("#address", data.CMS.ContactsAddress);



                    mm.Body = msg;
                    mm.IsBodyHtml = true;
                    mm.Priority = MailPriority.High;

                    response.Status = EmailHelper.Send(mm, data.Template.EmailId, data.Template.EmailPassword, (Boolean)data.Template.SSL, data.Template.Host, data.Template.Port.Value);
                }
                else
                {
                    MailMessage mm = new MailMessage();
                    var addr = new MailAddress(data.User.EmailId, data.Template.Sender);

                    mm.From = addr;
                    mm.Sender = addr;

                    if (request.MailType == "Customer")
                    {
                        mm.To.Add(data.CMS.ContactsEmail);
                    }
                    else if (request.MailType == "Engineer")
                    {
                        mm.To.Add(data.CMS.AssignedToEmail);
                    }
                    else if (request.MailType == "ClosedMail")
                    {
                        mm.To.Add(data.CMS.ContactsEmail);
                    }


                    if (emails != null)
                    {
                        for (int i = 0; i < emails.Count; i++)
                        {
                            mm.CC.Add(emails.ElementAt(i));
                        }
                    }

                    mm.Subject = data.Template.Subject;

                    String msg = null;
                    if (request.MailType == "Customer")
                    {
                        msg = data.Template.EmailTemplate;
                    }
                    else if (request.MailType == "Engineer")
                    {
                        msg = data.Template.EngineerEmailTemplate;
                    }
                    else if (request.MailType == "ClosedMail")
                    {
                        msg = data.Template.ClosedEmailTemplate;
                    }

                    msg = msg.Replace("#customername", data.CMS.ContactsName);
                    msg = msg.Replace("#product", data.CMS.ProductsName);
                    msg = msg.Replace("#complaintdate", data.CMS.Date.Value.ToShortDateString());
                    if (data.CMS.CompletionDate.HasValue)
                    {
                        msg = msg.Replace("#completiondate", data.CMS.CompletionDate.Value.ToShortDateString());
                    }
                    msg = msg.Replace("#complaintno", data.CMS.Id.Value.ToString());
                    msg = msg.Replace("#expecteddate", data.CMS.ExpectedCompletion.Value.ToShortDateString());
                    msg = msg.Replace("#representative", data.CMS.AssignedToDisplayName);
                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    msg = msg.Replace("#complaintdetails", data.CMS.ComplaintComplaintType);
                    msg = msg.Replace("#instructions", data.CMS.Instructions);
                    msg = msg.Replace("#phone", data.CMS.ContactsPhone);
                    msg = msg.Replace("#address", data.CMS.ContactsAddress);

                    mm.Body = msg;
                    mm.IsBodyHtml = true;
                    mm.Priority = MailPriority.High;

                    response.Status = EmailHelper.Send(mm, data.User.EmailId, data.User.EmailPassword, (Boolean)data.User.SSL, data.User.Host, data.User.Port.Value);

                }
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        //Send SMS
        [HttpPost]
        public StandardResponse SendSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new CMSData();

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                var c = CMSRow.Fields;

                data.CMS = connection.TryById<CMSRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.ContactsName)
                     .Select(c.ContactsPhone)
                     .Select(c.ContactsAddress)
                     .Select(c.ContactsEmail)
                     .Select(c.ProductsName)
                     .Select(c.Phone)
                     .Select(c.Address)
                     .Select(c.CompletionDate)
                     .Select(c.ExpectedCompletion)
                     .Select(c.AssignedToDisplayName)
                     .Select(c.ComplaintComplaintType)
                     .Select(c.Instructions)
                     );

                var ct = CmsTemplateRow.Fields;
                data.Template = connection.TryFirst<CmsTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(ct.SMSTemplate)
                   .Select(ct.ClosedSMSTemplate)
                   .Select(ct.EngineerSMSTemplate)
                   .Select(ct.CcsmSs)
                   .Select(ct.SmsTemplateId)
                   .Select(ct.ClosedTemplateId)
                   .Select(ct.EmgineerTemplateId)
                  .Where(ct.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword)
                    );

                data.Engineer = connection.TryById<UserRow>(request.EngineerID, q => q
                    .SelectTableFields()
                    .Select(u.Phone)
                    );
            }

            String Phn = null;

            String msg = null;
            String tempId = null;
            if (request.SMSType == "Customer")
            {
                msg = data.Template.SMSTemplate;
                tempId = data.Template.SmsTemplateId;

                if (data.CMS.Phone != null)
                {
                    Phn = data.CMS.Phone;
                }
                else
                {
                    Phn = data.CMS.ContactsPhone;
                }

                if (data.Template.CcsmSs != null)
                {
                    Phn = Phn + "," + data.Template.CcsmSs;
                }
            }
            else if (request.SMSType == "Engineer")
            {
                msg = data.Template.EngineerSMSTemplate;
                tempId = data.Template.EmgineerTemplateId;

                Phn = data.Engineer.Phone;
            }
            else if (request.SMSType == "ClosedSMS")
            {
                msg = data.Template.ClosedSMSTemplate;
                tempId = data.Template.ClosedTemplateId;

                if (data.CMS.Phone != null)
                {
                    Phn = data.CMS.Phone;
                }
                else
                {
                    Phn = data.CMS.ContactsPhone;
                }

                if (data.Template.CcsmSs != null)
                {
                    Phn = Phn + "," + data.Template.CcsmSs;
                }
            }

            msg = msg.Replace("#customername", data.CMS.ContactsName);
            msg = msg.Replace("#product", data.CMS.ProductsName);
            msg = msg.Replace("#complaintdate", data.CMS.Date.Value.ToShortDateString());
            msg = msg.Replace("#complaintno", data.CMS.Id.Value.ToString());
            msg = msg.Replace("#expecteddate", data.CMS.ExpectedCompletion.Value.ToShortDateString());
            msg = msg.Replace("#representative", data.CMS.AssignedToDisplayName);
            msg = msg.Replace("#complaintdetails", data.CMS.ComplaintComplaintType);
            msg = msg.Replace("#instructions", data.CMS.Instructions);
            if (data.CMS.Phone != null)
            {
                msg = msg.Replace("#phone", data.CMS.Phone);
            }
            else
            {
                msg = msg.Replace("#phone", data.CMS.ContactsPhone);
            }

            if (data.CMS.Address != null)
            {
                msg = msg.Replace("#address", data.CMS.Address);
            }
            else
            {
                msg = msg.Replace("#address", data.CMS.ContactsAddress);
            }

            try
            {
                response.Status = SMSHelper.SendSMS(Phn, msg,tempId);
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }


        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context, _connections).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
               return new MyRepository(Context, _connections).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }

        [ServiceAuthorize("CMS:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.CMSColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "CMS_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //MoveToQuotation
        [HttpPost]
        [ServiceAuthorize("CMS:Move to Quotation")]
        public StandardResponse MoveToQuotation(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();

            var data = new QuotData();

            using (var connection = _connections.NewFor<CMSRow>())
            {
                var cms = CMSRow.Fields;
                data.CMS = connection.TryById<CMSRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(cms.ContactsId)
                   .Select(cms.AssignedBy)
                   .Select(cms.AssignedTo)
                   );

                var cmsp = CMSProductsRow.Fields;
                data.CMSProducts = connection.List<CMSProductsRow>(q => q
                    .SelectTableFields()
                    .Select(cmsp.ProductsId)
                    .Select(cmsp.Quantity)
                    .Select(cmsp.Price)
                    .Where(cmsp.CMSId == request.Id)
                    );
            }

            try
            {
                using (var connection = _connections.NewFor<QuotationRow>())
                {

                    GetNextNumberResponse nextNumber = new QuotationController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    String str = "INSERT INTO Quotation(QuotationNo,QuotationN,ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId) VALUES(" + nextNumber.Serial + ",'" + nextNumber.SerialN + "',','" + Convert.ToString(data.CMS.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1','1','1','" + Convert.ToString(data.CMS.AssignedBy.Value) + "','" + Convert.ToString(data.CMS.AssignedTo.Value) + "')";

                    connection.Execute(str);

                    var quo = QuotationRow.Fields;
                    data.LastQuot = connection.TryFirst<QuotationRow>(l => l
                    .Select(quo.Id)
                    .Select(quo.ContactsId)
                    .OrderBy(quo.Id, desc: true)
                    );
                }

                if (data.CMS.ContactsId == data.LastQuot.ContactsId)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Enquiry\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<QuotationProductsRow>())
                    {
                        foreach (var item in data.CMSProducts)
                        {
                            String str = "INSERT INTO QuotationProducts(ProductsId,Quantity,Price,Percentage1,Percentage2,QuotationId,DiscountAmount,MRP,SellingPrice) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Price.Value) + "','0','0','" + Convert.ToString(data.LastQuot.Id.Value) + "','0','0','0')";

                            connection.Execute(str);
                        }
                    }
                }

                response.Id = data.LastQuot.Id.Value;
                response.Status = "Quotation generated successfully";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        public  GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";

            try
            {
                var sl = MyRow.Fields;
                var data = new MyRow();
                var br = UserRow.Fields;
                var UData = new UserRow();

                UData = connection.First<UserRow>(q => q
               .SelectTableFields()
               .Select(br.CompanyId)
               .Where(br.UserId == Context.User.GetIdentifier())
              );

                var br1 = CompanyDetailsRow.Fields;
                var Bdata = new CompanyDetailsRow();
                Bdata = connection.First<CompanyDetailsRow>(q => q
                  .SelectTableFields()
                  .Select(br1.CmSprefix)
                  .Select(br1.CmsSuffix)
                  .Select(br1.CmsStartNo)
                    .Where(br1.Id == Convert.ToInt32(UData.CompanyId))
                 );

                data = connection.TryFirst<MyRow>(q => q
                    .SelectTableFields()
                    .Select(sl.Id)
                    .Select(sl.CMSNo)
                    .OrderBy(sl.Id, desc: true)
                    );

                string stPre = string.Empty;
                string stsuf = string.Empty;
                if (Bdata.CmSprefix != null)
                {
                    stPre = Bdata.CmSprefix;
                }
                if (Bdata.CmsSuffix != null)
                {
                    stsuf = Bdata.CmSprefix;
                }

                if (Bdata.CmsStartNo == null)
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "-"+ (data.CMSNo + 1).ToString() + " " + stsuf;

                        response.Serial = (data.CMSNo + 1).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "-" +  (1).ToString() + " " + stsuf;

                        response.Serial = (1).ToString();
                    }
                }
                else
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "-" +  (Bdata.CmsStartNo).ToString() + " " + stsuf;

                        response.Serial = (Bdata.CmsStartNo).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "-" +  (Bdata.CmsStartNo).ToString() + " " + stsuf;

                        response.Serial = (Bdata.CmsStartNo).ToString();
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }

            return response;
        }
    }

    public class CMSData
    {
        public ContactsRow Contact { get; set; }
        public CMSRow CMS { get; set; }
        public UserRow Engineer { get; set; }
        public CmsTemplateRow Template { get; set; }
        public UserRow User { get; set; }
        public CMSFollowupsRow CMSFollowups { get; set; }
    }

    public class QuotData
    {
        public ContactsRow Contact { get; set; }
        public CMSRow CMS { get; set; }
        public List<CMSProductsRow> CMSProducts { get; set; }
        public UserRow User { get; set; }
        public UserRow Engineer { get; set; }
        public QuotationRow LastQuot { get; set; }
    }
}
