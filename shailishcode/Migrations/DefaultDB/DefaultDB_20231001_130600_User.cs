
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
    [Migration(20231001130600)]
    public class DefaultDB_20231001_130600_User : Migration
    {

        public override void Up()
        {

            Alter.Table("Users")
                .AddColumn("Location").AsString(250).Nullable()
                .AddColumn("Coordinates").AsString(250).Nullable();
                

        }

        public override void Down()
        {
        }
    }
}