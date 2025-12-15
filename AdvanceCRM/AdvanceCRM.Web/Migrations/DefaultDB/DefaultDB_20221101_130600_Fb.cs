
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
    [Migration(20221101130600)]
    public class DefaultDB_20221101_130600_Fb : Migration
    {

        public override void Up()
        {
            Delete.Table("FacebookDetails");
           
            Alter.Table("CompanyDetails")
              .AddColumn("Name").AsString(255).Nullable()
             ;

            Create.Table("FacebookDetails")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
               .WithColumn("Name").AsString(255).Nullable()
               .WithColumn("Phone").AsString(20).Nullable()
               .WithColumn("Address").AsString(255).Nullable()
               .WithColumn("Email").AsString().Nullable()
               .WithColumn("CompaignName").AsString(50).Nullable()
               .WithColumn("AdSetName").AsString(50).Nullable()
               .WithColumn("CreatedTime").AsDateTime()
               .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false)
               .WithColumn("LeadId").AsString(100).Nullable()
               .WithColumn("Campaignid").AsString(100).Nullable()
               .WithColumn("Company").AsString(100).Nullable()
               .WithColumn("AdId").AsString(100).Nullable()
               .WithColumn("AdName").AsString(100).Nullable()
               .WithColumn("AdSetId").AsString(100).Nullable()
               .WithColumn("AdditionalDetails").AsString(500).Nullable()
               .WithColumn("Feedback").AsString(500).Nullable(); 



        }

        public override void Down()
        {
        }
    }
}