using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200706014514)]
    public class DefaultDB_20200706_014514_IndiaMARTRequirement : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("IndiaMartDetails")
                .AddColumn("Source").AsInt32().NotNullable().WithDefaultValue(1)
                .AlterColumn("DateRe").AsDateTime();
        }
    }
}