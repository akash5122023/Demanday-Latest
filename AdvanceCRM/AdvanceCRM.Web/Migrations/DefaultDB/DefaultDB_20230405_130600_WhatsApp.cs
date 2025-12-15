
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
    [Migration(20230405130600)]
    public class DefaultDB_20230405_130600_WhatsApp : Migration
    {

        public override void Up()
        {
            Create.Table("BizWAConfig")
                .WithColumn("WhatsAppNo").AsString(100).Nullable()
                .WithColumn("PhoneNoID").AsString(100).Nullable()
                .WithColumn("WBAID").AsString(100).Nullable()
                .WithColumn("Accesstoken").AsString(int.MaxValue).Nullable();
        }

        public override void Down()
        {
        }
    }
}