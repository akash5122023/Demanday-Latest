
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
    [Migration(20220122130600)]
    public class DefaultDB_20220122_130600_Facebook : Migration
    {

        public override void Up()
        {

            Alter.Table("FacebookDetails")
                .AddColumn("LeadId").AsString(100).Nullable()
                .AddColumn("Campaignid").AsString(100).Nullable();


            Alter.Table("Facebook")
               .AddColumn("AppID").AsString(100).Nullable()
               .AddColumn("AccessToken Key").AsString(1000).Nullable();
            

           
            Insert.IntoTable("Stage").Row(new
            {
                Stage = "New",
                Type = "1",
                
            });
            Insert.IntoTable("Stage").Row(new
            {
                Stage = "New",
                Type = "2",

            });
            Insert.IntoTable("Tasktype").Row(new
            {
                Type = "General",
              

            });



        }

        public override void Down()
        {
        }
    }
}