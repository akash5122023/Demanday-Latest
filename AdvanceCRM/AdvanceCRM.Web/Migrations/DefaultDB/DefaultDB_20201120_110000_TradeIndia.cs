using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201120110000)]
    public class DefaultDB_20201120_110000_TradeIndia : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("TradeIndiaConfiguration")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("UserId").AsString(20).Nullable()
               .WithColumn("ProfileId").AsString(20).NotNullable()
               .WithColumn("APIKey").AsString(100).Nullable()
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

            Create.Table("TradeIndiaDetails")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("RfiId").AsInt32().NotNullable()
              .WithColumn("Source").AsString(200).Nullable()
              .WithColumn("ProductSource").AsString(200).NotNullable()
              .WithColumn("GeneratedDateTime").AsDateTime().Nullable()
               .WithColumn("InquiryType").AsString().Nullable()
                .WithColumn("Subject").AsString(500).Nullable()
                .WithColumn("ProductName").AsString(500).Nullable()
                .WithColumn("Quantity").AsString(500).Nullable().WithDefaultValue(0)
                .WithColumn("OrderValueMin").AsInt32().Nullable().WithDefaultValue(0)
                .WithColumn("Message").AsString(int.MaxValue).Nullable()
                .WithColumn("SenderCo").AsString(500).Nullable()
                .WithColumn("SenderName").AsString(500).Nullable()
                .WithColumn("SenderMobile").AsString(500).Nullable()
                .WithColumn("SenderEmail").AsString(500).Nullable()
                .WithColumn("SenderAddress").AsString(int.MaxValue).Nullable()
                .WithColumn("SenderCity").AsString(50).Nullable()
                .WithColumn("SenderState").AsString(50).Nullable()
                .WithColumn("SenderCountry").AsString(50).Nullable()
                .WithColumn("MonthSlot").AsString(500).Nullable()
                .WithColumn("LandlineNumber").AsString(500).Nullable()
                .WithColumn("PrefSuppLocation").AsString(int.MaxValue).Nullable()
                .WithColumn("IsMoved").AsBoolean().NotNullable().WithDefaultValue(false);

            Insert.IntoTable("TradeIndiaConfiguration").Row(new
            {
                UserId = "User Id",
                ProfileId = "Profile Id",
                APIKey = "xxxx-xxxx-xxxx-xxxx",
                Sender = "Your Company Name",
                Subject = "India Mart",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });
        }
    }
}