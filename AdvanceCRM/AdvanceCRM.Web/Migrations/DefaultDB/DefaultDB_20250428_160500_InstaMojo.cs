using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250428160500)]
    public class DefaultDB_20250428_160500_InstaMojo : AutoReversingMigration
    {

        public override void Up()
        {


            Create.Table("InstamojoDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(255).Unique()
                .WithColumn("Phone").AsString(20).Nullable()
                .WithColumn("Address").AsString(255).Nullable()
                .WithColumn("Email").AsString(50).Nullable()
                 .WithColumn("Purpose").AsString(20).Nullable()
                .WithColumn("PaymentMode").AsString(50).Nullable()
                .WithColumn("Status").AsString(50).Nullable()
                .WithColumn("PayoutDate").AsDateTime()
                .WithColumn("InstaID").AsString(100).Nullable()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);


            Create.Table("Instamojo").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                  .WithColumn("AppID").AsString(100).Nullable()
               .WithColumn("AccessTokenKey").AsString(1000).Nullable()
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

            Insert.IntoTable("Instamojo").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Instamojo Payment",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });
        }
    }
}