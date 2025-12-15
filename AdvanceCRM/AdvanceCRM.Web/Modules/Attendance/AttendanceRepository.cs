
namespace AdvanceCRM.Attendance.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Attendance;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
  
    using MyRow =AttendanceRow;

    public class AttendanceRepository : BaseRepository
	{
    public AttendanceRepository(IRequestContext context) : base(context) { }
		public AttendanceRepository(IRequestContext context ,ISqlConnections connections) : base(context) { _connections = connections; }
		private readonly ISqlConnections _connections;

        
        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
             return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Update);
        }







        public SaveResponse PunchIn(IUnitOfWork uow, MyRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));

            // Ensure PunchIn time is set to the current time
            row.PunchIn = DateTime.Now;

            // Insert the record into the database
            return new SaveResponse
            {
                EntityId = (int)this.Create(uow, new SaveRequest<MyRow> { Entity = row }).EntityId
            };
        }

        public SaveResponse PunchOut(IUnitOfWork uow, int userId)
        {
            // Create a ListRequest with the criteria
            var request = new ListRequest
            {
                Criteria = new Criteria(fld.Name) == userId &
                           new Criteria(fld.PunchOut).IsNull()
            };

            // Find the latest PunchIn record for the user
            var row = this.List(uow.Connection, request).Entities.FirstOrDefault();

            if (row == null)
                throw new ValidationError("No active PunchIn record found for the user.");

            // Set PunchOut time to the current time
            row.PunchOut = DateTime.Now;

            // Update the record in the database
            return new SaveResponse
            {
                EntityId = (int)this.Update(uow, new SaveRequest<MyRow> { Entity = row }).EntityId
            };
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
             public MySaveHandler(IRequestContext context) : base(context) { }

             protected override void ValidateRequest()
             {
                 // ensure UserId is populated before base validation runs
                 if (string.IsNullOrWhiteSpace(Row.UserId))
                 {
                     if (Row.Name != null)
                         Row.UserId = Row.Name.Value.ToString();
                     else
                         Row.UserId = Context.User.GetIdentifier()?.ToString();
                 }

                 base.ValidateRequest();
             }
         }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            private readonly ISqlConnections _connections;
            public MyListHandler(IRequestContext context, ISqlConnections connections) : base(context) { _connections = connections; }
            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                //var user = (UserDefinition)Context.User.ToUserDefinition();
                var user = Context.User.ToUserDefinition();
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

                var str = fld.Name + " = " + user.UserId + " OR " + fld.ApprovedBy + " = " + user.UserId;

                var str1 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.Name + " = " + item.UserId.Value + " OR " + fld.ApprovedBy + " = " + item.UserId.Value;
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