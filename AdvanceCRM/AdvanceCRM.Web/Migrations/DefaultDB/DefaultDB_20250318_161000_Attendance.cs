
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
    [Migration(20250318161000)]
    public class DefaultDB_20250318_161000_Attendance : Migration
    {
        public override void Up()
        {
            Alter.Table("Attendance")
                .AddColumn("UserId").AsInt32().NotNullable().WithDefaultValue(0); // Add UserId column
        }

        public override void Down()
        {

        }
    }
}