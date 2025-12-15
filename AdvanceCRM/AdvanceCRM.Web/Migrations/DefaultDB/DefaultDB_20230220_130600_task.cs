
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
    [Migration(20230220130600)]
    public class DefaultDB_20230220_130600_task : Migration
    {

        public override void Up()
        {
            Alter.Table("Tasks").AddColumn("Recurring").AsBoolean().WithDefaultValue(false); 
        }

        public override void Down()
        {
        }
    }
}