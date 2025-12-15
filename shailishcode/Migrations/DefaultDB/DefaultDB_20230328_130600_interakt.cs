
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
    [Migration(20230328130600)]
    public class DefaultDB_20230328_130600_interakt : Migration
    {

        public override void Up()
        {
            Create.Table("InteraktConfig")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()                
                .WithColumn("SecretKey").AsString(1000).NotNullable()
                .WithColumn("AutoEmail").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoSMS").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("Sender").AsString(250).Nullable()
                .WithColumn("Subject").AsString(250).Nullable()
                .WithColumn("EmailTemplate").AsString(2000).Nullable()
                .WithColumn("Attachment").AsString(1000).Nullable()
                .WithColumn("SMSTemplate").AsString(1000).Nullable()
                .WithColumn("TemplateID").AsString(1000).Nullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable();

            Create.Table("InteraktUser")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("InteraktID").AsString(100).Nullable()
                .WithColumn("Created").AsDateTime().Nullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("Phone").AsString(50).Nullable()
                .WithColumn("CountryCode").AsString(50).Nullable()
                .WithColumn("UserId").AsString(100).Nullable()
                .WithColumn("FullName").AsString(100).Nullable()
                .WithColumn("Email").AsString(100).Nullable()
                .WithColumn("WPOptedIn").AsBoolean().Nullable();

            Insert.IntoTable("InteraktConfig").Row(new
            {
                SecretKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                Sender = "Your Company Name",
                Subject = "Woocommerce",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });
        }

        public override void Down()
        {
        }
    }
}