namespace AdvanceCRM.Products.Scripts
{
    using AdvanceCRM.Scripts;
    using AdvanceCRM.MultiTenancy;

    using Serenity.ComponentModel;
    using Serenity.Abstractions;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Abstractions;
    using Serenity.Web;

    [LookupScript("Products.RawMaterialLookup", Permission = "?")]
    public class RawMaterialLookup : MultiCompanyRowLookupScript<ProductsRow>
    {
        public RawMaterialLookup(ISqlConnections connections, IUserAccessor userAccessor, ITenantAccessor tenantAccessor)
            : base(connections, userAccessor, tenantAccessor)
        {
            IdField = ProductsRow.Fields.Id.PropertyName;
            TextField = ProductsRow.Fields.Name.PropertyName;
        }

        protected override void ApplyOrder(SqlQuery query)
        {

        }

        protected override void PrepareQuery(SqlQuery query)
        {
            base.PrepareQuery(query);

            query.Where(ProductsRow.Fields.RawMaterial == 1);

            AddCompanyFilter(query);
        }
    }
}