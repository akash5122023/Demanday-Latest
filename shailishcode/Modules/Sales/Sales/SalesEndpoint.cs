
namespace AdvanceCRM.Sales.Endpoints
{
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Common;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Template;
    using MailChimp.Net;
    using MailChimp.Net.Models;
    using Nito.AsyncEx;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using Serenity.Abstractions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    
    using MyRepository = Repositories.SalesRepository;
    using MyRow = SalesRow;
    using AdvanceCRM.Administration;
    using Serenity.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    [Route("Services/Sales/Sales/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class SalesController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        [ActivatorUtilitiesConstructor]
        public SalesController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Manager = new MailChimpManager(assignKey());
        }

        public SalesController(ISqlConnections connections)
            : this(connections, Dependency.Resolve<IRequestContext>())
        {
        }

        public SalesController()
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

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, SalesListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }


        //Add to MailChimp

        private string assignKey()
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
        public MailChimpManager Manager;

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
        [HttpPost, ServiceAuthorize("Sales:Move to Clone")]
        public StandardResponse MoveToClone(IUnitOfWork uow, SendMailRequest request)
        {
            var response = new StandardResponse();

            //var exist = new SalesRow();
            //var i = SalesRow.Fields;
            //exist = uow.Connection.TryFirst<SalesRow>(q => q
            //.SelectTableFields()
            //.Select(i.Id)
            //.Where(i.QuotationNo == request.Id));

            //if (exist != null)
            //{
            //    response.Id = exist.Id.Value;
            //    response.Status = "Already Moved!";
            //    return response;
            //}

            var data = new SalesData();

            var quot = SalesRow.Fields;
            data.Sales = uow.Connection.TryById<SalesRow>(request.Id, q => q
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

            var quotp = SalesProductsRow.Fields;
            data.SalesProducts = uow.Connection.List<SalesProductsRow>(q => q
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
                .Where(quotp.SalesId == request.Id)
                );

            var quott = SalesTermsRow.Fields;
            data.SalesTerms = uow.Connection.List<SalesTermsRow>(q => q
                .SelectTableFields()
                .Select(quott.TermsId)
                .Select(quott.SalesId)
                .Where(quott.SalesId == request.Id)
                );

            var quotcha = SalesChargesRow.Fields;
            data.AdditionalCharges = uow.Connection.List<SalesChargesRow>(q => q
                .SelectTableFields()
                .Select(quotcha.ChargesId)
                .Select(quotcha.SalesId)
                .Where(quotcha.SalesId == request.Id)
                );

            var quotcon = SalesConcessionRow.Fields;
            data.AdditionalConcession = uow.Connection.List<SalesConcessionRow>(q => q
                .SelectTableFields()
                .Select(quotcon.ConcessionId)
                .Select(quotcon.SalesId)
                .Where(quotcon.SalesId == request.Id)
                );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );

            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Sales.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Sales as closed or pending before moving");
                }
            }

            int contactsid;
            int insalid;

         try
            {
                using (var connection = _connections.NewFor<InvoiceRow>())
                {
                    dynamic typ, brnh, con, po, Advacne, PackagingCharges, FreightCharges, Roundup, msg, refr, sub;
                    DateTime podate;

                    if (data.Sales.Type != null)
                        typ = (int)data.Sales.Type.Value;
                    else
                        typ = "";

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
                        msg = (int)data.Sales.MessageId.Value;
                    else
                        msg = 0;                   

                    if (data.Sales.Subject != null)
                        sub = data.Sales.Subject;
                    else sub = "";

                    if (data.Sales.Reference != null)
                        refr = data.Sales.Reference;
                    else refr = "";



                    GetNextNumberResponse nextNumber = GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    String str;
                    if (data.Sales.QuotationNo != null)
                    {
                        if (msg == 0)
                        {
                            str = "INSERT INTO Sales(InvoiceNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,PurchaseOrderNo,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,InvoiceN,QuotationN,Subject,Reference) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Sales.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Sales.AdditionalInfo + "','" + Convert.ToString(data.Sales.SourceId) + "','" + Convert.ToString(data.Sales.StageId.Value) + "'," + po + "," + brnh + ",'" + Convert.ToString(data.Sales.OwnerId.Value) + "','" + Convert.ToString(data.Sales.AssignedId.Value) + "'," + Advacne + "," + PackagingCharges + "," + FreightCharges + "," + Roundup + ",'" + Convert.ToString(data.Sales.Conversion) + "'," + (data.Sales.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Sales.FromCurrency) + "','" + Convert.ToString((Int32?)data.Sales.ToCurrency) + "'," + Convert.ToString(data.Sales.QuotationNo) + ",'" + data.Sales.QuotationDate.Value.ToString("yyyy-MM-dd") + "','" + nextNumber.SerialN + "','" + data.Sales.QuotationN + "','" + sub + "','" + refr + "')";
                        }
                        else
                        {
                            str = "INSERT INTO Sales(InvoiceNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,PurchaseOrderNo,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency, QuotationNo, QuotationDate,InvoiceN,QuotationN,Subject,Reference,MessageId) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Sales.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Sales.AdditionalInfo + "','" + Convert.ToString(data.Sales.SourceId) + "','" + Convert.ToString(data.Sales.StageId.Value) + "'," + po + "," + brnh + ",'" + Convert.ToString(data.Sales.OwnerId.Value) + "','" + Convert.ToString(data.Sales.AssignedId.Value) + "'," + Advacne + "," + PackagingCharges + "," + FreightCharges + "," + Roundup + ",'" + Convert.ToString(data.Sales.Conversion) + "'," + (data.Sales.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Sales.FromCurrency) + "','" + Convert.ToString((Int32?)data.Sales.ToCurrency) + "'," + Convert.ToString(data.Sales.QuotationNo) + ",'" + data.Sales.QuotationDate.Value.ToString("yyyy-MM-dd") + "','" + nextNumber.SerialN + "','" + data.Sales.QuotationN + "','" + sub + "','" + refr + "','" + msg + "')";
                        }
                    }
                    else
                    {
                        if (msg == 0)
                        {
                            str = "INSERT INTO Sales(InvoiceNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,PurchaseOrderNo,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency,InvoiceN,Subject,Reference) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Sales.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Sales.AdditionalInfo + "','" + Convert.ToString(data.Sales.SourceId) + "','" + Convert.ToString(data.Sales.StageId.Value) + "'," + po + "," + brnh + ",'" + Convert.ToString(data.Sales.OwnerId.Value) + "','" + Convert.ToString(data.Sales.AssignedId.Value) + "'," + Advacne + "," + PackagingCharges + "," + FreightCharges + "," + Roundup + ",'" + Convert.ToString(data.Sales.Conversion) + "'," + (data.Sales.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Sales.FromCurrency) + "','" + Convert.ToString((Int32?)data.Sales.ToCurrency) + "','" + nextNumber.SerialN + "','" + sub + "','" + refr + "')";

                        }
                        else
                        {
                            str = "INSERT INTO Sales(InvoiceNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,PurchaseOrderNo,BranchId,OwnerId,AssignedId,Advacne,PackagingCharges,FreightCharges,Roundup, Conversion, CurrencyConversion, FromCurrency, ToCurrency,InvoiceN,Subject,Reference,MessageId) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Sales.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Sales.AdditionalInfo + "','" + Convert.ToString(data.Sales.SourceId) + "','" + Convert.ToString(data.Sales.StageId.Value) + "'," + po + "," + brnh + ",'" + Convert.ToString(data.Sales.OwnerId.Value) + "','" + Convert.ToString(data.Sales.AssignedId.Value) + "'," + Advacne + "," + PackagingCharges + "," + FreightCharges + "," + Roundup + ",'" + Convert.ToString(data.Sales.Conversion) + "'," + (data.Sales.CurrencyConversion.Value ? 1 : 0) + ",'" + Convert.ToString((Int32?)data.Sales.FromCurrency) + "','" + Convert.ToString((Int32?)data.Sales.ToCurrency) + "','" + nextNumber.SerialN + "','" + sub + "','" + refr + "','" + msg + "')";
                        }
                    }

                    //if (data.Enquiry.ClosingType != null)
                    //    ctyp = (int)data.Enquiry.ClosingType.Value;
                    //else
                    //    ctyp = "null";

                    connection.Execute(str);


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
                        foreach (var item in data.SalesProducts)
                        {
                            String str;

                            str = "INSERT INTO SalesProducts(ProductsId,Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,SalesId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + item.TaxType1 + "','" + Convert.ToString(item.Percentage1.Value) + "','" + item.TaxType2 + "','" + Convert.ToString(item.Percentage2.Value) + "','" + Convert.ToString(insalid) + "','" + Convert.ToString(item.DiscountAmount.Value) + "','" + item.Description + "')";

                            connection.Execute(str);
                        }
                    }

                    using (var connection = _connections.NewFor<InvoiceTermsRow>())
                    {
                        foreach (var item in data.SalesTerms)
                        {
                            String str;
                            str = "INSERT INTO SalesTerms(TermsId,SalesId) VALUES('" + item.TermsId + "','" + Convert.ToString(insalid) + "')";

                            connection.Execute(str);
                        }
                    }

                    using (var connection = _connections.NewFor<InvoiceChargesRow>())
                    {
                        foreach (var item in data.AdditionalCharges)
                        {
                            String str;
                            str = "INSERT INTO SalesCharges(ChargesId,SalesId) VALUES('" + item.ChargesId + "','" + Convert.ToString(insalid) + "')";

                            connection.Execute(str);
                        }
                    }

                    using (var connection = _connections.NewFor<InvoiceConcessionRow>())
                    {
                        foreach (var item in data.AdditionalConcession)
                        {
                            String str;
                            str = "INSERT INTO SalesConcession(ConcessionId,SalesId) VALUES('" + item.ConcessionId + "','" + Convert.ToString(insalid) + "')";

                            connection.Execute(str);
                        }
                    }
                }

                response.Id = insalid;
                response.Status = "Sales Cloned scucessfully";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = ex.Message.ToString();
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


        [HttpPost]
        public MailChimpResponse AddToMailChimp(MailChimpRequest request)
        {
            var response = new MailChimpResponse();

            List<SalesRow> contacts;
            List<SalesRow> contacts1 = new List<SalesRow>();


            var e = SalesRow.Fields;

            using (var connection = _connections.NewFor<SalesRow>())
            {
                contacts = connection.List<SalesRow>(q => q
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

            var SalesListId = "";

            foreach (var item in ViewBag.ListData)
            {
                if (item.Name == request.ListName.Trim())
                {
                    SalesListId = item.Id;
                    break;
                }
            }

            if (SalesListId.IsNullOrEmpty())
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
                        ListId = SalesListId,
                        EmailType = "html",
                        IpSignup = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                        TimestampSignup = DateTime.UtcNow.ToString("s"),
                        MergeFields = new Dictionary<string, object>
                {
                    {"FNAME", item.ContactsName },
                    {"LNAME","" }
                }
                    };
                    var result = AsyncContext.Run(() => Manager.Members.AddOrUpdateAsync(SalesListId, member));
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

        //Getting next invoice number
        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";

           // try
            {
                var sl = MyRow.Fields;
                var data = new MyRow();

                var user = Context.User.ToUserDefinition() as UserDefinition;
                if (user == null)
                    return response;

                var companyId = user.CompanyId;

                var br1 = CompanyDetailsRow.Fields;
                var Bdata = new CompanyDetailsRow();
                Bdata = connection.First<CompanyDetailsRow>(q => q
                  .SelectTableFields()
                  .Select(br1.InvoicePrefix)
                  .Select(br1.InvoiceSuffix)
                  .Select(br1.YearInPrefix)
                  .Select(br1.InvStartNo)
                    .Where(br1.Id == companyId)
                 );

                // if (request.BranchId.Trim() != "")
                {
                    data = connection.TryFirst<MyRow>(q => q
                        .SelectTableFields()
                        .Select(sl.Id)
                        .Select(sl.InvoiceNo)
                         .Where(sl.CompanyId == companyId)
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
            //catch (Exception ex)
            //{
            //    return null;
            //}

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

        //Send SMS
        [HttpPost]
        public StandardResponse SendSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();
            ContactsRow Contact;
            SalesRow Sales;

            InvoiceTemplateRow Template;

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                var sl = SalesRow.Fields;
                Sales = connection.TryById<SalesRow>(request.Id, q => q
                     .SelectTableFields()
                                 .Select(sl.Id)
                                 .Select(sl.Total)
                                 .Select(sl.GrandTotal)
                                 );

                var c = ContactsRow.Fields;
                Contact = connection.TryFirst<ContactsRow>(q => q
                     .SelectTableFields()
                                 .Select(c.Id)
                                 .Select(c.Name)
                                 .Select(c.Phone)
                                 .Where(c.Id == Sales.ContactsId.Value)
                                 );

                var qt = InvoiceTemplateRow.Fields;
                Template = connection.TryFirst<InvoiceTemplateRow>(q => q
                   .SelectTableFields()
                               .Select(qt.SMSTemplate)
                               .Select(qt.TemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                                );
            }
            String msg = Template.SMSTemplate;
            String templid = Template.TemplateId;

            msg = msg.Replace("#customername", Contact.Name);
            msg = msg.Replace("#amount", Sales.GrandTotal.Value.ToString());
            msg = msg.Replace("#invoiceno", Sales.Id.Value.ToString());
            msg = HttpUtility.HtmlEncode(msg);

            try
            {
                response.Status = SMSHelper.SendSMS(Contact.Phone, msg,templid);
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        //Send SMS
        [HttpPost]
        public StandardResponse SendWati(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new SalesData();
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

                var qt = InvoiceTemplateRow.Fields;
                data.Template = connection.TryFirst<InvoiceTemplateRow>(q => q
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
            String tempId = "XXXXXXX";

            if (msg.Trim() == "#customername token can be used in message to auto add customers name")
            {
                throw new Exception("It seems you havent chnaged the default text, hence sending SMS is been cancelled");
            }

            var newmsg = msg.Replace("#customername", contact.ContactsName);
           // var newphn = msg.Replace("#MobileNo", contact.ContactsPhone);

            response.Status = SMSHelper.SendSMS(contact.ContactsPhone, newmsg,tempId);

            if (!response.Status.Contains(("SMS").ToUpper()))
            {
                throw new Exception("SMS Sending Failed!");
            }

            response.Status = "Successfull!!!";

            return response;
        }

        [ServiceAuthorize("Cashbook:Can Approve")]
        public StandardResponse Approve(SendSMSRequest request)
        {
            var response = new StandardResponse();

            try
            {
                var connection = _connections.NewByKey("Default");
                connection.Execute("UPDATE Sales SET ApprovedBy=" + Convert.ToInt32(Context.User.GetIdentifier()) + "WHERE Id=" + request.Id);

                var em = SalesRow.Fields;
                //var data = connection.TryById<CashbookRow>(request.Id, q => q
                //        .SelectTableFields()
                //        .Select(em.Id)
                //        .Select(em.Head)
                //        .Select(em.CashIn)
                //        .Select(em.CashOut)
                //        .Select(em.ContactsName)
                //        .Select(em.EmployeeName)
                //        .Select(em.RepresentativeDisplayName)


                //    );

                //connection.Execute("INSERT INTO Cashbook(Date,Type,Head,CashOut,Narration) VALUES('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "'," + 2 + "," + data.Head + "," + data.CashIn + ",'" + data.CashOut + ", For:" + data.RepresentativeDisplayName + " - Cashbook Id:  " + data.Id + "')");



                response.Status = "Approved";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message;
            }

            return response;
        }

        [ServiceAuthorize("Sales:Export")]
        public FileContentResult ListExcel(IDbConnection connection, SalesListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.SalesColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Sales_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        public class SalesData
        {
            public ContactsRow Contact { get; set; }
            public UserRow User { get; set; }
            public MyRow Sales { get; set; }
            public SalesRow LastInv { get; set; }
            public SalesFollowupsRow SalesFollowups { get; set; }
            public CompanyDetailsRow Company { get; set; }
            public InvoiceTemplateRow Template { get; set; }
            public List<SalesProductsRow> SalesProducts { get; set; }
            public List<SalesTermsRow> SalesTerms { get; set; }
            public List<SalesChargesRow> AdditionalCharges { get; set; }
            public List<SalesConcessionRow> AdditionalConcession { get; set; }            
            public SalesFollowupsRow InvoiceFollowups { get; set; }

        }
    }
}
