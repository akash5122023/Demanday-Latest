using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200706014501)]
    public class DefaultDB_20200706_014501_AttachmentsInSP : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails").AddColumn("RoundupInPurchase").AsBoolean().WithDefaultValue(false).Nullable();
            Alter.Table("Invoice").AddColumn("Attachments").AsString(1000).Nullable();
            Alter.Table("Sales").AddColumn("Attachments").AsString(1000).Nullable();
            Alter.Table("Challan").AddColumn("Attachments").AsString(1000).Nullable();
            Alter.Table("Purchase").AddColumn("Attachments").AsString(1000).Nullable().AddColumn("Roundup").AsDouble().Nullable();
            Alter.Table("PurchaseOrder").AddColumn("Attachments").AsString(1000).Nullable().AddColumn("Roundup").AsDouble().Nullable();

        }
    }
}