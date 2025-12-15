
namespace AdvanceCRM.Administration.Repositories
{
    using AdvanceCRM.Template;
    using AdvanceCRM.Template.Repositories;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Data;
    using MyRow =CompanyDetailsRow;
    using Serenity.Extensions.DependencyInjection;

    public class CompanyDetailsRepository : BaseRepository
    {
    public CompanyDetailsRepository(IRequestContext context) : base(context) { }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
             return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyDeleteHandler(Context).Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
             return new MyListHandler(Context).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            public MySaveHandler(IRequestContext context) : base(context) { }
            protected override void AfterSave()
            {
                base.AfterSave();
                if (this.IsCreate)
                {

                    var dailywishestemplate = new DailyWishesTemplateRow();

                    dailywishestemplate.From = "Your company name";
                    dailywishestemplate.Subject = "Subject";
                    dailywishestemplate.BirthdaySMS = "We wish you a happy birthday!!! dear #customername";
                    dailywishestemplate.MarriageSMS = "We wish you a happy marriage anniversary!!! dear #customername";
                    dailywishestemplate.DofAnniversarySMS = "We wish you a happy anniversary!!!";
                    dailywishestemplate.BirthdayEmail = "We wish you a happy birthday!!! dear #customername<br/><br/><br/>Regards<br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    dailywishestemplate.MarriageEmail = "We wish you a happy marriage anniversary!!! dear #customername<br/><br/><br/>Regards<br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    dailywishestemplate.DofAnniversaryEmail = "We wish you a happy anniversary!!!<br/><br/><br/>Regards<br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    dailywishestemplate.CompanyId = Request.Entity.Id;
                    new DailyWishesTemplateRepository(Context).Create(this.UnitOfWork, new SaveRequest<DailyWishesTemplateRow>
                    {
                        Entity = dailywishestemplate
                    });

                    var appointmenttemplate = new AppointmentTemplateRow();

                    appointmenttemplate.Sender = "Your Company Name";
                    appointmenttemplate.Subject = "Appointment";
                    appointmenttemplate.EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>Your appointment for date #appointmentdate is been booked<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    appointmenttemplate.SMSTemplate = "Dear #customername, your appointment for date #appointmentdate is been booked";
                    appointmenttemplate.CompanyId = Request.Entity.Id;
                    new AppointmentTemplateRepository(Context).Create(this.UnitOfWork, new SaveRequest<AppointmentTemplateRow>
                    {
                        Entity = appointmenttemplate
                    });

                    var telecallingtemplate = new TeleCallingTemplateRow();

                    telecallingtemplate.From = "Your company name";
                    telecallingtemplate.Subject = "Subject";
                    telecallingtemplate.CustomerSms = "Dear #customername, appointment by your time convinience is been scheduled on #appointmentdate, for demonstration of #product";
                    telecallingtemplate.ExecutiveSms = "Dear #executive, new appointment for customer #customername is been scheduled on #appointmentdate, for demonstration of #product";
                    telecallingtemplate.CustomerEmail = "Dear #customername,<br/><br/>appointment by your time convinience is been scheduled on #appointmentdate, for demonstration of #product<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    telecallingtemplate.ExecutiveEmail = "Dear #executive,<br/><br/>new appointment for customer #customername is been scheduled on #appointmentdate, for demonstration of #product<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    telecallingtemplate.CustomerReminderSMS = "Dear #customername, our executive will be reaching your office by #appointmentdate, thankyou";
                    telecallingtemplate.ExecutiveReminderSMS = "Dear #executive, your appointment with #customername will be at #appointmentdate";
                    telecallingtemplate.CompanyId = Request.Entity.Id;
                    new TeleCallingTemplateRepository(Context).Create(this.UnitOfWork, new SaveRequest<TeleCallingTemplateRow>
                    {
                        Entity = telecallingtemplate
                    });

                    var purchaseordertemplate = new PurchaseOrderTemplateRow();

                    purchaseordertemplate.Sender = "Your Company Name";
                    purchaseordertemplate.Subject = "Purchase Order";
                    purchaseordertemplate.EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached Purchase Order with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    purchaseordertemplate.CompanyId = Request.Entity.Id;
                    new PurchaseOrderTemplateRepository(Context).Create(this.UnitOfWork, new SaveRequest<PurchaseOrderTemplateRow>
                    {
                        Entity = purchaseordertemplate
                    });

                    var enquirytemplate = new EnquiryTemplateRow();

                    enquirytemplate.Sender = "Your Company Name";
                    enquirytemplate.Subject = "Enquiry";
                    enquirytemplate.EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    enquirytemplate.SMSTemplate = "Dear #customername thankyou for showing interest in us";
                    enquirytemplate.CompanyId = Request.Entity.Id;
                    new EnquiryTemplateRepository(Context).Create(this.UnitOfWork, new SaveRequest<EnquiryTemplateRow>
                    {
                        Entity = enquirytemplate
                    });

                    var quotationtemplate = new QuotationTemplateRow();

                    quotationtemplate.Sender = "Your Company Name";
                    quotationtemplate.Subject = "Quotation";
                    quotationtemplate.EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached quotation with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    quotationtemplate.SMSTemplate = "Dear #customername thankyou for showing interest in us";
                    quotationtemplate.CompanyId = Request.Entity.Id;
                    new QuotationTemplateRepository(Context).Create(this.UnitOfWork, new SaveRequest<QuotationTemplateRow>
                    {
                        Entity = quotationtemplate
                    });

                    var cmstemplate = new CmsTemplateRow();

                    cmstemplate.Sender = "Your Company Name";
                    cmstemplate.Subject = "CMS";
                    cmstemplate.EmailTemplate = "Dear customer #customername,<br/>Greetings!!!<br/><p>Your complaint regarding product #product is been logged on #complaintdate,<br/>Complaint no is #complaintno, your complaint is assigned to #representative and shall be closed by #expecteddate<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    cmstemplate.EmailTemplateReceipt = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached receipt with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    cmstemplate.ClosedEmailTemplate = "Dear customer #customername,<br/>Greetings!!!<br/><p>Your complaint regarding product #product is been closed on #completiondate,<br/>Complaint no was #complaintno, if you are having any query feel free to connect us <br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    cmstemplate.EngineerEmailTemplate = "Dear #representative,<br/>Greetings!!!<br/><p>New complaint #complaintno is been logged by customer #customername - #phone,<br/>Address - #address,<br/>For product #product,<br/>Complaint details are \"#complaintdetails\", expected closing is on #expecteddate <br/> #instructions<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    cmstemplate.SMSTemplate = "Dear customer #customername, your complaint regarding product #product is been logged on #complaintdate, Complaint no is #complaintno, your complaint is assigned to #representative and shall be closed by #expecteddate";
                    cmstemplate.ClosedSMSTemplate = "Dear customer #customername, your complaint regarding product #product is been closed, if you have any query feel free to connect us";
                    cmstemplate.EngineerSMSTemplate = "New complaint #complaintno is been logged by customer #customername - #phone, address - #address, for product #product, complaint details #complaintdetails, expected closing is on #expecteddate - #instructions";
                    cmstemplate.CompanyId = Request.Entity.Id;
                    new CmsTemplateRepository(Context).Create(this.UnitOfWork, new SaveRequest<CmsTemplateRow>
                    {
                        Entity = cmstemplate
                    });

                    var amctemplate = new AMCTemplateRow();

                    amctemplate.Sender = "Your Company Name";
                    amctemplate.Subject = "AMC Invoice";
                    amctemplate.EmailTemplateReceipt = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached Invoice with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    amctemplate.SMSTemplate = "Dear customer #customername, your AMC invoice of amount #amount is created, Invoice no is #amcno";
                    amctemplate.VisitSMSTemplate = "Dear #customername, AMC visit is done by our engineer #engineername on #completiondate";
                    amctemplate.CompanyId = Request.Entity.Id;
                    new AMCTemplateRepository(Context, Dependency.Resolve<ISqlConnections>()).Create(this.UnitOfWork, new SaveRequest<AMCTemplateRow>
                    {
                        Entity = amctemplate
                    });

                    var invoicetemplate = new InvoiceTemplateRow();

                    invoicetemplate.Sender = "Your Company Name";
                    invoicetemplate.Subject = "Invoice";
                    invoicetemplate.EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached invoice with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    invoicetemplate.SMSTemplate = "Dear #customername thankyou for purchasing, your invoice amount is #amount for invoice no. #invoiceno ";
                    invoicetemplate.CompanyId = Request.Entity.Id;
                    new InvoiceTemplateRepository(Context).Create(this.UnitOfWork, new SaveRequest<InvoiceTemplateRow>
                    {
                        Entity = invoicetemplate
                    });

                    var challantemplate = new ChallanTemplateRow();

                    challantemplate.Sender = "Your Company Name";
                    challantemplate.Subject = "Delivery Challan";
                    challantemplate.EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached delivery challan with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM";
                    challantemplate.SMSTemplate = "Dear #customername thankyou for purchasing, your challan amount is #amount for challan no. #challanno ";
                    challantemplate.CompanyId = Request.Entity.Id;
                    new ChallanTemplateRepository(Context, Dependency.Resolve<ISqlConnections>()).Create(this.UnitOfWork, new SaveRequest<ChallanTemplateRow>
                    {
                        Entity = challantemplate
                    });
                }
            }
        }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow>
        {
            public MyDeleteHandler(IRequestContext context) : base(context) { }
            protected override void ValidateRequest()
            {
                if (Row.Id == 1)
                    throw new ValidationError("Cannot delete Default Company");

                base.ValidateRequest();
            }
        }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }

        private class MyListHandler : ListRequestHandler<MyRow>
        {
            public MyListHandler(IRequestContext context) : base(context) { }
            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                //var user = (UserDefinition)Context.User.ToUserDefinition();
                //if (!Authorization.HasPermission(PermissionKeys.Company))
                //    query.Where(fld.Id == user.CompanyId);
                var user = Context.User.ToUserDefinition();
                var permissions = Context.Permissions;

                if (!permissions.HasPermission(PermissionKeys.Company))
                    query.Where(fld.Id == user.CompanyId);
            }
        }
    }
}