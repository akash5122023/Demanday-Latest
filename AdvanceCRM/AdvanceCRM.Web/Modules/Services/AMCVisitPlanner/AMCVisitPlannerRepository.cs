
namespace AdvanceCRM.Services.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Services;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MyRow = AMCVisitPlannerRow;

    public class AMCVisitPlannerRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext _context;

        public AMCVisitPlannerRepository(IRequestContext context, ISqlConnections connections) : base(context)
        {
            _context = context;
            _connections = connections;
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public List<int> GetNotifyUsers(MyRow Entity)
        {
            List<int> userIds;
            AMCRow eq;
            using (var conn = this._connections.NewFor<AMCRow>())
            {
                var od = AMCRow.Fields;
                eq = conn.TryById<AMCRow>(Entity.AMCId, q => q
                    .SelectTableFields()
                    .Select(od.Id)
                    );
            }
            var repo = new AMCRepository(Context, _connections);
            userIds = repo.GetNotifyUsers(eq);

            if (!userIds.Contains(Entity.RepresentativeId.Value) && Convert.ToInt32(_context.User.GetIdentifier()) != Entity.RepresentativeId.Value)
                userIds.Add(Entity.RepresentativeId.Value);

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
             return new MyListHandler(Context).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly AMCVisitPlannerRepository _repository;

            public MySaveHandler(AMCVisitPlannerRepository repository) : base(repository._context)
            {
                _repository = repository;
            }
            protected override void AfterSave()
            {
                base.AfterSave();

                if (this.IsCreate)
                {
                    UserRow user;
                    using (var connection = _repository._connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = connection.TryById<UserRow>(Request.Entity.RepresentativeId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }

                    var Timeline = new TimelineRow();

                    Timeline.EntityType = AMCRow.Fields.TableName;
                    Timeline.EntityId = Request.Entity.AMCId;
                    Timeline.InsertDate = null;
                    Timeline.Type = 5;
                    Timeline.Text = "Visit Scheduled Added and Assigned To: <b>" + user.Username + "</b><br/> Visit Details: " + Request.Entity.VisitDetails;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                List<int> userIds = _repository.GetNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.AMC;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Services/AMC#edit/" + Request.Entity.AMCId;

                notify.Text = _repository._context.User.Identity.Name + " has" + (this.IsUpdate ? " Updated" : " Created") + " an AMC Visit";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow> { public MyListHandler(IRequestContext context) : base(context) { } }
    }
}