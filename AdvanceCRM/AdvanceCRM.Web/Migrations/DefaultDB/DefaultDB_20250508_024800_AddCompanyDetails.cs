using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250508024800)]
    public class DefaultDB_20250508_024800_AddCompanyDetails : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("CompanyDetails")
                  .AddColumn("HSN").AsBoolean().Nullable()
                  .AddColumn("Code").AsBoolean().Nullable()
                  .AddColumn("Unit").AsBoolean().Nullable()
                  .AddColumn("OpeningStock").AsBoolean().Nullable()
                  .AddColumn("RawMaterial").AsBoolean().Nullable()
                  .AddColumn("Group").AsBoolean().Nullable()
                  .AddColumn("ToInvoice").AsBoolean().Nullable()
                  .AddColumn("ToPerforma").AsBoolean().Nullable()
                ;
        }
    }
}