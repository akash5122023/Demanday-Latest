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
    [Migration(20250318171400)]
    public class DefaultDB_20250318_171400_Attendance : Migration
    {
        public override void Up()
        {
            // Check if UserId exists before dropping it
            if (Schema.Table("Attendance").Column("UserId").Exists())
            {
                Delete.Column("UserId").FromTable("Attendance");
            }

            // Add UserId as a string
            Alter.Table("Attendance")
                .AddColumn("UserId").AsString(50).NotNullable().WithDefaultValue(""); // Now as NVARCHAR(50)
        }

        public override void Down()
        {

        }
    }
}
