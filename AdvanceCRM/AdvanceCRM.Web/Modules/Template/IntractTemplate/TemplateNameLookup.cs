using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Extensions.DependencyInjection;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.Template.IntractTemplate
{
    [LookupScript("Template.TemplateNameLookup")]
    public class TemplateNameLookup : RowLookupScript<AdvanceCRM.Template.IntractTemplateRow>
    {
        public TemplateNameLookup(ISqlConnections connections)
            : base(connections)
        {
            IdField = TextField = AdvanceCRM.Template.IntractTemplateRow.Fields.Name.PropertyName;
        }

        public TemplateNameLookup()
            : this(Dependency.Resolve<ISqlConnections>())
        {
        }

        protected override void PrepareQuery(SqlQuery query)
        {
            var fld = AdvanceCRM.Template.IntractTemplateRow.Fields;
            query.Distinct(true)
                 .Select(fld.Name)
                 .Where(
                    new Criteria(fld.Name) != "" &
                    new Criteria(fld.Name).IsNotNull());
        }
    }
}