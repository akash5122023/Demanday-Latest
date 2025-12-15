using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250424115800)]
    public class DefaultDB_20250424_115800_CompanyDetailsTasks : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("CompanyDetails")
                  .AddColumn("TaskTitleInTask").AsBoolean().Nullable().WithDefaultValue(true)
                  .AddColumn("TaskMasterInTask").AsBoolean().Nullable()

                ;
        }
    }
}