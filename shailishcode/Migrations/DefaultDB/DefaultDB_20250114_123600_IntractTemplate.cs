
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
    [Migration(20250114123600)]
    public class DefaultDB_20250114_123600_IntractTemplate : Migration
    {

        public override void Up()
        {

            Create.Table("IntractTemplate")
                 .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("IntractId").AsString(int.MaxValue).Nullable()
                 .WithColumn("created_at_utc").AsDateTime().Nullable()
                 .WithColumn("name").AsString(255).Nullable()
                 .WithColumn("language").AsString(255).Nullable()
                 .WithColumn("category").AsString(255).Nullable()
                 .WithColumn("template_category_label").AsString(255).Nullable()
                 .WithColumn("header_format").AsString(255).Nullable()
                 .WithColumn("header").AsString(255).Nullable()
                 .WithColumn("body").AsString(int.MaxValue).Nullable()
                 .WithColumn("footer").AsString(int.MaxValue).Nullable()
                 .WithColumn("buttons").AsString(int.MaxValue).Nullable()
                 .WithColumn("autosubmitted_for").AsString(int.MaxValue).Nullable()
                 .WithColumn("display_name").AsString(int.MaxValue).Nullable()
                 .WithColumn("approval_status").AsString(255).Nullable()
                 .WithColumn("wa_template_id").AsString(int.MaxValue).Nullable()
                 .WithColumn("variable_present").AsString(int.MaxValue).Nullable()                 
                ;
        }

        public override void Down()
        {
        }
    }
}