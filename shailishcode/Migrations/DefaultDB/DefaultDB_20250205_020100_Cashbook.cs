
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
    [Migration(20250205020100)]
    public class DefaultDB_20250205_020100_Cashbook : Migration
    {
        public override void Up()
        {
            // Step 1: Add a temporary column of type nvarchar(255) to store the DateTime as a string
            Alter.Table("CashBook")
                .AddColumn("ProjectAmtIn").AsDouble().Nullable();
            Alter.Table("CashBook")
               .AddColumn("Purpose").AsString().Nullable();
            Alter.Table("CashBook")
             .AddColumn("IsCashIn").AsBoolean().Nullable();
            
            Insert.IntoTable("AccountingHeads").Row(new
            {
                Head = "Employee",
                Type = "2"
            });

            Insert.IntoTable("AccountingHeads").Row(new
            {
               Head = "Project Cost",
               Type = "2"
            });

            Insert.IntoTable("AccountingHeads").Row(new
            {
                Head = "Projects",
                Type = "1"
            });

           

        }

        public override void Down()
        {

        }
    }
}