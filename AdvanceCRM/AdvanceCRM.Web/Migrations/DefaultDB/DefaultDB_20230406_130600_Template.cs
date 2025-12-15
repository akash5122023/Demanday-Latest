
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
    [Migration(20230406130600)]
    public class DefaultDB_20230406_130600_Template : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("BizWAConfig").Row(new
            {
                WhatsAppNo = "+91XXXXXXXXX",
                PhoneNoID = "XXXXXXX",
                WBAID = "XXXXXXXXXXXX",
                Accesstoken = "Access Token Key"
            });
        }

        public override void Down()
        {
        }
    }
}