using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250429154400)]
    public class DefaultDB_20250429_154400_AddWinPercentage : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("Quotation")
                 .AddColumn("WinPercentage").AsInt32().Nullable()
                 .AddColumn("ExpectedClosingDate").AsDateTime().Nullable()
              ;

            Alter.Table("Enquiry")
                 .AddColumn("WinPercentage").AsInt32().Nullable()
                 .AddColumn("ExpectedClosingDate").AsDateTime().Nullable()
               ;
        }
    }
}