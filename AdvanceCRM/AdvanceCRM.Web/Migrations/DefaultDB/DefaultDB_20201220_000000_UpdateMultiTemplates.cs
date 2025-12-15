
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
    [Migration(20201220000001)]
    public class DefaultDB_20201220_000000_UpdateMultiTemplates : Migration
    {

        public override void Up()
        {

            if (Schema.Table("CompanyDetails").Column("WebsiteEnquiryUser").Exists())
            {
                Delete.ForeignKey("FK_WEUserId_UserId").OnTable("CompanyDetails");
                Delete.Column("WebsiteEnquiryUser").FromTable("CompanyDetails");
            }

            if (!Schema.Table("AppointmentTemplate").Column("CompanyId").Exists())
                Alter.Table("AppointmentTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_AppointmentTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);
            
            if (!Schema.Table("AMCTemplate").Column("CompanyId").Exists())
                Alter.Table("AMCTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_AMCTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            if (!Schema.Table("ChallanTemplate").Column("CompanyId").Exists())
                Alter.Table("ChallanTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_ChallanTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            if (!Schema.Table("CMSTemplate").Column("CompanyId").Exists())
                Alter.Table("CMSTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_CMSTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            if (!Schema.Table("EnquiryTemplate").Column("CompanyId").Exists())
                Alter.Table("EnquiryTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_EnquiryTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            if (!Schema.Table("InvoiceTemplate").Column("CompanyId").Exists())
                Alter.Table("InvoiceTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_InvoiceTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            if (!Schema.Table("PurchaseOrderTemplate").Column("CompanyId").Exists())
                Alter.Table("PurchaseOrderTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_PurchaseOrderTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            if (!Schema.Table("QuotationTemplate").Column("CompanyId").Exists())
                Alter.Table("QuotationTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_QuotationTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            if (!Schema.Table("TeleCallingTemplate").Column("CompanyId").Exists())
                Alter.Table("TeleCallingTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_TeleCallingTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            if (!Schema.Table("DailyWishesTemplate").Column("CompanyId").Exists())
                Alter.Table("DailyWishesTemplate").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_DailyWishesTemplate_CompanyId", "dbo", "CompanyDetails", "Id").OnDeleteOrUpdate(System.Data.Rule.Cascade);

        }

        public override void Down()
        {
        }
    }
}