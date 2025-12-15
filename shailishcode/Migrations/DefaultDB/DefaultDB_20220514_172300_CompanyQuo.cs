
using AdvanceCRM.Administration;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220514172300)]
    public class DefaultDB_20220514_172300_CompanyQuo : Migration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails")
             .AddColumn("RemoveGTColumn").AsBoolean().WithDefaultValue(false);

            Alter.Table("WoocommerceDetails")
                .AddColumn("WooID").AsString().Nullable();
        }

        public override void Down()
        {
        }
    }
}