
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
    [Migration(20201221000000)]
    public class DefaultDB_20201221_000000_WebsiteEnquiry : Migration
    {

        public override void Up()
        {

            Create.Table("WebsiteEnquiryDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Phone").AsString(255).Nullable()
                .WithColumn("Email").AsString(255).Nullable()
                .WithColumn("Address").AsString(int.MaxValue).Nullable()
                .WithColumn("Requirement").AsString(int.MaxValue).Nullable()
                .WithColumn("DateTime").AsDateTime().NotNullable()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);



            Create.Table("WebsiteEnquiry").WithColumn("Id").AsInt32().Identity().PrimaryKey()
               .WithColumn("Username").AsString(20).Nullable()
               .WithColumn("Password").AsString(20).Nullable()
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

            Insert.IntoTable("WebsiteEnquiry").Row(new
            {
                Username = "Website",
                Password = "WebsiteAPI",
                Sender = "Your Company Name",
                Subject = "Website Enquiry",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });

        }

        public override void Down()
        {
        }
    }
}