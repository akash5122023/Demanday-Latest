using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250425132200)]
    public class DefaultDB_20250425_132200_CompanyIdInSourceAndStage : AutoReversingMigration
    {

        public override void Up()
        {
            if (!Schema.Table("Source").Column("CompanyId").Exists())
            {
                Alter.Table("Source")
                    .AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1)
                    .ForeignKey("FK_Source_CompanyId", "dbo", "CompanyDetails", "Id");
            }

            if (!Schema.Table("Stage").Column("CompanyId").Exists())
            {
                Alter.Table("Stage")
                    .AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1)
                    .ForeignKey("FK_Stage_CompanyId", "dbo", "CompanyDetails", "Id");
            }
        }
    }
}