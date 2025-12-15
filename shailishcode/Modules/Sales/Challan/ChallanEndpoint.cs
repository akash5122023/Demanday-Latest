
namespace AdvanceCRM.Sales.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Template;
using Microsoft.AspNetCore.Mvc;
    using Serenity;
    using Serenity.Data;  
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRepository = Repositories.ChallanRepository;
    using MyRow = ChallanRow;

    [Route("Services/Sales/Challan/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize("Sales:Challan")]
    public class ChallanController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private IRequestContext Context { get; }

        public ChallanController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context, _connections).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context, _connections).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }


        [ServiceAuthorize("Challan:Can Approve")]
        public StandardResponse Approve(SendSMSRequest request)
        {
            var response = new StandardResponse();

            try
            {
                using var connection = _connections.NewFor<ChallanRow>();
                const string sql = "UPDATE Challan SET ApprovedBy=@UserId WHERE Id=@Id";
                connection.Execute(sql, new { UserId = Convert.ToInt32(Context.User.GetIdentifier()), Id = request.Id });

                response.Status = "Approved";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message;
            }

            return response;
        }



        [ServiceAuthorize("Challan:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ChallanColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Challan_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }
        [HttpPost, ServiceAuthorize("Challan:Move to Outward")]
        public StandardResponse MoveToOutward(IUnitOfWork uow, SendMailRequest request)
        {
            var response = new StandardResponse();

            var exist = new OutwardRow();
            var i = OutwardRow.Fields;
            exist = uow.Connection.TryFirst<OutwardRow>(q => q
            .SelectTableFields()
            .Select(i.Id)
            .Where(i.ChallanNo == request.Id));

            if (exist != null)
            {
                response.Id = exist.Id.Value;
                response.Status = "Already Moved!";
                return response;
            }

            var data = new ChallanData();

            var quot = ChallanRow.Fields;
            data.Outward = uow.Connection.TryById<ChallanRow>(request.Id, q => q
               .SelectTableFields()
               .Select(quot.ContactsId)
               .Select(quot.Type)
               .Select(quot.AdditionalInfo)
               .Select(quot.SourceId)
               .Select(quot.StageId)
               .Select(quot.BranchId)
               .Select(quot.OwnerId)
               .Select(quot.AssignedId)
               .Select(quot.Status)
               .Select(quot.Advacne)
               .Select(quot.PackagingCharges)
               .Select(quot.FreightCharges)
               .Select(quot.Date)
               .Select(quot.OtherAddress)
               .Select(quot.ShippingAddress)
               .Select(quot.Advacne)
               .Select(quot.QuotationNo)
               .Select(quot.QuotationDate)
               .Select(quot.DueDate)
               .Select(quot.DispatchDetails)
               .Select(quot.Total)
               .Select(quot.InvoiceMade)
                .Select(quot.ContactPersonId)
               .Select(quot.ClosingDate)
               .Select(quot.Attachments)
               .Select(quot.ChallanNo)
               );

            var quotp = ChallanProductsRow.Fields;
            data.OutwardProducts = uow.Connection.List<ChallanProductsRow>(q => q
                .SelectTableFields()
                .Select(quotp.ProductsId)
                .Select(quotp.ProductsName)
                .Select(quotp.Quantity)
                .Select(quotp.Mrp)
                .Select(quotp.Unit)
                .Select(quotp.SellingPrice)
                .Select(quotp.Price)
                .Select(quotp.Discount)
                .Select(quotp.DiscountAmount)
                .Select(quotp.Description)
                .Select(quotp.BranchId)
                .Where(quotp.ChallanId == request.Id)
                );


            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );

            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Outward.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Challan as closed or pending before moving");
                }
            }

            int contactsid;
            int insalid;

            try
            {
                using (var connection = _connections.NewFor<ChallanRow>())
                {
                    dynamic typ, brnh, con, Advacne, PackagingCharges, FreightCharges, Roundup, msg, refr, sub;
                    var po = string.Empty;
                    DateTime podate;

                    if (data.Outward.Type != null)
                        typ = (int)data.Outward.Type.Value;
                    else
                        typ = "null";

                    if (data.Outward.BranchId != null)
                        brnh = Convert.ToString(data.Outward.BranchId.Value);
                    else
                        brnh = "null";
                    if (data.Outward.Advacne != null)
                        Advacne = data.Outward.Advacne;
                    else
                        Advacne = 0;

                    if (data.Outward.PackagingCharges != null)
                        PackagingCharges = data.Outward.PackagingCharges;
                    else
                        PackagingCharges = 0;

                    if (data.Outward.FreightCharges != null)
                        FreightCharges = data.Outward.FreightCharges;
                    else
                        FreightCharges = 0;
                    if (data.Outward.ContactPersonId == null)
                    {
                        throw new Exception($"Contact Person Name can not Empty");
                    }
                    String str;
                    if (data.Outward.Id != null)
                    {
                        str = "INSERT INTO Outward(ChallanNo, ContactsId, Date, Status, Type, OtherAddress, ShippingAddress, PackagingCharges, FreightCharges, Advacne, DueDate, DispatchDetails, AdditionalInfo, SourceId, StageId, BranchId, OwnerId, AssignedId, Total, InvoiceMade, ContactPersonId, QuotationNo, ClosingDate, Attachments) " + "VALUES('" + data.Outward.Id + "','" + Convert.ToString(data.Outward.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Outward.OtherAddress + "','" + data.Outward.ShippingAddress + "','" + PackagingCharges + "','" + FreightCharges + "','" + Advacne + "','" + data.Outward.DueDate.Value.ToString("yyyy-MM-dd") + "','" + data.Outward.DispatchDetails + "','" + data.Outward.AdditionalInfo + "','" + Convert.ToString(data.Outward.SourceId) + "','" + Convert.ToString(data.Outward.StageId.Value) + "','" + brnh + "','" + Convert.ToString(data.Outward.OwnerId.Value) + "','" + Convert.ToString(data.Outward.AssignedId.Value) + "','" + data.Outward.Total + "','" + data.Outward.InvoiceMade + "','" + Convert.ToString(data.Outward.ContactPersonId.Value) + "','" + Convert.ToString(data.Outward.QuotationNo) + "','" + data.Outward.ClosingDate.Value.ToString("yyyy-MM-dd") + "','" + data.Outward.Attachments + "')";
                    }
                    else
                    {
                        str = "INSERT INTO Outward(ChallanNo, ContactsId, Date, Status, Type, OtherAddress, ShippingAddress, PackagingCharges, FreightCharges, Advacne, DueDate, DispatchDetails, AdditionalInfo, SourceId, StageId, BranchId, OwnerId, AssignedId, Total, InvoiceMade, ContactPersonId, QuotationNo, ClosingDate, Attachments) " + "VALUES('" + data.Outward.Id + "','" + Convert.ToString(data.Outward.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Outward.OtherAddress + "','" + data.Outward.ShippingAddress + "','" + PackagingCharges + "','" + FreightCharges + "','" + Advacne + "','" + data.Outward.DueDate.Value.ToString("yyyy-MM-dd") + "','" + data.Outward.DispatchDetails + "','" + data.Outward.AdditionalInfo + "','" + Convert.ToString(data.Outward.SourceId) + "','" + Convert.ToString(data.Outward.StageId.Value) + "','" + brnh + "','" + Convert.ToString(data.Outward.OwnerId.Value) + "','" + Convert.ToString(data.Outward.AssignedId.Value) + "','" + data.Outward.Total + "','" + data.Outward.InvoiceMade + "','" + Convert.ToString(data.Outward.ContactPersonId.Value) + "','" + Convert.ToString(data.Outward.QuotationNo) + "','" + data.Outward.ClosingDate.Value.ToString("yyyy-MM-dd") + "','" + data.Outward.Attachments + "')";

                    }
                    connection.Execute(str);

                    var inv = ChallanRow.Fields;
                    data.LastInv = connection.TryFirst<OutwardRow>(l => l
                    .Select(inv.Id)
                    .Select(inv.ContactsId)
                    .OrderBy(inv.Id, desc: true)
                    );
                    contactsid = data.LastInv.ContactsId.Value;
                    insalid = data.LastInv.Id.Value;

                }
                if (data.Outward.ContactsId == contactsid)
                {
                    using (var connection = _connections.NewFor<ChallanProductsRow>())
                    {
                        foreach (var item in data.OutwardProducts)
                        {
                            
                                if (item.BranchId == null)
                                {
                                    throw new Exception($"BranchId is NULL for product {item.ProductsName} (ChallanId: {request.Id})");
                                }
                            

                            string str = "INSERT INTO OutwardProducts(ProductsId,Quantity,MRP,SellingPrice,Price,Unit,Discount,OutwardId,DiscountAmount,Description,BranchId) " +
                                             "VALUES(@ProductsId, @Quantity, @Mrp, @SellingPrice, @Price, @Unit, @Discount, @OutwardId, @DiscountAmount, @Description, @BranchId)";

                                connection.Execute(str, new
                                {
                                    ProductsId = item.ProductsId.Value,
                                    Quantity = item.Quantity.Value,
                                    Mrp = item.Mrp.Value,
                                    SellingPrice = item.SellingPrice.Value,
                                    Price = item.Price.Value,
                                    Unit = item.Unit,
                                    Discount = item.Discount.Value,
                                    OutwardId = insalid,
                                    DiscountAmount = item.DiscountAmount.Value,
                                    Description = item.Description,
                                    BranchId = item.BranchId
                                });
                            }
                        }
                    }
                


                response.Id = insalid;
                response.Status = "Challan moved to Outward scucessfully";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = ex.Message.ToString();
            }

            return response;

        }
        [HttpPost, ServiceAuthorize("Challan:Move to Sales")]
        public StandardResponse MoveToSales(IUnitOfWork uow, SendMailRequest request)
        {
            var response = new StandardResponse();

            var exist = new SalesRow();
            var i = SalesRow.Fields;
            exist = uow.Connection.TryFirst<SalesRow>(q => q
            .SelectTableFields()
            .Select(i.Id)
            .Where(i.QuotationNo == request.Id));

            if (exist != null)
            {
                response.Id = exist.Id.Value;
                response.Status = "Already Moved!";
                return response;
            }

            var data = new ChallanData();

            var quot = ChallanRow.Fields;
            data.Outward = uow.Connection.TryById<ChallanRow>(request.Id, q => q
               .SelectTableFields()
               .Select(quot.ContactsId)
               .Select(quot.Type)
               .Select(quot.AdditionalInfo)
               .Select(quot.SourceId)
               .Select(quot.StageId)
               .Select(quot.BranchId)
               .Select(quot.OwnerId)
               .Select(quot.AssignedId)
               .Select(quot.Status)
               .Select(quot.Advacne)
               .Select(quot.PackagingCharges)
               .Select(quot.FreightCharges)
               .Select(quot.Date)
               .Select(quot.OtherAddress)
               .Select(quot.ShippingAddress)
               .Select(quot.Advacne)
               .Select(quot.QuotationNo)
               .Select(quot.QuotationDate)
               .Select(quot.DueDate)
               .Select(quot.DispatchDetails)
               .Select(quot.Total)
               .Select(quot.InvoiceMade)
                .Select(quot.ContactPersonId)
               .Select(quot.ClosingDate)
               .Select(quot.Attachments)
               );

            var quotp = ChallanProductsRow.Fields;
            data.OutwardProducts = uow.Connection.List<ChallanProductsRow>(q => q
                .SelectTableFields()
                .Select(quotp.ProductsId)
                .Select(quotp.ProductsName)
                .Select(quotp.Quantity)
                .Select(quotp.Mrp)
                .Select(quotp.Unit)
                .Select(quotp.SellingPrice)
                .Select(quotp.Price)
                .Select(quotp.Discount)
                .Select(quotp.DiscountAmount)
                .Select(quotp.Description)
                .Select(quotp.BranchId)
                .Where(quotp.ChallanId == request.Id)
                );


            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );

            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Outward.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Challan as closed or pending before moving");
                }
            }

            int contactsid;
            int insalid;

            try
            {
                using (var connection = _connections.NewFor<ChallanRow>())
                {
                    dynamic typ, brnh, con, Advacne, PackagingCharges, FreightCharges, Roundup, msg, refr, sub;
                    var po = string.Empty;
                    DateTime podate;

                    if (data.Outward.Type != null)
                        typ = (int)data.Outward.Type.Value;
                    else
                        typ = "null";

                    if (data.Outward.BranchId != null)
                        brnh = Convert.ToString(data.Outward.BranchId.Value);
                    else
                        brnh = "null";


                    if (data.Outward.Advacne != null)
                        Advacne = data.Outward.Advacne;
                    else
                        Advacne = 0;

                    if (data.Outward.PackagingCharges != null)
                        PackagingCharges = data.Outward.PackagingCharges;
                    else
                        PackagingCharges = 0;

                    if (data.Outward.FreightCharges != null)
                        FreightCharges = data.Outward.FreightCharges;
                    else
                        FreightCharges = 0;



                    if (data.Outward.ContactPersonId == null)
                    {
                        throw new Exception($"Contact Person Name can not Empty");
                    }

                    //GetNextNumberResponse nextNumber = new OutwardController().GetNextNumber(uow.Connection, new GetNextNumberRequest());

                    GetNextNumberResponse nextNumber1 = new SalesController().GetNextNumber(uow.Connection, new GetNextNumberRequest());

                    foreach (var item in data.OutwardProducts)
                    {
                        // Query the Products for the available quantity
                        var inventoryQty = connection.Query<double?>(
                            "SELECT Quantity FROM Inventory WHERE Id = @ProductsId AND BranchId = @BranchId",
                            new { ProductsId = item.ProductsId.Value, BranchId = item.BranchId }
                        ).FirstOrDefault();

                        // If Products is not found or quantity is 0
                        if (inventoryQty == 0 || inventoryQty == null)
                        {
                            throw new Exception($"Product {item.ProductsName} is not available in the Branch.");
                        }


                        if (item.Quantity.Value > inventoryQty)
                        {
                            throw new Exception($"Insufficient quantity: Product '{item.ProductsName}' has only {inventoryQty} units in stock, but {item.Quantity.Value} units were requested.");
                        }
                    }


                    String str;

                    if (data.Outward.Id != null)
                    {
                        str = "INSERT INTO Sales (InvoiceNo, ContactsId, Date, Status, Type, AdditionalInfo, SourceId, StageId, BranchId, OwnerId, AssignedId, Advacne, PackagingCharges, FreightCharges, QuotationNo, InvoiceN) VALUES (" + nextNumber1.Serial + ", '" + Convert.ToString(data.Outward.ContactsId.Value) + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '1', " + typ + ", '" + data.Outward.AdditionalInfo + "', '" + Convert.ToString(data.Outward.SourceId) + "', '" + Convert.ToString(data.Outward.StageId.Value) + "', '" + brnh + "', '" + Convert.ToString(data.Outward.OwnerId.Value) + "', '" + Convert.ToString(data.Outward.AssignedId.Value) + "', " + Advacne + ", " + PackagingCharges + ", " + FreightCharges + ", '" + Convert.ToString(request.Id) + "','" + nextNumber1.SerialN + "')";

                    }
                    else
                    {
                        str = "INSERT INTO Sales (InvoiceNo, ContactsId, Date, Status, Type, AdditionalInfo, SourceId, StageId, BranchId, OwnerId, AssignedId, Advacne, PackagingCharges, FreightCharges, QuotationNo, InvoiceN) VALUES (" + nextNumber1.Serial + ", '" + Convert.ToString(data.Outward.ContactsId.Value) + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '1', " + typ + ", '" + data.Outward.AdditionalInfo + "', '" + Convert.ToString(data.Outward.SourceId) + "', '" + Convert.ToString(data.Outward.StageId.Value) + "', '" + brnh + "', '" + Convert.ToString(data.Outward.OwnerId.Value) + "', '" + Convert.ToString(data.Outward.AssignedId.Value) + "', " + Advacne + ", " + PackagingCharges + ", " + FreightCharges + ", '" + Convert.ToString(request.Id) + "','" + nextNumber1.SerialN + "')";

                    }
                    connection.Execute(str);

                    var inv = ChallanRow.Fields;
                    data.LastInvS = connection.TryFirst<SalesRow>(l => l
                    .Select(inv.Id)
                    .Select(inv.ContactsId)
                    .OrderBy(inv.Id, desc: true)
                    );
                    contactsid = data.LastInvS.ContactsId.Value;
                    insalid = data.LastInvS.Id.Value;

                }

                if (data.Outward.ContactsId == contactsid)
                {
                    using (var connection = _connections.NewFor<ChallanProductsRow>())
                    {
                        foreach (var item in data.OutwardProducts)
                        {

                            string str = "INSERT INTO SalesProducts(ProductsId,Quantity,MRP,SellingPrice,Price,Unit,Discount,SalesId,DiscountAmount,Description) " +
                                         "VALUES(@ProductsId, @Quantity, @Mrp, @SellingPrice, @Price, @Unit, @Discount, @SalesId, @DiscountAmount, @Description)";

                            connection.Execute(str, new
                            {
                                ProductsId = item.ProductsId.Value,
                                Quantity = item.Quantity.Value,
                                Mrp = item.Mrp.Value,
                                SellingPrice = item.SellingPrice.Value,
                                Price = item.Price.Value,
                                Unit = item.Unit,
                                Discount = item.Discount.Value,
                                SalesId = insalid,
                                DiscountAmount = item.DiscountAmount.Value,
                                Description = item.Description,
                                BranchId = item.BranchId.Value
                            });

                            // }
                        }
                    }
                }


                response.Id = insalid;
                response.Status = "Challan moved to Sales scucessfully";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = ex.Message.ToString();
            }

            return response;

        }
        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";
            try
            {
                var sl = MyRow.Fields;
                var data = new MyRow();
                data = connection.First<MyRow>(q => q
                    .SelectTableFields()
                    .Select(sl.Id)
                    .Select(sl.ChallanNo)
                    .OrderBy(sl.Id, desc: true)
                    );

                if (data != null)
                    response.Serial = (data.ChallanNo + 1).ToString();
            }
            catch (Exception)
            {

                return null;
            }

            return response;
        }

        public class ChallanData
        {
            public ContactsRow Contact { get; set; }
            public UserRow User { get; set; }
            public MyRow Outward { get; set; }
            public OutwardRow LastInv { get; set; }
            public SalesRow LastInvS { get; set; }

            public ChallanRow LastIn { get; set; }
            public List<ChallanProductsRow> OutwardProducts { get; set; }

            public CompanyDetailsRow Company { get; set; }
            public ChallanTemplateRow Template { get; set; }

        }
    }
}
