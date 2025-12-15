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
    [Migration(20250315125500)]
    public class DefaultDB_20250325_125500_EmployeeAssests : Migration
    {
        public override void Up()
        {
            Create.Table("EmployeeAssests")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Items").AsString(200).NotNullable()
                .WithColumn("Quantity").AsInt32().Nullable()
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("EmployeeId").AsInt32().NotNullable().ForeignKey("FK_EAEmployee_EmployeeId", "dbo", "Employee", "Id");
        }

        public override void Down()
        {

        }
    }
}
