namespace AdvanceCRM.Settings.Repositories
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = ProductModuleRow;

    public class ProductModulesRepository : BaseRepository
    {
        public ProductModulesRepository(IRequestContext context)
            : base(context)
        {
        }

        private static MyRow.RowFields fld => MyRow.Fields;

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
            try
            {
                return new MyListHandler(Context).Process(connection, request);
            }
            catch (Exception ex) when (ProductModulePricingHelper.IsMissingPricingColumnException(ex))
            {
                var fallback = CloneWithoutPricingColumns(request);
                return new MyListHandler(Context).Process(connection, fallback);
            }
        }

        private ListRequest CloneWithoutPricingColumns(ListRequest request)
        {
            var fallback = new ListRequest
            {
                ColumnSelection = request?.ColumnSelection ?? ColumnSelection.List,
                ContainsText = request?.ContainsText,
                Criteria = request?.Criteria,
                ExcludeTotalCount = request?.ExcludeTotalCount ?? false,
                IncludeDeleted = request?.IncludeDeleted ?? false
            };

            if (request != null)
            {
                fallback.Skip = request.Skip;
                fallback.Take = request.Take;

                if (request.Sort != null)
                {
                    fallback.Sort = request.Sort
                        .Select(x => new SortBy
                        {
                            Field = x.Field,
                            Descending = x.Descending
                        })
                        .ToArray();
                }

                if (request.IncludeColumns != null)
                    fallback.IncludeColumns = new HashSet<string>(request.IncludeColumns, StringComparer.OrdinalIgnoreCase);

                if (request.EqualityFilter != null)
                    fallback.EqualityFilter = new Dictionary<string, object>(request.EqualityFilter, StringComparer.OrdinalIgnoreCase);
            }

            var excludes = request?.ExcludeColumns != null
                ? new HashSet<string>(request.ExcludeColumns, StringComparer.OrdinalIgnoreCase)
                : new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            excludes.Add(fld.Price.PropertyName ?? fld.Price.Name);
            excludes.Add(fld.Currency.PropertyName ?? fld.Currency.Name);

            fallback.ExcludeColumns = excludes;

            return fallback;
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            public MySaveHandler(IRequestContext context)
                : base(context)
            {
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                var now = DateTime.UtcNow;
                var userName = Context.User?.Identity?.Name;

                if (string.IsNullOrWhiteSpace(userName))
                    userName = Context.User?.GetIdentifier() ?? "system";

                if (IsCreate)
                {
                    Row.CreatedOn = now;
                    Row.CreatedBy = userName;
                }

                Row.ModifiedOn = now;
                Row.ModifiedBy = userName;
            }
        }

        private class MyDeleteHandler : DeleteRequestHandler<MyRow>
        {
            public MyDeleteHandler(IRequestContext context)
                : base(context)
            {
            }
        }

        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow>
        {
            public MyRetrieveHandler(IRequestContext context)
                : base(context)
            {
            }
        }

        private class MyListHandler : ListRequestHandler<MyRow>
        {
            public MyListHandler(IRequestContext context)
                : base(context)
            {
            }
        }
    }
}
