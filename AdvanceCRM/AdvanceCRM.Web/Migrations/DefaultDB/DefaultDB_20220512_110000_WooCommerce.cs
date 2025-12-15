using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220512110000)]
    public class DefaultDB_20220512_110000_WooCommerce : AutoReversingMigration
    {
        public override void Up()
        {


            Create.Table("WoocommerceDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("FirstName").AsString(255).Nullable()
                .WithColumn("LastName").AsString(255).Nullable()
                .WithColumn("Company").AsString(255).Nullable()
               .WithColumn("Email").AsString(50).Nullable()
                .WithColumn("Phone").AsString(20).Nullable()
                .WithColumn("Address").AsString(255).Nullable()
                .WithColumn("City").AsString(255).Nullable()
                .WithColumn("State").AsString(255).Nullable()
                 .WithColumn("Country").AsString(255).Nullable()
                .WithColumn("CreatedDate").AsDateTime()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);


            Create.Table("Woocommerce").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                 .WithColumn("SiteURL").AsString(100).Nullable()
                  .WithColumn("ConsumerKey").AsString(100).Nullable()
               .WithColumn("consumerSecret").AsString(1000).Nullable()
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
            ;

            Insert.IntoTable("Woocommerce").Row(new
            {
                SiteURL = "https://example.com/",
                Sender = "Your Company Name",
                Subject = "Woocommerce",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            }) ;
        }
    }
}