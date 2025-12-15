
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
    [Migration(20220413130600)]
    public class DefaultDB_20220413_130600_CompanyCMS : Migration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails")
             .AddColumn("ProductsInCMS").AsBoolean().WithDefaultValue(false);  
        }

        public override void Down()
        {
        }
    }
}