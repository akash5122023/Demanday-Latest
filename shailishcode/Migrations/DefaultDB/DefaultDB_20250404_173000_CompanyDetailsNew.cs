using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250404173000)]
    public class DefaultDB_20250404_173000_CompanyDetailsNew : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("CompanyDetails")
                  .AddColumn("PassportDetails").AsBoolean().Nullable()
                ;
        }
    }
}