
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
    [Migration(20250203110800)]
    public class DefaultDB_20250203_110800_Po_Amc_dueDate : Migration
    {
        public override void Up()
        {
            // Step 1: Add a temporary column of type nvarchar(255) to store the DateTime as a string
            Alter.Table("AMC")
                .AddColumn("DueDate").AsDateTime().Nullable();
            Alter.Table("PurchaseOrder")
               .AddColumn("DueDate").AsDateTime().Nullable();

        }

        public override void Down()
        {

        }
    }
}