
namespace AdvanceCRM.Enquiry.Repositories
{
    using Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Template;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using RestSharp;
    using AdvanceCRM.BizMail;
    using Newtonsoft.Json.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Mail;
    using System.IO;
    using MyRow = EnquiryRow;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
    using Serenity;
    using Serenity.Extensions.DependencyInjection;

    public class EnquiryRepository : BaseRepository
    {
        protected IWebHostEnvironment HostEnvironment { get; }
        protected ISqlConnections _connections { get; }

        public EnquiryRepository(IRequestContext context, ISqlConnections connections, IWebHostEnvironment hostEnvironment)
            : base(context)
        {
            HostEnvironment = hostEnvironment;
            _connections = connections;
        }

        public EnquiryRepository(IRequestContext context)
            : this(context,
                   Dependency.Resolve<ISqlConnections>(),
                   Dependency.Resolve<IWebHostEnvironment>())
        {
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public List<int> GetNotifyUsers(MyRow entity)
        {
            var userIds = new List<int>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();

                if (owner.HasValue())
                {
                    owner = conn.TryById<UserRow>(entity.OwnerId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );
                }
                if (assigned.HasValue())
                {
                    assigned = conn.TryById<UserRow>(entity.AssignedId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );
                }

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
                //if (entity.MultiAssignList.HasValue() && entity.MultiAssignList.Count > 0)
                //    userIds.AddRange(entity.MultiAssignList);
            }

            userIds = userIds.Distinct().ToList();
            userIds.Remove(Convert.ToInt32(Context.User.GetIdentifier()));

            return userIds;
        }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context, _connections, HostEnvironment).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context, _connections, HostEnvironment).Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyDeleteHandler(Context).Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, EnquiryListRequest request)
        {
            return new MyListHandler(Context, _connections).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>

        {
            private readonly ISqlConnections _connections;
            private readonly IWebHostEnvironment _hostEnvironment;

            public MySaveHandler(IRequestContext context, ISqlConnections connections, IWebHostEnvironment hostEnvironment)
                : base(context)
            {
                _connections = connections;
                _hostEnvironment = hostEnvironment;
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

                    str = "Enquiry Created and Assigned to <b>" + user.Username + "</b>";
                }
                else if (this.IsUpdate)
                {
                    if (Old.Status != Request.Entity.Status)
                    {
                        str = "Status Changed to <b>" + Request.Entity.Status.GetEnumDescription(Localizer) + "</b>.<br/>";
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

                        str = str + "Enquiry Assigned to <b>" + user.Username + "</b>.<br/>";
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
                    //if (Old.Type != Request.Entity.Type)
                    //{
                    //    StageRow stage;
                    //    using (var conn = _connections.NewFor<StageRow>())
                    //    {
                    //        var u = StageRow.Fields;

                    //        stage = conn.TryById<StageRow>(Request.Entity.StageId, q => q
                    //        .SelectTableFields()
                    //        .Select(u.Stage));
                    //    }
                    //    str = "Stage Changed to <b>" + stage.Stage + "</b>.<br/>";
                    //    type = 4;
                    //}
                }

                if (type != 0 && str.Length > 0)
                {
                    var Timeline = new TimelineRow();

                    Timeline.EntityType = Request.Entity.Table;
                    // Request.Entity.Id might not be populated for freshly
                    // inserted rows until AfterSave.  Use the row that was
                    // actually saved to retrieve the generated identity.
                    Timeline.EntityId = this.Row.Id;
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
                        new SqlUpdate("dbo.EnquiryFollowups")
                            .Set("Status", 2)
                            .Where("EnquiryId=" + Request.Entity.Id)
                            .Execute(this.Connection, ExpectedRows.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                }

                //Checking for auto Email and SMS
                var connection = _connections.NewByKey("Default");

                var Company = new CompanyDetailsRow();

                var e = CompanyDetailsRow.Fields;
                Company = connection.TryFirst<CompanyDetailsRow>(l => l
                    .Select(e.AutoEmailEnquiry)
                    .Select(e.AutoSMSEnquiry)
                );

                if (IsCreate)
                {
                    var c = EnquiryTemplateRow.Fields;
                    var Template = connection.TryFirst<EnquiryTemplateRow>( q => q
                         .SelectTableFields()
                         .Select(c.Sender)
                         .Select(c.Subject)
                         .Select(c.SMSTemplate)
                         .Select(c.EmailTemplate)
                         .Select(c.Host)
                         .Select(c.Port)
                         .Select(c.SSL)
                         .Select(c.EmailId)
                         .Select(c.EmailPassword)
                   .Where(c.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId));

                    var ct = ContactsRow.Fields;
                    var Contact = connection.TryById<ContactsRow>(Request.Entity.ContactsId, q => q
                       .SelectTableFields()
                       .Select(ct.Name)
                       .Select(ct.Email));


                    if (Company.AutoEmailEnquiry.HasValue)
                    {
                        if (Company.AutoEmailEnquiry.Value == true)
                        {
                            EnquiryController obj = new EnquiryController();

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
                                if (Template.Host != null)
                                {
                                    MailMessage mm = new MailMessage();
                                    var addr = new MailAddress(Template.EmailId, Template.Sender);

                                    mm.From = addr;
                                    mm.Sender = addr;
                                    mm.To.Add(Contact.Email);
                                    mm.Subject = Template.Subject;
                                    var msg = Template.EmailTemplate;
                                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                    msg = msg.Replace("#customername", Contact.Name);
                                    mm.Body = msg;

                                    if (Template.Attachment != null)
                                    {
                                        JArray att = JArray.Parse(Template.Attachment);
                                        foreach (var f in att)
                                        {
                                            if (f["Filename"].HasValue())
                                            {
                                                var path = Path.Combine(_hostEnvironment.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                                mm.Attachments.Add(new Attachment(path));
                                            }
                                        }
                                    }
                                    mm.IsBodyHtml = true;

                                    EmailHelper.Send(mm, Template.EmailId, Template.EmailPassword, (Boolean)Template.SSL, Template.Host, Template.Port.Value);
                                }
                                else
                                {
                                    MailMessage mm = new MailMessage();
                                    var addr = new MailAddress(User.EmailId, Template.Sender);

                                    mm.From = addr;
                                    mm.Sender = addr;
                                    mm.To.Add(Contact.Email);
                                    mm.Subject = Template.Subject;
                                    var msg = Template.EmailTemplate;
                                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                    msg = msg.Replace("#customername", Contact.Name);
                                    mm.Body = msg;

                                    if (Template.Attachment != null)
                                    {
                                        JArray att = JArray.Parse(Template.Attachment);
                                        foreach (var f in att)
                                        {
                                            if (f["Filename"].HasValue())
                                            {
                                                var path = Path.Combine(_hostEnvironment.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                                mm.Attachments.Add(new Attachment(path));
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
                    }

                    if (Company.AutoSMSEnquiry.HasValue)
                    {
                        if (Company.AutoSMSEnquiry.Value == true)
                        {
                            String msg = Template.SMSTemplate;
                            String TempId = Template.TemplateId;
                            msg = msg.Replace("#customername", Contact.Name);

                            SMSHelper.SendSMS(Contact.Phone, msg,TempId);
                        }
                    }
                }

                var repo = new EnquiryRepository(Context, _connections, _hostEnvironment);
                List<Int32> userIds = repo.GetNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Enquiry;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Enquiry#edit/" + Request.Entity.Id;
                notify.Text =Context.User.Identity.Name + " has" + (this.IsUpdate ? " Updated" : " Created") + " an Enquiry";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
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
                    .Select(e.EnquiryFollwupMandatory)
                    .Select(e.EnquiryProductsMandatory)
                    .Select(e.EnqEditNo)
                    
                );

                if(this.IsCreate)
                {
                    if (Company.EnqEditNo.Value == false)
                    {
                        GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(connection, new GetNextNumberRequest());
                        Request.Entity.EnquiryN = nextNumber.SerialN;
                        Request.Entity.EnquiryNo = int.Parse(nextNumber.Serial);
                    }
                }

                if (this.IsUpdate && Old.Status == Masters.StatusMaster.Closed)
                {
                    if (!Context.Permissions.HasPermission("Enquiry:Re-open Enquiry"))
                    {
                        throw new Exception("Authorization failed to change status!");
                    }
                }

                if (Company.EnquiryProductsMandatory.HasValue)
                {
                    if (Company.EnquiryProductsMandatory.Value == true)
                    {
                        if (Request.Entity.Products.Count < 1)
                        {
                            throw new Exception("Kindly add atleast one product for this Enquiry and then try saving");
                        }
                    }
                }

                /////MailWizz/// 
                var model = new MailModel();
                BizMailConfigRow Config;

                var menq = BizMailEnquiryRow.Fields;

                var br = UserRow.Fields;
                var UData = new UserRow();
                try
                {
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
                            // .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                            );
                       
                            model.EnqRow = connection1.List<BizMailEnquiryRow>(q => q
                                  .SelectTableFields()
                                  .Select(menq.Rule)
                                   .Select(menq.EnquiryStatus)
                                    .Select(menq.BmListId)
                                    .Select(menq.BmListListId)
                                  .Select(menq.StageId)
                                      .Select(menq.SourceId)
                                       .Select(menq.ClosingType)
                                        .Select(menq.Type)
                                  .Select(menq.Status)
                                  .Where(menq.CompanyId == Convert.ToInt32(UData.CompanyId))
                                );

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
                                    var stat = ruletype.EnquiryStatus;
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
                                    var request = new RestRequest($"/lists/{list}/subscribers", Method.Post);
                                    //request.AddHeader("postman-token", "a62f832c-c9e2-47a7-7769-2938f3b900ae");
                                    request.AddHeader("cache-control", "no-cache");
                                    request.AddHeader("x-mw-public-key", Config.Apikey);
                                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                                    request.AddParameter("application/x-www-form-urlencoded", "EMAIL=" + mail + "&FNAME=" + name + "&LNAME=" + name + "", ParameterType.RequestBody);
                                    RestResponse response = client.Execute(request);
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                { 
                }
                ////// MailWizz End/////

                if (this.IsUpdate)
                {
                    if (Company.EnquiryFollwupMandatory.HasValue && this.IsUpdate)
                    {
                        if (Company.EnquiryFollwupMandatory.Value == true)
                        {
                            if (connection.Count<EnquiryFollowupsRow>(new Criteria("EnquiryId = " + Request.Entity.Id)) < 1)
                            {
                                throw new Exception("Kindly add atleast one followup for this Enquiry and then try saving");
                            }
                        }
                    }
                }
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow, EnquiryListRequest>
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
                           .Select(qt.Enquiry)
                          .Where(qt.UserId == Convert.ToInt32(user.UserId))
                         );
                    }

                    if (User != null && User.Enquiry == true)
                    {
                        uid = Convert.ToInt32(User.UserId);                                            
                    }
                   
                }
                //For products filter
                if (user.UserId == 1 || user.UserId == uid)
                {
                    if (Request.ProductsId != null)
                    {
                        var od = EnquiryProductsRow.Fields.As("od");

                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.EnquiryId == fld.Id &
                                od.ProductsId == Request.ProductsId.Value)
                            .ToString()));
                    }

                    if (Request.DivisionId != null)
                    {
                        var od = EnquiryProductsRow.Fields.As("od");

                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.EnquiryId == fld.Id &
                                od.ProductsDivisionId == Request.DivisionId.Value)
                            .ToString()));
                    }

                    if (Request.AreaId != null)
                    {
                        var od = EnquiryRow.Fields.As("od");

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
                    var mr = MultiRepEnquiryRow.Fields;
                    data.Users2 = connection.List<MultiRepEnquiryRow>(q => q
                        .SelectTableFields()
                        .Select(mr.EnquiryId)
                        .Where(mr.AssignedId == user.UserId)
                        );
                }
                var str = "";
               
                
                    str = fld.OwnerId + " = " + user.UserId + " OR " + fld.AssignedId + " = " + user.UserId;

                    var str1 = "";
                    var str2 = "";

                    foreach (var item in data.Users1)
                    {
                        str1 = str1 + " OR " + fld.OwnerId + " = " + item.UserId.Value + " OR " + fld.AssignedId + " = " + item.UserId.Value;
                    }

                    foreach (var item in data.Users2)
                    {
                        str2 = str2 + " OR " + fld.Id + " = " + item.EnquiryId.Value;
                    }

                    if (Request.ProductsId.HasValue || Request.AreaId.HasValue || Request.DivisionId.HasValue)
                    {
                        if (Request.ProductsId != null)
                        {
                            var od = EnquiryProductsRow.Fields.As("od");

                            query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                                .Select("1")
                                .From(od)
                                .Where(
                                    od.EnquiryId == fld.Id &
                                    od.ProductsId == Request.ProductsId.Value)
                                .ToString()).ToString());
                        }

                        if (Request.AreaId != null)
                        {
                            var od = EnquiryRow.Fields.As("od");

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
                            var od = EnquiryProductsRow.Fields.As("od");

                            query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                                .Select("1")
                                .From(od)
                                .Where(
                                    od.EnquiryId == fld.Id &
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
                public List<MultiRepEnquiryRow> Users2 { get; set; }
            }
        }
        public class MailModel
        {
            public List<BizMailEnquiryRow> EnqRow { get; set; }
        }
    }
}