
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
    [Migration(20210228130600)]
    public class DefaultDB_20210228_130600_Visit1 : Migration
    {

        public override void Up()
        {
            if (Schema.Table("Visit").Exists())
                return;

            Create.Table("Visit").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("CompanyName").AsString(255)
                .WithColumn("Name").AsString(255)
                .WithColumn("MobileNo").AsString(20).Nullable()
                .WithColumn("Address").AsString(255).Nullable()
                .WithColumn("Email").AsString(25).Nullable()
                .WithColumn("Location").AsString(250).Nullable()
                .WithColumn("DateNTime").AsDateTime()
                .WithColumn("Requirements").AsString(150).Nullable()
                .WithColumn("Purpose").AsString(150).Nullable()
                .WithColumn("Attachments").AsString(150).Nullable()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);

        }

        public override void Down()
        {
        }
    }
}