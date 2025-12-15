
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
    [Migration(20250423170100)]
    public class DefaultDB_20250423_170100_RenameTaskTitle : Migration
    {

        public override void Up()
        {
            Rename.Column("Task").OnTable("Tasks").To("TaskTitle");
        }

        public override void Down()
        {
        }
    }
}