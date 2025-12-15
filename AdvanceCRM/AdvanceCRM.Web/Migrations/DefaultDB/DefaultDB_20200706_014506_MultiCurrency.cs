using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200706014506)]
    public class DefaultDB_20200706_014506_MultiCurrency : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("Quotation").AddColumn("CurrencyConversion").AsBoolean().WithDefaultValue(false).Nullable()
                .AddColumn("FromCurrency").AsInt32().Nullable()
                .AddColumn("ToCurrency").AsInt32().Nullable();

            Alter.Table("Invoice").AddColumn("CurrencyConversion").AsBoolean().WithDefaultValue(false).Nullable()
                .AddColumn("FromCurrency").AsInt32().Nullable()
                .AddColumn("ToCurrency").AsInt32().Nullable();

            Alter.Table("Sales").AddColumn("CurrencyConversion").AsBoolean().WithDefaultValue(false).Nullable()
                .AddColumn("FromCurrency").AsInt32().Nullable()
                .AddColumn("ToCurrency").AsInt32().Nullable();
        }
    }
}