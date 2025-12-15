using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220415110000)]
    public class DefaultDB_20220415_110000_IVRconfig : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("IVRConfiguration")              
                .AddColumn("AutoEmail").AsBoolean().Nullable().WithDefaultValue(false)
                .AddColumn("AutoSMS").AsBoolean().Nullable().WithDefaultValue(false)
                .AddColumn("Sender").AsString(250).Nullable()
                .AddColumn("Subject").AsString(250).Nullable()
                .AddColumn("EmailTemplate").AsString(2000).Nullable()
                .AddColumn("Attachment").AsString(1000).Nullable()
                .AddColumn("SMSTemplate").AsString(1000).Nullable()
                 .AddColumn("TemplateID").AsString(20).Nullable()
                .AddColumn("Host").AsString(200).Nullable()
                .AddColumn("Port").AsInt32().Nullable()
                .AddColumn("SSL").AsBoolean().Nullable()
                .AddColumn("EmailId").AsString(200).Nullable()
                .AddColumn("EmailPassword").AsString(200).Nullable();

          

            Insert.IntoTable("IVRConfiguration").Row(new
            {
               
                Sender = "Your Company Name",
                Subject = "BizplusIVR",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });
        }
    }
}