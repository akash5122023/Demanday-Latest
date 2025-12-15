using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250429112200)]
    public class DefaultDB_20250429_112200_AddSMSTemplate : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("RazorPay")
          .AddColumn("SMSTemplateId").AsString(100).Nullable();

            Alter.Table("Instamojo")
          .AddColumn("SMSTemplateId").AsString(100).Nullable();

        }
    }
}