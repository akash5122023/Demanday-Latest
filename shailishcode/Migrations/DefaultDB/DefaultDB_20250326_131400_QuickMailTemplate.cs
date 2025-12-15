
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
    [Migration(20250326131400)]
    public class DefaultDB_20250326_131400_QuickMailTemplate : Migration
    {
        public override void Up()
        {
            Create.Table("QuickMailTemplate")
                 .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("Name").AsString(int.MaxValue).Nullable()
                 .WithColumn("Subject").AsString(int.MaxValue).Nullable()
                 .WithColumn("Message").AsString(int.MaxValue).Nullable()
                 .WithColumn("Attachments").AsString(int.MaxValue).Nullable();
        }
        public override void Down()
        {

            Delete.Table("QuickMailTemplate");

        }
    }
}