
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
    [Migration(20220517172300)]
    public class DefaultDB_20220517_172300_User : Migration
    {

        public override void Up()
        {
            Alter.Table("Users")
             .AddColumn("Enquiry").AsBoolean().WithDefaultValue(false)
             .AddColumn("Quotation").AsBoolean().WithDefaultValue(false)
             .AddColumn("Tasks").AsBoolean().WithDefaultValue(false)
             .AddColumn("Contacts").AsBoolean().WithDefaultValue(false)
             .AddColumn("Purchase").AsBoolean().WithDefaultValue(false)
             .AddColumn("Sales").AsBoolean().WithDefaultValue(false);

            Alter.Table("PurchaseOrder")
            .AddColumn("Conversion").AsDouble().Nullable()
            .AddColumn("CurrencyConversion").AsBoolean().WithDefaultValue(false).Nullable()
             .AddColumn("FromCurrency").AsInt32().Nullable()
             .AddColumn("ToCurrency").AsInt32().Nullable(); 



        }

        public override void Down()
        {
        }
    }
}