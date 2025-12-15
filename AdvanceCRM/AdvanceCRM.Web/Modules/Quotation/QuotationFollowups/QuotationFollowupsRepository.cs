
namespace AdvanceCRM.Quotation.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Quotation;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MyRow = QuotationFollowupsRow;

    public class QuotationFollowupsRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public QuotationFollowupsRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
            Context = context;
        }

        public QuotationFollowupsRepository(IRequestContext context)
            : this(context, Dependency.Resolve<ISqlConnections>())
        {
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        private static List<int> getNotifyUsers(MyRow entity, IRequestContext context, ISqlConnections connections)
        {
            List<int> userIds;
            QuotationRow eq;
            using (var conn = connections.NewFor<QuotationRow>())
            {
                var od = QuotationRow.Fields;
                eq = conn.TryById<QuotationRow>(entity.QuotationId, q => q
                    .SelectTableFields()
                    .Select(od.Id));
            }

            userIds = QuotationRepository.getNotifyUsers(eq, context, connections);

            if (!userIds.Contains(entity.RepresentativeId.Value) && Convert.ToInt32(context.User.GetIdentifier()) != entity.RepresentativeId.Value)
                userIds.Add(entity.RepresentativeId.Value);

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

                if (this.IsCreate)
                {
                    UserRow user;
                    using (var connection = _connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = connection.TryById<UserRow>(Request.Entity.RepresentativeId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }

                    var Timeline = new TimelineRow();

                    Timeline.EntityType = QuotationRow.Fields.TableName;
                    Timeline.EntityId = Request.Entity.QuotationId;
                    Timeline.InsertDate = null;
                    Timeline.Type = 4;
                    Timeline.Text = "Followup Added and Assigned To: <b>" + user.Username + "</b><br/> Followup Note: " + Request.Entity.FollowupNote;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                List<int> userIds = getNotifyUsers(Request.Entity, Context, _connections);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Quotation;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Quotation#edit/" + Request.Entity.QuotationId;
                notify.Text = Serenity.Authorization.Username + " has" + (this.IsUpdate ? " Updated" : " Created") + " a Quotation Followup";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            public MyListHandler(IRequestContext context, ISqlConnections connections) : base(context)
            {
            }
        }
    }
}