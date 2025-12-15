
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
    [Migration(20220519172300)]
    public class DefaultDB_20220519_172300_UserCompany : Migration
    {

        public override void Up()
        {
            Alter.Table("Users")
             .AddColumn("CMS").AsBoolean().WithDefaultValue(false);

            Alter.Table("AdditionalConcession")
            .AddColumn("Amount").AsDouble().Nullable();

            Alter.Table("CompanyDetails")
             .AddColumn("MailToSubContacts").AsBoolean().WithDefaultValue(false);

        }

        public override void Down()
        {
        }
    }
}