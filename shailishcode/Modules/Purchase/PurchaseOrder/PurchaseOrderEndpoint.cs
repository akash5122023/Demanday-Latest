
namespace AdvanceCRM.Purchase.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Template;
using Microsoft.AspNetCore.Mvc;
    using Serenity.Data;  
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    
    using MyRepository = Repositories.PurchaseOrderRepository;
    using MyRow = PurchaseOrderRow;

    [Route("Services/Purchase/PurchaseOrder/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class PurchaseOrderController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }


        [ServiceAuthorize("PurchaseOrder:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.PurchaseOrderColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "PO_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";
            try
            {
                var sl = MyRow.Fields;
                var data = new MyRow();


                data = connection.TryFirst<MyRow>(q => q
                    .SelectTableFields()
                    .Select(sl.Id)
                    .Select(sl.PurchaseOrderNo)
                    .OrderBy(sl.Id, desc: true)
                    );

                if (data != null)
                    response.Serial = (data.PurchaseOrderNo + 1).ToString();
            }
            catch (Exception)
            {

                return null;
            }

            return response;
        }


        // MoveToGRN
        [HttpPost]
        [ServiceAuthorize("Proforma:Move to GRN")]
        public StandardResponse MoveToGRN(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            var data = new PurchaseOrderData();

            var inv = PurchaseOrderRow.Fields;
            var purchaseOrderData = uow.Connection.TryById<PurchaseOrderRow>(request.Id, q => q
                .SelectTableFields()  // Changed to SelectTableFields to ensure Status is loaded
                .Select(inv.PurchaseOrderNo)
                .Select(inv.ContactsId)
                .Select(inv.ContactsPhone)
                .Select(inv.Date)
                .Select(inv.OwnerId)
                .Select(inv.AssignedId)
                .Select(inv.Status)
            );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );

            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (purchaseOrderData.Status == (Masters.StatusMaster)1) // Check the actual loaded status
                {
                    throw new Exception("Please set the status of this Purchase Order as closed or pending before moving");
                }
            }




            var invNumber = purchaseOrderData.PurchaseOrderNo?.ToString();

            var indentFields = GrnTwoRow.Fields;
            var existingEntry = uow.Connection.TryFirst<GrnTwoRow>(q => q
                .Select(indentFields.Id)
                .Where(indentFields.Po == invNumber)
            );

            if (existingEntry != null)
            {
                response.Id = existingEntry.Id.Value;
                response.Status = "Already Moved!";
                return response;
            }

            // Insert Indent
            uow.Connection.Execute(@"
        INSERT INTO GrnTwo 
            (Po, ContactsId, PoDate, GrnDate, Status, GrnType, OwnerId, AssignedId) 
        VALUES 
            (@Po, @ContactsId, @PoDate, @Date, @Status, 1, @OwnerId, @AssignedId)",
                new
                {
                    Po = purchaseOrderData.PurchaseOrderNo,
                    ContactsId = purchaseOrderData.ContactsId,
                    PoDate = purchaseOrderData.Date,
                    Date = DateTime.Now,
                    Status = 1,
                    GRNType = 1,
                    OwnerId = purchaseOrderData.OwnerId,
                    AssignedId = purchaseOrderData.AssignedId
                });

            // Fetch inserted indent ID
            var ind = GrnTwoRow.Fields;
            var lastGrn = uow.Connection.TryFirst<GrnTwoRow>(l => l
                .Select(ind.Id)
                .OrderBy(ind.Id, desc: true)
            );

            response.Id = lastGrn.Id.Value;
            response.Status = "Purchase Order moved to GRN successfully";

            // Fetch Invoice Products
            var invpr = PurchaseOrderProductsRow.Fields;
            var invoiceprDataList = uow.Connection.List<PurchaseOrderProductsRow>(q => q
                .Select(invpr.ProductsId)
                .Select(invpr.Quantity)
                .Select(invpr.Price)
                .Where(invpr.PurchaseOrderId == request.Id)
            );

            // Insert each product
            foreach (var product in invoiceprDataList)
            {
                uow.Connection.Execute(@"
            INSERT INTO GrnProductsTwo 
                (GrnId, ProductsId, OrderQuantity, Price, ExtraQuantity, RejectedQuantity, ReceivedQuantity) 
            VALUES 
                (@GrnId, @ProductsId, @Quantity, @Price, @ExtraQuantity, @RejectedQuantity, @ReceivedQuantity)",
                    new
                    {
                        GrnId = response.Id,
                        ProductsId = product.ProductsId,
                        Quantity = product.Quantity,
                        Price = product.Price,
                        ExtraQuantity = 0,
                        RejectedQuantity = 0,
                        ReceivedQuantity = 0

                    });
            }

            return response;
        }

        public class PurchaseOrderData
        {
            public ContactsRow Contact { get; set; }
            public UserRow User { get; set; }

            public PurchaseOrderRow LastIn { get; set; }
            public List<PurchaseOrderProductsRow> SalesProducts { get; set; }

            public CompanyDetailsRow Company { get; set; }
        }


    }
}
