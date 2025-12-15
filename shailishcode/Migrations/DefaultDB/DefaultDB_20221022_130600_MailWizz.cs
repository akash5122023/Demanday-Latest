
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
    [Migration(20221022130600)]
    public class DefaultDB_20221022_130600_MailWizz : Migration
    {

        public override void Up()
        {
            Create.Table("BizMailConfig")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("apiurl").AsString(200).Nullable()
               .WithColumn("apikey").AsString(200).Nullable();

            Create.Table("BMList")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ListId").AsString(200).Nullable()
                .WithColumn("CompanyName").AsString(200).Nullable()
               .WithColumn("Name").AsString(200).Nullable()
               .WithColumn("City").AsString(200).Nullable()
               .WithColumn("DisplayName").AsString(200).Nullable()
               .WithColumn("Description").AsString(200).Nullable()
               .WithColumn("From").AsString(200).Nullable()
               .WithColumn("ReplyTo").AsString(200).Nullable()
               ;
            Create.Table("BMSubscribers")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("SubscriberId").AsString(200).Nullable()
               .WithColumn("Email").AsString(200).Nullable()
              .WithColumn("FirstName").AsString(200).Nullable()
              .WithColumn("LastName").AsString(200).Nullable()
              .WithColumn("Status").AsString(200).Nullable()
              .WithColumn("Source").AsString(200).Nullable()
              .WithColumn("IPAddress").AsString(200).Nullable()
              .WithColumn("DateAdded").AsString(200).Nullable()
              .WithColumn("ListName").AsString(200).Nullable()
              .WithColumn("ListID").AsString(20).Nullable()
              .WithColumn("Phone").AsString(20).Nullable()
              .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);

            Create.Table("BMCampaign")
             .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("CampaignId").AsString(200).Nullable()
              .WithColumn("Name").AsString(200).Nullable()
             .WithColumn("Status").AsString(200).Nullable();

            Create.Table("BizMailEnquiry").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Rule").AsInt32().NotNullable()
              .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_EBMList_BMListId", "dbo", "BMList", "Id")
             .WithColumn("Status").AsBoolean().WithDefaultValue(0)
              .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_ECompany_CompanyId", "dbo", "CompanyDetails", "Id")
             .WithColumn("Description").AsString(2000).Nullable()
             .WithColumn("EnquiryStatus").AsInt32().Nullable()
                 .WithColumn("Type").AsInt32().Nullable()
                  .WithColumn("SourceId").AsInt32().Nullable().ForeignKey("FK_BEMSource_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().Nullable().ForeignKey("FK_BEMStage_StageId", "dbo", "Stage", "Id")
                 .WithColumn("ClosingType").AsInt32().Nullable(); 

            Create.Table("BizMailQuotation").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Rule").AsInt32().NotNullable()
               .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_QBMList_MWListId", "dbo", "BMList", "Id")
               .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_QCompany_CompanyId", "dbo", "CompanyDetails", "Id")
              .WithColumn("Status").AsBoolean().WithDefaultValue(0)
               .WithColumn("Description").AsString(2000).Nullable()
               .WithColumn("QuotationStatus").AsInt32().Nullable()
                 .WithColumn("Type").AsInt32().Nullable()
                  .WithColumn("SourceId").AsInt32().Nullable().ForeignKey("FK_BQMSource_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().Nullable().ForeignKey("FK_BQMStage_StageId", "dbo", "Stage", "Id")
                 .WithColumn("ClosingType").AsInt32().Nullable();

            Create.Table("BizMailFacebook").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Rule").AsInt32().NotNullable()
              .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_FBMList_MWListId", "dbo", "BMList", "Id")
             .WithColumn("Status").AsBoolean().WithDefaultValue(0)
              .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_FCompany_CompanyId", "dbo", "CompanyDetails", "Id")
              .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailWooCom").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Rule").AsInt32().NotNullable()
               .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_WBMList_BMListId", "dbo", "BMList", "Id")
              .WithColumn("Status").AsBoolean().WithDefaultValue(0)
               .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_WCompany_CompanyId", "dbo", "CompanyDetails", "Id")
               .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailTask").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Rule").AsInt32().NotNullable()
              .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_TBMList_BMListId", "dbo", "BMList", "Id")
             .WithColumn("Status").AsBoolean().WithDefaultValue(0)
              .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_TCompany_CompanyId", "dbo", "CompanyDetails", "Id")
              .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailIdiaMart").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Rule").AsInt32().NotNullable()
               .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_IBMList_BMListId", "dbo", "BMList", "Id")
              .WithColumn("Status").AsBoolean().WithDefaultValue(0)
               .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_ICompany_CompanyId", "dbo", "CompanyDetails", "Id")
               .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailTradeIdia").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Rule").AsInt32().NotNullable()
               .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_TIBMList_BMListId", "dbo", "BMList", "Id")
              .WithColumn("Status").AsBoolean().WithDefaultValue(0)
               .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_TICompany_CompanyId", "dbo", "CompanyDetails", "Id")
               .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailJustDial").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Rule").AsInt32().NotNullable()
               .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_JBMList_BMListId", "dbo", "BMList", "Id")
              .WithColumn("Status").AsBoolean().WithDefaultValue(0)
               .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_JCompany_CompanyId", "dbo", "CompanyDetails", "Id")
               .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailIVR").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Rule").AsInt32().NotNullable()
               .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_IVRBMList_BMListId", "dbo", "BMList", "Id")
              .WithColumn("Status").AsBoolean().WithDefaultValue(0)
               .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_IVRCompany_CompanyId", "dbo", "CompanyDetails", "Id")
               .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailIstamojo").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Rule").AsInt32().NotNullable()
              .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_IMBMList_BMListId", "dbo", "BMList", "Id")
             .WithColumn("Status").AsBoolean().WithDefaultValue(0)
              .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_IMCompany_CompanyId", "dbo", "CompanyDetails", "Id")
              .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailVisit").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Rule").AsInt32().NotNullable()
              .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_VBMList_BMListId", "dbo", "BMList", "Id")
             .WithColumn("Status").AsBoolean().WithDefaultValue(0)
              .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_VCompany_CompanyId", "dbo", "CompanyDetails", "Id")
              .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailWeb").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Rule").AsInt32().NotNullable()
              .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_WebBMList_BMListId", "dbo", "BMList", "Id")
             .WithColumn("Status").AsBoolean().WithDefaultValue(0)
              .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_WebCompany_CompanyId", "dbo", "CompanyDetails", "Id")
              .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailContact").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Rule").AsInt32().NotNullable()
              .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_CBMList_BMListId", "dbo", "BMList", "Id")
             .WithColumn("Status").AsBoolean().WithDefaultValue(0)
              .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_CCompany_CompanyId", "dbo", "CompanyDetails", "Id")
              .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BizMailCMS").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Rule").AsInt32().NotNullable()
              .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_CMSBMList_BMListId", "dbo", "BMList", "Id")
             .WithColumn("Status").AsBoolean().WithDefaultValue(0)
              .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_CMSCompany_CompanyId", "dbo", "CompanyDetails", "Id")
              .WithColumn("Description").AsString(2000).Nullable();

            Create.Table("BMTemplate")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(200).Nullable()
                .WithColumn("Content").AsString(8000).Nullable()
               .WithColumn("InlineCSS").AsInt32().Nullable();

            Create.Table("CampaignBM")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Campaignuid").AsString(200).Nullable()
               .WithColumn("Name").AsString(200).Nullable()
               .WithColumn("Type").AsInt32().Nullable()
              .WithColumn("FromName").AsString(200).Nullable()
              .WithColumn("FromEmail").AsString(200).Nullable()
              .WithColumn("Subject").AsString(200).Nullable()
              .WithColumn("ReplyTo").AsString(200).Nullable()
               .WithColumn("BMListId").AsInt32().NotNullable().ForeignKey("FK_CABMList_BMListId", "dbo", "BMList", "Id")
               .WithColumn("SendAt").AsDateTime().Nullable()
                .WithColumn("BMTemplateId").AsInt32().NotNullable().ForeignKey("FK_BMTemplate_BMTemplateId", "dbo", "BMTemplate", "Id")
                .WithColumn("InlineCSS").AsInt32().Nullable()
                .WithColumn("AutoPlaneTest").AsInt32().Nullable();
        }

        public override void Down()
        {
        }
    }
}