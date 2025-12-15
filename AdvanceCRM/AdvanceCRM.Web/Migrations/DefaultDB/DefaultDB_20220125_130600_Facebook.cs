
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
    [Migration(20220125130600)]
    public class DefaultDB_20220125_130600_Facebook : Migration
    {

        public override void Up()
        {

            Alter.Table("FacebookDetails")
                .AddColumn("Company").AsString(100).Nullable()
                .AddColumn("AdId").AsString(100).Nullable()
                .AddColumn("AdName").AsString(100).Nullable()
                .AddColumn("AdSetId").AsString(100).Nullable()
                .AddColumn("AdditionalDetails").AsString(500).Nullable();


            Alter.Table("Facebook")
               .AddColumn("TokenExpiryDate").AsDateTime().Nullable();
            
        }

        public override void Down()
        {
        }
    }
}