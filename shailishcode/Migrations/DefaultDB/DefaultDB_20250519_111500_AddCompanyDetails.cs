using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250519111500)]
    public class DefaultDB_20250519_111500_AddCompanyDetails : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails")
               .AddColumn("ItineraryHeaderContent").AsString(int.MaxValue).Nullable()
               .AddColumn("ItineraryFooterContent").AsString(int.MaxValue).Nullable()
               .AddColumn("ItineraryHeaderImage").AsString(500).Nullable()
               .AddColumn("ItineraryHeaderHeight").AsInt32().Nullable()
               .AddColumn("ItineraryHeaderWidth").AsInt32().Nullable()
               .AddColumn("ItineraryFooterImage").AsString(500).Nullable()
               .AddColumn("ItineraryFooterHeight").AsInt32().Nullable()
               .AddColumn("ItineraryFooterWidth").AsInt32().Nullable()
               ;



        }
    }
}