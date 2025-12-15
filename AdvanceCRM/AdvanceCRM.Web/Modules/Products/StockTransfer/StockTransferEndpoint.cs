
namespace AdvanceCRM.Products.Endpoints
{
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Products;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Sales;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    
    using MyRepository = Repositories.StockTransferRepository;
    using MyRow = StockTransferRow;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Caching.Memory;
    using Serenity.Abstractions;

    [Route("Services/Products/StockTransfer/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class StockTransferController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;


        private readonly IUserAccessor userAccessor;
        private readonly IPermissionService permissionService;
        private readonly IRequestContext requestContext;
        private readonly IMemoryCache memoryCache;
        private readonly ITypeSource typeSource;
        private readonly IUserRetrieveService userRetriever;

        public StockTransferController(
            IUserAccessor userAccessor,
            ISqlConnections connections,
            IPermissionService permissionService,
            IRequestContext requestContext,
            IMemoryCache memoryCache,
            ITypeSource typeSource,
            IUserRetrieveService userRetriever)
        {
            this.userAccessor = userAccessor;
            this.permissionService = permissionService;
            this.requestContext = requestContext;
            this.memoryCache = memoryCache;
            this.typeSource = typeSource;
            this.userRetriever = userRetriever;
            _connections = connections;
        }
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

        [AllowAnonymous]
        [HttpPost]
        public StandardResponse CheckReorder(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();

            var data = new StockData();

            var pp = PurchaseProductsRow.Fields;
            var sp = SalesProductsRow.Fields;
            var prp = PurchaseReturnProductsRow.Fields;
            var srp = SalesReturnProductsRow.Fields;
            var cp = ChallanProductsRow.Fields;
            var osp = ProductsRow.Fields;


            using (var connection = _connections.NewFor<PurchaseProductsRow>())
            {
                data.PurchaseProducts = connection.List<PurchaseProductsRow>(q => q
                 .SelectTableFields()
                 .Select(pp.Quantity)
                 .Where(pp.ProductsId == request.Id)
                 );

                data.SalesProducts = connection.List<SalesProductsRow>(q => q
                 .SelectTableFields()
                 .Select(sp.Quantity)
                 .Where(sp.ProductsId == request.Id)
                 );

                data.PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(q => q
                 .SelectTableFields()
                 .Select(prp.Quantity)
                 .Where(prp.ProductsId == request.Id)
                 );

                data.SalesReturnProducts = connection.List<SalesReturnProductsRow>(q => q
                 .SelectTableFields()
                 .Select(srp.Quantity)
                 .Where(srp.ProductsId == request.Id)
                 );

                data.ChallanProducts = connection.List<ChallanProductsRow>(q => q
                 .SelectTableFields()
                 .Select(cp.Quantity)
                 .Where(cp.ProductsId == request.Id)
                 .Where(cp.ChallanInvoiceMade != 1)
                 );
                data.Products = connection.List<ProductsRow>(q => q
                .SelectTableFields()
                 .Select(osp.OpeningStock)
                 .Where(osp.Id == request.Id)                 
                );
            }

            double pptot = (double)data.PurchaseProducts.Sum(x => x.Quantity);
            double sptot = (double)data.SalesProducts.Sum(x => x.Quantity);
            double prptot = (double)data.PurchaseReturnProducts.Sum(x => x.Quantity);
            double srptot = (double)data.SalesReturnProducts.Sum(x => x.Quantity);
            double cptot = (double)data.ChallanProducts.Sum(x => x.Quantity);

            double ospt = (double)data.Products.Sum(x => x.OpeningStock);

            double Total = (pptot + srptot + ospt) - (sptot + prptot + cptot);
            response.Status = Total.ToString();

            return response;
        }

        [ServiceAuthorize("StockTransfer:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ProductsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "StockTransfer_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }
    }

    public class StockData
    {
        public ContactsRow Contact { get; set; }

        public List<PurchaseProductsRow> PurchaseProducts { get; set; }
        public List<SalesProductsRow> SalesProducts { get; set; }

        public List<PurchaseReturnProductsRow> PurchaseReturnProducts { get; set; }
        public List<SalesReturnProductsRow> SalesReturnProducts { get; set; }

        public List<ChallanProductsRow> ChallanProducts { get; set; }

        public List<ProductsRow> Products { get; set; }
    }
}
