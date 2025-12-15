using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250430121900)]
    public class DefaultDB_20250430_121900_CompanyDetailsWinPercentage : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("CompanyDetails")
                  .AddColumn("WinPercentageInEnquiry").AsBoolean().Nullable()
                  .AddColumn("ExpectedClosingDateInEnquiry").AsBoolean().Nullable()
                  .AddColumn("WinPercentageInQuotation").AsBoolean().Nullable()
                  .AddColumn("ExpectedClosingDateInQuotation").AsBoolean().Nullable()
                ;
        }
    }
}