
namespace AdvanceCRM.Quotation.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Quotation.Endpoints;
    using AdvanceCRM.Masters;
    using RestSharp;
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Contacts;
    using Newtonsoft.Json.Linq;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity;
    using Serenity.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Mail;
    using System.Web;
    using MyRow = QuotationRow;
    using UserRow = Administration.UserRow;

    public class QuotationRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public QuotationRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }


        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public static List<int> getNotifyUsers(MyRow entity,IRequestContext context,ISqlConnections _connections)
        {
            var userIds = new List<int>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();
                owner = conn.TryById<UserRow>(entity.OwnerId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                assigned = conn.TryById<UserRow>(entity.AssignedId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                if (owner.HasValue())
                {
                    userIds.Add(owner.UserId.Value);
                    userIds.Add(owner.UpperLevel.Value);
                    userIds.Add(owner.UpperLevel2.Value);
                    userIds.Add(owner.UpperLevel3.Value);
                    userIds.Add(owner.UpperLevel4.Value);
                    userIds.Add(owner.UpperLevel5.Value);
                }
                if (assigned.HasValue())
                {
                    userIds.Add(assigned.UserId.Value);
                    userIds.Add(assigned.UpperLevel.Value);
                    userIds.Add(assigned.UpperLevel2.Value);
                    userIds.Add(assigned.UpperLevel3.Value);
                    userIds.Add(assigned.UpperLevel4.Value);
                    userIds.Add(assigned.UpperLevel5.Value);
                }
            }

            userIds = userIds.Distinct().ToList();
            userIds.Remove(Convert.ToInt32(context.User.GetIdentifier()));

            return userIds;
        }
        
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context, _connections).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context, _connections).Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyDeleteHandler(Context).Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, QuotationListRequest request)
        {
            return new MyListHandler(Context, _connections).Process(connection, request);
        }

        //Undelete option
        //private class MyUndeleteHandler : UndeleteRequestHandler<MyRow> { }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly ISqlConnections _connections;
            public MySaveHandler(IRequestContext context, ISqlConnections connections) : base(context)
            {
                _connections = connections;
            }

            protected override void AfterSave()
            {

                base.AfterSave();

                int type = 0; 
                string str = "";
                if (this.IsCreate)
                {                 

                    UserRow user;
                    using (var conn = _connections.NewFor<UserRow>())
                    {

                        var u = UserRow.Fields;

                        user = conn.TryById<UserRow>(Request.Entity.AssignedId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }
                    type = 1;

                    str = "Quotation Created and Assigned to " + user.Username + "</b>"; ;
                }
                else if (this.IsUpdate)
                {
                    if (Old.Status != Request.Entity.Status)
                    {
                        str = "Status Changed to <b>" + Request.Entity.Status.GetDescription() + "</b>.<br/>";
                        type = 2;
                    }

                    if (Old.AssignedId != Request.Entity.AssignedId)
                    {
                        UserRow user;
                        using (var conn = _connections.NewFor<UserRow>())
                        {
                            var u = UserRow.Fields;

                            user = conn.TryById<UserRow>(Request.Entity.AssignedId, q => q
                            .SelectTableFields()
                            .Select(u.Username));
                        }

                        str = str + "Quotation Assigned to <b>" + user.Username + "</b>.<br/>";
                        type = 3;
                    }
                    if (Old.StageId != Request.Entity.StageId)
                    {
                        StageRow stage;
                        using (var conn = _connections.NewFor<StageRow>())
                        {
                            var u = StageRow.Fields;

                            stage = conn.TryById<StageRow>(Request.Entity.StageId, q => q
                            .SelectTableFields()
                            .Select(u.Stage));
                        }
                        str = "Stage Changed to <b>" + stage.Stage + "</b>.<br/>";
                        type = 4;
                    }
                }

                if (type != 0 && str.Length > 0)
                {
                    var Timeline = new TimelineRow();

                    Timeline.EntityType = Request.Entity.Table;
                    Timeline.EntityId = Row.Id;
                    Timeline.InsertDate = null;
                    Timeline.Type = type;
                    Timeline.Text = str;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                if (Request.Entity.Status == (Masters.StatusMaster)2)
                {
                    try
                    {
                        new SqlUpdate("dbo.QuotationFollowups")
                            .Set("Status", 2)
                            .Where("QuotationId=" + Request.Entity.Id)
                            .Execute(this.Connection, ExpectedRows.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                }
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                var connection = _connections.NewByKey("Default");

                var Company = new CompanyDetailsRow();

                var ct = ContactsRow.Fields;
                var Contact = connection.TryById<ContactsRow>(Request.Entity.ContactsId, q => q
                   .SelectTableFields()
                   .Select(ct.Name)
                   .Select(ct.Email));


                var e = CompanyDetailsRow.Fields;
                Company = connection.TryFirst<CompanyDetailsRow>(l => l
                    .Select(e.QuotationFollwupMandatory)
                    .Select(e.QuotationProductsMandatory)
                    .Select(e.QuoEditNo)
                ) ;

                if (this.IsCreate)
                {
                    if (Company.QuoEditNo.Value == false)
                    {
                        GetNextNumberResponse nextNumber = new QuotationController().GetNextNumber(connection, new GetNextNumberRequest());
                        Request.Entity.QuotationN = nextNumber.SerialN;
                        Request.Entity.QuotationNo = int.Parse(nextNumber.Serial);
                    }
                }

                if (this.IsUpdate && Old.Status == Masters.StatusMaster.Closed)
                {
                    if (!Context.Permissions.HasPermission("Quotation:Re-open Quotation"))
                    {
                        throw new Exception("Authorization failed to change status!");
                    }
                }

                if (Company.QuotationProductsMandatory.HasValue)
                {
                    if (Company.QuotationProductsMandatory.Value == true)
                    {
                        if (Request.Entity.Products.Count < 1)
                        {
                            throw new Exception("Kindly add atleast one product for this Quotation and then try saving");
                        }
                    }
                }

                /////MailWizz/// 
                var model = new MailModel();
                BizMailConfigRow Config;

                var menq = BizMailQuotationRow.Fields;

                var br = UserRow.Fields;
                var UData = new UserRow();

                using (var connection1 = _connections.NewFor<BizMailConfigRow>())
                {
                    UData = connection1.First<UserRow>(q => q
                   .SelectTableFields()
                   .Select(br.CompanyId)
                   .Where(br.UserId == Context.User.GetIdentifier())
                  );

                    var s = BizMailConfigRow.Fields;
                    Config = connection1.TryFirst<BizMailConfigRow>(q => q
                        .SelectTableFields()
                        .Select(s.Apiurl)
                        .Select(s.Apikey)
                        //.Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                    model.EnqRow = connection1.List<BizMailQuotationRow>(q => q
                          .SelectTableFields()
                          .Select(menq.Rule)
                          .Select(menq.QuotationStatus)
                          .Select(menq.BmListId)
                          .Select(menq.BmListListId)
                          .Select(menq.StageId)
                          .Select(menq.SourceId)
                          .Select(menq.ClosingType)
                          .Select(menq.Type)
                          .Select(menq.Status)
                          .Where(menq.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                }
                if (Config.Apiurl != null && Config.Apikey != null)
                {
                    foreach (var ruletype in model.EnqRow)
                    {
                        bool condition = false;
                        dynamic list = null;
                        var name = Contact.Name;
                        var mail = Contact.Email;
                        if (ruletype.Rule == Masters.MailRuleMaster.Status)
                        {
                            var stat = ruletype.QuotationStatus;
                            if (stat == Request.Entity.Status)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }
                        if (ruletype.Rule == Masters.MailRuleMaster.Stage)
                        {
                            var stage = ruletype.StageId;
                            if (stage == Request.Entity.StageId)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }
                        if (ruletype.Rule == Masters.MailRuleMaster.Source)
                        {
                            var sorce = ruletype.SourceId;
                            if (sorce == Request.Entity.SourceId)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }
                        if (ruletype.Rule == Masters.MailRuleMaster.Type)
                        {
                            var typ = ruletype.Type;
                            if (typ == Request.Entity.Type)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }
                        if (ruletype.Rule == Masters.MailRuleMaster.ClosingType)
                        {
                            var ctyp = ruletype.ClosingType;
                            if (ctyp == Request.Entity.ClosingType)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }


                        if (condition == true)
                        {
                            var client = new RestClient(Config.Apiurl + "/lists/" + list + "/subscribers");
                            var request = new RestRequest("", Method.Post);
                            //request.AddHeader("postman-token", "a62f832c-c9e2-47a7-7769-2938f3b900ae");
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("x-mw-public-key", Config.Apikey);
                            request.AddHeader("content-type", "application/x-www-form-urlencoded");
                            request.AddParameter("application/x-www-form-urlencoded", "EMAIL=" + mail + "&FNAME=" + name + "&LNAME=" + name + "", ParameterType.RequestBody);
                            RestResponse response = client.Execute(request);
                        }
                    }
                }
                ////// MailWizz End/////

                if (this.IsUpdate)
                {
                    if (Company.QuotationFollwupMandatory.HasValue && this.IsUpdate)
                    {
                        if (Company.QuotationFollwupMandatory.Value == true)
                        {
                            if (connection.Count<QuotationFollowupsRow>(new Criteria("QuotationId = " + Request.Entity.Id)) < 1)
                            {
                                throw new Exception("Kindly add atleast one followup for this Quotation and then try saving");
                            }
                        }
                    }
                }

                List<Int32> userIds = getNotifyUsers(Request.Entity,Context, _connections);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Quotation;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Quotation#edit/" + Request.Entity.Id;
                notify.Text = Context.User.Identity.Name+ " has" + (this.IsUpdate ? " Updated" : " Created") + " a Quotation";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }

            private string NumberToWords(int number)
            {
                if (number == 0)
                    return "zero";

                if (number < 0)
                    return "minus " + NumberToWords(Math.Abs(number));

                string words = "";
                if ((number / 1000000000) > 0)
                {
                    words += NumberToWords(number / 1000000000) + " Billion ";
                    number %= 1000000000;
                }

                if ((number / 10000000) > 0)
                {
                    words += NumberToWords(number / 10000000) + " Crore ";
                    number %= 10000000;
                }

                if ((number / 1000000) > 0)
                {
                    words += NumberToWords(number / 1000000) + " Million ";
                    number %= 1000000;
                }


                if ((number / 100000) > 0)
                {
                    words += NumberToWords(number / 100000) + " Lakh ";
                    number %= 100000;
                }


                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " Thousand ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += NumberToWords(number / 100) + " Hundred ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != "")
                        words += "and ";

                    var unitsMap = new[] { "zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                    var tensMap = new[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)
                            words += "-" + unitsMap[number % 10];
                    }
                }

                return words;
            }

            //protected override void AfterSave()
            //{
            //    base.AfterSave();

            //    //Sending mail
            //    int cId = Request.Entity.ContactsId.Value;

            //    var model = new QuotationReportData();
            //    var q = MyRow.Fields;
            //    var c = ContactsRow.Fields;
            //    var qp = QuotationProductsRow.Fields;

            //    using (var connection = _connections.NewFor<MyRow>())
            //    {
            //        model.Contacts = connection.TryById<ContactsRow>(Request.Entity.ContactsId.Value, cnts => cnts
            //        .SelectTableFields()
            //        .Select(c.Id)
            //        .Select(c.Name)
            //        .Select(c.Email)
            //        .Select(c.Phone)
            //        );

            //        model.Quotation = connection.TryById<MyRow>(Request.Entity.Id.Value, qtn => qtn
            //        .SelectTableFields()
            //        .Select(q.Id)
            //        .Select(q.Date)
            //        );

            //        model.QuotationProducts = connection.List<QuotationProductsRow>(qps => qps
            //            .SelectTableFields()
            //            .Select(qp.ProductsCode)
            //            .Select(qp.ProductsName)
            //            .Select(qp.Price)
            //            .Select(qp.Quantity)
            //            .Select(qp.Discount)
            //            .Select(qp.TaxType1)
            //            .Select(qp.Percentage1)
            //            .Select(qp.TaxType2)
            //            .Select(qp.Percentage2)
            //            .Select(qp.LineTotal)
            //            .Where(qp.QuotationId == Request.Entity.Id.Value));

            //        var qt = QuotationTermsRow.Fields;
            //        model.QuotationTerms = connection.List<QuotationTermsRow>(qts => qts
            //            .SelectTableFields()
            //            .Select(qt.Terms)
            //            .Where(qt.QuotationId == Request.Entity.Id.Value));
            //    }

            //    var message = new MailMessage();
            //    var addr = new MailAddress("vinaykulkarni89@hotmail.com", Texts.Site.Layout.CompanyName);

            //    if ((model.Contacts.Email.Trim() != "") || (model.Contacts.Email != null))
            //    {
            //        message.To.Add(model.Contacts.Email);
            //        message.Subject = "Quotation";
            //        message.Body ="<html><head><title></title></head>" +
            //            "<body style=\"align - self:stretch; align - items:center; align - items:center\">" +
            //            "<table style=\"width: 100%; border-collapse:collapse\" border=\"1\">" +
            //            "<tr>" +
            //            "<td align=\"center\"><h3> Quotation </h3></td>" +
            //            "</tr>" +
            //            "</table>" +

            //            "<table style=\"width: 100% ; border-collapse:collapse\" border=\"1\">" +
            //            "<tr>" +
            //            "<td align=\"center\" style=\"width: 20 % \">" +
            //            "&nbsp; <img src=\""+ Texts.Site.Layout.LogoLink +"\" height=\"80\" width=\"80\" />&nbsp;" +
            //            "</td>" +
            //            "<td align=\"center\" style=\"width: 40 % \">" +
            //            "<h3>"
            //            ;
            //        message.Body = message.Body + Texts.Site.Layout.CompanyName;
            //        message.Body = message.Body + "</h3>";
            //        message.Body = message.Body + Texts.Site.Layout.CompanyAddress + "<br />";
            //        message.Body = message.Body + Texts.Site.Layout.CompanyPhone +
            //            "</td>" +
            //            "<td align=\"left\" style=\"width: 40 % \">" +
            //            "<strong>&nbsp; Quotaion No </strong>: ";
            //        message.Body = message.Body + DateTime.Now.Year + "/" + Request.Entity.Id.Value + ", " +
            //            "<strong> Date </strong>:" +
            //            model.Quotation.Date.Value.ToShortDateString() + "<br />" +
            //            "<strong>&nbsp; Name </strong> :" +
            //            model.Contacts.Name + "<br />" +
            //            "<strong>&nbsp; Phone </strong>: " + model.Contacts.Phone + "<br />" +
            //            "<strong>&nbsp; Address </strong>: " + model.Contacts.Address +
            //            "</td>" +
            //            "</tr>" +
            //            "</table>"
            //            ;
            //        //Products

            //        message.Body = message.Body +
            //            "<table style=\"width: 100%; border-collapse:collapse\" border=\"1\">" +
            //            "<tr>" +
            //            "<td align=\"center\"><strong>Sr. No.</strong></td>" +
            //            "<td align=\"center\"><strong>Code</strong></td>" +
            //            "<td align=\"center\"><strong>Product</strong></td>" +
            //            "<td align=\"center\"><strong>Price</strong></td>" +
            //            "<td align=\"center\"><strong>Quantity</strong></td>" +
            //            "<td align=\"center\"><strong>Discount</strong></td>" +
            //            "<td align=\"center\"><strong>Tax</strong></td>" +
            //            "<td align=\"center\"><strong>Total</strong></td>" +
            //            "<td align=\"center\"><strong>Grand Total</strong></td>" +
            //            "</tr>";
            //        int srno = 1; decimal Total = 0; decimal gTotal = 0;

            //        foreach (var item in model.QuotationProducts)
            //        {
            //            message.Body = message.Body + "<tr>" +
            //                "<td align=\"center\">" + srno + "</td>" +
            //                "<td align=\"Left\">&nbsp;" + item.ProductsCode + "</td>" +
            //                "<td align=\"left\">&nbsp;" + item.ProductsName + "</td>" +
            //                "<td align=\"right\">" + item.Price.Value + "&nbsp;</td>" +
            //                "<td align=\"right\">" + item.Quantity.Value + "&nbsp;</td>" +
            //                "<td align=\"right\">" + item.Discount.Value + "&nbsp;</td>" +
            //                "<td align=\"left\">";

            //                if (item.TaxType1 != null)
            //                {
            //                    message.Body = message.Body + item.TaxType1 + ":" + item.Percentage1;
            //                }
            //                if (item.TaxType1 != null && item.TaxType2 != null)
            //                {
            //                    message.Body = message.Body + ",";
            //                }
            //                if (item.TaxType2 != null)
            //                {
            //                    message.Body = message.Body + item.TaxType2 + ":" + item.Percentage2;
            //                }

            //            message.Body = message.Body + "</td>" +
            //                "<td align=\"right\">" + item.LineTotal.Value + "&nbsp;</td>" +
            //                "<td align=\"right\">" + item.LineTotal.Value + "&nbsp;</td>";

            //            Total = Total + item.LineTotal.Value;
            //            gTotal = gTotal + item.LineTotal.Value;
            //            srno++;

            //            message.Body = message.Body + "</tr>";
            //        }

            //        int diff = 10 - srno;

            //        for (int i = diff; i > 0; i--)
            //        {
            //            message.Body = message.Body +
            //                "<tr>"+
            //                "<td>&nbsp;</td>"+
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "</tr>";
            //        }
            //        //Total

            //        message.Body = message.Body + "<tr>" +
            //            "<td colspan=\"8\" align=\"right\"><strong> Total:&nbsp;</strong></td>" +
            //            "<td align=\"right\"><strong>" + Total.ToString() + "&nbsp;</strong></td>" +
            //            "</tr>";

            //        //Grand total
            //        message.Body = message.Body +
            //            "<tr>" +
            //            "<td colspan=\"8\" align=\"right\"><strong> Grand Total:&nbsp;</strong></td>" +
            //            "<td align=\"right\"><strong>" + gTotal.ToString() + "&nbsp;</strong></td>" +
            //            "</tr>" +
            //            "<tr>" +
            //            "<td colspan=\"9\" bgcolor=\"wheat\">" +
            //            "<strong>Total in Words: &nbsp;</strong>" +
            //            NumberToWords((int)gTotal) +
            //            " Only </td>" +
            //            "</tr>" +
            //            "</table>" +

            //            //Terms

            //            "<h4>Terms &amp; Condition's</h4>" +
            //            "<table style=\"width: 100%; border-collapse:collapse\" border=\"1\">";
            //            int Tsrno = 1;

            //        foreach (var item in model.QuotationTerms)
            //        {
            //            message.Body = message.Body +
            //                "<tr>"+
            //                "<td align=\"Left\">"+Tsrno+ ".&nbsp;" + item.Terms+"</td>"+
            //                "</tr>";
            //            Tsrno++;
            //        }

            //        message.Body = message.Body +
            //            "</table>"+
            //            "</body>" +
            //            "</html>";


            //        message.IsBodyHtml = true;
            //        message.From = addr;
            //        message.Sender = addr;

            //        var mail = new SmtpClient();

            //        //Configuration
            //        NetworkCredential nc = new NetworkCredential("vinaykulkarni89@hotmail.com", "password");
            //        mail.Credentials = nc;
            //        mail.EnableSsl = true;
            //        mail.Host = "smtp.live.com";
            //        mail.Port = 587;

            //        mail.Send(message);
            //    }
            //    else
            //    {
            //        //throw new FileNotFoundException(@"[Email Id not found]");
            //    }            
            //}
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }

        private class MyListHandler : ListRequestHandler<MyRow, QuotationListRequest>
        {
            private readonly ISqlConnections _connections;
            public MyListHandler(IRequestContext context, ISqlConnections connections) : base(context)
            {
                _connections = connections;
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                int uid = 0;
                var User = new UserRow();
                var user = (UserDefinition)Context.User.ToUserDefinition();
                if (user.UserId != 1)
                {

                    using (var connection = _connections.NewFor<UserRow>())
                    {
                        var qt = UserRow.Fields;
                        User = connection.TryFirst<UserRow>(q => q
                           .SelectTableFields()
                           .Select(qt.Quotation)
                          .Where(qt.UserId == Convert.ToInt32(user.UserId))
                         );
                    }

                    if (User.Quotation == true)
                    {
                        uid = Convert.ToInt32(User.UserId);
                    }

                }
                //For products filter
                if (user.UserId == 1 || user.UserId == uid)
                {
                    if (Request.ProductsId != null)
                    {
                        var od = QuotationProductsRow.Fields.As("od");

                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.QuotationId == fld.Id &
                                od.ProductsId == Request.ProductsId.Value)
                            .ToString()));
                    }

                    if (Request.DivisionId != null)
                    {
                        var od = QuotationProductsRow.Fields.As("od");

                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.QuotationId == fld.Id &
                                od.ProductsDivisionId == Request.DivisionId.Value)
                            .ToString()));
                    }

                    if (Request.AreaId != null)
                    {
                        var od = QuotationRow.Fields.As("od");

                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.Id == fld.Id &
                                od.ContactsAreaId == Request.AreaId.Value)
                            .ToString()));
                    }

                    return;
                }

                var data = new UsersList();

                using (var connection = _connections.NewFor<UserRow>())
                {
                    var od = UserRow.Fields;
                    data.Users1 = connection.List<UserRow>(q => q
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Where(od.UpperLevel == user.UserId || od.UpperLevel2 == user.UserId || od.UpperLevel3 == user.UserId || od.UpperLevel4 == user.UserId || od.UpperLevel5 == user.UserId)
                        );
                    var mr = MultiRepQuotationRow.Fields;
                    data.Users2 = connection.List<MultiRepQuotationRow>(q => q
                        .SelectTableFields()
                        .Select(mr.QuotationId)
                        .Where(mr.AssignedId == user.UserId)
                        );
                }

                var str = fld.OwnerId + " = " + user.UserId + " OR " + fld.AssignedId + " = " + user.UserId;

                var str1 = "";
                var str2 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.OwnerId + " = " + item.UserId.Value + " OR " + fld.AssignedId + " = " + item.UserId.Value;
                }
                foreach (var item in data.Users2)
                {
                    str2 = str2 + " OR " + fld.Id + " = " + item.QuotationId.Value;
                }

                if (Request.ProductsId.HasValue || Request.AreaId.HasValue || Request.DivisionId.HasValue)
                {
                    if (Request.ProductsId != null)
                    {
                        var od = QuotationProductsRow.Fields.As("od");

                        query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.QuotationId == fld.Id &
                                od.ProductsId == Request.ProductsId.Value)
                            .ToString()).ToString());
                    }

                    if (Request.AreaId != null)
                    {
                        var od = QuotationRow.Fields.As("od");

                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.Id == fld.Id &
                                od.ContactsAreaId == Request.AreaId.Value)
                            .ToString()));
                    }

                    if (Request.DivisionId != null)
                    {
                        var od = QuotationProductsRow.Fields.As("od");

                        query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.QuotationId == fld.Id &
                                od.ProductsDivisionId == Request.DivisionId.Value)
                            .ToString()).ToString());
                    }
                }
                else
                {
                    query.Where(new Criteria("(" + str + str1 + str2 + ")"));
                }


                //query.Where((fld.OwnerId == user.UserId) | (fld.AssignedId == user.UserId) | (fld.OwnerId == item.UserId.Value) |(fld.AssignedId == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
                public List<MultiRepQuotationRow> Users2 { get; set; }
            }
        }
        public class MailModel
        {
            public List<BizMailQuotationRow> EnqRow { get; set; }
        }

    }
}