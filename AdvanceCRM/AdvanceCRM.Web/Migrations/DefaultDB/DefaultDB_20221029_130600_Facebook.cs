
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
    [Migration(20221029130600)]
    public class DefaultDB_20221029_130600_Facebook : Migration
    {

        public override void Up()
        {

            Alter.Table("FacebookDetails")
                .AlterColumn("Name").AsString(255).Nullable()
               ;


           

        }

        public override void Down()
        {
        }
    }
}