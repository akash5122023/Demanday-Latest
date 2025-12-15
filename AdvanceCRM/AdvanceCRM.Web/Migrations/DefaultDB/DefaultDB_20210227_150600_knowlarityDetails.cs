
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
    [Migration(20210227150600)]
    public class DefaultDB_20210227_150600_knowlarityDetails : Migration
    {

        public override void Up()
        {

            Create.Table("KnowlarityDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(255)
                .WithColumn("CustomerNumber").AsString(20).Nullable()
                .WithColumn("CompanyType").AsInt32().Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("EmployeeNumber").AsString(100).Nullable()
                .WithColumn("Duration").AsString(50).Nullable()
                .WithColumn("Recording").AsString(1000).Nullable()
                .WithColumn("DateTime").AsDateTime().NotNullable()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);
             }

        public override void Down()
        {
        }
    }
}