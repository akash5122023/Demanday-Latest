using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220707110000)]
    public class DefaultDB_20220707_110000_MailInbox : AutoReversingMigration
    {
        public override void Up()
        {


            Create.Table("MailInboxDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
           .WithColumn("Subject").AsString(255).Nullable()
           .WithColumn("Phone").AsString(20).Nullable()
           .WithColumn("ToName").AsString(255).Nullable()
           .WithColumn("To").AsString(255).Nullable()
           .WithColumn("ToAddress").AsString(255).Nullable()
           .WithColumn("FromName").AsString(255).Nullable()
           .WithColumn("From").AsString(255).Nullable()
           .WithColumn("FromAddress").AsString(255).Nullable()
           .WithColumn("CreatedDate").AsDateTime()
           .WithColumn("Content").AsString(4000).Nullable()
           .WithColumn("Attachment").AsString(2000).Nullable();  

                Create.Table("MailInbox").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()                
                .WithColumn("AutoEmail").AsBoolean().Nullable().WithDefaultValue(false)              
                .WithColumn("Sender").AsString(250).Nullable()
                .WithColumn("Subject").AsString(250).Nullable()
                .WithColumn("EmailTemplate").AsString(2000).Nullable()
                .WithColumn("Attachment").AsString(1000).Nullable()   
            ;

            Insert.IntoTable("MailInbox").Row(new
            {
               
                Sender = "Your Company Name",
                Subject = "Woocommerce",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We will get back to soon.<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                
            }) ;
        }
    }
}