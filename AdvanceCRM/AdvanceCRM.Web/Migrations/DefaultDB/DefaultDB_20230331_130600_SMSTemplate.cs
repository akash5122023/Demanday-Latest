
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
    [Migration(20230331130600)]
    public class DefaultDB_20230331_130600_SMSTemplate : Migration
    {
        public override void Up()
        {
            Alter.Table("Sales")
              .AddColumn("BillingAddress").AsBoolean().Nullable();

            Alter.Table("Invoice")
              .AddColumn("BillingAddress").AsBoolean().Nullable();          

            Create.Table("OtherTemplates")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("TicketSMSTemplate").AsString(int.MaxValue).Nullable()
                .WithColumn("TicketSMSTemplateID").AsString(100).Nullable()
                .WithColumn("TaskSMSTemplate").AsString(int.MaxValue).Nullable()
                .WithColumn("TaskSMSTemplateID").AsString(100).Nullable()
                .WithColumn("FeedbackSMSTemplate").AsString(int.MaxValue).Nullable()
                .WithColumn("FeedbackSMSTemplateID").AsString(100).Nullable()
                .WithColumn("FeedbackWATemplate").AsString(int.MaxValue).Nullable()
                .WithColumn("FeedbackSWATemplateID").AsString(100).Nullable()
                .WithColumn("TicketWATemplate").AsString(int.MaxValue).Nullable()
                .WithColumn("TicketWATemplateID").AsString(100).Nullable()
                .WithColumn("TaskWATemplate").AsString(int.MaxValue).Nullable()
                .WithColumn("TaskWATemplateID").AsString(100).Nullable()
                 .WithColumn("OTPSMSTemplate").AsString(int.MaxValue).Nullable()
                .WithColumn("OTPSMSTemplateID").AsString(100).Nullable()
                .WithColumn("OTPWATemplate").AsString(int.MaxValue).Nullable()
                .WithColumn("OTPWATemplateID").AsString(100).Nullable(); 

            //done reminder
            Alter.Table("EnquiryTemplate")
                .AddColumn("SMSReminder").AsString(int.MaxValue).Nullable()
                .AddColumn("SMSRTemplateId").AsString(100).Nullable()
               .AddColumn("WAReminder").AsString(int.MaxValue).Nullable()
                .AddColumn("WARTemplateId").AsString(100).Nullable()
                .AddColumn("WATemplateId").AsString(100).Nullable();
           
            //done reminder
            Alter.Table("QuotationTemplate")
              .AddColumn("SMSReminder").AsString(int.MaxValue).Nullable()
              .AddColumn("SMSRTemplateId").AsString(100).Nullable()
              .AddColumn("WAReminder").AsString(int.MaxValue).Nullable()
              .AddColumn("WARTemplateId").AsString(100).Nullable()
              .AddColumn("WATemplateId").AsString(100).Nullable();
                    

            Alter.Table("CMSTemplate")
                .AddColumn("SMSReminder").AsString(int.MaxValue).Nullable()
                .AddColumn("SMSRTemplateId").AsString(100).Nullable()
                .AddColumn("WAReminder").AsString(int.MaxValue).Nullable()
                .AddColumn("WARTemplateId").AsString(100).Nullable()
                .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
                .AddColumn("WATemplateId").AsString(100).Nullable()
                .AddColumn("WAClosedTemplate").AsString(int.MaxValue).Nullable()
                .AddColumn("WAClosedTemplateId").AsString(100).Nullable()
                .AddColumn("WAENGTemplate").AsString(int.MaxValue).Nullable()
                .AddColumn("WAENGTemplateId").AsString(100).Nullable();

            Alter.Table("AMCTemplate")
              .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
              .AddColumn("WATemplateId").AsString(100).Nullable()
              .AddColumn("WAVisitTemplate").AsString(int.MaxValue).Nullable()
              .AddColumn("WAVisitTemplateId").AsString(100).Nullable();

            //done reminder sales& invoice
            Alter.Table("InvoiceTemplate")
            .AddColumn("SMSReminder").AsString(int.MaxValue).Nullable()
            .AddColumn("SMSRTemplateId").AsString(100).Nullable()
           .AddColumn("WAReminder").AsString(int.MaxValue).Nullable()
            .AddColumn("WARTemplateId").AsString(100).Nullable()
            .AddColumn("WATemplateId").AsString(100).Nullable();

            Alter.Table("ChallanTemplate")           
          .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()          
           .AddColumn("WATemplateId").AsString(100).Nullable();

            Alter.Table("TeleCallingTemplate")
            .AddColumn("WACustomTemplate").AsString(int.MaxValue).Nullable()
            .AddColumn("WACustomTemplateId").AsString(100).Nullable()
            .AddColumn("WAExeTemplate").AsString(int.MaxValue).Nullable()
            .AddColumn("WAExeTemplateId").AsString(100).Nullable()
             .AddColumn("RWACustomTemplate").AsString(int.MaxValue).Nullable()
            .AddColumn("RWACustomTemplateId").AsString(100).Nullable()
            .AddColumn("RWAExeTemplate").AsString(int.MaxValue).Nullable()
            .AddColumn("RWAExeTemplateId").AsString(100).Nullable()
            ;

            Alter.Table("AppointmentTemplate")
           .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
           .AddColumn("WATemplateId").AsString(100).Nullable()
           .AddColumn("WAMonTemplate").AsString(int.MaxValue).Nullable()
           .AddColumn("WAMonTemplateId").AsString(100).Nullable()
           .AddColumn("WATueTemplate").AsString(int.MaxValue).Nullable()
           .AddColumn("WATueTemplateId").AsString(100).Nullable()
           .AddColumn("WAWedTemplate").AsString(int.MaxValue).Nullable()
           .AddColumn("WAWebTemplateId").AsString(100).Nullable()
           .AddColumn("WAThurTemplate").AsString(int.MaxValue).Nullable()
           .AddColumn("WAThurTemplateId").AsString(100).Nullable()
           .AddColumn("WAFriTemplate").AsString(int.MaxValue).Nullable()
           .AddColumn("WAFriTemplateId").AsString(100).Nullable()
           .AddColumn("WASatTemplate").AsString(int.MaxValue).Nullable()
           .AddColumn("WASatTemplateId").AsString(100).Nullable()
           .AddColumn("WASunTemplate").AsString(int.MaxValue).Nullable()
           .AddColumn("WASunTemplateId").AsString(100).Nullable();

           Alter.Table("DailyWishesTemplate")
          .AddColumn("WABirTemplate").AsString(int.MaxValue).Nullable()
          .AddColumn("WABirTemplateId").AsString(100).Nullable()
          .AddColumn("WAMarTemplate").AsString(int.MaxValue).Nullable()
          .AddColumn("WAMarTemplateId").AsString(100).Nullable()
          .AddColumn("WAAnnTemplate").AsString(int.MaxValue).Nullable()
          .AddColumn("WAAnnTemplateId").AsString(100).Nullable();


           Alter.Table("WebsiteEnquiry")
          .AddColumn("SMSTemplateId").AsString(100).Nullable()
          .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
          .AddColumn("WATemplateId").AsString(100).Nullable();

            Alter.Table("IVRConfiguration")
          .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
        .AddColumn("WATemplateId").AsString(100).Nullable();

            Alter.Table("IndiaMART")
         .AddColumn("SMSTemplateId").AsString(100).Nullable()
         .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
        .AddColumn("WATemplateId").AsString(100).Nullable();

         Alter.Table("JustDial")
         .AddColumn("SMSTemplateId").AsString(100).Nullable()
         .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
         .AddColumn("WATemplateId").AsString(100).Nullable();

          Alter.Table("TradeIndiaConfiguration")
         .AddColumn("SMSTemplateId").AsString(100).Nullable()
         .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
         .AddColumn("WATemplateId").AsString(100).Nullable();

           Alter.Table("Facebook")
          .AddColumn("SMSTemplateId").AsString(100).Nullable()
          .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
          .AddColumn("WATemplateId").AsString(100).Nullable();

          Alter.Table("InteraktConfig")
       // .AddColumn("SMSTemplateId").AsString(100).Nullable()
        .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
        .AddColumn("WATemplateId").AsString(100).Nullable();

            Alter.Table("Woocommerce")  
        // .AddColumn("SMSTemplateId").AsString(100).Nullable()
        .AddColumn("WATemplate").AsString(int.MaxValue).Nullable()
        .AddColumn("WATemplateId").AsString(100).Nullable();

          //  Alter.Table("RazorPay")
          //.AddColumn("SMSTemplateId").AsString(100).Nullable();

          //  Alter.Table("Instamojo")
          //.AddColumn("SMSTemplateId").AsString(100).Nullable();

        }

        public override void Down()
        {
        }
    }
}