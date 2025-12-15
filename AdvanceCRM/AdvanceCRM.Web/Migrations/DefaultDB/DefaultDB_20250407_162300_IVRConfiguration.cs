
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
    [Migration(20250407162300)]
    public class DefaultDB_20250407_162300_IVRConfiguration : Migration
    {

        public override void Up()
        {

            Alter.Table("IVRConfiguration")
              .AddColumn("roundRobin").AsBoolean().WithDefaultValue(false)
              .AddColumn("autoRefresh").AsBoolean().WithDefaultValue(false)
              .AddColumn("autoRefreshTime").AsInt32().Nullable();

        }

        public override void Down()
        {
        }
    }
}