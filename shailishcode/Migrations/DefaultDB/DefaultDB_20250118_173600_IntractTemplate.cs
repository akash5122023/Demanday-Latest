
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
    [Migration(20250118173600)]
    public class DefaultDB_20250118_173600_IntractTemplate : Migration
    {
        public override void Up()
        {
            // Step 1: Add a temporary column of type nvarchar(255) to store the DateTime as a string
            Alter.Table("IntractTemplate")
                .AddColumn("created_at_utc_temp").AsString(255).Nullable();

            // Step 2: Update the new column with the string representation of DateTime values
            Execute.Sql("UPDATE IntractTemplate SET created_at_utc_temp = CONVERT(VARCHAR(255), created_at_utc, 120)");

            // Step 3: Drop the original DateTime column
            Delete.Column("created_at_utc").FromTable("IntractTemplate");

            // Step 4: Rename the temporary column to the original column name using a different approach
            Execute.Sql("EXEC sp_rename 'IntractTemplate.created_at_utc_temp', 'created_at_utc', 'COLUMN'");

        }

        public override void Down()
        {

            // Step 1: If rolling back, add the DateTime column back
            Alter.Table("IntractTemplate")
                .AddColumn("created_at_utc").AsDateTime().Nullable();

            // Step 2: Convert the string values back to DateTime (ensure the format matches)
            Execute.Sql("UPDATE IntractTemplate SET created_at_utc = CONVERT(DATETIME, created_at_utc, 120)");

            // Step 3: Drop the temporary string column
            Delete.Column("created_at_utc_temp").FromTable("IntractTemplate");
        }
    }
}