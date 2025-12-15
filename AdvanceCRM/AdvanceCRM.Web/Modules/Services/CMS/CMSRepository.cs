
namespace AdvanceCRM.Services.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Masters;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using RestSharp;
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Settings;
    using Newtonsoft.Json.Linq;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Mail;
    using System.Web;
    using MyRow = CMSRow;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Services.Endpoints;

    public class CMSRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext _context;

        public CMSRepository(IRequestContext context, ISqlConnections connections) : base(context)
        {
            _context = context;
            _connections = connections;
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public List<Int32> getNotifyUsers(MyRow Entity)
        {
            List<Int32> userIds = new List<Int32>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();
                owner = conn.TryById<UserRow>(Entity.AssignedBy, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                assigned = conn.TryById<UserRow>(Entity.AssignedTo, q => q
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
            userIds.Remove(Convert.ToInt32(Context.User.GetIdentifier()));

            return userIds;
        }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
             return new MySaveHandler(this).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(this).Process(uow, request, SaveRequestType.Update);
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
             return new MyListHandler(this).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly CMSRepository _repository;

            public MySaveHandler(CMSRepository repository) : base(repository._context)
            {
                _repository = repository;
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                int type = 0;
                string str = "";
                if (this.IsCreate)
                {
                    UserRow user;
                    using (var conn = _repository._connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = conn.TryById<UserRow>(Request.Entity.AssignedTo, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }
                    type = 1;

                    str = "CMS Created and Assigned to " + user.Username + "</b>"; ;
                }
                else if (this.IsUpdate)
                {
                    if (Old.Status != Request.Entity.Status)
                    {
                        str = "Status Changed to <b>" + Request.Entity.Status.GetEnumDescription(Localizer) + "</b>.<br/>";
                        type = 2;
                    }

                    if (Old.AssignedTo != Request.Entity.AssignedTo)
                    {
                        UserRow user;
                        using (var conn = _repository._connections.NewFor<UserRow>())
                        {
                            var u = UserRow.Fields;

                            user = conn.TryById<UserRow>(Request.Entity.AssignedTo, q => q
                            .SelectTableFields()
                            .Select(u.Username));
                        }

                        str = str + "CMS Assigned to <b>" + user.Username + "</b>.<br/>";
                        type = 3;
                    }
                    if (Old.StageId != Request.Entity.StageId)
                    {
                        StageRow stage;
                        using (var conn = _repository._connections.NewFor<StageRow>())
                        {
                            var u = StageRow.Fields;

                            stage = conn.TryById<StageRow>(Request.Entity.StageId, q => q
                            .SelectTableFields()
                            .Select(u.Stage));
                        }
                        str = "Stage Changed to <b>" + stage.Stage + "</b>.<br/>";
                        type = 4;
                    }
                    if (Old.ExpectedCompletion != Request.Entity.ExpectedCompletion)
                    {
                        str = "Expected Completion Date is Changed to <b>" + Request.Entity.ExpectedCompletion + "</b>.<br/>";
                        type = 5;
                    }
                }

                if (type != 0 && str.Length > 0)
                {
                    var Timeline = new TimelineRow();

                    // Request.Entity.Id may not be populated yet for new
                    // entities. Use the row instance that was actually saved
                    // to retrieve the generated identity.
                    Timeline.EntityType = Row.Table;
                    Timeline.EntityId = Row.Id.Value;
                    Timeline.InsertDate = null;
                    Timeline.Type = type;
                    Timeline.Text = str;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                List<Int32> userIds = _repository.getNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.CMS;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Services/CMS#edit/" + Request.Entity.Id;

                notify.Text = Context.User.Identity.Name + " has" + (this.IsUpdate ? " Updated" : " Created") + " a CMS";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                var connection = _repository._connections.NewByKey("Default");

                var Company = new CompanyDetailsRow();
                var e = CompanyDetailsRow.Fields;
                Company = connection.TryFirst<CompanyDetailsRow>(l => l

                    .Select(e.CmsEditNo)

                );
                if (this.IsCreate)
                {
                    if (Company.CmsEditNo.Value == false)
                    {
                        var nextNumberResponse = new CMSController(_repository._connections, _repository._context).GetNextNumber(connection, new GetNextNumberRequest
                        {
                            Prefix = "", // Add prefix if required
                            Length = 5   // Define the length of the number
                        });

                        if (nextNumberResponse != null)
                        {
                            Request.Entity.Cmsn = nextNumberResponse.SerialN; // Use the SerialN from the response
                            int cmsNo;
                            if (!int.TryParse(nextNumberResponse.Serial, out cmsNo))
                                cmsNo = 0;
                            Request.Entity.CMSNo = cmsNo; // Convert Serial to integer and set it safely
                        }
                    }
                }




                var ct = ContactsRow.Fields;
                var Contact = connection.TryById<ContactsRow>(Request.Entity.ContactsId, q => q
                   .SelectTableFields()
                   .Select(ct.Name)
                   .Select(ct.Email));

                /////MailWizz/// 
                var model = new MailModel();
                BizMailConfigRow Config;

                var menq = BizMailCmsRow.Fields;

                var br = UserRow.Fields;
                var UData = new UserRow();

                using (var connection1 = _repository._connections.NewFor<BizMailConfigRow>())
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

                    model.EnqRow = connection1.List<BizMailCmsRow>(q => q
                          .SelectTableFields()
                          .Select(menq.Rule)
                          .Select(menq.BmListId)
                          .Select(menq.BmListListId)
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
                        condition = true;
                        list = ruletype.BmListListId;


                        if (condition == true)
                        {
                            var client = new RestClient(Config.Apiurl + "/lists/" + list + "/subscribers");
                            var request = new RestRequest(string.Empty, Method.Post);
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

                if (this.IsUpdate && Old.Status == Masters.CMSStatusMaster.Closed)
                {
                    if (!Context.Permissions.HasPermission("CMS:Re-open CMS"))
                    {
                        throw new Exception("Authorization failed to change status!");
                    }
                }
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            private readonly CMSRepository _repository;

            public MyListHandler(CMSRepository repository) : base(repository._context)
            {
                _repository = repository;
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                int uid = 0;
                var User = new UserRow();
                var user = (UserDefinition)Context.User.ToUserDefinition();
                if (user.UserId != 1)
                {

                    using (var connection = _repository._connections.NewFor<UserRow>())
                    {
                        var qt = UserRow.Fields;
                        User = connection.TryFirst<UserRow>(q => q
                           .SelectTableFields()
                           .Select(qt.Enquiry)
                          .Where(qt.UserId == Convert.ToInt32(user.UserId))
                         );
                    }

                    if (User != null && User.Cms == true)
                    {
                        uid = Convert.ToInt32(User.UserId);
                    }

                }
                //For products filter
                if (user.UserId == 1 || user.UserId == uid)
                {
                    
                    return;
                }

                var data = new UsersList();

                using (var connection = _repository._connections.NewFor<UserRow>())
                {
                    var od = UserRow.Fields;
                    data.Users1 = connection.List<UserRow>(q => q
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Where(od.UpperLevel == user.UserId || od.UpperLevel2 == user.UserId || od.UpperLevel3 == user.UserId || od.UpperLevel4 == user.UserId || od.UpperLevel5 == user.UserId)
                        );
                }

                var str = fld.AssignedBy + " = " + user.UserId + " OR " + fld.AssignedTo + " = " + user.UserId;

                var str1 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.AssignedBy + " = " + item.UserId.Value + " OR " + fld.AssignedTo + " = " + item.UserId.Value;
                }

                query.Where(new Criteria("(" + str + str1 + ")"));

                //query.Where((fld.AssignedBy == user.UserId) | (fld.AssignedTo == user.UserId) | (fld.AssignedBy == item.UserId.Value) |(fld.AssignedTo == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
            }

        }

        public class MailModel
        {
            public List<BizMailCmsRow> EnqRow { get; set; }
        }
    }
}