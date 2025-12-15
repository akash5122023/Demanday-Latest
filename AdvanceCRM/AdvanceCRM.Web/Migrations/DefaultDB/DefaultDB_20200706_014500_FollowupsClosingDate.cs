using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200706014500)]
    public class DefaultDB_20200706_014500_FollowupsClosingDate : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("EnquiryFollowups").AddColumn("ClosingDate").AsDate().Nullable();
            Alter.Table("QuotationFollowups").AddColumn("ClosingDate").AsDate().Nullable();
            Alter.Table("InvoiceFollowups").AddColumn("ClosingDate").AsDate().Nullable();
            Alter.Table("CMSFollowups").AddColumn("ClosingDate").AsDate().Nullable();
            Alter.Table("TeleCallingFollowups").AddColumn("ClosingDate").AsDate().Nullable();

            Alter.Table("Invoice").AddColumn("ClosingDate").AsDate().Nullable();
            Alter.Table("Sales").AddColumn("ClosingDate").AsDate().Nullable();
            Alter.Table("Challan").AddColumn("ClosingDate").AsDate().Nullable();

        }
    }
}