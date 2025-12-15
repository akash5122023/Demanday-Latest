
namespace AdvanceCRM.Sales.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Sales.Endpoints;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = InvoiceRow;
    using Serenity.Extensions.DependencyInjection;

    public class InvoiceRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public InvoiceRepository(IRequestContext context, ISqlConnections connections) : base(context)
        {
            _connections = connections;
        }

        public InvoiceRepository(IRequestContext context) : this(context, Dependency.Resolve<ISqlConnections>())
        {
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public static List<int> getNotifyUsers(MyRow entity, IRequestContext context, ISqlConnections connections)
        {
            var userIds = new List<int>();

            using (var conn = connections.NewFor<UserRow>())
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

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
             return new MyListHandler(Context, _connections).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly ISqlConnections _connections;

            public MySaveHandler(IRequestContext context, ISqlConnections connections)
                : base(context)
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
                    var connection = _connections.NewByKey("Default");

                    var Company = new CompanyDetailsRow();

                    var e = CompanyDetailsRow.Fields;
                    Company = connection.TryFirst<CompanyDetailsRow>(l => l
                        .Select(e.ValidDate)                        
                    );

                    if (Company.ValidDate.HasValue)
                    {
                        if (Company.ValidDate.Value == 1)
                        {
                            if (Request.Entity.DueDate.Value ==null)//|| Request.Entity.StateId.Value=="")

                            //if (Request.Entity.Products.Count < 1)
                            {
                                throw new Exception("Please Add Valid Date");
                            }
                        }
                    }

                    UserRow user;
                    using (var conn = _connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = conn.TryById<UserRow>(Request.Entity.AssignedId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }
                    type = 1;

                    str = "Proforma Created and Assigned to " + user.Username + "</b>"; 

                }
                else if (this.IsUpdate)
                {
                    var connection = _connections.NewByKey("Default");

                    var Company = new CompanyDetailsRow();

                    var e = CompanyDetailsRow.Fields;
                    Company = connection.TryFirst<CompanyDetailsRow>(l => l
                        .Select(e.ValidDate)
                    );

                    if (Company.ValidDate.HasValue)
                    {
                        if (Company.ValidDate.Value == 1)
                        {
                            if (Request.Entity.DueDate.Value == null)//|| Request.Entity.StateId.Value=="")
                            //if (Request.Entity.Products.Count < 1)
                            {
                                throw new Exception("Please Add Valid Date");
                            }
                        }
                    }

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

                        str = str + "Proforma Assigned to <b>" + user.Username + "</b>.<br/>";
                        type = 3;
                    }
                    if(Old.StageId!=Request.Entity.StageId)
                    {
                        StageRow stage;
                        using (var conn = _connections.NewFor<StageRow>())
                        {
                            var u = StageRow.Fields;

                            stage = conn.TryById<StageRow>(Request.Entity.StageId, q => q
                            .SelectTableFields()
                            .Select(u.Stage));
                        }
                        str = "Stage Changed to <b>" + stage.Stage+ "</b>.<br/>";
                        type = 4;
                    }                    
                    //if (Old.SourceId != Request.Entity.SourceId)
                    //{
                    //    SourceRow source;
                    //    using (var conn = _connections.NewFor<StageRow>())
                    //    {
                    //        var u = SourceRow.Fields;

                    //        source = conn.TryById<SourceRow>(Request.Entity.SourceId, q => q
                    //        .SelectTableFields()
                    //        .Select(u.Source));
                    //    }
                    //    str = "Stage Changed to <b>" + source.Source + "</b>.<br/>";
                    //    type = 6;
                    //}
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

                List<int> userIds = getNotifyUsers(Request.Entity, Context, _connections);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Proforma;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Proforma#edit/" + Request.Entity.Id;
                notify.Text = Serenity.Authorization.Username + " has" + (this.IsUpdate ? " Updated" : " Created") + " a Proforma";
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

                var e = CompanyDetailsRow.Fields;
                Company = connection.TryFirst<CompanyDetailsRow>(l => l
                    .Select(e.EnquiryFollwupMandatory)
                    .Select(e.EnquiryProductsMandatory)
                    .Select(e.InvEditNo)

                );

                if (this.IsCreate)
                {
                    if (Company.InvEditNo.Value == false || string.IsNullOrWhiteSpace(Request.Entity.InvoiceN))
                    {
                        GetNextNumberResponse nextNumber = Dependency.Resolve<InvoiceController>().GetNextNumber(connection, new GetNextNumberRequest());
                        Request.Entity.InvoiceN = nextNumber.SerialN;
                        Request.Entity.InvoiceNo = int.Parse(nextNumber.Serial);
                    }
                }


                if (this.IsUpdate && Old.Status == Masters.StatusMaster.Closed)
                {
                    if (!Serenity.Authorization.HasPermission("Proforma:Re-open Proforma"))
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
            private readonly ISqlConnections _connections;

            public MyListHandler(IRequestContext context, ISqlConnections connections)
                : base(context)
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

                    if (User != null && User.Sales == true)
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

                using (var connection = _connections.NewFor<UserRow>())
                {
                    var od = UserRow.Fields;
                    data.Users1 = connection.List<UserRow>(q => q
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Where(od.UpperLevel == user.UserId || od.UpperLevel2 == user.UserId || od.UpperLevel3 == user.UserId || od.UpperLevel4 == user.UserId || od.UpperLevel5 == user.UserId)
                        );
                }

                var str = fld.OwnerId + " = " + user.UserId + " OR " + fld.AssignedId + " = " + user.UserId;

                var str1 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.OwnerId + " = " + item.UserId.Value + " OR " + fld.AssignedId + " = " + item.UserId.Value;
                }

                query.Where(new Criteria("(" + str + str1 + ")"));

                //query.Where((fld.OwnerId == user.UserId) | (fld.AssignedId == user.UserId) | (fld.OwnerId == item.UserId.Value) |(fld.AssignedId == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
            }
        }
    }
}