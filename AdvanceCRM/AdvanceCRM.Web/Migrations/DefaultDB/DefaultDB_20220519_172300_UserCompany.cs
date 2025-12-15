
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
    [Migration(20220711172300)]
    public class DefaultDB_20220711_172300_MailCompany : Migration
    {

        public override void Up()
        {
           
            Alter.Table("CompanyDetails")
             .AddColumn("MailToOrganisation").AsBoolean().WithDefaultValue(true);

        }

        public override void Down()
        {
        }
    }
}