using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201104154600)]
    public class DefaultDB_20201104_154600_TeleCMI : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("IVRConfiguration").AddColumn("IVRType").AsInt32().NotNullable().WithDefaultValue(1)
                .AddColumn("AppID").AsString(1024).NotNullable().WithDefaultValue("xxxxxxx")
                .AddColumn("AppSecret").AsString(1024).NotNullable().WithDefaultValue("xxxx-xxxx-xxxx-xxx");
        }
    }
}