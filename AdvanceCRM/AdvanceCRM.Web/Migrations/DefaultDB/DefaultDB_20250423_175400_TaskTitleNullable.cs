
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
    [Migration(20250423175400)]
    public class DefaultDB_20250423_175400_TaskTitleNullable : Migration
    {

        public override void Up()
        {
            Alter.Column("TaskTitle").OnTable("Tasks")
                            .AsString(150).Nullable();
        }

        public override void Down()
        {
        }
    }
}