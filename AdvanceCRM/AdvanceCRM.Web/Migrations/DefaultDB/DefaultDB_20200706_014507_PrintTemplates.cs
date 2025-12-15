using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200706014507)]
    public class DefaultDB_20200706_014507_PrintTemplates : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("Quotation").AddColumn("Taxable").AsBoolean().WithDefaultValue(true).Nullable();

            Alter.Table("Invoice").AddColumn("Taxable").AsBoolean().WithDefaultValue(true).Nullable();

            Alter.Table("Sales").AddColumn("Taxable").AsBoolean().WithDefaultValue(true).Nullable();

            Alter.Table("CompanyDetails").AddColumn("InvoiceTemplate").AsInt32().WithDefaultValue(1).Nullable()
                .AddColumn("CompanyDetailsInQuotation").AsBoolean().WithDefaultValue(true).Nullable();
        }
    }
}