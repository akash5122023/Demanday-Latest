
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
    [Migration(20220516172300)]
    public class DefaultDB_20220516_172300_Integration : Migration
    {

        public override void Up()
        {
            Alter.Table("FacebookDetails")
             .AddColumn("Feedback").AsString(500).Nullable();

            Alter.Table("IndiaMartDetails")
             .AddColumn("Feedback").AsString(500).Nullable();

            Alter.Table("JustDialDetails")
            .AddColumn("Feedback").AsString(500).Nullable();

            Alter.Table("SIndiaMartDetails")
            .AddColumn("Feedback").AsString(500).Nullable();

            Alter.Table("TradeIndiaDetails")
            .AddColumn("Feedback").AsString(500).Nullable();

            Alter.Table("Visit")
            .AddColumn("Feedback").AsString(500).Nullable();

            Alter.Table("WebsiteEnquiryDetails")
            .AddColumn("Feedback").AsString(500).Nullable();

            Alter.Table("WoocommerceDetails")
            .AddColumn("Feedback").AsString(500).Nullable();
        }

        public override void Down()
        {
        }
    }
}