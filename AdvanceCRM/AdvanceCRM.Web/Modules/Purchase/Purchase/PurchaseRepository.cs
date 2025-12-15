
namespace AdvanceCRM.Purchase.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Accounting;
    using AdvanceCRM.Accounting.Repositories;
    using AdvanceCRM.Masters;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = PurchaseRow;

    public class PurchaseRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public PurchaseRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public PurchaseRepository(IRequestContext context) : base(context) { }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public List<Int32> getNotifyUsers(MyRow Entity)
        {
            List<Int32> userIds = new List<Int32>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();

                if (owner.HasValue())
                {
                    owner = conn.TryById<UserRow>(Entity.OwnerId, q => q
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
                    assigned = conn.TryById<UserRow>(Entity.AssignedId, q => q
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
            private readonly PurchaseRepository _repository;

            public MySaveHandler(PurchaseRepository repository) : base(repository.Context)
            {
                _repository = repository;
            }

            protected override void AfterSave()
            {
                base.AfterSave();
                var connection = _repository._connections.NewByKey("Default");

                List<Int32> userIds = _repository.getNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Purchase;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Purchase#edit/" + Request.Entity.Id;
                notify.Text = Serenity.Authorization.Username + " has" + (this.IsUpdate ? " Updated" : " Created") + " a Purchase";
                notify.UserList = userIds;

                new NotificationsRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });

                if (Request.Entity.Type == (Masters.InvoiceTypeMaster)1)
                {
                    // Cashobook ///
                    var cashbook = new CashbookRow();

                    cashbook.Date = System.DateTime.Now;
                    cashbook.Type = (Masters.TransactionTypeMaster)2;

                    var s = AccountingHeadsRow.Fields;
                    var account = connection.TryFirst<AccountingHeadsRow>(l => l
                        .Select(s.Id)
                        .Select(s.Head)
                        .Where((s.Head == "Sundry creditors"))
                        );

                    if (account == null)
                    {

                        throw new Exception("Sundry creditors Account Head is not found in Accounting Heads\nKindly add in masters and try again");
                    }

                    cashbook.Head = account.Id;
                    cashbook.ContactsId = Request.Entity.PurchaseFromId;
                    cashbook.InvoiceNo = Request.Entity.InvoiceNo;
                    cashbook.CashOut = Request.Entity.Total;
                    cashbook.Narration = Convert.ToString((Masters.InvoiceTypeMaster)1);
                    cashbook.BankId = 1;

                    new CashbookRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<CashbookRow>
                    {
                        Entity = cashbook
                    });
                }
                else if (Request.Entity.Type == (Masters.InvoiceTypeMaster)3)
                {
                    // Cashobook ///
                    var cashbook = new CashbookRow();

                    cashbook.Date = System.DateTime.Now;
                    cashbook.Type = (Masters.TransactionTypeMaster)2;

                    var s = AccountingHeadsRow.Fields;
                    var account = connection.TryFirst<AccountingHeadsRow>(l => l
                        .Select(s.Id)
                        .Select(s.Head)
                        .Where((s.Head == "Sundry creditors"))
                        );

                    if (account == null)
                    {

                        throw new Exception("Sundry creditors Account Head is not found in Accounting Heads\nKindly add in masters and try again");
                    }

                    cashbook.Head = account.Id;
                    cashbook.ContactsId = Request.Entity.PurchaseFromId;
                    cashbook.InvoiceNo = Request.Entity.InvoiceNo;
                    cashbook.CashOut = Request.Entity.Total;
                    cashbook.Narration = Convert.ToString((Masters.InvoiceTypeMaster)3);
                    cashbook.BankId = 1;

                    new CashbookRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<CashbookRow>
                    {
                        Entity = cashbook
                    });
                }
                else if (Request.Entity.Type == (Masters.InvoiceTypeMaster)4)
                {
                    // Cashobook ///
                    var cashbook = new CashbookRow();

                    cashbook.Date = System.DateTime.Now;
                    cashbook.Type = (Masters.TransactionTypeMaster)2;

                    var s = AccountingHeadsRow.Fields;
                    var account = connection.TryFirst<AccountingHeadsRow>(l => l
                        .Select(s.Id)
                        .Select(s.Head)
                        .Where((s.Head == "Sundry creditors"))
                        );

                    if (account == null)
                    {

                        throw new Exception("Sundry debtors Account Head is not found in Accounting Heads\nKindly add in masters and try again");
                    }

                    cashbook.Head = account.Id;
                    cashbook.ContactsId = Request.Entity.PurchaseFromId;
                    cashbook.InvoiceNo = Request.Entity.InvoiceNo;
                    cashbook.CashOut = Request.Entity.Total;
                    cashbook.Narration = Convert.ToString((Masters.InvoiceTypeMaster)4);
                    cashbook.BankId = 1;

                    new CashbookRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<CashbookRow>
                    {
                        Entity = cashbook
                    });
                }



            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                if (this.IsUpdate && Old.Status == Masters.StatusMaster.Closed)
                {
                    if (!Serenity.Authorization.HasPermission("Purchase:Re-open Purchase"))
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
            private readonly PurchaseRepository _repository;

            public MyListHandler(PurchaseRepository repository) : base(repository.Context)
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

                    if (User.Purchase == true)
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

                var str = fld.OwnerId + " = " + user.UserId + " OR " + fld.AssignedId + " = " + user.UserId;

                var str1 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.OwnerId + " = " + item.UserId.Value + " OR " + fld.AssignedId + " = " + item.UserId.Value;
                }


                query.Where(new Criteria("(" + str + str1 + ")"));

                //query.Where((fld.AssignedBy == user.UserId) | (fld.AssignedTo == user.UserId) | (fld.AssignedBy == item.UserId.Value) |(fld.AssignedTo == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
            }
        }
    }
}
