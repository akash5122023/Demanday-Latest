using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250919123000)]
    public class DefaultDB_20250919_123000_CompanyDetailsIndent : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("IndentHeaderContent").AsString(int.MaxValue).Nullable()
                .AddColumn("IndentFooterContent").AsString(int.MaxValue).Nullable()
                .AddColumn("IndentHeaderImage").AsString(500).Nullable()
                .AddColumn("IndentHeaderHeight").AsInt32().Nullable()
                .AddColumn("IndentHeaderWidth").AsInt32().Nullable()
                .AddColumn("IndentFooterImage").AsString(500).Nullable()
                .AddColumn("IndentFooterHeight").AsInt32().Nullable()
                .AddColumn("IndentFooterWidth").AsInt32().Nullable();

        }

        public override void Down()
        {

        }
    }
}