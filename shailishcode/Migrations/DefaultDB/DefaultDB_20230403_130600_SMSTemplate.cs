
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
    [Migration(20230403130600)]
    public class DefaultDB_20230403_130600_SMSTemplate : Migration
    {
        public override void Up()
        {
            Alter.Table("TeleCallingTemplate")
            .AddColumn("SMSReminder").AsString(int.MaxValue).Nullable()
            .AddColumn("SMSRTemplateId").AsString(100).Nullable()
            ;
        }

        public override void Down()
        {
        }
    }
}