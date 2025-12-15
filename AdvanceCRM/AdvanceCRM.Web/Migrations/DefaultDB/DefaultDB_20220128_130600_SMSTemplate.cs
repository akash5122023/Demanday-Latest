
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
    [Migration(20220128130600)]
    public class DefaultDB_20220128_130600_SMSTemplate : Migration
    {

        public override void Up()
        {

            Alter.Table("EnquiryTemplate")
                .AddColumn("TemplateID").AsString(20).Nullable();

            Alter.Table("QuotationTemplate")
               .AddColumn("TemplateID").AsString(20).Nullable();

            Alter.Table("CMSTemplate")
              .AddColumn("SMSTemplateID").AsString(20).Nullable()
              .AddColumn("ClosedTemplateID").AsString(20).Nullable()
              .AddColumn("EmgineerTemplateID").AsString(20).Nullable();

            Alter.Table("InvoiceTemplate")
             .AddColumn("TemplateID").AsString(20).Nullable();

            Alter.Table("ChallanTemplate")
            .AddColumn("TemplateID").AsString(20).Nullable();

            Alter.Table("TeleCallingTemplate")
           .AddColumn("CustTemplateID").AsString(20).Nullable()
            .AddColumn("ExeTemplateID").AsString(20).Nullable()
             .AddColumn("CustRTemplateID").AsString(20).Nullable()
              .AddColumn("ExeRTemplateID").AsString(20).Nullable();


            Alter.Table("DailyWishesTemplate")
              .AddColumn("BirthTempID").AsString(20).Nullable()
              .AddColumn("MarriageTempID").AsString(20).Nullable()
              .AddColumn("DOFTempID").AsString(20).Nullable();

            Alter.Table("AMCTemplate")
         .AddColumn("SMSTempID").AsString(20).Nullable()
          .AddColumn("VisitTempID").AsString(20).Nullable();

            Alter.Table("AppointmentTemplate")
          .AddColumn("SMSTempID").AsString(20).Nullable()
           .AddColumn("MonTempID").AsString(20).Nullable()
            .AddColumn("TueTempID").AsString(20).Nullable()
            .AddColumn("WedTempID").AsString(20).Nullable()
            .AddColumn("ThurTempID").AsString(20).Nullable()
            .AddColumn("FriTempID").AsString(20).Nullable()
            .AddColumn("SatTempID").AsString(20).Nullable()
             .AddColumn("SunTempID").AsString(20).Nullable();


        }

        public override void Down()
        {
        }
    }
}