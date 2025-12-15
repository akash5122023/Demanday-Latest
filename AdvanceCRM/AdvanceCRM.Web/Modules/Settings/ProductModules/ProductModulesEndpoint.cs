namespace AdvanceCRM.Settings.Endpoints
{
    using Microsoft.AspNetCore.Mvc;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRepository = Repositories.ProductModulesRepository;
    using MyRow = ProductModuleRow;

    [Route("Services/Settings/ProductModules/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class ProductModulesController : ServiceEndpoint
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

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse UpdatePrices(IUnitOfWork uow, ModulePriceUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Modules == null || request.Modules.Count == 0)
                return new SaveResponse();

            var now = DateTime.UtcNow;
            var userName = Context?.User?.Identity?.Name;

            if (string.IsNullOrWhiteSpace(userName))
                userName = Context?.User?.GetIdentifier() ?? "system";

            var updates = request.Modules
                .Where(x => x?.Id != null)
                .GroupBy(x => x!.Id!.Value)
                .Select(g => g.Last())
                .ToList();

            if (updates.Count == 0)
                return new SaveResponse();

            foreach (var module in updates)
            {
                var update = new SqlUpdate(MyRow.Fields.TableName)
                    .Set(MyRow.Fields.Price, module!.Price)
                    .Set(MyRow.Fields.Currency, module.Currency)
                    .Set(MyRow.Fields.ModifiedOn, now)
                    .Set(MyRow.Fields.ModifiedBy, userName)
                    .Where(MyRow.Fields.Id == module.Id!.Value);

                update.Execute(uow.Connection);
            }

            return new SaveResponse();
        }
    }

    public class ModulePriceUpdateRequest
    {
        public List<ModulePriceUpdateItem>? Modules { get; set; }
    }

    public class ModulePriceUpdateItem
    {
        public int? Id { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
    }
}
