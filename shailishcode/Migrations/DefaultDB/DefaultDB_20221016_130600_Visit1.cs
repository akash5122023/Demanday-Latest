
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
    [Migration(20221016130600)]
    public class DefaultDB_20221016_130600_Visit1 : Migration
    {

        public override void Up()
        {
            Alter.Table("Visit")   
                .AlterColumn("Address").AsString(1000).Nullable()
                .AlterColumn("Email").AsString(250).Nullable()
                .AlterColumn("Location").AsString(500).Nullable()   
                .AlterColumn("Requirements").AsString(1000).Nullable()
             ;
        }

        public override void Down()
        {
        }
    }
}