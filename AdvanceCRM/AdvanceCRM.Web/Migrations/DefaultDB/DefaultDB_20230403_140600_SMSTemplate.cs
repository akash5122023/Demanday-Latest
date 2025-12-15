
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
    [Migration(20230403140600)]
    public class DefaultDB_20230403_140600_SMSTemplate : Migration
    {
        public override void Up()
        {
            Alter.Table("TeleCallingTemplate")
            .AddColumn("WAReminder").AsString(int.MaxValue).Nullable()
            .AddColumn("WARTemplateId").AsString(100).Nullable()
            ;
        }

        public override void Down()
        {
        }
    }
}