using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201001131200)]
    public class DefaultDB_20201001_131200_SuffixPreffix : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails").AddColumn("QuotationSuffix").AsString(10).WithDefaultValue("").Nullable()
                .AddColumn("QuotationPrefix").AsString(10).WithDefaultValue("").Nullable()
                .AddColumn("InvoiceSuffix").AsString(10).WithDefaultValue("").Nullable()
                .AddColumn("InvoicePrefix").AsString(10).WithDefaultValue("").Nullable()
                .AddColumn("ChallanSuffix").AsString(10).WithDefaultValue("").Nullable()
                .AddColumn("ChallanPrefix").AsString(10).WithDefaultValue("").Nullable();
        }
    }
}