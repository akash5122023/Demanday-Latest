using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200222180300)]
    public class DefaultDB_20200222_180300_AdditionalSettings : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("Timeline").AddColumn("Type").AsInt32().NotNullable().WithDefaultValue(1);

            Alter.Table("Enquiry").AddColumn("Attachments").AsString(1000).Nullable();

            Alter.Table("Contacts").AddColumn("Whatsapp").AsString(16).Nullable();

            Alter.Table("SubContacts").AddColumn("Whatsapp").AsString(16).Nullable();

            Alter.Table("PurchaseOrder").AddColumn("SMSTemplate").AsString(1000).Nullable();

            Alter.Table("SMSConfiguration").AddColumn("BulkAPI").AsString(2048).NotNullable().WithDefaultValue("Bulk_API_Url")
            .AddColumn("ScheduleAPI").AsString(2048).NotNullable().WithDefaultValue("Schedule_API_Url");

            Alter.Table("Village").AddColumn("PIN").AsString(50).Nullable();

        }
    }
}