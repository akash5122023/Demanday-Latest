using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250919123400)]
    public class DefaultDB_20250919_123400_CompanyDetails : Migration

    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("PurchaseHeaderContent").AsString(int.MaxValue).Nullable()
                .AddColumn("PurchaseFooterContent").AsString(int.MaxValue).Nullable()
                .AddColumn("PurchaseHeaderImage").AsString(500).Nullable()
                .AddColumn("PurchaseHeaderHeight").AsInt32().Nullable()
                .AddColumn("PurchaseHeaderWidth").AsInt32().Nullable()
                .AddColumn("PurchaseFooterImage").AsString(500).Nullable()
                .AddColumn("PurchaseFooterHeight").AsInt32().Nullable()
                .AddColumn("PurchaseFooterWidth").AsInt32().Nullable();

        }

        public override void Down()
        {

        }
    }
}