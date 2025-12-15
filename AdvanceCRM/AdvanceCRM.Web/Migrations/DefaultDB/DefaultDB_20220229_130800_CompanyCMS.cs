
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
    [Migration(20220229130800)]
    public class DefaultDB_20220229_130800_CompanyCMS : Migration
    {

        public override void Up()
        {

            if (Schema.Table("CompanyDetails").Column("CMSSuffix").Exists())
            {
                Delete.Column("CMSSuffix").FromTable("CompanyDetails");
                Delete.Column("CMSprefix").FromTable("CompanyDetails");
            }

            Alter.Table("CompanyDetails")
            
             .AddColumn("CMSSuffix").AsString(20).Nullable()
             .AddColumn("CMSprefix").AsString(20).Nullable();

          
        }

        public override void Down()
        {
        }
    }
}