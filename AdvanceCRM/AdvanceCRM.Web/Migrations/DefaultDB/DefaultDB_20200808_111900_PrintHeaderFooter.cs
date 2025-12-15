using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200808111900)]
    public class DefaultDB_20200808_111900_PrintHeaderFooter : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails").AddColumn("AdditionalImages").AsString(int.MaxValue).Nullable()
                .AddColumn("QuotationHeaderContent").AsString(int.MaxValue).Nullable()
                .AddColumn("QuotationFooterContent").AsString(int.MaxValue).Nullable();
        }
    }
}