
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
    [Migration(20250110173900)]
    public class DefaultDB_20250110_173900_IVRConfiguration : Migration
    {

        public override void Up()
        {

            Alter.Table("IVRConfiguration")
              .AddColumn("token_id").AsString().Nullable()
              .AddColumn("userType").AsString().Nullable()
              .AddColumn("number").AsString().Nullable();

        }

        public override void Down()
        {
        }
    }
}