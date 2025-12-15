
namespace AdvanceCRM.Administration
{
    using AdvanceCRM.Administration;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Web;
    using Serenity.Extensions.DependencyInjection;

    [LookupScript("Administration.Language", Permission = "?")]
    public sealed class LanguageLookup : RowLookupScript<LanguageRow>
    {
        public LanguageLookup(ISqlConnections connections)
            : base(connections)
        {
            IdField = LanguageRow.Fields.LanguageId.PropertyName;
            Permission = "?";
        }

        public LanguageLookup()
            : this(Dependency.Resolve<ISqlConnections>())
        {
        }

        protected override void PrepareQuery(SqlQuery query)
        {
            base.PrepareQuery(query);

            query.Select(LanguageRow.Fields.LanguageId);
        }
    }
}