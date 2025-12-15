
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
    [Migration(20230402130600)]
    public class DefaultDB_20230402_130600_Template : Migration
    {
        public override void Up()
        {
            Alter.Table("Sales")
              .AlterColumn("BillingAddress").AsBoolean().Nullable().WithDefaultValue(true);

            Alter.Table("Invoice")
              .AlterColumn("BillingAddress").AsBoolean().Nullable().WithDefaultValue(true);
        }

        public override void Down()
        {
        }
    }
}