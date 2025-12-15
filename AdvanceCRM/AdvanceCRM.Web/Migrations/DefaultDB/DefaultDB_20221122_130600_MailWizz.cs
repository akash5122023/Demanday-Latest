
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
    [Migration(20221122130600)]
    public class DefaultDB_20221122_130600_MailWizz : Migration
    {

        public override void Up()
        {
            Alter.Table("BMTemplate")
                .AddColumn("TemplateUID").AsString(200).Nullable();
               

           
        }

        public override void Down()
        {
        }
    }
}