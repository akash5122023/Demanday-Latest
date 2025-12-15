using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201015110200)]
    public class DefaultDB_20201015_110200_SMSResponse : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("SMSConfiguration").AddColumn("SuccessResponse").AsString(2048).NotNullable().WithDefaultValue("SMS");
        }
    }
}