namespace AdvanceCRM.Services.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Services;
    using Serenity;
    using Serenity.Data;
    using Serenity.Extensions.DependencyInjection;
    using Serenity.Services;

    public class TeleCallingAppointmentsRepository : BaseRepository
    {
        public TeleCallingAppointmentsRepository(IRequestContext context)
            : base(context)
        {
        }

        private static TeleCallingAppointmentsRow.RowFields fld
            => TeleCallingAppointmentsRow.Fields;

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<TeleCallingAppointmentsRow> request)
            => new MySaveHandler(Context).Process(uow, request);

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<TeleCallingAppointmentsRow> request)
            => new MySaveHandler(Context).Process(uow, request);

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
            => new MyDeleteHandler(Context).Process(uow, request);

        public RetrieveResponse<TeleCallingAppointmentsRow> Retrieve(IDbConnection connection, RetrieveRequest request)
            => new MyRetrieveHandler(Context).Process(connection, request);

        public ListResponse<TeleCallingAppointmentsRow> List(IDbConnection connection, ListRequest request)
            => new MyListHandler(Context).Process(connection, request);

        /// <summary>
        /// Pulls the TeleCalling header row, delegates to TeleCallingRepository.GetNotifyUsers,
        /// then ensures both the rep and current user are in the list.
        /// </summary>
        private static List<int> GetNotifyUsers(TeleCallingRow header, IRequestContext context, ISqlConnections connections)
        {
            // Delegate to your header repo (must be an instance method)
            var users = new TeleCallingRepository(context, connections)
                            .GetNotifyUsers(header);

            // ensure the rep is in
            if (!users.Contains(header.RepresentativeId.Value))
                users.Add(header.RepresentativeId.Value);

            // ensure the current user is in
            var me = int.Parse(context.User.GetIdentifier());
            if (!users.Contains(me))
                users.Add(me);

            return users;
        }

        private class MySaveHandler : SaveRequestHandler<TeleCallingAppointmentsRow>
        {
            public MySaveHandler(IRequestContext context)
                : base(context)
            {
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                // —— timeline entry on create ——
                if (IsCreate)
                {
                    var user = UnitOfWork.Connection.TryById<UserRow>(
                        Request.Entity.RepresentativeId.Value,
                        q => q.Select(UserRow.Fields.Username));

                    var tl = new TimelineRow
                    {
                        EntityType = TeleCallingRow.Fields.TableName,
                        EntityId = Request.Entity.TeleCallingId,
                        Type = 5,
                        Text = $"Appointment Scheduled and Assigned To: <b>{user.Username}</b><br/>Agenda: {Request.Entity.Details}",
                        InsertDate = null   // instead of ClearAssignment(...)
                    };

                    new TimelineRepository(Context).Create(
                        UnitOfWork,
                        new SaveRequest<TimelineRow> { Entity = tl });
                }

                // —— notifications ——
                // fetch the header row
                var header = UnitOfWork.Connection.TryById<TeleCallingRow>(
                    Request.Entity.TeleCallingId,
                    q => q.SelectTableFields()
                          .Select(TeleCallingRow.Fields.RepresentativeId));

                var users = GetNotifyUsers(header, Context, Dependency.Resolve<ISqlConnections>());

                var note = new NotificationsRow
                {
                    Module = Masters.NotificationModules.TeleCalling,
                    InsertDate = DateTime.Now,
                    InsertUserId = int.Parse(Context.User.GetIdentifier()),
                    Url = $"/Services/TeleCalling#edit/{Request.Entity.TeleCallingId}",
                    Text = $"{Context.User.Identity.Name} has {(IsUpdate ? "Updated" : "Created")} a TeleCalling Appointment",
                    UserList = users
                };

                new NotificationsRepository(Context).Create(
                    UnitOfWork,
                    new SaveRequest<NotificationsRow> { Entity = note });
            }
        }

        private class MyDeleteHandler : DeleteRequestHandler<TeleCallingAppointmentsRow>
        {
            public MyDeleteHandler(IRequestContext context) : base(context) { }
        }

        private class MyRetrieveHandler : RetrieveRequestHandler<TeleCallingAppointmentsRow>
        {
            public MyRetrieveHandler(IRequestContext context) : base(context) { }
        }

        private class MyListHandler : ListRequestHandler<TeleCallingAppointmentsRow>
        {
            public MyListHandler(IRequestContext context) : base(context) { }
        }
    }
}
