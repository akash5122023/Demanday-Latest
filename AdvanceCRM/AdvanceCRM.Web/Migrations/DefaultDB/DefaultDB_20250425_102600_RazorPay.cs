using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250425102600)]
    public class DefaultDB_20250425_102600_RazorPay : AutoReversingMigration
    {

        public override void Up()
        {


            Create.Table("RPPaymentDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("PaymentId").AsString(40).Nullable()
                .WithColumn("Name").AsString(50).Nullable()
                .WithColumn("Phone").AsString(20).Nullable()
                 .WithColumn("Email").AsString(50).Nullable()
                .WithColumn("Entity").AsString(20).Nullable()
                 .WithColumn("Amount").AsString(20).Nullable()
                 .WithColumn("Currency").AsString(20).Nullable()
                 .WithColumn("Status").AsString(20).Nullable()
                 .WithColumn("OrderId").AsString(20).Nullable()
                 .WithColumn("InvoiceId").AsString(20).Nullable()
                 .WithColumn("international").AsString(20).Nullable()
                 .WithColumn("Method").AsString(20).Nullable()
                .WithColumn("RefundedAmt").AsString(25).Nullable()
                .WithColumn("RefundStatus").AsString(20).Nullable()
                 .WithColumn("captured").AsString(20).Nullable()
                .WithColumn("Discription").AsString(2000).Nullable()
                .WithColumn("CardId").AsString(50).Nullable()
                 .WithColumn("Bank").AsString(50).Nullable()
                 .WithColumn("Wallet").AsString(50).Nullable()
                  .WithColumn("VPA").AsString(50).Nullable()
                .WithColumn("CreatedDate").AsDateTime()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false)
                .WithColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_RP_CompanyId", "dbo", "CompanyDetails", "Id");



            Create.Table("RazorPay").WithColumn("Id").AsInt32().Identity().PrimaryKey()
               .WithColumn("AppID").AsString(100).Nullable()
               .WithColumn("SecretKey").AsString(1000).Nullable()
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
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_RPC_CompanyId", "dbo", "CompanyDetails", "Id");


            ;

            Insert.IntoTable("RazorPay").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Razor Payment",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });
        }
    }
}