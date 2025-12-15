
namespace AdvanceCRM.Accounting.Repositories
{
    using AdvanceCRM.Administration;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MyRow =CashbookRow;

    public class CashbookRepository : BaseRepository
    {
        public CashbookRepository(IRequestContext context) : base(context) { }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Update);
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

        private class MySaveHandler : SaveRequestHandler<MyRow> { public MySaveHandler(IRequestContext context) : base(context) { } }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow> { public MyListHandler(IRequestContext context) : base(context) { } }
       // {
            //protected override void ApplyFilters(SqlQuery query)
            //{
            //    base.ApplyFilters(query);

            //    var user = (UserDefinition)Context.User.ToUserDefinition();


            //    var data = new UsersList();

            //    using (var connection = SqlConnections.NewFor<UserRow>())
            //    {
            //        var od = UserRow.Fields;
            //        data.Users1 = connection.List<UserRow>(q => q
            //            .SelectTableFields()
            //            .Select(od.UserId)
            //            .Where(od.UpperLevel == user.UserId));

            //        data.Users2 = connection.List<UserRow>(q => q
            //            .SelectTableFields()
            //            .Select(od.UserId)
            //            .Where(od.UpperLevel2 == user.UserId));

            //        data.Users3 = connection.List<UserRow>(q => q
            //            .SelectTableFields()
            //            .Select(od.UserId)
            //            .Where(od.UpperLevel3 == user.UserId));

            //        data.Users4 = connection.List<UserRow>(q => q
            //            .SelectTableFields()
            //            .Select(od.UserId)
            //            .Where(od.UpperLevel4 == user.UserId));

            //        data.Users5 = connection.List<UserRow>(q => q
            //            .SelectTableFields()
            //            .Select(od.UserId)
            //            .Where(od.UpperLevel5 == user.UserId));
            //    }

            //    var str = fld.RepresentativeId + " = " + user.UserId + " OR " + fld.ApprovedBy + " = " + user.UserId;

            //    var str1 = ""; var str2 = ""; var str3 = ""; var str4 = ""; var str5 = "";

            //    foreach (var item in data.Users1)
            //    {
            //        str1 = str1 + " OR " + fld.RepresentativeId + " = " + item.UserId.Value + " OR " + fld.ApprovedBy + " = " + item.UserId.Value;
            //    }

            //    foreach (var item in data.Users2)
            //    {
            //        str2 = str2 + " OR " + fld.RepresentativeId + " = " + item.UserId.Value + " OR " + fld.ApprovedBy + " = " + item.UserId.Value;
            //    }

            //    foreach (var item in data.Users3)
            //    {
            //        str3 = str3 + " OR " + fld.RepresentativeId + " = " + item.UserId.Value + " OR " + fld.ApprovedBy + " = " + item.UserId.Value;
            //    }

            //    foreach (var item in data.Users4)
            //    {
            //        str4 = str4 + " OR " + fld.RepresentativeId + " = " + item.UserId.Value + " OR " + fld.ApprovedBy + " = " + item.UserId.Value;
            //    }

            //    foreach (var item in data.Users5)
            //    {
            //        str5 = str5 + " OR " + fld.RepresentativeId + " = " + item.UserId.Value + " OR " + fld.ApprovedBy + " = " + item.UserId.Value;
            //    }


            //    query.Where(new Criteria("(" + str + str1 + str2 + str3 + str4 + str5 + ")"));

            //    //query.Where((fld.OwnerId == user.UserId) | (fld.AssignedId == user.UserId) | (fld.OwnerId == item.UserId.Value) |(fld.AssignedId == item.UserId.Value));
            //}
            //public class UsersList
            //{
            //    public List<UserRow> Users1 { get; set; }
            //    public List<UserRow> Users2 { get; set; }
            //    public List<UserRow> Users3 { get; set; }
            //    public List<UserRow> Users4 { get; set; }
            //    public List<UserRow> Users5 { get; set; }
            //}
      //  }



    }
}