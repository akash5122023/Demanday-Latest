
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
    [Migration(20250304105000)]
    public class DefaultDB_20250304_105000_CMS : Migration
    {
        public override void Up()
        {
            // Step 1: Add a temporary column of type nvarchar(255) to store the DateTime as a string
            Alter.Table("cms")
                .AddColumn("Qty").AsInt32().Nullable();
            Alter.Table("cms")
                .AddColumn("Representative").AsString(int.MaxValue).Nullable();
        }

        public override void Down()
        {

        }
    }
}