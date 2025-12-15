
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
    [Migration(20201229130600)]
    public class DefaultDB_20201229_130600_Zapier : Migration
    {

        public override void Up()
        {

            Create.Table("FacebookDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(255).Unique()
                .WithColumn("Phone").AsString(20).Nullable()
                .WithColumn("Address").AsString(255).Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("CompaignName").AsString(50).Nullable()
                .WithColumn("AdSetName").AsString(50).Nullable()
                .WithColumn("CreatedTime").AsDateTime()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);


            Create.Table("Facebook").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("AutoEmail").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoSMS").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("Sender").AsString(250).Nullable()
                .WithColumn("Subject").AsString(250).Nullable()
                .WithColumn("EmailTemplate").AsString(2000).Nullable()
                .WithColumn("Attachment").AsString(1000).Nullable()
                .WithColumn("SMSTemplate").AsString(1000).Nullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable();
            ;

            Insert.IntoTable("Facebook").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Facebook Enquiry",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });

        }

        public override void Down()
        {
        }
    }
}