
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
    [Migration(20250404100000)]
    public class DefaultDB_20250404_100000_ChallanProducts : Migration
    {
        public override void Up()
        {
            Alter.Table("ChallanProducts")
                .AddColumn("TaxType1").AsString(100).Nullable()
                .AddColumn("Percentage1").AsDouble().Nullable()
                .AddColumn("TaxType2").AsString(100).Nullable()
                .AddColumn("Percentage2").AsDouble().Nullable();
        }

        public override void Down()
        {

        }
    }
}