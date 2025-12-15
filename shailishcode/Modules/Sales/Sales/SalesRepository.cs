
namespace AdvanceCRM.Sales.Repositories
{
    using AdvanceCRM.Accounting;
    using AdvanceCRM.Accounting.Repositories;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Products;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Sales.Endpoints;
using Microsoft.AspNetCore.Mvc;
    using Serenity;
    using Serenity.Data;  
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = SalesRow;

    public class SalesRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public SalesRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public SalesRepository(IRequestContext context) : base(context) { }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public List<Int32> getNotifyUsers(MyRow Entity)
        {
            List<Int32> userIds = new List<Int32>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();
                owner = conn.TryById<UserRow>(Entity.OwnerId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                assigned = conn.TryById<UserRow>(Entity.AssignedId, q => q
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

        public ListResponse<MyRow> List(IDbConnection connection, SalesListRequest request)
        {
            return new MyListHandler(this).Process(connection, request);
        }


        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly SalesRepository _repository;

            public MySaveHandler(SalesRepository repository) : base(repository.Context)
            {
                _repository = repository;
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                var connection = _repository._connections.NewByKey("Default");

                int type = 0;
                string str = "";
                if (this.IsCreate)
                {
                    UserRow user;
                    using (var conn = _repository._connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = conn.TryById<UserRow>(Request.Entity.AssignedId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }
                    type = 1;

                    str = "Sales Created and Assigned to " + user.Username + "</b>";

                    if (Request.Entity.Advacne > 0)
                    {
                        var cashbook = new CashbookRow();

                        cashbook.Date = System.DateTime.Now;
                        cashbook.Type = (Masters.TransactionTypeMaster)1;

                        var s = AccountingHeadsRow.Fields;
                        var account = connection.TryFirst<AccountingHeadsRow>(l => l
                            .Select(s.Id)
                            .Select(s.Head)
                            .Where((s.Head == "Sundry debtors"))
                            );

                        if (account == null)
                        {

                            throw new Exception("Sundry debtors Account Head is not found in Accounting Heads\nKindly add in masters and try again");
                        }

                        cashbook.Head = account.Id;
                        cashbook.ContactsId = Request.Entity.ContactsId;
                        cashbook.InvoiceNo = Request.Entity.InvoiceN;
                        cashbook.CashIn = Request.Entity.Advacne;
                        cashbook.Narration = Convert.ToString((Masters.InvoiceTypeMaster)Request.Entity.Type);
                        cashbook.BankId = 1;

                        new CashbookRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<CashbookRow>
                        {
                            Entity = cashbook
                        });
                    }
                    else if (Request.Entity.Type == (Masters.InvoiceTypeMaster)1)
                    {
                        // Cashobook ///
                        var cashbook = new CashbookRow();

                        cashbook.Date = System.DateTime.Now;
                        cashbook.Type = (Masters.TransactionTypeMaster)1;

                        var s = AccountingHeadsRow.Fields;
                        var account = connection.TryFirst<AccountingHeadsRow>(l => l
                            .Select(s.Id)
                            .Select(s.Head)
                            .Where((s.Head == "Sundry debtors"))
                            );

                        if (account == null)
                        {

                            throw new Exception("Sundry debtors Account Head is not found in Accounting Heads\nKindly add in masters and try again");
                        }

                        cashbook.Head = account.Id;
                        cashbook.ContactsId = Request.Entity.ContactsId;
                        cashbook.InvoiceNo = Request.Entity.InvoiceN;
                        cashbook.CashIn = Request.Entity.GrandTotal;
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
                        cashbook.Type = (Masters.TransactionTypeMaster)1;

                        var s = AccountingHeadsRow.Fields;
                        var account = connection.TryFirst<AccountingHeadsRow>(l => l
                            .Select(s.Id)
                            .Select(s.Head)
                            .Where((s.Head == "Sundry debtors"))
                            );

                        if (account == null)
                        {

                            throw new Exception("Sundry debtors Account Head is not found in Accounting Heads\nKindly add in masters and try again");
                        }

                        cashbook.Head = account.Id;
                        cashbook.ContactsId = Request.Entity.ContactsId;
                        cashbook.InvoiceNo = Request.Entity.InvoiceN;
                        cashbook.CashIn = Request.Entity.GrandTotal;
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
                        cashbook.Type = (Masters.TransactionTypeMaster)1;

                        var s = AccountingHeadsRow.Fields;
                        var account = connection.TryFirst<AccountingHeadsRow>(l => l
                            .Select(s.Id)
                            .Select(s.Head)
                            .Where((s.Head == "Sundry debtors"))
                            );

                        if (account == null)
                        {

                            throw new Exception("Sundry debtors Account Head is not found in Accounting Heads\nKindly add in masters and try again");
                        }

                        cashbook.Head = account.Id;
                        cashbook.ContactsId = Request.Entity.ContactsId;
                        cashbook.InvoiceNo = Request.Entity.InvoiceN;
                        cashbook.CashIn = Request.Entity.GrandTotal;
                        cashbook.Narration = Convert.ToString((Masters.InvoiceTypeMaster)4);
                        cashbook.BankId = 1;

                        new CashbookRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<CashbookRow>
                        {
                            Entity = cashbook
                        });
                    }


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
                        using (var conn = _repository._connections.NewFor<UserRow>())
                        {
                            var u = UserRow.Fields;

                            user = conn.TryById<UserRow>(Request.Entity.AssignedId, q => q
                            .SelectTableFields()
                            .Select(u.Username));
                        }

                        str = str + "Sales Assigned to <b>" + user.Username + "</b>.<br/>";
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

                    new TimelineRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                List<Int32> userIds = _repository.getNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Sales;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Sales#edit/" + Request.Entity.Id;

                notify.Text = Serenity.Authorization.Username + " has" + (this.IsUpdate ? " Updated" : " Created") + " a Sales";
                notify.UserList = userIds;

                new NotificationsRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });

              

                //using (var connection = _connections.NewFor<MyRow>())
                //{
                //    var str = "UPDATE Sales SET Total=" + Request.Entity.Total.Value + " WHERE Id=" + Request.Entity.Id.Value;

                //    connection.Execute(str);
                //}

                //Set serial key as sold
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                var connection = _repository._connections.NewByKey("Default");

                var Company = new CompanyDetailsRow();

                var e = CompanyDetailsRow.Fields;
                Company = connection.TryFirst<CompanyDetailsRow>(l => l
                    .Select(e.EnquiryFollwupMandatory)
                    .Select(e.EnquiryProductsMandatory)
                    .Select(e.InvEditNo)

                );

                if (this.IsCreate)
                {
                    if (Company.InvEditNo.Value == false)
                    {
                        GetNextNumberResponse nextNumber = new SalesController(_repository._connections, _repository.Context).GetNextNumber(connection, new GetNextNumberRequest());
                        Request.Entity.InvoiceN = nextNumber.SerialN;
                        Request.Entity.InvoiceNo = int.Parse(nextNumber.Serial);
                    }
                }

                if (this.IsUpdate && Old.Status == Masters.StatusMaster.Closed)
                {
                    if (!Serenity.Authorization.HasPermission("Sales:Re-open Sales"))
                    {
                        throw new Exception("Authorization failed to change status!");
                    }
                }

        //        var fld = InventoryRow.Fields;
        //        var bld = BomRow.Fields;
        //        var bpld = BomProductsRow.Fields;

        //        // Loop through all products in SalesProducts
        //        for (var i = 0; i < Row.Products.Count; i++)
        //        {
        //            var salesProduct = Row.Products[i];  // Current SalesProduct

        //            // Fetch BOM based on selected BOM in SalesProduct (finished good product)
        //            //var bom = UnitOfWork.Connection.ById<BomRow>(salesProduct.ProductsId);
        //            //                    var bom = UnitOfWork.Connection.TryById<BomRow>(salesProduct.ProductsId);  // Use TryById to handle non-existing records

        //            // Fetch BOM based on ProductsId in SalesProduct
        //            var bom = UnitOfWork.Connection.Query<BomRow>(
        //                "SELECT * FROM Bom WHERE ProductsId = @ProductsId",
        //                new { ProductsId = salesProduct.ProductsId }
        //            ).FirstOrDefault();


        //            if (bom != null)
        //            {
        //                var bomId = bom.Id;  // Use this for clarity

        //                // --- Fetch Previous SalesProduct Quantity from Database ---
        //                var previousSalesProduct = UnitOfWork.Connection.Query<SalesProductsRow>(
        //                    "SELECT * FROM SalesProducts WHERE ProductsId = @ProductsId AND SalesId = @SalesId",
        //                    new { ProductsId = salesProduct.ProductsId, SalesId = Row.Id }
        //                ).FirstOrDefault();

        //                // If no previous record exists, set the previous quantity to 0 (for new records)
        //                var previousQuantity = previousSalesProduct?.Quantity ?? 0;
        //                var currentQuantity = salesProduct.Quantity ?? 0;

        //                // Calculate the difference between the new quantity and the old quantity
        //                var quantityDifference = currentQuantity - previousQuantity;

        //                // If the quantity difference is 0, skip further processing
        //                if (quantityDifference == 0)
        //                {
        //                    continue;
        //                }

        //                // --- BOM Quantity Validation and Update ---
        //                // Check if BOM quantity is valid (greater than 0)
        //                //if (bom.Quantity <= 0)
        //                //{
        //                //    throw new Exception($"The BOM for {bom.ItemName} has insufficient quantity (0 or less) and cannot be sold.");
        //                //}

        //                //// Ensure that the reduction won't result in a negative BOM quantity
        //                //if (bom.Quantity < quantityDifference)
        //                //{
        //                //    throw new Exception($"Insufficient BOM quantity for {bom.ItemName}. Cannot reduce by {quantityDifference} as only {bom.Quantity} is available.");
        //                //}

        //                // Update the BOM quantity based on the difference
        //                //                var sqlUpdateBomQuery = @"
        //                //    UPDATE Bom
        //                //    SET 
        //                //        Quantity = Quantity - @QuantityToReduce
        //                //    WHERE Id = @BomId;
        //                //";

        //                //                UnitOfWork.Connection.Execute(sqlUpdateBomQuery, new
        //                //                {
        //                //                    QuantityToReduce = quantityDifference,
        //                //                    BomId = bomId
        //                //                });

        //                var sqlUpdateInvBomQuery = @"
        //    UPDATE Inventory
        //    SET 
        //        Quantity = Quantity - @QuantityToReduce
        //    WHERE BomId = @BomId;
        //";

        //                UnitOfWork.Connection.Execute(sqlUpdateInvBomQuery, new
        //                {
        //                    QuantityToReduce = quantityDifference,
        //                    BomId = bomId
        //                });



        //                // --- Handle Inventory Reduction (For BOM Products) ---
        //                // Fetch BOM Products associated with the BOM
        //                var sql = "SELECT * FROM BomProducts WHERE BomId = @BomId";
        //                var bomProducts = UnitOfWork.Connection.Query<BomProductsRow>(sql, new { BomId = bomId });

        //                foreach (var bomProduct in bomProducts)
        //                {
        //                    // Fetch the existing inventory record
        //                    var existingInventory = UnitOfWork.Connection.Query<InventoryRow>(
        //                        "SELECT * FROM Inventory WHERE Id = @ProductsId",
        //                        new { ProductsId = bomProduct.ProductsId }  // Use named parameter
        //                    ).FirstOrDefault();

        //                    if (existingInventory != null)
        //                    {
        //                        // Calculate the quantity to reduce from the inventory based on the difference
        //                        var quantityToReduceFromInventory = quantityDifference * (bomProduct.Quantity ?? 0);

        //                        // Check if Inventory quantity is valid (greater than 0)
        //                        if (existingInventory.Quantity <= 0)
        //                        {
        //                            throw new Exception($"Insufficient inventory for product {existingInventory.Name}. Quantity is 0 or less.");
        //                        }

        //                        // Ensure that the reduction won't result in a negative inventory quantity
        //                        if (existingInventory.Quantity < quantityToReduceFromInventory)
        //                        {
        //                            throw new Exception($"Insufficient inventory for product {existingInventory.Name}. Cannot reduce by {quantityToReduceFromInventory} as only {existingInventory.Quantity} is available.");
        //                        }

        //                        // Update the inventory quantity based on the difference
        //                        var sqlUpdateInventoryQuery = @"
        //                       UPDATE Inventory
        //                        SET 
        //                       Quantity = Quantity - @QuantityToReduce
        //                   WHERE Id = @InventoryId;
        //                       ";

        //                        UnitOfWork.Connection.Execute(sqlUpdateInventoryQuery, new
        //                        {
        //                            QuantityToReduce = quantityToReduceFromInventory,
        //                            InventoryId = existingInventory.Id
        //                        });
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // --- BOM Not Found: Update Inventory Directly ---
        //                // Fetch the existing inventory record based on the product
        //                var existingInventory = UnitOfWork.Connection.Query<InventoryRow>(
        //                    "SELECT * FROM Inventory WHERE Id = @ProductsId",
        //                    new { ProductsId = salesProduct.ProductsId }  // Use named parameter
        //                ).FirstOrDefault();

        //                if (existingInventory != null)
        //                {
        //                    // --- Fetch Previous SalesProduct Quantity ---
        //                    var previousSalesProduct = UnitOfWork.Connection.Query<SalesProductsRow>(
        //                        "SELECT * FROM SalesProducts WHERE ProductsId = @ProductsId AND SalesId = @SalesId",
        //                        new { ProductsId = salesProduct.ProductsId, SalesId = Row.Id }
        //                    ).FirstOrDefault();

        //                    var previousQuantity = previousSalesProduct?.Quantity ?? 0;
        //                    var currentQuantity = salesProduct.Quantity ?? 0;
        //                    var quantityDifference = currentQuantity - previousQuantity;

        //                    if (quantityDifference == 0)
        //                    {
        //                        continue;
        //                    }

        //                    // Check if Inventory quantity is valid (greater than 0)
        //                    if (existingInventory.Quantity <= 0)
        //                    {
        //                        throw new Exception($"Insufficient inventory for product {existingInventory.Name}. Quantity is 0 or less.");
        //                    }

        //                    // Ensure that the reduction won't result in a negative inventory quantity
        //                    if (existingInventory.Quantity < quantityDifference)
        //                    {
        //                        throw new Exception($"Insufficient inventory for product {existingInventory.Name}. Cannot reduce by {quantityDifference} as only {existingInventory.Quantity} is available.");
        //                    }

        //                    // Update the inventory quantity based on the difference
        //                    var sqlUpdateInventoryQuery = @"
        //        UPDATE Inventory
        //        SET 
        //            Quantity = Quantity - @QuantityToReduce
        //        WHERE Id = @InventoryId;
        //    ";

        //                    UnitOfWork.Connection.Execute(sqlUpdateInventoryQuery, new
        //                    {
        //                        QuantityToReduce = quantityDifference,
        //                        InventoryId = existingInventory.Id
        //                    });
        //                }
        //            }

        //        }


                //
            }
        }
    
        



         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow, SalesListRequest>
        {
            private readonly SalesRepository _repository;

            public MyListHandler(SalesRepository repository) : base(repository.Context)
            {
                _repository = repository;
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                int uid = 0;
                UserRow userRow = null;
                var user = (UserDefinition)Context.User.ToUserDefinition();
                if (user.UserId != 1)
                {

                    using (var connection = _repository._connections.NewFor<UserRow>())
                    {
                        var qt = UserRow.Fields;
                        userRow = connection.TryFirst<UserRow>(q => q
                           .SelectTableFields()
                           .Select(qt.Enquiry)
                          .Where(qt.UserId == Convert.ToInt32(user.UserId))
                         );
                    }

                    if (userRow != null && userRow.Sales == true)
                    {
                        uid = Convert.ToInt32(userRow.UserId);
                    }

                }
                //For products filter
                if (user.UserId == 1 || user.UserId == uid)
                {
                    if (Request.ProductsId != null)
                    {
                        var od = SalesProductsRow.Fields.As("od");

                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.SalesId == fld.Id &
                                od.ProductsId == Request.ProductsId.Value)
                            .ToString()));
                    }

