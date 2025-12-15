using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250425171900)]
    public class DefaultDB_20250425_171900_CompanyDetails : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Column("QuotationPrefix").OnTable("CompanyDetails").AsString(15).Nullable();
        }
    }
}