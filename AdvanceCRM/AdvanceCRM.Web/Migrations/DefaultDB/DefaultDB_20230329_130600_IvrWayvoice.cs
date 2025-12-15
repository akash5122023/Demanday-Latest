
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
    [Migration(20230329130600)]
    public class DefaultDB_20230329_130600_IvrWayvoice : Migration
    {

        public override void Up()
        {

            Alter.Table("InteraktUser")
                .AddColumn("IsMoved").AsBoolean().WithDefaultValue(false);

            Alter.Table("IVRConfiguration")
                .AddColumn("Username").AsString(100).Nullable()
                .AddColumn("Password").AsString(100).Nullable()
                .AddColumn("PostURL").AsString(int.MaxValue).Nullable();

            Alter.Table("JustDial")
                 .AddColumn("PostURL").AsString(int.MaxValue).Nullable();

            
        }

        public override void Down()
        {
        }
    }
}