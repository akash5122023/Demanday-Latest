namespace AdvanceCRM.Quotation.Endpoints
{
    
    using MailChimp.Net;
    using MailChimp.Net.Models;
    using Nito.AsyncEx;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using AdvanceCRM.Products;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Template;
    using MyRepository = Repositories.QuotationRepository;
    using MyRow = QuotationRow;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Common;

    using AdvanceCRM.Settings;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Modules.ThirdParty.IVRDetails;
    using AdvanceCRM.Sales.Endpoints;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Purchase.Endpoints;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/Quotation/Quotation/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class QuotationController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private IRequestContext Context { get; }

        [ActivatorUtilitiesConstructor]
        public QuotationController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
            Manager = new MailChimpManager(AssignKey());
        }

        public QuotationController()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Create(uow, request);
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

        public ListResponse<MyRow> List(IDbConnection connection, QuotationListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }



        [ServiceAuthorize("Quotation:Can Approve")]
        public StandardResponse Approve(SendSMSRequest request)
        {
            var response = new StandardResponse();

            try
            {
                var connection = _connections.NewByKey("Default");
                connection.Execute("UPDATE Quotation SET ApprovedBy=" + Convert.ToInt32(Context.User.GetIdentifier()) + "WHERE Id=" + request.Id);

                var em = QuotationRow.Fields;

                response.Status = "Approved";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message;
            }

            return response;
        }




        //Send SMS
        [HttpPost]
        public StandardResponse SendSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new QuotationData();

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                var c = ContactsRow.Fields;

                data.Contact = connection.TryById<ContactsRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.Name)
                     .Select(c.Phone)
                     );

                var qt = QuotationTemplateRow.Fields;
                data.Template = connection.TryFirst<QuotationTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SMSTemplate)
                   .Select(qt.TemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );
            }

            String msg = data.Template.SMSTemplate;
            String tempId = data.Template.TemplateId;

            msg = msg.Replace("#customername", data.Contact.Name);

            response.Status = SMSHelper.SendSMS(data.Contact.Phone, msg, tempId);

            return response;
        }

        [HttpPost]
        public StandardResponse SendWati(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new QuotationData();
            var c = ContactsRow.Fields;

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                data.Contact = connection.TryById<ContactsRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.Name)
                     .Select(c.Whatsapp)
                     .Select(c.Phone)
                     );

                var qt = QuotationTemplateRow.Fields;
                data.Template = connection.TryFirst<QuotationTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.WaTemplate)
                   .Select(qt.WaTemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

            }
            string con = data.Contact.Whatsapp;
            if (data.Contact.Whatsapp == null)
            {
                con = data.Contact.Phone;
            }

            String temId = data.Template.WaTemplateId;

            response.Status = WAHelper.SendBizWA(con, temId);
            return response;
        }

        //Send SMS for SMS Sender


        [HttpPost, ServiceAuthorize("SMS:BulkSMS")]
        public StandardResponse SendBulkSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            MyRow contact;

            var e = MyRow.Fields;

            using (var connection = _connections.NewFor<MyRow>())
            {
                contact = connection.TryById<MyRow>(request.Id, q => q
                    .Select(e.Id)
                    .Select(e.ContactsName)
                    .Select(e.ContactsPhone)
                );
            }

            String msg = request.SMSType;
            String tempid = request.TemplateID;

            if (msg.Trim() == "#customername token can be used in message to auto add customers name")
            {
                throw new Exception("It seems you havent chnaged the default text, hence sending SMS is been cancelled");
            }

            var newmsg = msg.Replace("#customername", contact.ContactsName);

            response.Status = SMSHelper.SendSMS(contact.ContactsPhone, newmsg, tempid);

            if (!response.Status.Contains(("SMS").ToUpper()))
            {
                throw new Exception("SMS Sending Failed!");
            }

            response.Status = "Successfull!!!";

            return response;
        }

        //Add to MailChimp

        private string AssignKey()
        {
            MailChimpConfigurationRow MCConfig;

            using (var connection = _connections.NewFor<MailChimpConfigurationRow>())
            {
                var s = MailChimpConfigurationRow.Fields;
                MCConfig = connection.TryById<MailChimpConfigurationRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.ApiKey)
                );
            }

            return MCConfig.ApiKey;
        }
        public MailChimpManager Manager { get; private set; }

        private async Task<IEnumerable> ListDetails()
        {
            var listoptions = new MailChimp.Net.Core.ListRequest
            {
                Limit = 1000
            };
            var ModelList = await Manager.Lists.GetAllAsync(listoptions);
            var NewList = ModelList.OrderBy(x => x.Stats.MemberCount).ToList();
            return ViewBag.ListData = NewList;
        }


        [HttpPost]
        public MailChimpResponse AddToMailChimp(MailChimpRequest request)
        {
            var response = new MailChimpResponse();

            List<QuotationRow> contacts;
            List<QuotationRow> contacts1 = new List<QuotationRow>();


            var e = QuotationRow.Fields;

            using (var connection = _connections.NewFor<QuotationRow>())
            {
                contacts = connection.List<QuotationRow>(q => q
                    .Select(e.Id)
                    .Select(e.ContactsName)
                    .Select(e.ContactsEmail)
                    .Where(e.ContactsEmail.IsNotNull())
                );
            }

            foreach (var item in request.MailChimpIds)
            {
                contacts1.Add(contacts.Find(d => d.Id.ToString() == item));
            }

            contacts1.RemoveAll(x => x == null);


            var result1 = AsyncContext.Run(() => ListDetails());

            var QuotationListId = "";

            foreach (var item in ViewBag.ListData)
            {
                if (item.Name == request.ListName.Trim())
                {
                    QuotationListId = item.Id;
                    break;
                }
            }

            if (QuotationListId.IsNullOrEmpty())
            {
                response.MailChimpReturnResponse = "List '" + request.ListName + "' not found, please create this list in MailChimp section";
                return response;
            }

            foreach (var item in contacts1)
            {
                //Add mailchimp sync code here for this list to add it to MailChimp Contact List

                //Id of contacts list

                try
                {
                    var member = new Member
                    {
                        EmailAddress = item.ContactsEmail,
                        Status = Status.Subscribed,
                        StatusIfNew = Status.Subscribed,
                        ListId = QuotationListId,
                        EmailType = "html",
                        IpSignup = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        TimestampSignup = DateTime.UtcNow.ToString("s"),
                        MergeFields = new Dictionary<string, object>
                {
                    {"FNAME", item.ContactsName },
                    {"LNAME","" }
                }
                    };
                    var result = AsyncContext.Run(() => Manager.Members.AddOrUpdateAsync(QuotationListId, member));
                }
                catch (Exception ex)
                {

                    response.MailChimpReturnResponse = "Error\n" + ex.Message.ToString();
                }
            }

            if (contacts1.Count() > 0)
            {
                response.MailChimpReturnResponse = "Success";
            }
            else
            {
                response.MailChimpReturnResponse = "In selected list no valid email Id where found";
            }

            return response;
        }

        [HttpPost, ServiceAuthorize("SMS:BulkMail")]
        public StandardResponse SendBulkMail(IUnitOfWork uow, SendEmailRequest request)
        {
            var response = new StandardResponse();

            MyRow contact;

            var e = MyRow.Fields;

            using (var connection = _connections.NewFor<MyRow>())
            {
                contact = connection.TryById<MyRow>(request.Id, q => q
                    .Select(e.Id)
                    .Select(e.ContactsName)
                    .Select(e.ContactsPhone)
                    .Select(e.ContactsEmail)
                );
            }
            var to = contact.ContactsEmail;
            var msg = request.EmailType;
            var sub = request.Subject;
            DateTime dt = request.Senddate;

            List<string> Records = new List<string>();

            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);

            var newmsg = msg.Replace("#customername", contact.ContactsName);

            response.Status = MailHelper.SendBulkMail(contact.ContactsName, to, sub, newmsg, dt);


            response.Status = "Successfull!!!";

            return response;
        }

        [ServiceAuthorize("Quotation:Export")]
        public FileContentResult ListExcel(IDbConnection connection, QuotationListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.QuotationColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Quotation_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }


        //MoveToInvoice
        [HttpPost]
        public StandardResponse MoveToInvoice(IUnitOfWork uow, SendMailRequest request)
        {
            var response = new StandardResponse();

            if (request.MailType == "Invoice")
            {
                var exist = new InvoiceRow();
                var i = InvoiceRow.Fields;
                exist = uow.Connection.TryFirst<InvoiceRow>(q => q
                .SelectTableFields()
                .Select(i.Id)
                .Where(i.QuotationNo == request.Id));

                if (exist != null)
                {
                    response.Id = exist.Id.Value;
                    response.Status = "Already Moved!";
                    return response;
                }
            }
            else
            {
                var exist = new SalesRow();
                var i = SalesRow.Fields;
                exist = uow.Connection.TryFirst<SalesRow>(q => q
                .SelectTableFields()
                .Select(i.Id)
                .Where(i.QuotationNo == request.Id));

                if (exist != null)
                {
                    response.Id = exist.Id.Value;
                    response.Status = "Already Moved!";
                    return response;
                }
            }

            var data = new QuotationData();

            var quot = QuotationRow.Fields;
            data.Quotation = uow.Connection.TryById<QuotationRow>(request.Id, q => q
               .SelectTableFields()
               .Select(quot.ContactsId)
               .Select(quot.Type)
               .Select(quot.AdditionalInfo)
               .Select(quot.SourceId)
               .Select(quot.StageId)
               .Select(quot.BranchId)
               .Select(quot.OwnerId)
               .Select(quot.AssignedId)
               .Select(quot.ReferenceName)
               .Select(quot.ReferencePhone)
               .Select(quot.LostReason)
               .Select(quot.Status)
               .Select(quot.CompanyId)
               .Select(quot.Conversion)
               .Select(quot.CurrencyConversion)
               .Select(quot.QuotationN)
               .Select(quot.ToCurrency)
               .Select(quot.FromCurrency)
               .Select(quot.Subject)
               .Select(quot.Reference)
               .Select(quot.MessageId)
               );

            var quotp = QuotationProductsRow.Fields;
            data.QuotationProducts = uow.Connection.List<QuotationProductsRow>(q => q
                .SelectTableFields()
                .Select(quotp.ProductsId)
                .Select(quotp.Quantity)
                .Select(quotp.Mrp)
                .Select(quotp.Unit)
                .Select(quotp.SellingPrice)
                .Select(quotp.Price)
                .Select(quotp.Discount)
                .Select(quotp.DiscountAmount)
                .Select(quotp.TaxType1)
                .Select(quotp.Percentage1)
                .Select(quotp.TaxType2)
                .Select(quotp.Percentage2)

                .Where(quotp.QuotationId == request.Id)
                );

            var quott = QuotationTermsRow.Fields;
            data.QuotationTerms = uow.Connection.List<QuotationTermsRow>(q => q
                .SelectTableFields()
                .Select(quott.TermsId)
                .Select(quott.QuotationId)
                .Where(quott.QuotationId == request.Id)
                );

            var quotcha = QuotationChargesRow.Fields;
            data.AdditionalCharges = uow.Connection.List<QuotationChargesRow>(q => q
                .SelectTableFields()
                .Select(quotcha.ChargesId)
                .Select(quotcha.QuotationId)
                .Where(quotcha.QuotationId == request.Id)
                );

            var quotcon = QuotationConcessionRow.Fields;
            data.AdditionalConcession = uow.Connection.List<QuotationConcessionRow>(q => q
                .SelectTableFields()
                .Select(quotcon.ConcessionId)
                .Select(quotcon.QuotationId)
                .Where(quotcon.QuotationId == request.Id)
                );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );


            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Quotation.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Quotation as closed or pending before moving");
                }
            }

            int contactsid;
            int insalid;

            try
            {
                using (var connection = _connections.NewFor<InvoiceRow>())
                {
                    dynamic typ, brnh, curcon, msg, refr, sub;

                    if (data.Quotation.Type != null)
                        typ = (int)data.Quotation.Type.Value;
                    else
                        typ = "null";

                    if (data.Quotation.BranchId != null)
                        brnh = Convert.ToString(data.Quotation.BranchId.Value);
                    else
                        brnh = "null";

                    if (data.Quotation.CurrencyConversion != null)
                        curcon = data.Quotation.CurrencyConversion.Value;
                    else curcon = 1.00;

                    //if (data.Quotation.FromCurrency != null)
                    //    confrom = data.Quotation.FromCurrency.Value;
                    //else confrom = "null";

                    //if (data.Quotation.ToCurrency != null)
                    //    conto = data.Quotation.ToCurrency.Value;
                    //else conto = "null";

                    if (data.Quotation.MessageId != null)
                        msg = data.Quotation.MessageId.Value;
                    else msg = 0;

                    if (data.Quotation.Subject != null)
                        sub = data.Quotation.Subject;
                    else sub = "";

                    if (data.Quotation.Reference != null)
                        refr = data.Quotation.Reference;
                    else refr = "";


                    String str;
                    if (request.MailType == "Invoice")
                    {
                        GetNextNumberResponse nextNumber = Dependency.Resolve<InvoiceController>().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                        //  str = "INSERT INTO Invoice(InvoiceNo,InvoiceN,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,QuotationN) VALUES(" + nextNumber.Serial + ",'" + nextNumber.SerialN + "','" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "',0,0,0,0,'" + Convert.ToString(data.Quotation.Conversion) + "'," + (data.Quotation.CurrencyConversion.HasValue() ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Quotation.FromCurrency) + "','" + Convert.ToString((Int32?)data.Quotation.ToCurrency) + "'," + Convert.ToString(request.Id) + ",'" + data.Quotation.Date.Value.ToString("yyyy-MM-dd") + "','" + data.Quotation.QuotationN + "')";
                        if (msg == 0)
                        {
                            str = "INSERT INTO Invoice(InvoiceNo,InvoiceN,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,QuotationN,CompanyId,Subject,Reference) VALUES(" + nextNumber.Serial + ",'" + nextNumber.SerialN + "','" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "',0,0,0,0,'" + Convert.ToString(data.Quotation.Conversion) + "'," + (data.Quotation.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Quotation.FromCurrency) + "','" + Convert.ToString((Int32?)data.Quotation.ToCurrency) + "'," + Convert.ToString(request.Id) + ",'" + data.Quotation.Date.Value.ToString("yyyy-MM-dd") + "','" + data.Quotation.QuotationN + "','" + Convert.ToString(data.Quotation.CompanyId.Value) + "','" + sub + "','" + refr + "')";

                        }
                        else
                        {
                            str = "INSERT INTO Invoice(InvoiceNo,InvoiceN,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,QuotationN,CompanyId,Subject,Reference,MessageId) VALUES(" + nextNumber.Serial + ",'" + nextNumber.SerialN + "','" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "',0,0,0,0,'" + Convert.ToString(data.Quotation.Conversion) + "'," + (data.Quotation.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Quotation.FromCurrency) + "','" + Convert.ToString((Int32?)data.Quotation.ToCurrency) + "'," + Convert.ToString(request.Id) + ",'" + data.Quotation.Date.Value.ToString("yyyy-MM-dd") + "','" + data.Quotation.QuotationN + "','" + Convert.ToString(data.Quotation.CompanyId.Value) + "','" + sub + "','" + refr + "','" + msg + "')";
                        }
                    }
                    else
                    {
                        GetNextNumberResponse nextNumber = new SalesController(_connections, Context).GetNextNumber(uow.Connection, new GetNextNumberRequest());
                        if (msg == 0)
                        {
                            str = "INSERT INTO Sales(InvoiceNo,InvoiceN,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,QuotationN,CompanyId,Subject,Reference) VALUES(" + nextNumber.Serial + ",'" + nextNumber.SerialN + "','" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "',0,0,0,0,'" + Convert.ToString(data.Quotation.Conversion) + "'," + (data.Quotation.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Quotation.FromCurrency) + "','" + Convert.ToString((Int32?)data.Quotation.ToCurrency) + "'," + Convert.ToString(request.Id) + ",'" + data.Quotation.Date.Value.ToString("yyyy-MM-dd") + "','" + data.Quotation.QuotationN + "','" + Convert.ToString(data.Quotation.CompanyId.Value) + "','" + sub + "','" + refr + "')";
                        }
                        else
                        {
                            str = "INSERT INTO Sales(InvoiceNo,InvoiceN,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,QuotationN,CompanyId,Subject,Reference,MessageId) VALUES(" + nextNumber.Serial + ",'" + nextNumber.SerialN + "','" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "',0,0,0,0,'" + Convert.ToString(data.Quotation.Conversion) + "'," + (data.Quotation.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Quotation.FromCurrency) + "','" + Convert.ToString((Int32?)data.Quotation.ToCurrency) + "'," + Convert.ToString(request.Id) + ",'" + data.Quotation.Date.Value.ToString("yyyy-MM-dd") + "','" + data.Quotation.QuotationN + "','" + Convert.ToString(data.Quotation.CompanyId.Value) + "','" + sub + "','" + refr + "','" + msg + "')";
                        }
                    }

                    connection.Execute(str);


                    if (request.MailType == "Invoice")
                    {
                        var inv = InvoiceRow.Fields;
                        data.LastInv = connection.TryFirst<InvoiceRow>(l => l
                        .Select(inv.Id)
                        .Select(inv.ContactsId)
                        .OrderBy(inv.Id, desc: true)
                        );

                        contactsid = data.LastInv.ContactsId.Value;
                        insalid = data.LastInv.Id.Value;
                    }
                    else
                    {
                        var sal = SalesRow.Fields;
                        data.LastSale = connection.TryFirst<SalesRow>(l => l
                        .Select(sal.Id)
                        .Select(sal.ContactsId)
                        .OrderBy(sal.Id, desc: true)
                        );

                        contactsid = data.LastSale.ContactsId.Value;
                        insalid = data.LastSale.Id.Value;
                    }
                }

                if (data.Quotation.ContactsId == contactsid)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Quotation\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<QuotationProductsRow>())
                    {
                        foreach (var item in data.QuotationProducts)
                        {
                            String str;
                            if (request.MailType == "Invoice")
                            {
                                str = "INSERT INTO InvoiceProducts(ProductsId,[From],[To],[Date],[Destination],[HotelName],[Nights],[Adults],[Childrens],[MealPlan],Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,InvoiceId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + item.From + "','" + item.To + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + item.Destination + "','" + item.HotelName + "','" + item.Nights + "','" + item.Adults + "','" + item.Childrens + "','" + item.MealPlan + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";
                            }
                            else
                            {
                                //str = "INSERT INTO SalesProducts(ProductsId,Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,SalesId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";

                                str = "INSERT INTO SalesProducts(ProductsId,[From],[To],[Date],[Destination],[HotelName],[Nights],[Adults],[Childrens],[MealPlan],Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,SalesId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + item.From + "','" + item.To + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + item.Destination + "','" + item.HotelName + "','" + item.Nights + "','" + item.Adults + "','" + item.Childrens + "','" + item.MealPlan + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";

                            }

                            connection.Execute(str);
                        }
                    }

                    using (var connection = _connections.NewFor<QuotationTermsRow>())
                    {
                        foreach (var item in data.QuotationTerms)
                        {
                            String str;
                            if (request.MailType == "Invoice")
                            {
                                str = "INSERT INTO InvoiceTerms(TermsId,InvoiceId) VALUES('" + item.TermsId + "','" + Convert.ToString(insalid) + "')";
                            }
                            else
                            {
                                str = "INSERT INTO SalesTerms(TermsId,SalesId) VALUES('" + item.TermsId + "','" + Convert.ToString(insalid) + "')";
                            }

                            connection.Execute(str);
                        }
                    }

                    using (var connection = _connections.NewFor<QuotationChargesRow>())
                    {
                        foreach (var item in data.AdditionalCharges)
                        {
                            String str;
                            if (request.MailType == "Invoice")
                            {
                                str = "INSERT INTO InvoiceCharges(ChargesId,InvoiceId) VALUES('" + item.ChargesId + "','" + Convert.ToString(insalid) + "')";
                            }
                            else
                            {
                                str = "INSERT INTO SalesCharges(ChargesId,SalesId) VALUES('" + item.ChargesId + "','" + Convert.ToString(insalid) + "')";
                            }

                            connection.Execute(str);
                        }
                    }

                    using (var connection = _connections.NewFor<QuotationConcessionRow>())
                    {
                        foreach (var item in data.AdditionalConcession)
                        {
                            String str;
                            if (request.MailType == "Invoice")
                            {
                                str = "INSERT INTO InvoiceConcession(ConcessionId,InvoiceId) VALUES('" + item.ConcessionId + "','" + Convert.ToString(insalid) + "')";
                            }
                            else
                            {
                                str = "INSERT INTO SalesConcession(ConcessionId,SalesId) VALUES('" + item.ConcessionId + "','" + Convert.ToString(insalid) + "')";
                            }

                            connection.Execute(str);
                        }
                    }

                }

                response.Id = insalid;
                response.Status = "Quotation moved to " + request.MailType + " scucessfully";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = ex.Message.ToString();
            }

            return response;

        }

        // MoveToPurchase
        [HttpPost]
        public StandardResponse MoveToPurchase(IUnitOfWork uow, SendMailRequest request)
        {
            var response = new StandardResponse();

            if (request.MailType == "Purchase")
            {
                var exist = new PurchaseRow();
                var i = PurchaseRow.Fields;
                exist = uow.Connection.TryFirst<PurchaseRow>(q => q
                .SelectTableFields()
                .Select(i.Id)
                .Where(i.QuotationNo == request.Id));

                if (exist != null)
                {
                    response.Id = exist.Id.Value;
                    response.Status = "Already Moved!";
                    return response;
                }
            }
            else
            {
                var exist = new PurchaseOrderRow();
                var i = PurchaseOrderRow.Fields;
                exist = uow.Connection.TryFirst<PurchaseOrderRow>(q => q
                .SelectTableFields()
                .Select(i.Id)
                .Where(i.QuotationNo == request.Id));

                if (exist != null)
                {
                    response.Id = exist.Id.Value;
                    response.Status = "Already Moved!";
                    return response;
                }
            }

            var data = new QuotationData();

            var quot = QuotationRow.Fields;
            data.Quotation = uow.Connection.TryById<QuotationRow>(request.Id, q => q
               .SelectTableFields()
               .Select(quot.ContactsId)
               .Select(quot.Type)
               .Select(quot.AdditionalInfo)
               .Select(quot.SourceId)
               .Select(quot.StageId)
               .Select(quot.BranchId)
               .Select(quot.OwnerId)
               .Select(quot.AssignedId)
               .Select(quot.ReferenceName)
               .Select(quot.ReferencePhone)
               .Select(quot.LostReason)
               .Select(quot.Status)
               .Select(quot.CompanyId)
               .Select(quot.Conversion)
               .Select(quot.CurrencyConversion)
               .Select(quot.QuotationN)
               .Select(quot.ToCurrency)
               .Select(quot.FromCurrency)
               .Select(quot.Subject)
               .Select(quot.Reference)
               .Select(quot.MessageId)
               );

            var quotp = QuotationProductsRow.Fields;
            data.QuotationProducts = uow.Connection.List<QuotationProductsRow>(q => q
                .SelectTableFields()
                .Select(quotp.ProductsId)
                .Select(quotp.Quantity)
                .Select(quotp.Mrp)
                .Select(quotp.Unit)
                .Select(quotp.SellingPrice)
                .Select(quotp.Price)
                .Select(quotp.Discount)
                .Select(quotp.DiscountAmount)
                .Select(quotp.TaxType1)
                .Select(quotp.Percentage1)
                .Select(quotp.TaxType2)
                .Select(quotp.Percentage2)

                .Where(quotp.QuotationId == request.Id)
                );

            var quott = QuotationTermsRow.Fields;
            data.QuotationTerms = uow.Connection.List<QuotationTermsRow>(q => q
                .SelectTableFields()
                .Select(quott.TermsId)
                .Select(quott.QuotationId)
                .Where(quott.QuotationId == request.Id)
                );

            var quotcha = QuotationChargesRow.Fields;
            data.AdditionalCharges = uow.Connection.List<QuotationChargesRow>(q => q
                .SelectTableFields()
                .Select(quotcha.ChargesId)
                .Select(quotcha.QuotationId)
                .Where(quotcha.QuotationId == request.Id)
                );

            var quotcon = QuotationConcessionRow.Fields;
            data.AdditionalConcession = uow.Connection.List<QuotationConcessionRow>(q => q
                .SelectTableFields()
                .Select(quotcon.ConcessionId)
                .Select(quotcon.QuotationId)
                .Where(quotcon.QuotationId == request.Id)
                );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );


            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Quotation.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Quotation as closed or pending before moving");
                }
            }

            int contactsid;
            int insalid;

            try
            {
                using (var connection = _connections.NewFor<PurchaseRow>())
                {
                    dynamic typ, brnh, curcon, msg, refr, sub;

                    if (data.Quotation.Type != null)
                        typ = (int)data.Quotation.Type.Value;
                    else
                        typ = "null";

                    if (data.Quotation.BranchId != null)
                        brnh = Convert.ToString(data.Quotation.BranchId.Value);
                    else
                        brnh = "null";

                    if (data.Quotation.CurrencyConversion != null)
                        curcon = data.Quotation.CurrencyConversion.Value;
                    else curcon = 1.00;

                    //if (data.Quotation.FromCurrency != null)
                    //    confrom = data.Quotation.FromCurrency.Value;
                    //else confrom = "null";

                    //if (data.Quotation.ToCurrency != null)
                    //    conto = data.Quotation.ToCurrency.Value;
                    //else conto = "null";

                    if (data.Quotation.MessageId != null)
                        msg = data.Quotation.MessageId.Value;
                    else msg = 0;

                    if (data.Quotation.Subject != null)
                        sub = data.Quotation.Subject;
                    else sub = "";

                    if (data.Quotation.Reference != null)
                        refr = data.Quotation.Reference;
                    else refr = "";

                    var InvoiceDate = DateTime.Now.ToString("yyyy-MM-dd");
                    String str;
                    if (request.MailType == "Purchase")
                    {
                        GetNextNumberResponse nextNumber = new PurchaseController(_connections, Context).GetNextNumber(uow.Connection, new GetNextNumberRequest());
                        //  str = "INSERT INTO Invoice(InvoiceNo,InvoiceN,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,QuotationN) VALUES(" + nextNumber.Serial + ",'" + nextNumber.SerialN + "','" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "',0,0,0,0,'" + Convert.ToString(data.Quotation.Conversion) + "'," + (data.Quotation.CurrencyConversion.HasValue() ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Quotation.FromCurrency) + "','" + Convert.ToString((Int32?)data.Quotation.ToCurrency) + "'," + Convert.ToString(request.Id) + ",'" + data.Quotation.Date.Value.ToString("yyyy-MM-dd") + "','" + data.Quotation.QuotationN + "')";
                        if (msg == 0)
                        {
                            str = "INSERT INTO Purchase([Invoice No],PurchaseFromId ,InvoiceDate ,Total,Status,Type,AdditionalInfo,BranchId,OwnerId,AssignedId,InvoiceType,Roundup,QuotationNo) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + 0 + "','1'," + typ + ",'" + (data.Quotation.AdditionalInfo ?? string.Empty).Replace("'", "''") + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "','" + 1 + "','" + 0 + "'," + Convert.ToString(request.Id) + ")";

                        }
                        else
                        {
                            //str = "INSERT INTO Purchase([Invoice No],PurchaseFromId ,InvoiceDate ,Total,Status,Type,AdditionalInfo,BranchId,OwnerId," +
                            //     "AssignedId,InoviceType,Roundup,QuotationNo)" +
                            //     " VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Convert.ToString(data.Quotation.Total.Value) + "','1'," + typ + ",'" + data.Quotation.AdditionalInfo + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "',,'" + 0 + "'," + Convert.ToString(request.Id) + "')";
                            str = "INSERT INTO Purchase([Invoice No],PurchaseFromId ,InvoiceDate ,Total,Status,Type,AdditionalInfo,BranchId,OwnerId,AssignedId,InoviceType,Roundup,QuotationNo) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + 0 + "','1'," + typ + ",'" + (data.Quotation.AdditionalInfo ?? string.Empty).Replace("'", "''") + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "','" + 1 + "','" + 0 + "'," + Convert.ToString(request.Id) + "')";

                        }
                    }
                    else
                    {
                        GetNextNumberResponse nextNumber = new SalesController(_connections, Context).GetNextNumber(uow.Connection,  new GetNextNumberRequest());

                        if (msg == 0)
                        {
                            str = "INSERT INTO PurchaseOrder(PurchaseOrderNo, ContactsId, Date, Status, AdditionalInfo, SourceId, OwnerId, AssignedId, Roundup, Conversion, CurrencyConversion, Lines, QuotationNo) " +
           "VALUES (" +
           $"{nextNumber.Serial}, " +  $"'{data.Quotation.ContactsId}', " +
           $"'{DateTime.Now:yyyy-MM-dd}', " +
           $"1, " +
           $"'{(data.Quotation.AdditionalInfo ?? "").Replace("'", "''")}', " +
           $"'{data.Quotation.SourceId}', " +
           $"'{data.Quotation.OwnerId}', " +
           $"'{data.Quotation.AssignedId}', " +
           $"0, " +
           $"'{data.Quotation.Conversion}', " +
           $"{(data.Quotation.CurrencyConversion == true ? 1 : 0)}, " +
         
           $"10, " +
           $"'{request.Id}'" +
           ")";

                        }
                        else
                        {
                            str = "INSERT INTO PurchaseOrder(PurchaseOrderNo, ContactsId, Date, Status, AdditionalInfo, SourceId, OwnerId, AssignedId, Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, Lines, QuotationNo) " +
            "VALUES (" +
            $"{nextNumber.Serial}, " +
            $"'{data.Quotation.ContactsId}', " +
            $"'{DateTime.Now:yyyy-MM-dd}', " +
            $"1, " +
            $"'{(data.Quotation.AdditionalInfo ?? "").Replace("'", "''")}', " +
            $"'{data.Quotation.SourceId}', " +
            $"'{data.Quotation.OwnerId}', " +
            $"'{data.Quotation.AssignedId}', " +
            $"0, " +
            $"'{data.Quotation.Conversion}', " +
            $"{(data.Quotation.CurrencyConversion == true ? 1 : 0)}, " +
            $"'{(data.Quotation.FromCurrency.HasValue ? (object)data.Quotation.FromCurrency.Value : DBNull.Value)}', " +
            $"'{(data.Quotation.ToCurrency.HasValue ? (object)data.Quotation.ToCurrency.Value : DBNull.Value)}', " +
            $"10, " +
            $"'{request.Id}'" +
            ")";

                        }
                    }

                    connection.Execute(str);

                    //                    string str;

                    //                    if (request.MailType == "Purchase")
                    //                    {
                    //                        GetNextNumberResponse nextNumber = new PurchaseController().GetNextNumber(uow.Connection, new GetNextNumberRequest());


                    //                        if (msg == 0)
                    //                        {
                    //                            str = @"INSERT INTO Purchase([Invoice No], PurchaseFromId, InvoiceDate, Total, Status, Type, AdditionalInfo, BranchId, OwnerId, AssignedId, InvoiceType, Roundup, QuotationNo)
                    //            VALUES (@InvoiceNo, @PurchaseFromId, @InvoiceDate, @Total, @Status, @Type, @AdditionalInfo, @BranchId, @OwnerId, @AssignedId, @InvoiceType, @Roundup, @QuotationNo)";
                    //                        }
                    //                        else
                    //                        {
                    //                            str = @"INSERT INTO Purchase([Invoice No], PurchaseFromId, InvoiceDate, Total, Status, Type, AdditionalInfo, BranchId, OwnerId, AssignedId, InoviceType, Roundup, QuotationNo)
                    //            VALUES (@InvoiceNo, @PurchaseFromId, @InvoiceDate, @Total, @Status, @Type, @AdditionalInfo, @BranchId, @OwnerId, @AssignedId, @InvoiceType, @Roundup, @QuotationNo)";
                    //                        }

                    //                        // Values to inject
                    //                        var values = new
                    //                        {
                    //                            InvoiceNo = nextNumber.Serial,
                    //                            PurchaseFromId = data.Quotation.ContactsId,
                    //                            InvoiceDate = InvoiceDate,
                    //                            Status = 1,
                    //                            Total = 0,
                    //                            Type = typ,
                    //                            AdditionalInfo = data.Quotation.AdditionalInfo,
                    //                            BranchId = brnh,
                    //                            OwnerId = data.Quotation.OwnerId,
                    //                            AssignedId = data.Quotation.AssignedId,
                    //                            InvoiceType = 1,
                    //                            Roundup = 0,
                    //                            QuotationNo = request.Id
                    //                        };

                    //                        // Optional: Construct raw SQL string for debugging
                    //                        string debugSql = $@"
                    //INSERT INTO Purchase([Invoice No], PurchaseFromId, InvoiceDate, Total, Status, Type, AdditionalInfo, BranchId, OwnerId, AssignedId, InvoiceType, Roundup, QuotationNo)
                    //VALUES ({values.InvoiceNo}, {values.PurchaseFromId}, '{values.InvoiceDate:yyyy-MM-dd}', {values.Total}, {values.Status}, {values.Type},
                    //        '{values.AdditionalInfo?.Replace("'", "''")}', {values.BranchId}, {values.OwnerId}, {values.AssignedId},
                    //        {values.InvoiceType}, {values.Roundup}, {values.QuotationNo})";

                    //                        // Print or log the full SQL for debugging
                    //                        Console.WriteLine(debugSql); // or log to file if needed

                    //                        // Execute the actual safe parameterized query
                    //                        uow.Connection.Execute(debugSql);
                    //                    }
                    //                    else
                    //                    {
                    //                        GetNextNumberResponse nextNumber = new SalesController().GetNextNumber(uow.Connection, new GetNextNumberRequest());

                    //                        //            str = @"INSERT INTO PurchaseOrder(PurchaseOrderNo, ContactsId, Date, Status, AdditionalInfo, SourceId, BranchId, OwnerId, AssignedId, Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, Lines, QuotationNo)
                    //                        //VALUES (@PurchaseOrderNo, @ContactsId, @Date, 1, @AdditionalInfo, @SourceId, @BranchId, @OwnerId, @AssignedId, @Roundup, @Conversion, @CurrencyConversion, @FromCurrency, @ToCurrency, @Lines, @QuotationNo)";

                    //                        //uow.Connection.Execute(str, new
                    //                        //{
                    //                        //    PurchaseOrderNo = nextNumber.Serial,
                    //                        //    ContactsId = data.Quotation.ContactsId,
                    //                        //    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    //                        //    AdditionalInfo = data.Quotation.AdditionalInfo,
                    //                        //    SourceId = data.Quotation.SourceId,
                    //                        //    BranchId = brnh,
                    //                        //    OwnerId = data.Quotation.OwnerId,
                    //                        //    AssignedId = data.Quotation.AssignedId,
                    //                        //    Roundup = 0,
                    //                        //    Conversion = data.Quotation.Conversion,
                    //                        //    CurrencyConversion = data.Quotation.CurrencyConversion.HasValue && data.Quotation.CurrencyConversion.Value ? 1 : 0,
                    //                        //    FromCurrency = data.Quotation.FromCurrency ?? 0,
                    //                        //    ToCurrency = data.Quotation.ToCurrency ?? 0,
                    //                        //    Lines = 10, // hardcoded as in your original
                    //                        //    QuotationNo = request.Id
                    //                        //});

                    //                        if (msg == 0)
                    //                        {
                    //                            str = @"INSERT INTO PurchaseOrder(PurchaseOrderNo, ContactsId, Date, Status, AdditionalInfo, SourceId, BranchId, OwnerId, AssignedId, Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, Lines, QuotationNo)
                    //                        VALUES (@PurchaseOrderNo, @ContactsId, @Date, 1, @AdditionalInfo, @SourceId, @BranchId, @OwnerId, @AssignedId, @Roundup, @Conversion, @CurrencyConversion, @FromCurrency, @ToCurrency, @Lines, @QuotationNo)";

                    //                        }
                    //                        else
                    //                        {
                    //                            str = @"INSERT INTO PurchaseOrder(PurchaseOrderNo, ContactsId, Date, Status, AdditionalInfo, SourceId, BranchId, OwnerId, AssignedId, Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, Lines, QuotationNo)
                    //                        VALUES (@PurchaseOrderNo, @ContactsId, @Date, 1, @AdditionalInfo, @SourceId, @BranchId, @OwnerId, @AssignedId, @Roundup, @Conversion, @CurrencyConversion, @FromCurrency, @ToCurrency, @Lines, @QuotationNo)";

                    //                        }
                    //                        var values = new
                    //                        {
                    //                            PurchaseOrderNo = nextNumber.Serial,
                    //                                ContactsId = data.Quotation.ContactsId,
                    //                                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    //                            AdditionalInfo = data.Quotation.AdditionalInfo,
                    //                            SourceId = data.Quotation.SourceId,
                    //                            BranchId = brnh,
                    //                            OwnerId = data.Quotation.OwnerId,
                    //                            AssignedId = data.Quotation.AssignedId,
                    //                            Roundup = 0,
                    //                            Conversion = data.Quotation.Conversion,
                    //                            CurrencyConversion = data.Quotation.CurrencyConversion.HasValue && data.Quotation.CurrencyConversion.Value ? 1 : 0,
                    //                            FromCurrency = data.Quotation.FromCurrency ?? 0,
                    //                            ToCurrency = data.Quotation.ToCurrency ?? 0,
                    //                            Lines = 10, // hardcoded as in your original
                    //                            QuotationNo = request.Id
                    //                        };
                    //                        string debugSql = $@"
                    //INSERT INTO PurchaseOrder(PurchaseOrderNo, ContactsId, Date, SourceId, AdditionalInfo, BranchId, OwnerId, AssignedId, Roundup,Conversion,FromCurrency,ToCurrency, Lines,  QuotationNo)
                    //VALUES ({values.PurchaseOrderNo}, {values.ContactsId}, '{values.Date:yyyy-MM-dd}', {values.SourceId},
                    //        '{values.AdditionalInfo?.Replace("'", "''")}', {values.BranchId}, {values.OwnerId}, {values.AssignedId},
                    //         {values.Roundup},{values.Conversion},{values.CurrencyConversion},{values.FromCurrency}, {values.ToCurrency}, {values.Lines}, {values.QuotationNo})";


                    //                        Console.WriteLine(debugSql); // or log to file if needed

                    //                        // Execute the actual safe parameterized query
                    //                        uow.Connection.Execute(debugSql);
                    //                    }
                    //                    connection.Execute(str);

                    if (request.MailType == "Purchase")
                    {
                        var inv1 = PurchaseRow.Fields;
                        data.LastInv1 = connection.TryFirst<PurchaseRow>(l => l
                        .Select(inv1.Id)
                        .Select(inv1.PurchaseFromId)
                        .OrderBy(inv1.Id, desc: true)
                        );

                        contactsid = data.LastInv1.PurchaseFromId.Value;
                        insalid = data.LastInv1.Id.Value;

                        //var inv1 = PurchaseRow.Fields;

                        //data.LastInv1 = connection.TryFirst<PurchaseRow>(q => q
                        //    .Select(inv1.Id)
                        //    .Select(inv1.PurchaseFromId)
                        //    .OrderBy(inv1.Id, desc: true)
                        //    .Take(1) // Explicit limit to 1 row
                        //);
                        //var sql = "SELECT TOP 1 Id, PurchaseFromId FROM Purchase ORDER BY Id DESC";
                        //var row = connection.Query<PurchaseRow>(sql).FirstOrDefault();

                        //if (data.LastInv1 != null)
                        //{
                        //contactsid = data.LastInv1.PurchaseFromId ?? 0;
                        //insalid = data.LastInv1.Id ?? 0;
                        //}
                        //contactsid = row.PurchaseFromId ?? 0;
                        //insalid = row.Id ?? 0;
                    }
                    else
                    {
                        var sal = PurchaseOrderRow.Fields;
                        data.LastSale1 = connection.TryFirst<PurchaseOrderRow>(l => l
                        .Select(sal.Id)
                        .Select(sal.ContactsId)
                        .OrderBy(sal.Id, desc: true)
                        );

                        contactsid = data.LastSale1.ContactsId.Value;
                        insalid = data.LastSale1.Id.Value;
                    }
                }

                if (data.Quotation.ContactsId == contactsid)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Quotation\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<QuotationProductsRow>())
                    {
                        foreach (var item in data.QuotationProducts)
                        {
                            String str;
                            if (request.MailType == "Purchase")
                            {
                                str = "INSERT INTO PurchaseProducts(ProductsId,Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,PurchaseId,DiscountAmount) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "')";
                            }
                            else
                            {
                                str = "INSERT INTO PurchaseOrderProducts(ProductsId,Quantity,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,PurchaseOrderId,DiscountAmount) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "')";
                            }

                            connection.Execute(str);
                        }
                    }

                    using (var connection = _connections.NewFor<QuotationTermsRow>())
                    {
                        foreach (var item in data.QuotationTerms)
                        {
                            String str;
                            //if (request.MailType == "Purchase")
                            //{
                            str = "INSERT INTO PurchaseOrderTerms(TermsId,PurchaseOrderId) VALUES('" + item.TermsId + "','" + Convert.ToString(insalid) + "')";
                            //}
                            //else
                            //{
                            //    str = "INSERT INTO PurchaseOrderTerms(TermsId,SalesId) VALUES('" + item.TermsId + "','" + Convert.ToString(insalid) + "')";
                            //}

                            connection.Execute(str);
                        }
                    }

                    ////using (var connection = _connections.NewFor<QuotationChargesRow>())
                    //{
                    //    foreach (var item in data.AdditionalCharges)
                    //    {
                    //        String str;
                    //        if (request.MailType == "Purchase")
                    //        {
                    //            str = "INSERT INTO PurchaseCharges(ChargesId,InvoiceId) VALUES('" + item.ChargesId + "','" + Convert.ToString(insalid) + "')";
                    //        }
                    //        else
                    //        {
                    //            str = "INSERT INTO PurchaseOrderCharges(ChargesId,SalesId) VALUES('" + item.ChargesId + "','" + Convert.ToString(insalid) + "')";
                    //        }

                    //        connection.Execute(str);
                    //    }
                    //}

                    //using (var connection = _connections.NewFor<QuotationConcessionRow>())
                    //{
                    //    foreach (var item in data.AdditionalConcession)
                    //    {
                    //        String str;
                    //        if (request.MailType == "Purchase")
                    //        {
                    //            str = "INSERT INTO PurchaseConcession(ConcessionId,InvoiceId) VALUES('" + item.ConcessionId + "','" + Convert.ToString(insalid) + "')";
                    //        }
                    //        else
                    //        {
                    //            str = "INSERT INTO PurchaseOrderConcession(ConcessionId,SalesId) VALUES('" + item.ConcessionId + "','" + Convert.ToString(insalid) + "')";
                    //        }

                    //        connection.Execute(str);
                    //    }
                    //}

                }

                response.Id = insalid;
                response.Status = "Quotation moved to " + request.MailType + " scucessfully";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = ex.Message.ToString();
            }

            return response;

        }


        [HttpPost]
        [ServiceAuthorize("Quotation:Move To Quotation")]
        public StandardResponse MoveToQuotation(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();

            //var exist = new QuotationRow();
            //var i = QuotationRow.Fields;
            //exist = uow.Connection.TryFirst<QuotationRow>(q => q
            //.SelectTableFields()
            //.Select(i.Id)
            //.Where(i.EnquiryNo == request.Id));

            //if (exist != null)
            //{
            //    response.Id = exist.Id.Value;
            //    response.Status = "Already Moved!";
            //    return response;
            //}

            var data = new QuotationData();

            var enq = QuotationRow.Fields;
            data.Quotation = uow.Connection.TryById<QuotationRow>(request.Id, q => q
               .SelectTableFields()
               .Select(enq.ContactsId)
               .Select(enq.ContactPersonId)
               .Select(enq.Type)
               .Select(enq.AdditionalInfo)
               .Select(enq.SourceId)
               .Select(enq.StageId)
               .Select(enq.BranchId)
               .Select(enq.OwnerId)
               .Select(enq.AssignedId)
               .Select(enq.ReferenceName)
               .Select(enq.ReferencePhone)
               .Select(enq.LostReason)
               .Select(enq.QuotationN)
               .Select(enq.Status)
               .Select(enq.Date)
               );

            var enqp = QuotationProductsRow.Fields;
            data.QuotationProducts = uow.Connection.List<QuotationProductsRow>(q => q
                .SelectTableFields()
                .Select(enqp.ProductsId)
                .Select(enqp.Quantity)
                .Select(enqp.Mrp)
                .Select(enqp.Capacity)
                .Select(enqp.Unit)
                .Select(enqp.SellingPrice)
                .Select(enqp.Price)
                .Select(enqp.Discount)
                .Select(enqp.Description)
                .Where(enqp.QuotationId == request.Id)
                );

            var enqma = MultiRepQuotationRow.Fields;
            data.MultiAssign = uow.Connection.List<MultiRepQuotationRow>(q => q
                  .SelectTableFields()
                  .Select(enqma.Id)
                  .Select(enqma.AssignedId)
                  .Where(enqp.QuotationId == request.Id)
                );

            var quott = QuotationTermsRow.Fields;
            data.QuotationTerms = uow.Connection.List<QuotationTermsRow>(q => q
                .SelectTableFields()
                .Select(quott.TermsId)
                .Select(quott.QuotationId)
                .Where(quott.QuotationId == request.Id)
                );

            var quotcha = QuotationChargesRow.Fields;
            data.AdditionalCharges = uow.Connection.List<QuotationChargesRow>(q => q
                .SelectTableFields()
                .Select(quotcha.ChargesId)
                .Select(quotcha.QuotationId)
                .Where(quotcha.QuotationId == request.Id)
                );

            var quotcon = QuotationConcessionRow.Fields;
            data.AdditionalConcession = uow.Connection.List<QuotationConcessionRow>(q => q
                .SelectTableFields()
                .Select(quotcon.ConcessionId)
                .Select(quotcon.QuotationId)
                .Where(quotcon.QuotationId == request.Id)
                );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );

            //if (data.Company.AllowMovingNonClosedRecords != true)
            //{
            //    if (data.Quotation.Status == (Masters.StatusMaster)1)
            //    {
            //        throw new Exception("Please set the status of this Enquiry as closed, Pending before Cloning");
            //    }
            //}

            // try
            {
                using (var connection = _connections.NewFor<QuotationRow>())
                {
                    dynamic typ, brnh, subcontact, refname, refphone;

                    if (data.Quotation.Type != null)
                        typ = (int)data.Quotation.Type.Value;
                    else
                        typ = "null";

                    if (data.Quotation.BranchId != null)
                        brnh = Convert.ToString(data.Quotation.BranchId.Value);
                    else
                        brnh = "null";

                    if (data.Quotation.ContactPersonId != null)
                        subcontact = (int)data.Quotation.ContactPersonId.Value;
                    else
                        subcontact = null;

                    if (data.Quotation.ReferenceName != null)
                        refname = Convert.ToString(data.Quotation.ReferenceName);
                    else
                        refname = "null";

                    if (data.Quotation.ReferencePhone != null)
                        refphone = Convert.ToString(data.Quotation.ReferencePhone);
                    else
                        refphone = "null";

                    String str;
                    GetNextNumberResponse nextNumber = new QuotationController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    if (subcontact != null)
                    {
                        str = "INSERT INTO Quotation(QuotationNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,ReferenceName,ReferencePhone,QuotationN,ContactPersonId) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",N'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId.Value) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "'," + refname + "," + refphone + ",'" + nextNumber.SerialN + "'," + subcontact + ")";
                    }
                    else
                    {
                        str = "INSERT INTO Quotation(QuotationNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,ReferenceName,ReferencePhone,QuotationN) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",N'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId.Value) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "'," + refname + "," + refphone + ",'" + nextNumber.SerialN + "')";
                    }
                    //str = str.Replace("\n", " char(13) ");
                    connection.Execute(str);

                    var quo = QuotationRow.Fields;
                    data.Quotation = connection.TryFirst<QuotationRow>(l => l
                    .Select(quo.Id)
                    .Select(quo.ContactsId)
                    .OrderBy(quo.Id, desc: true)
                    );
                }

                //if (data.Enquiry.ContactsId == data.Enquiry.ContactsId)
                //{
                //    //throw new Exception("Something went wrong while generating Quotation from Enquiry\nOnly Quotation got generated, products are not copied");
                //    using (var connection = _connections.NewFor<MultiRepEnquiryRow>())
                //    {
                //        foreach (var item1 in data.MultiAssign)
                //        {
                //            String str1 = "INSERT INTO MultiRepEnquiry(AssignedId,EnquiryId) VALUES('" + Convert.ToString(item1.AssignedId.Value) + "','" + Convert.ToString(data.Enquiry.Id.Value) + "')";

                //            connection.Execute(str1);
                //        }
                //    }
                //}
                if (data.Quotation.ContactsId == data.Quotation.ContactsId)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Enquiry\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<QuotationProductsRow>())
                    {
                        foreach (var item in data.QuotationProducts)
                        {
                            var pro = ProductsRow.Fields;
                            var enqproduct = connection.TryById<ProductsRow>(item.ProductsId.Value, l => l
                            .Select(pro.TaxId1)
                            .Select(pro.TaxId1Type)
                            .Select(pro.TaxId1Percentage)
                            .Select(pro.TaxId2)
                            .Select(pro.TaxId2Type)
                            .Select(pro.TaxId2Percentage));

                            dynamic capacity, unit;
                            if (item.Capacity != null)
                                capacity = Convert.ToString(item.Capacity);
                            else
                                capacity = "null";
                            if (item.Unit != null)
                                unit = Convert.ToString(item.Capacity);
                            else
                                unit = "null";

                            //String str = "INSERT INTO QuotationProducts(ProductsId,Capacity,Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,QuotationId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + capacity + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(data.Quotation.Id.Value) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";
                            String str = "INSERT INTO QuotationProducts(ProductsId,[From],[To],[Date],[Destination],[HotelName],[Nights],[Adults],[Childrens],[MealPlan],Capacity,Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,QuotationId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + item.From + "','" + item.To + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + item.Destination + "','" + item.HotelName + "','" + item.Nights + "','" + item.Adults + "','" + item.Childrens + "','" + item.MealPlan + "','" + capacity + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(data.Quotation.Id.Value) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";


                            connection.Execute(str);
                        }
                    }
                }

                using (var connection = _connections.NewFor<QuotationTermsRow>())
                {
                    foreach (var item in data.QuotationTerms)
                    {


                        String str = "INSERT INTO QuotationTerms(TermsId,QuotationId) VALUES('" + item.TermsId + "','" + Convert.ToString(data.Quotation.Id.Value) + "')";


                        connection.Execute(str);
                    }
                }

                using (var connection = _connections.NewFor<QuotationChargesRow>())
                {
                    foreach (var item in data.AdditionalCharges)
                    {

                        String str = "INSERT INTO QuotationCharges(ChargesId,QuotationId) VALUES('" + item.ChargesId + "','" + Convert.ToString(data.Quotation.Id.Value) + "')";


                        connection.Execute(str);
                    }
                }

                using (var connection = _connections.NewFor<QuotationConcessionRow>())
                {
                    foreach (var item in data.AdditionalConcession)
                    {


                        String str = "INSERT INTO QuotationConcession(ConcessionId,QuotationId) VALUES('" + item.ConcessionId + "','" + Convert.ToString(data.Quotation.Id.Value) + "')";


                        connection.Execute(str);
                    }
                }

                response.Id = data.Quotation.Id.Value;
                response.Status = "Quotation Cloned successfully";
            }
            //catch (Exception ex)
            //{
            //    response.Status = ex.Message.ToString();
            //}

            return response;
        }

        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";
            try
            {
                //var sl = MyRow.Fields;
                //var data = new MyRow();

                //data = connection.TryFirst<MyRow>(q => q
                //    .SelectTableFields()
                //    .Select(sl.Id)
                //    .Select(sl.QuotationNo)
                //    .OrderBy(sl.Id, desc: true)
                //    );

                //if (data != null)
                //    response.Serial = (data.QuotationNo + 1).ToString();

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
                  .Select(br1.QuotationPrefix)
                  .Select(br1.QuotationSuffix)
                  .Select(br1.YearInPrefix)
                  .Select(br1.QuoStartNo)
                    .Where(br1.Id == Convert.ToInt32(UData.CompanyId))
                 );

                // if (request.BranchId.Trim() != "")
                {
                    data = connection.TryFirst<MyRow>(q => q
                        .SelectTableFields()
                        .Select(sl.Id)
                        .Select(sl.QuotationNo)
                         .Where(sl.CompanyId == Convert.ToInt32(UData.CompanyId))
                        .OrderBy(sl.Id, desc: true)
                        );
                }


                var Ypre = string.Empty;
                //int month = 2;
                int month = Convert.ToInt32(DateTime.Now.Month);
                var year = Convert.ToString(DateTime.Now.Year);
                // Ypre = year;

                if (Bdata.YearInPrefix == AdvanceCRM.Masters.YearInPrefix.Year)
                {
                    Ypre = year;
                }
                else if (Bdata.YearInPrefix == AdvanceCRM.Masters.YearInPrefix.FinacialYear)
                {
                    var yearF = year.Substring(year.Length - 2);
                    if (month >= 4)
                    {
                        var year1 = Convert.ToString(Convert.ToInt32(yearF) + 1);
                        Ypre = yearF + "" + year1;
                    }
                    else
                    {
                        var year1 = Convert.ToString(Convert.ToInt32(yearF) - 1);
                        Ypre = year1 + "" + yearF;
                    }
                }


                string stPre = string.Empty;
                string stsuf = string.Empty;
                if (Bdata.QuotationPrefix != null)
                {
                    stPre = Bdata.QuotationPrefix;
                }
                if (Bdata.QuotationSuffix != null)
                {
                    stsuf = Bdata.QuotationSuffix;
                }

                if (Bdata.QuoStartNo == null)
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "" + Ypre + "" + (data.QuotationNo + 1).ToString() + "" + stsuf;

                        response.Serial = (data.QuotationNo + 1).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "" + Ypre + "" + (1).ToString() + "" + stsuf;

                        response.Serial = (1).ToString();
                    }
                }
                else
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "" + Ypre + "" + (Bdata.QuoStartNo).ToString() + "" + stsuf;

                        response.Serial = (Bdata.QuoStartNo).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "" + Ypre + "" + (Bdata.QuoStartNo).ToString() + "" + stsuf;

                        response.Serial = (Bdata.QuoStartNo).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return response;
        }
    }

    public class QuotationData
    {
        public ContactsRow Contact { get; set; }
        public QuotationTemplateRow Template { get; set; }
        public QuotationRow Quotation { get; set; }
        public UserRow User { get; set; }

        public List<MultiRepQuotationRow> MultiAssign { get; set; }
        public List<QuotationProductsRow> QuotationProducts { get; set; }
        public List<QuotationTermsRow> QuotationTerms { get; set; }
        public List<QuotationChargesRow> AdditionalCharges { get; set; }
        public List<QuotationConcessionRow> AdditionalConcession { get; set; }
        public PurchaseRow LastInv1 { get; set; }
        public PurchaseOrderRow LastSale1 { get; set; }

        public InvoiceRow LastInv { get; set; }
        public SalesRow LastSale { get; set; }
        public QuotationFollowupsRow QuotationFollowups { get; set; }
        public CompanyDetailsRow Company { get; set; }
    }
}