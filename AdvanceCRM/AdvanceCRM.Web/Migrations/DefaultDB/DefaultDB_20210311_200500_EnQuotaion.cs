using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210311200500)]
    public class DefaultDB_20210311_200500_EnQuotaion : AutoReversingMigration
    {
        public override void Up()
        {
           
            

            Alter.Table("QuotationProducts")
               .AddColumn("Hypothecation").AsDouble().Nullable().WithDefaultValue(0)
               .AddColumn("Accessories").AsDouble().Nullable().WithDefaultValue(0)
               .AddColumn("RoadSideAssistance").AsDouble().Nullable().WithDefaultValue(0)
               .AddColumn("AMC").AsDouble().Nullable().WithDefaultValue(0)
               .AddColumn("ExtendedWarranty").AsDouble().Nullable().WithDefaultValue(0)
               .AddColumn("Others").AsDouble().Nullable().WithDefaultValue(0)
               .AddColumn("Concession").AsDouble().WithDefaultValue(0);


        }
    }
}