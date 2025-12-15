
namespace AdvanceCRM.Sales.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Common;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Settings;

    using MailChimp.Net;
    using MailChimp.Net.Models;
    //using Nito.AsyncEx;
    //using Serenity;
    using Serenity.Data;
    using Dapper;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    
    using Template;


    using MyRepository = Repositories.InvoiceRepository;
    using MyRow = InvoiceRow;
    using Serenity;
    using Serenity.Extensions.DependencyInjection;

    //sing Authorization = Serenity.Authorization;

    [Route("Services/Sales/Invoice/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class InvoiceController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly MailChimpManager _mailChimp;
        private IRequestContext Context { get; }

        public InvoiceController(ISqlConnections connections,
                                 IConfiguration configuration,
                                 IRequestContext context,
                                 IWebHostEnvironment environment,
                                 MailChimpManager mailChimp)
        {
            _connections = connections;
            _configuration = configuration;
            _environment = environment;
            _mailChimp = mailChimp;
            Context = context;
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

        [HttpPost, ServiceAuthorize("Invoice:Can Approve")]
        public StandardResponse Approve(SendSMSRequest request)
        {
            var response = new StandardResponse();
            try
            {
                using var connection = _connections.NewByKey("Default");
                const string sql = "UPDATE Invoice SET ApprovedBy=@UserId WHERE Id=@Id";

                Dapper.SqlMapper.Execute(connection, sql, new { UserId = Convert.ToInt32(Context.User.GetIdentifier()), Id = request.Id });

                response.Status = "Approved";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message;
            }
            return response;
        }

        //Add to MailChimp

        private async Task<List<MailChimp.Net.Models.List>> ListDetails()
        {
            var listoptions = new MailChimp.Net.Core.ListRequest
            {
                Limit = 1000
            };
            var modelList = await _mailChimp.Lists.GetAllAsync(listoptions);
            var newList = modelList.OrderBy(x => x.Stats.MemberCount).ToList();
            return newList;
        }


        [HttpPost]
        public async Task<IActionResult> AddToMailChimp([FromBody] MailChimpRequest request)
        {
            var response = new MailChimpResponse();

            List<InvoiceRow> contacts;
            List<InvoiceRow> contacts1 = new List<InvoiceRow>();

            using (var connection = _connections.NewFor<InvoiceRow>())
            {
                connection.Open();
                const string sql = "SELECT Id, ContactsName, ContactsEmail FROM Invoice WHERE ContactsEmail IS NOT NULL";
                var data = await connection.QueryAsync<InvoiceRow>(sql);
                contacts = data.ToList();
            }

            foreach (var item in request.MailChimpIds)
            {
                contacts1.Add(contacts.Find(d => d.Id.ToString() == item));
            }

            contacts1.RemoveAll(x => x == null);

            var listData = await ListDetails();

            var SalesListId = "";

            foreach (var item in listData)
            {
                if (item.Name == request.ListName.Trim())
                {
                    SalesListId = item.Id;
                    break;
                }
            }

            if (string.IsNullOrEmpty(SalesListId))
            {
                return BadRequest($"List '{request.ListName}' not found, please create this list in MailChimp section");
            }

            foreach (var item in contacts1)
            {
                try
                {
                    var member = new Member
                    {
                        EmailAddress = item.ContactsEmail,
                        Status = Status.Subscribed,
                        StatusIfNew = Status.Subscribed,
                        ListId = SalesListId,
                        EmailType = "html",
                        IpSignup = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        TimestampSignup = DateTime.UtcNow.ToString("s"),
                        MergeFields = new Dictionary<string, object>
                        {
                            {"FNAME", item.ContactsName},
                            {"LNAME", ""}
                        }
                    };
                    await _mailChimp.Members.AddOrUpdateAsync(SalesListId, member);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error\n{ex.Message}");
                }
            }

            if (contacts1.Count > 0)
                return Ok(new { MailChimpReturnResponse = "Success" });

            return Ok(new { MailChimpReturnResponse = "In selected list no valid email Id where found" });
        }

        [ServiceAuthorize("Proforma:Export")]
        public FileContentResult ListExcel([FromServices] IDbConnection connection, [FromBody] ListRequest request)
        {
            var data = new MyRepository(Context).List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.InvoiceColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Invoice_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }


        //MoveToSales
        [HttpPost, ServiceAuthorize("Proforma:Move to Invoice")]
        public async Task<StandardResponse> MoveToInvoice([FromServices] IUnitOfWork uow, SendMailRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = new StandardResponse();

            // ensure we always have a valid connection
            var db = uow.Connection ?? _connections.NewByKey("Default");

            var exist = new SalesRow();
            var i = SalesRow.Fields;
            exist = db.TryFirst<SalesRow>(q => q
            .SelectTableFields()
            .Select(i.Id)
            .Where(i.QuotationNo == request.Id));

            if (exist != null)
            {
                response.Id = exist.Id.Value;
                response.Status = "Already Moved!";
                return response;
            }

            var data = new InvoiceData();

            var quot = InvoiceRow.Fields;
            data.Sales = db.TryById<InvoiceRow>(request.Id, q => q
               .SelectTableFields()
               .Select(quot.ContactsId)
               .Select(quot.Type)
               .Select(quot.AdditionalInfo)
               .Select(quot.SourceId)
               .Select(quot.StageId)
               .Select(quot.BranchId)
               .Select(quot.OwnerId)
               .Select(quot.AssignedId)
               .Select(quot.Status)
               .Select(quot.Advacne)
               .Select(quot.PackagingCharges)
               .Select(quot.FreightCharges)
               .Select(quot.Roundup)
               .Select(quot.Conversion)
               .Select(quot.CurrencyConversion)
               .Select(quot.ToCurrency)
               .Select(quot.QuotationNo)
               .Select(quot.QuotationDate)
               .Select(quot.QuotationN)
               .Select(quot.CompanyId)
               .Select(quot.PurchaseOrderNo)
               .Select(quot.PurchaseOrderDate)
                .Select(quot.Subject)
               .Select(quot.Reference)
               .Select(quot.MessageId)
               );

            var quotp = InvoiceProductsRow.Fields;
            data.SalesProducts = db.List<InvoiceProductsRow>(q => q
                .SelectTableFields()
                .Select(quotp.ProductsId)
                .Select(quotp.From)
                .Select(quotp.To)
                .Select(quotp.Date)
                .Select(quotp.Destination)
                .Select(quotp.HotelName)
                .Select(quotp.Nights)
                .Select(quotp.Adults)
                .Select(quotp.Childrens)
                .Select(quotp.MealPlan)
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
                .Where(quotp.InvoiceId == request.Id)
                );

            var quott = InvoiceTermsRow.Fields;
            data.SalesTerms = db.List<InvoiceTermsRow>(q => q
                .SelectTableFields()
                .Select(quott.TermsId)
                .Select(quott.InvoiceId)
                .Where(quott.InvoiceId == request.Id)
                );

            var quotcha = InvoiceChargesRow.Fields;
            data.AdditionalCharges = db.List<InvoiceChargesRow>(q => q
                .SelectTableFields()
                .Select(quotcha.ChargesId)
                .Select(quotcha.InvoiceId)
                .Where(quotcha.InvoiceId == request.Id)
                );

            var quotcon = InvoiceConcessionRow.Fields;
            data.AdditionalConcession = db.List<InvoiceConcessionRow>(q => q
                .SelectTableFields()
                .Select(quotcon.ConcessionId)
                .Select(quotcon.InvoiceId)
                .Where(quotcon.InvoiceId == request.Id)
                );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = db.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );

            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Sales.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Proforma as closed or pending before moving");
                }
            }

            int contactsid;
            int insalid;

            try
            {
                using (var connection = _connections.NewFor<InvoiceRow>())
                {
                    connection.Open();
                    dynamic typ, brnh, con, Advacne, PackagingCharges, FreightCharges, Roundup, msg, refr, sub;
                    var po=string.Empty;
                    DateTime podate;

                    if (data.Sales.Type != null)
                        typ = (int)data.Sales.Type.Value;
                    else
                        typ = "null";

                    if (data.Sales.BranchId != null)
                        brnh = Convert.ToString(data.Sales.BranchId.Value);
                    else
                        brnh = "null";

                    if (data.Sales.Conversion != null)
                        con = data.Sales.Conversion;
                    else
                        con = 1.00;

                    if (data.Sales.PurchaseOrderNo != null)
                        po = data.Sales.PurchaseOrderNo;
                    else
                        po = "null";

                    if (data.Sales.Advacne != null)
                        Advacne = data.Sales.Advacne;
                    else
                        Advacne = 0;

                    if (data.Sales.PackagingCharges != null)
                        PackagingCharges = data.Sales.PackagingCharges;
                    else
                        PackagingCharges = 0;

                    if (data.Sales.FreightCharges != null)
                        FreightCharges = data.Sales.FreightCharges;
                    else
                        FreightCharges = 0;

                    if (data.Sales.Roundup != null)
                        Roundup = data.Sales.Roundup;
                    else
                        Roundup = 0;


                    //if (data.Sales.MessageId != null)
                    //    msg = data.Sales.MessageId.Value;
                    //else msg = "null";

                    //if (data.Sales.MessageId != null)
                    //    msg = data.Sales.MessageId.Value;
                    //else msg = null;

                    if (data.Sales.MessageId != null)
                        msg = data.Sales.MessageId.Value;
                    else msg = "NULL";

                    if (data.Sales.Subject != null)
                        sub = data.Sales.Subject;
                    else sub = "";

                    if (data.Sales.Reference != null)
                        refr = data.Sales.Reference;
                    else refr = "";




                    var nextNumberResponse = this.GetNextNumber(db, new GetNextNumberRequest());
                    String str;
                    if (data.Sales.QuotationNo != null)
                    {
                        str = "INSERT INTO Sales(InvoiceNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,PurchaseOrderNo,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,InvoiceN,QuotationN,CompanyId,Subject,Reference,MessageId) VALUES(" + nextNumberResponse.Serial + ",'" + Convert.ToString(data.Sales.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Sales.AdditionalInfo + "','" + Convert.ToString(data.Sales.SourceId) + "','" + Convert.ToString(data.Sales.StageId.Value) + "','" + data.Sales.PurchaseOrderNo + "'," + brnh + ",'" + Convert.ToString(data.Sales.OwnerId.Value) + "','" + Convert.ToString(data.Sales.AssignedId.Value) + "'," + Advacne + "," + PackagingCharges + "," + FreightCharges + "," + Roundup + ",'" + Convert.ToString(data.Sales.Conversion) + "'," + (data.Sales.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Sales.FromCurrency) + "','" + Convert.ToString((Int32?)data.Sales.ToCurrency) + "'," + Convert.ToString(data.Sales.QuotationNo) + ",'" + data.Sales.QuotationDate.Value.ToString("yyyy-MM-dd") + "','" + nextNumberResponse.SerialN + "','" + data.Sales.QuotationN + "','" + data.Sales.CompanyId + "','" + sub + "','" + refr + "'," + msg + ")";
                    }
                    else
                    {
                        str = "INSERT INTO Sales(InvoiceNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,PurchaseOrderNo,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency,InvoiceN,CompanyId,Subject,Reference,MessageId) VALUES(" + nextNumberResponse.Serial + ",'" + Convert.ToString(data.Sales.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Sales.AdditionalInfo + "','" + Convert.ToString(data.Sales.SourceId) + "','" + Convert.ToString(data.Sales.StageId.Value) + "','" + data.Sales.PurchaseOrderNo + "'," + brnh + ",'" + Convert.ToString(data.Sales.OwnerId.Value) + "','" + Convert.ToString(data.Sales.AssignedId.Value) + "'," + Advacne + "," + PackagingCharges + "," + FreightCharges + "," + Roundup + ",'" + Convert.ToString(data.Sales.Conversion) + "'," + (data.Sales.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Sales.FromCurrency) + "','" + Convert.ToString((Int32?)data.Sales.ToCurrency) + "','" + nextNumberResponse.SerialN + "','" + data.Sales.CompanyId + "','" + sub + "','" + refr + "'," + msg + ")";
                    }

                    //if (data.Enquiry.ClosingType != null)
                    //    ctyp = (int)data.Enquiry.ClosingType.Value;
                    //else
                    //    ctyp = "null";

                    await connection.ExecuteAsync(str);


                    var inv = InvoiceRow.Fields;
                    data.LastInv = connection.TryFirst<SalesRow>(l => l
                        .Select(inv.Id)
                        .Select(inv.ContactsId)
                        .OrderBy(inv.Id, desc: true)
                    );

                    contactsid = data.LastInv.ContactsId.Value;
                    insalid = data.LastInv.Id.Value;

                }

                if (data.Sales.ContactsId == contactsid)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Enquiry\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<InvoiceProductsRow>())
                    {
                        connection.Open();
                        foreach (var item in data.SalesProducts)
                        {
                            String str;

                            //str = "INSERT INTO SalesProducts(ProductsId,Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,SalesId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";
                            str = "INSERT INTO SalesProducts(ProductsId,[From],[To],[Date],[Destination],[HotelName],[Nights],[Adults],[Childrens],[MealPlan],Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,SalesId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + item.From + "','" + item.To + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + item.Destination + "','" + item.HotelName + "','" + item.Nights + "','" + item.Adults + "','" + item.Childrens + "','" + item.MealPlan + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";


                            await connection.ExecuteAsync(str);
                        }
                    }

                    using (var connection = _connections.NewFor<InvoiceTermsRow>())
                    {
                        connection.Open();
                        foreach (var item in data.SalesTerms)
                        {
                            String str;
                            str = "INSERT INTO SalesTerms(TermsId,SalesId) VALUES('" + item.TermsId + "','" + Convert.ToString(insalid) + "')";

                            await connection.ExecuteAsync(str);
                        }
                    }

                    using (var connection = _connections.NewFor<InvoiceChargesRow>())
                    {
                        connection.Open();
                        foreach (var item in data.AdditionalCharges)
                        {
                            String str;
                            str = "INSERT INTO SalesCharges(ChargesId,SalesId) VALUES('" + item.ChargesId + "','" + Convert.ToString(insalid) + "')";

                            await connection.ExecuteAsync(str);
                        }
                    }

                    using (var connection = _connections.NewFor<InvoiceConcessionRow>())
                    {
                        connection.Open();
                        foreach (var item in data.AdditionalConcession)
                        {
                            String str;
                            str = "INSERT INTO SalesConcession(ConcessionId,SalesId) VALUES('" + item.ConcessionId + "','" + Convert.ToString(insalid) + "')";

                            await connection.ExecuteAsync(str);
                        }
                    }
                }

                response.Id = insalid;
                response.Status = "Proforma moved to Sales scucessfully";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = ex.Message.ToString();
            }

            return response;

        }

        //Send SMS
        [HttpPost]
        public StandardResponse SendSMS([FromServices] IUnitOfWork uow, [FromBody] SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new InvoiceData();

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                connection.Open();
                var sl = InvoiceRow.Fields;
                data.Sales = connection.TryById<InvoiceRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(sl.Id)
                     .Select(sl.Total)
                     .Select(sl.GrandTotal)
                     );

                var c = ContactsRow.Fields;
                data.Contact = connection.TryFirst<ContactsRow>(q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.Name)
                     .Select(c.Phone)
                     .Where(c.Id == data.Sales.ContactsId.Value)
                     );

                var qt = InvoiceTemplateRow.Fields;
                data.Template = connection.TryFirst<InvoiceTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SMSTemplate)
                   .Select(qt.TemplateId)
                  .Where(qt.CompanyId == (Context.User.ToUserDefinition()).CompanyId)
                    );
            }
            String msg = data.Template.SMSTemplate;
            String tempId = data.Template.TemplateId;

            msg = msg.Replace("#customername", data.Contact.Name);
            msg = msg.Replace("#amount", data.Sales.GrandTotal.Value.ToString());
            msg = msg.Replace("#invoiceno", data.Sales.Id.Value.ToString());
            msg = WebUtility.HtmlEncode(msg);

            try
            {
                response.Status = SMSHelper.SendSMS(data.Contact.Phone, msg,tempId);
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        //Send SMS
        [HttpPost]
        public StandardResponse SendWati([FromServices] IUnitOfWork uow, [FromBody] SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new InvoiceData();
            var c = ContactsRow.Fields;

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                connection.Open();
                data.Contact = connection.TryById<ContactsRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.Name)
                     .Select(c.Whatsapp)
                     .Select(c.Phone)
                     );

                var qt = InvoiceTemplateRow.Fields;
                data.Template = connection.TryFirst<InvoiceTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.WaTemplate)
                   .Select(qt.WaTemplateId)
                  .Where(qt.CompanyId == (Context.User.ToUserDefinition()).CompanyId)
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

            //try
            //{
            //    response.Status = SMSHelper.SendWati(data.Contact.Whatsapp, msg);
            //}
            //catch (Exception ex)
            //{
            //    response.Status = ex.Message.ToString();
            //}

            //return response;
        }

        //Send SMS for SMS Sender
        [HttpPost, ServiceAuthorize("SMS:BulkSMS")]
        public StandardResponse SendBulkSMS([FromServices] IUnitOfWork uow, [FromBody] SendSMSRequest request)
        {
            var response = new StandardResponse();

            MyRow contact;

            var e = MyRow.Fields;

            using (var connection = _connections.NewFor<MyRow>())
            {
                connection.Open();
                contact = connection.TryById<MyRow>(request.Id, q => q
                    .Select(e.Id)
                    .Select(e.ContactsName)
                    .Select(e.ContactsPhone)
                );
            }

            String msg = request.SMSType;
            String tempId = request.TemplateID;

            if (msg.Trim() == "#customername token can be used in message to auto add customers name")
            {
                throw new Exception("It seems you havent chnaged the default text, hence sending SMS is been cancelled");
            }

            var newmsg = msg.Replace("#customername", contact.ContactsName);

            response.Status = SMSHelper.SendSMS(contact.ContactsPhone, newmsg,tempId);

            if (!response.Status.Contains(("SMS").ToUpper()))
            {
                throw new Exception("SMS Sending Failed!");
            }

            response.Status = "Successfull!!!";

            return response;
        }

        [HttpPost, ServiceAuthorize("SMS:BulkMail")]
        public StandardResponse SendBulkMail([FromServices] IUnitOfWork uow, [FromBody] SendEmailRequest request)
        {
            var response = new StandardResponse();

            MyRow contact;

            var e = MyRow.Fields;

            using (var connection = _connections.NewFor<MyRow>())
            {
                connection.Open();
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

        public AdvanceCRM.GetNextNumberResponse GetNextNumber([FromServices] IDbConnection connection, [FromBody] GetNextNumberRequest request)
        {
            var response = new AdvanceCRM.GetNextNumberResponse();
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
                 .Where(br.UserId == Context.User.ToUserDefinition().UserId)
                );

                var br1 = CompanyDetailsRow.Fields;
                var Bdata = new CompanyDetailsRow();
                Bdata = connection.First<CompanyDetailsRow>(q => q
                  .SelectTableFields()
                  .Select(br1.InvoicePrefix)
                  .Select(br1.InvoiceSuffix)
                  .Select(br1.YearInPrefix)
                  .Select(br1.InvStartNo)
                    .Where(br1.Id == Convert.ToInt32(UData.CompanyId))
                 );

                // if (request.BranchId.Trim() != "")
                {
                    data = connection.TryFirst<MyRow>(q => q
                        .SelectTableFields()
                        .Select(sl.Id)
                        .Select(sl.InvoiceNo)
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
                if (Bdata.InvoicePrefix != null)
                {
                    stPre = Bdata.InvoicePrefix;
                }
                if (Bdata.InvoiceSuffix != null)
                {
                    stsuf = Bdata.InvoiceSuffix;
                }

                if (Bdata.InvStartNo == null)
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "" + Ypre + "" + (data.InvoiceNo + 1).ToString() + "" + stsuf;

                        response.Serial = (data.InvoiceNo + 1).ToString();
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

                        response.SerialN = stPre + "" + Ypre + "" + (Bdata.InvStartNo).ToString() + "" + stsuf;

                        response.Serial = (Bdata.InvStartNo).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "" + Ypre + "" + (Bdata.InvStartNo).ToString() + "" + stsuf;

                        response.Serial = (Bdata.InvStartNo).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            //try
            //{
            //    var sl = MyRow.Fields;
            //    var data = new MyRow();

            //    data = connection.TryFirst<MyRow>(q => q
            //        .SelectTableFields()
            //        .Select(sl.Id)
            //        .Select(sl.InvoiceNo)
            //        .OrderBy(sl.Id, desc: true)
            //        );

            //    if (data != null)
            //        response.Serial = (data.InvoiceNo + 1).ToString();
            //}
            //catch (Exception)
            //{

            //    return null;
            //}

            return response;
        }


        //MoveToInvoice
        [HttpPost, ServiceAuthorize("Proforma:Move to Clone")]
        public async Task<StandardResponse> MoveToClone([FromServices] IUnitOfWork uow, [FromBody] SendMailRequest request)
        {
            var response = new StandardResponse();

            var db = uow.Connection ?? _connections.NewByKey("Default");

            //if (request.MailType == "Invoice")
            //{
            //var exist = new InvoiceRow();
            //var i = InvoiceRow.Fields;
            //exist = uow.Connection.TryFirst<InvoiceRow>(q => q
            //.SelectTableFields()
            //.Select(i.Id)
            //.Where(i.QuotationNo == request.Id));

            //if (exist != null)
            //{
            //    response.Id = exist.Id.Value;
            //    response.Status = "Already Moved!";
            //    return response;
            //}          


            var data = new InvoiceData();

            var quot = InvoiceRow.Fields;
            data.Sales = db.TryById<InvoiceRow>(request.Id, q => q
              .SelectTableFields()
               .Select(quot.ContactsId)
               .Select(quot.Type)
               .Select(quot.AdditionalInfo)
               .Select(quot.SourceId)
               .Select(quot.StageId)
               .Select(quot.BranchId)
               .Select(quot.OwnerId)
               .Select(quot.AssignedId)
               .Select(quot.Status)
               .Select(quot.Advacne)
               .Select(quot.PackagingCharges)
               .Select(quot.FreightCharges)
               .Select(quot.Roundup)
               .Select(quot.Conversion)
               .Select(quot.CurrencyConversion)
               .Select(quot.ToCurrency)
               .Select(quot.QuotationNo)
               .Select(quot.QuotationDate)
               .Select(quot.QuotationN)
               .Select(quot.PurchaseOrderNo)
               .Select(quot.PurchaseOrderDate)
                .Select(quot.Subject)
               .Select(quot.Reference)
               .Select(quot.MessageId)
               );

            var quotp = InvoiceProductsRow.Fields;
            data.SalesProducts = db.List<InvoiceProductsRow>(q => q
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
                .Where(quotp.InvoiceId == request.Id)
                );

            var quott = InvoiceTermsRow.Fields;
            data.SalesTerms = db.List<InvoiceTermsRow>(q => q
                .SelectTableFields()
                .Select(quott.TermsId)
                .Select(quott.InvoiceId)
                .Where(quott.InvoiceId == request.Id)
                );

            var quotcha = InvoiceChargesRow.Fields;
            data.AdditionalCharges = db.List<InvoiceChargesRow>(q => q
                .SelectTableFields()
                .Select(quotcha.ChargesId)
                .Select(quotcha.InvoiceId)
                .Where(quotcha.InvoiceId == request.Id)
                );

            var quotcon = InvoiceConcessionRow.Fields;
            data.AdditionalConcession = db.List<InvoiceConcessionRow>(q => q
                .SelectTableFields()
                .Select(quotcon.ConcessionId)
                .Select(quotcon.InvoiceId)
                .Where(quotcon.InvoiceId == request.Id)
                );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = db.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );

            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Sales.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Proforma as closed or pending before moving");
                }
            }
            int contactsid;
            int insalid;

            try
            {
                using (var connection = _connections.NewFor<InvoiceRow>())
                {
                    connection.Open();
                    dynamic typ, brnh, con, po, Advacne, PackagingCharges, FreightCharges, Roundup, msg, refr, sub;
                    DateTime podate;

                    if (data.Sales.Type != null)
                        typ = (int)data.Sales.Type.Value;
                    else
                        typ = "null";

                    if (data.Sales.BranchId != null)
                        brnh = Convert.ToString(data.Sales.BranchId.Value);
                    else
                        brnh = "null";

                    if (data.Sales.Conversion != null)
                        con = data.Sales.Conversion;
                    else
                        con = 1.00;

                    if (data.Sales.PurchaseOrderNo != null)
                        po = data.Sales.PurchaseOrderNo;
                    else
                        po = "null";

                    if (data.Sales.Advacne != null)
                        Advacne = data.Sales.Advacne;
                    else
                        Advacne = 0;

                    if (data.Sales.PackagingCharges != null)
                        PackagingCharges = data.Sales.PackagingCharges;
                    else
                        PackagingCharges = 0;

                    if (data.Sales.FreightCharges != null)
                        FreightCharges = data.Sales.FreightCharges;
                    else
                        FreightCharges = 0;

                    if (data.Sales.Roundup != null)
                        Roundup = data.Sales.Roundup;
                    else
                        Roundup = 0;

                    if (data.Sales.MessageId != null)
                        msg = data.Sales.MessageId.Value;
                    else msg = "null";

                    if (data.Sales.Subject != null)
                        sub = data.Sales.Subject;
                    else sub = "";

                    if (data.Sales.Reference != null)
                        refr = data.Sales.Reference;
                    else refr = "";




                    var nextNumberResponse = this.GetNextNumber(db, new GetNextNumberRequest());
                    //  str = "INSERT INTO Invoice(InvoiceNo,InvoiceN,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,QuotationN) VALUES(" + nextNumberResponse.Serial + ",'" + nextNumberResponse.SerialN + "','" + Convert.ToString(data.Quotation.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Quotation.AdditionalInfo + "','" + Convert.ToString(data.Quotation.SourceId) + "','" + Convert.ToString(data.Quotation.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Quotation.OwnerId.Value) + "','" + Convert.ToString(data.Quotation.AssignedId.Value) + "',0,0,0,0,'" + Convert.ToString(data.Quotation.Conversion) + "'," + (data.Quotation.CurrencyConversion.HasValue() ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Quotation.FromCurrency) + "','" + Convert.ToString((Int32?)data.Quotation.ToCurrency) + "'," + Convert.ToString(request.Id) + ",'" + data.Quotation.Date.Value.ToString("yyyy-MM-dd") + "','" + data.Quotation.QuotationN + "')";

                    String str = "INSERT INTO Invoice(InvoiceNo,InvoiceN,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,QuotationN,CompanyId,Subject,Reference,MessageId) VALUES(" + nextNumberResponse.Serial + ",'" + nextNumberResponse.SerialN + "','" + Convert.ToString(data.Sales.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Sales.AdditionalInfo + "','" + Convert.ToString(data.Sales.SourceId) + "','" + Convert.ToString(data.Sales.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Sales.OwnerId.Value) + "','" + Convert.ToString(data.Sales.AssignedId.Value) + "',0,0,0,0,'" + Convert.ToString(data.Sales.Conversion) + "'," + (data.Sales.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Sales.FromCurrency) + "','" + Convert.ToString((Int32?)data.Sales.ToCurrency) + "'," + Convert.ToString(request.Id) + ",'" + data.Sales.Date.Value.ToString("yyyy-MM-dd") + "','" + data.Sales.QuotationN + "','" + Convert.ToString(data.Sales.CompanyId.Value) + "','" + sub + "','" + refr + "','" + msg + "')";

                    await connection.ExecuteAsync(str);

                    var inv = InvoiceRow.Fields;
                    data.LastIn = connection.TryFirst<InvoiceRow>(l => l
                    .Select(inv.Id)
                    .Select(inv.ContactsId)
                    .OrderBy(inv.Id, desc: true)
                    );

                    contactsid = data.LastIn.ContactsId.Value;
                    insalid = data.LastIn.Id.Value;
                }

                if (data.Sales.ContactsId == contactsid)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Quotation\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<InvoiceProductsRow>())
                    {
                        connection.Open();
                        foreach (var item in data.SalesProducts)
                        {
                            String str;

                            str = "INSERT INTO InvoiceProducts(ProductsId,Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,InvoiceId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";
                            await connection.ExecuteAsync(str);
                        }
                    }

                    using (var connection = _connections.NewFor<InvoiceTermsRow>())
                    {
                        connection.Open();
                        foreach (var item in data.SalesTerms)
                        {
                            String str;

                            str = "INSERT INTO InvoiceTerms(TermsId,InvoiceId) VALUES('" + item.TermsId + "','" + Convert.ToString(insalid) + "')";


                            await connection.ExecuteAsync(str);
                        }
                    }

                    using (var connection = _connections.NewFor<QuotationChargesRow>())
                    {
                        connection.Open();
                        foreach (var item in data.AdditionalCharges)
                        {
                            String str;

                            str = "INSERT INTO InvoiceCharges(ChargesId,InvoiceId) VALUES('" + item.ChargesId + "','" + Convert.ToString(insalid) + "')";
                            await connection.ExecuteAsync(str);
                        }
                    }

                    using (var connection = _connections.NewFor<QuotationConcessionRow>())
                    {
                        connection.Open();
                        foreach (var item in data.AdditionalConcession)
                        {
                            String str;

                            str = "INSERT INTO InvoiceConcession(ConcessionId,InvoiceId) VALUES('" + item.ConcessionId + "','" + Convert.ToString(insalid) + "')";
                            await connection.ExecuteAsync(str);
                        }
                    }

                }

                response.Id = insalid;
                response.Status = "Invoice Cloned scucessfully";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = ex.Message.ToString();
            }

            return response;

        }

        public class InvoiceData
        {
            public ContactsRow Contact { get; set; }
            public UserRow User { get; set; }
            public MyRow Sales { get; set; }
            public SalesRow LastInv { get; set; }

            public InvoiceRow LastIn { get; set; }
            public List<InvoiceProductsRow> SalesProducts { get; set; }
            public List<InvoiceTermsRow> SalesTerms { get; set; }
            public List<InvoiceChargesRow> AdditionalCharges { get; set; }
            public List<InvoiceConcessionRow> AdditionalConcession { get; set; }
            public CompanyDetailsRow Company { get; set; }
            public InvoiceTemplateRow Template { get; set; }
            public InvoiceFollowupsRow InvoiceFollowups { get; set; }
        }

    }
}
