
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
    [Migration(20230222130600)]
    public class DefaultDB_20230222_130600_Mailwizz : Migration
    {

        public override void Up()
        {
            Alter.Table("BizMailConfig")
                .AddColumn("FromName").AsString(100).Nullable()
                .AddColumn("FromMail").AsString(100).Nullable()
                .AddColumn("ReplyToName").AsString(100).Nullable()
                .AddColumn("ReplyToMail").AsString(100).Nullable();


            Insert.IntoTable("BizMailConfig").Row(new
            {
                
                apiurl = "null",
                apikey = "null",
                FromName = "null",
                FromMail = "null",
                ReplyToName = "null",
                ReplyToMail = "null"
            });
        }

        public override void Down()
        {
        }
    }
}