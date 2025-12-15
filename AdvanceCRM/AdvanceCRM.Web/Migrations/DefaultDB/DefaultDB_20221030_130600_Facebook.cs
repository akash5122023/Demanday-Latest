
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
    [Migration(20221030130600)]
    public class DefaultDB_20221030_130600_Facebook : Migration
    {

        public override void Up()
        {
          
            if (Schema.Table("FacebookDetails").Column("Name").Exists())
            {
                Delete.Column("Name").FromTable("CompanyDetails");
            }

            Alter.Table("FacebookDetails")
              .AlterColumn("Name").AsString(255).Nullable()
             ;



        }

        public override void Down()
        {
        }
    }
}