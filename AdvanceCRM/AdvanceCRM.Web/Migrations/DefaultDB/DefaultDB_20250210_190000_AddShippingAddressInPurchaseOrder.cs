
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
    [Migration(20250210190000)]
    public class DefaultDB_20250210_190000_AddShippingAddressInPurchaseOrder : Migration
    {
        public override void Up()
        {
            Alter.Table("PurchaseOrder")
                .AddColumn("ShippingAddress").AsString(1000).Nullable();

        }

        public override void Down()
        {

        }
    }
}