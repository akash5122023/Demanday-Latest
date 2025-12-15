
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
    [Migration(20210302130600)]
    public class DefaultDB_20210302_130600_CompanyType : Migration
    {

        public override void Up()
        {

            Alter.Table("CompanyDetails")
                .AddColumn("CompanyType").AsInt32().Nullable();
        }

        public override void Down()
        {
        }
    }
}