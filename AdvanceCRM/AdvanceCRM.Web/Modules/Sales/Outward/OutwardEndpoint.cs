using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
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
using System.Globalization;
using System.Linq;
using MyRow = AdvanceCRM.Sales.OutwardRow;

namespace AdvanceCRM.Sales.Endpoints
{
    [Route("Services/Sales/Outward/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]

    public class OutwardController : ServiceEndpoint
    {
        private readonly ISqlConnections sqlConnections;
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IOutwardSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IOutwardSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IOutwardDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IOutwardRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IOutwardListHandler handler)
        {
            return handler.List(connection, request);
        }
        [ServiceAuthorize("Outward:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IOutwardListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.OutwardColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "OutwardList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
        public OutwardController(ISqlConnections sqlConnections)
        {
            this.sqlConnections = sqlConnections;
        }
        [HttpPost, ServiceAuthorize("Outward:Move to Inward")]
        public StandardResponse MoveToInward(IUnitOfWork uow, SendMailRequest request)
        {
           //this.sqlConnections = sqlConnections;
            var response = new StandardResponse();

            var exist = new InwardRow();
            var i = InwardRow.Fields;
            exist = uow.Connection.TryFirst<InwardRow>(q => q
            .SelectTableFields()
            .Select(i.Id)
            .Where(i.OutwardId == request.Id));

            if (exist != null)
            {
                response.Id = exist.Id.Value;
                response.Status = "Already Moved!";
                return response;
            }
            var data = new OutwardData();

            var quot = OutwardRow.Fields;
            data.Inward = uow.Connection.TryById<OutwardRow>(request.Id, q => q
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
            var quotp = OutwardProductsRow.Fields;
            data.InwardProducts = uow.Connection.List<OutwardProductsRow>(q => q
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
                .Where(quotp.OutwardId == request.Id)
                );
            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );
            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Inward.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Challan as closed or pending before moving");
                }
            }
            int contactsid;
            int insalid;
            try
            {
                using (var connection = sqlConnections.NewFor<OutwardRow>())
                {
                    dynamic typ, brnh, con, Advacne, PackagingCharges, FreightCharges, Roundup, msg, refr, sub;
                    var po = string.Empty;
                    DateTime podate;

                    if (data.Inward.Type != null)
                        typ = (int)data.Inward.Type.Value;
                    else
                        typ = "null";

                    if (data.Inward.BranchId != null)
                        brnh = Convert.ToString(data.Inward.BranchId.Value);
                    else
                        brnh = "null";

                    if (data.Inward.Advacne != null)
                        Advacne = data.Inward.Advacne;
                    else
                        Advacne = 0;

                    if (data.Inward.PackagingCharges != null)
                        PackagingCharges = data.Inward.PackagingCharges;
                    else
                        PackagingCharges = 0;

                    if (data.Inward.FreightCharges != null)
                        FreightCharges = data.Inward.FreightCharges;
                    else
                        FreightCharges = 0;
                    if (data.Inward.ContactPersonId == null)
                    {
                        throw new Exception($"Contact Person Name can not Empty");
                    }

                    GetNextNumberResponse nextNumber = this.GetNextNumber(uow.Connection, new GetNextNumberRequest());

                 /*   foreach (var item in data.InwardProducts)
                    {
                        var ProductQty = connection.Query<double?>(
                            "SELECT Quantity FROM Products WHERE Id = @ProductsId AND BranchId = @BranchId",
                            new { ProductsId = item.ProductsId.Value, BranchId = item.BranchId }
                        ).FirstOrDefault();
                        if (ProductQty == 0 || ProductQty == null)
                        {
                            throw new Exception($"Product {item.ProductsName} is not available in the Branch.");
                        }
                        if (item.Quantity.Value < ProductQty)
                        {
                            throw new Exception($"Insufficient Stock: Product '{item.ProductsName}' has only {ProductQty} units in stock, but {item.Quantity.Value} units were requested.");
                        }
                    }

*/
                    String str;
                    if (data.Inward.Id != null)
                    { 
                        str = "INSERT INTO Inward(ChallanNo, ContactsId, Date, Status, Type, OtherAddress, ShippingAddress, PackagingCharges, FreightCharges, Advacne, DueDate, DispatchDetails, AdditionalInfo, SourceId, StageId, BranchId, OwnerId, AssignedId, Total, InvoiceMade, ContactPersonId, QuotationNo, ClosingDate, Attachments,OutwardId) " + "VALUES('" + data.Inward.ChallanNo + "','" + Convert.ToString(data.Inward.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Inward.OtherAddress + "','" + data.Inward.ShippingAddress + "','" + PackagingCharges + "','" + FreightCharges + "','" + Advacne + "','" + data.Inward.DueDate.Value.ToString("yyyy-MM-dd") + "','" + data.Inward.DispatchDetails + "','" + data.Inward.AdditionalInfo + "','" + Convert.ToString(data.Inward.Source) + "','" + Convert.ToString(data.Inward.StageId.Value) + "','" + brnh + "','" + Convert.ToString(data.Inward.OwnerId.Value) + "','" + Convert.ToString(data.Inward.AssignedId.Value) + "','" + data.Inward.Total + "','" + data.Inward.InvoiceMade + "','" + Convert.ToString(data.Inward.ContactPersonId.Value) + "','" + Convert.ToString(data.Inward.QuotationNo) + "','" + data.Inward.ClosingDate.Value.ToString("yyyy-MM-dd") + "','" + data.Inward.Attachments + "','" + data.Inward.Id + "')";
                    }
                    else
                    {
                        str = "INSERT INTO Inward(ChallanNo, ContactsId, Date, Status, Type, OtherAddress, ShippingAddress, PackagingCharges, FreightCharges, Advacne, DueDate, DispatchDetails, AdditionalInfo, SourceId, StageId, BranchId, OwnerId, AssignedId, Total, InvoiceMade, ContactPersonId, QuotationNo, ClosingDate, Attachments,OutwardId) " + "VALUES('" + data.Inward.ChallanNo + "','" + Convert.ToString(data.Inward.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",'" + data.Inward.OtherAddress + "','" + data.Inward.ShippingAddress + "','" + PackagingCharges + "','" + FreightCharges + "','" + Advacne + "','" + data.Inward.DueDate.Value.ToString("yyyy-MM-dd") + "','" + data.Inward.DispatchDetails + "','" + data.Inward.AdditionalInfo + "','" + Convert.ToString(data.Inward.Source) + "','" + Convert.ToString(data.Inward.StageId.Value) + "','" + brnh + "','" + Convert.ToString(data.Inward.OwnerId.Value) + "','" + Convert.ToString(data.Inward.AssignedId.Value) + "','" + data.Inward.Total + "','" + data.Inward.InvoiceMade + "','" + Convert.ToString(data.Inward.ContactPersonId.Value) + "','" + Convert.ToString(data.Inward.QuotationNo) + "','" + data.Inward.ClosingDate.Value.ToString("yyyy-MM-dd") + "','" + data.Inward.Attachments + "','" + data.Inward.Id + "')";

                    }
                    connection.Execute(str);

                    var inv = OutwardRow.Fields;
                    data.LastInv = connection.TryFirst<InwardRow>(l => l
                    .Select(inv.Id)
                    .Select(inv.ContactsId)
                    .OrderBy(inv.Id, desc: true)
                    );
                    contactsid = data.LastInv.ContactsId.Value;
                    insalid = data.LastInv.Id.Value;

                }

                if (data.Inward.ContactsId == contactsid)
                {
                    using (var connection = sqlConnections.NewFor<OutwardProductsRow>())
                    {
                        foreach (var item in data.InwardProducts)
                        {
                          /*  // First, check the Quantity for the product
                            var ProductQty = connection.Query<double?>(
                                "SELECT Quantity FROM Products WHERE Id = @ProductsId AND BranchId = @BranchId",
                                new { ProductsId = item.ProductsId.Value, BranchId = item.BranchId }
                            ).FirstOrDefault();

                            if (ProductQty == 0 || ProductQty == null)
                            {
                                throw new Exception($"Product {item.ProductsName} is not available in the Branch.");
                            }
                            if (item.Quantity.Value < ProductQty)
                            {
                                throw new Exception($"Cannot move to Outward: Product '{item.ProductsName}' has only {ProductQty} units in the inventory. You requested {item.Quantity.Value}.");
                            }
                            else
                            {*/
                                string str = "INSERT INTO InwardProducts(ProductsId,Quantity,MRP,SellingPrice,Price,Unit,Discount,InwardId,DiscountAmount,Description,BranchId) " +
                                             "VALUES(@ProductsId, @Quantity, @Mrp, @SellingPrice, @Price, @Unit, @Discount, @InwardId, @DiscountAmount, @Description, @BranchId)";

                                connection.Execute(str, new
                                {
                                    ProductsId = item.ProductsId.Value,
                                    Quantity = item.Quantity.Value,
                                    Mrp = item.Mrp.Value,
                                    SellingPrice = item.SellingPrice.Value,
                                    Price = item.Price.Value,
                                    Unit = item.Unit,
                                    Discount = item.Discount.Value,
                                    InwardId = insalid,
                                    DiscountAmount = item.DiscountAmount.Value,
                                    Description = item.Description,
                                    BranchId = item.BranchId
                                });
                            /*
                                string updateProducts = "UPDATE Products SET Quantity = Quantity + @Quantity WHERE Id = @ProductsId AND BranchId = @BranchId";
                                connection.Execute(updateProducts, new
                                {
                                    Quantity = item.Quantity.Value,
                                    ProductsId = item.ProductsId.Value,
                                    BranchId = item.BranchId
                                });
                            }*/
                        }
                    }
                }
                response.Id = insalid;
                response.Status = "Outward moved to Inward scucessfully";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = ex.Message.ToString();
            }
            return response;
        }
        public class OutwardData
        {
            public ContactsRow Contact { get; set; }
            public UserRow User { get; set; }
            public MyRow Inward { get; set; }
            public InwardRow LastInv { get; set; }
            public OutwardRow LastIn { get; set; }
            public List<OutwardProductsRow> InwardProducts { get; set; }
            public CompanyDetailsRow Company { get; set; }
            public ChallanTemplateRow Template { get; set; }
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
    }
}