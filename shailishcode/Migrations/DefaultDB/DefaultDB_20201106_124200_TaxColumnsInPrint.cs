using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201106124200)]
    public class DefaultDB_20201106_124200_TaxColumnsInPrint : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("QuotationTaxColumnIncluded").AsBoolean().NotNullable().WithDefaultValue(true)
                .AddColumn("InvoiceTaxColumnIncluded").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}