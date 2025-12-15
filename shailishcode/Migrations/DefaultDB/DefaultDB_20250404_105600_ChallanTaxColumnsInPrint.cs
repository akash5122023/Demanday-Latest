using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250404105600)]
    public class DefaultDB_20250404_105600_ChallanTaxColumnsInPrint : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("ChallanTaxColumnIncluded").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}