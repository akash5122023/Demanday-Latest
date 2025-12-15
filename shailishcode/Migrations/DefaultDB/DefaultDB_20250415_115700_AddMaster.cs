
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
    [Migration(20250415115700)]
    public class DefaultDB_20250415_115700_AddMaster : Migration
    {

        public override void Up()
        {

            Create.Table("Task").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("Task").AsString(200).NotNullable();

            Alter.Table("Tasks")

               .AddColumn("TaskId").AsInt32().Nullable()
               .ForeignKey("FK_Tasks_TaskId", "dbo", "Task", "Id");


            Execute.Sql("ALTER TABLE Tasks ALTER COLUMN Task NVARCHAR(150) NULL");

        }

        public override void Down()
        {
        }
    }
}