
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
    [Migration(20220413172300)]
    public class DefaultDB_20220413_172300_CompanyCMS : Migration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails")
             .AddColumn("RemovePurchaseDate").AsBoolean().WithDefaultValue(false)
             .AddColumn("RemoveInvoiceNo").AsBoolean().WithDefaultValue(false);
        }

        public override void Down()
        {
        }
    }
}