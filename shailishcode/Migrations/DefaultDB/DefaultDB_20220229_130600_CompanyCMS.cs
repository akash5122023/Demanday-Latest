
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
    [Migration(20220229130600)]
    public class DefaultDB_20220229_130600_CompanyCMS : Migration
    {

        public override void Up()
        {

            Alter.Table("CompanyDetails")
             .AddColumn("CMSStartNo").AsInt32().Nullable()
             .AddColumn("CMSEditNo").AsBoolean().WithDefaultValue(false)
             .AddColumn("CMSSuffix").AsInt32().Nullable()
             .AddColumn("CMSprefix").AsBoolean().WithDefaultValue(false);

            Alter.Table("CMS")
                .AddColumn("CMSN").AsString(20).Nullable();
        }

        public override void Down()
        {
        }
    }
}