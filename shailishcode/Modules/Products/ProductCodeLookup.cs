namespace AdvanceCRM.Products.Scripts
{
    using AdvanceCRM.Products;
    using AdvanceCRM.Scripts;
    using Serenity.ComponentModel;
    using Serenity.Abstractions;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Abstractions;
    using Serenity.Web;

    [LookupScript("Products.ProductCodeLookup", Permission = "?", Expiration = -1)]
    public class ProductCodeLookup : MultiCompanyRowLookupScript<ProductsRow>
    {
        public ProductCodeLookup(ISqlConnections connections, IUserAccessor userAccessor)
            : base(connections, userAccessor)
        {
            IdField = ProductsRow.Fields.Code.PropertyName;
            TextField = ProductsRow.Fields.CodePlusName.PropertyName;
        }

        protected override void ApplyOrder(SqlQuery query)
        {

        }

        protected override void PrepareQuery(SqlQuery query)
        {
            base.PrepareQuery(query);

            query.Where(ProductsRow.Fields.CodePlusName.IsNotNull());

            AddCompanyFilter(query);
        }
    }
}