                    if (Request.DivisionId != null)
                    {
                        var od = SalesProductsRow.Fields.As("od");

                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.SalesId == fld.Id &
                                od.ProductsDivisionId == Request.DivisionId.Value)
                            .ToString()));
                    }

                    if (Request.AreaId != null)
                    {
                        var od = SalesRow.Fields.As("od");

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

                if (Request.ProductsId.HasValue || Request.AreaId.HasValue || Request.DivisionId.HasValue)
                {
                    if (Request.ProductsId != null)
                    {
                        var od = SalesProductsRow.Fields.As("od");

                        query.Where(new Criteria("(" + str + str1 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.SalesId == fld.Id &
                                od.ProductsId == Request.ProductsId.Value)
                            .ToString()).ToString());
                    }

                    if (Request.AreaId != null)
                    {
                        var od = SalesRow.Fields.As("od");

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
                        var od = SalesProductsRow.Fields.As("od");

                        query.Where(new Criteria("(" + str + str1 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.SalesId == fld.Id &
                                od.ProductsDivisionId == Request.DivisionId.Value)
                            .ToString()).ToString());
                    }
                }
                else
                {
                    query.Where(new Criteria("(" + str + str1 + ")"));
                }

                //query.Where((fld.OwnerId == user.UserId) | (fld.AssignedId == user.UserId) | (fld.OwnerId == item.UserId.Value) |(fld.AssignedId == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
            }
        }
    }
}