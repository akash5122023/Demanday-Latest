using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201123162700)]
    public class DefaultDB_20201123_162700_DiscountedPriceInPrint : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("QuotationDiscountedPriceIncluded").AsBoolean().NotNullable().WithDefaultValue(false)
                .AddColumn("InvoiceDiscountedPriceIncluded").AsBoolean().NotNullable().WithDefaultValue(false)
                .AddColumn("RoundupInQuotation").AsBoolean().NotNullable().WithDefaultValue(false);

            Alter.Table("Quotation")
                .AddColumn("Roundup").AsDouble().Nullable().WithDefaultValue(0);
        }
    }
}