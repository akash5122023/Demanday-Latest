
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
    [Migration(20230406140600)]
    public class DefaultDB_20230406_140600_WhatsApp : Migration
    {

        public override void Up()
        {
            Alter.Table("BizWAConfig")
                .AddColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                ;
        }

        public override void Down()
        {
        }
    }
}