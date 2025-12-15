using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210309200500)]
    public class DefaultDB_20210309_200500_Product : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Contacts").AddColumn("ConsentForCalling").AsBoolean().NotNullable().WithDefaultValue(false);

            Create.Table("ModelCategory")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("ModelCategory").AsString(200).NotNullable();

            Create.Table("ModelSegment")
             .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
             .WithColumn("ModelSegment").AsString(200).NotNullable();

            Create.Table("ModelName")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ModelName").AsString(200).NotNullable()
            .WithColumn("ModelSegmentId").AsInt32().ForeignKey("FK_ModelSegment_ModelSegmentId", "dbo", "ModelSegment", "Id");

            Create.Table("ModelCode")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ModelCode").AsString(200).NotNullable()
                .WithColumn("ModelNameID").AsInt32().ForeignKey("FK_ModelName_ModelNameId", "dbo", "ModelName", "Id")
             .WithColumn("ModelSegmentId").AsInt32().ForeignKey("FK_CModelSegment_ModelSegmentId", "dbo", "ModelSegment", "Id");

            Create.Table("ModelVarient")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("ModelVarient").AsString(200).NotNullable()
               .WithColumn("ModelNameID").AsInt32().ForeignKey("FK_VModelName_ModelNameId", "dbo", "ModelName", "Id")
            .WithColumn("ModelSegmentId").AsInt32().ForeignKey("FK_VModelSegment_ModelSegmentId", "dbo", "ModelSegment", "Id")
             .WithColumn("ModelCodeId").AsInt32().ForeignKey("FK_ModelCode_ModelCodeId", "dbo", "ModelCode", "Id");

           

            Create.Table("ModelColor")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("ModelColor").AsString(200).NotNullable();

            Create.Table("ProductType")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("ProductType").AsString(200).NotNullable();

            Create.Table("AutoProducts")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("ProductTypeId").AsInt32().ForeignKey("FK_ProductType_ProductTypeId", "dbo", "ProductType", "Id")
               .WithColumn("ModelSegmentId").AsInt32().ForeignKey("FK_PModelSegment_ModelSegmentId", "dbo", "ModelSegment", "Id")
              .WithColumn("ModelNameID").AsInt32().ForeignKey("FK_PModelName_ModelNameId", "dbo", "ModelName", "Id")
             .WithColumn("ModelCodeId").AsInt32().ForeignKey("FK_PModelCode_ModelCodeId", "dbo", "ModelCode", "Id")
               .WithColumn("ModelVarientId").AsInt32().ForeignKey("FK_PModelVarient_ModelVarientId", "dbo", "ModelVarient", "Id")
                .WithColumn("ModelColorId").AsInt32().ForeignKey("FK_PModelColor_ModelColorId", "dbo", "ModelColor", "Id")
                .WithColumn("HSNCode").AsInt32().NotNullable()
                .WithColumn("SerialNo").AsString(200).NotNullable()
                .WithColumn("ExShowroomPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("InsuranceAmount").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("RegistrationAmount").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("RoadTax").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("OnRoadPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("OtherTaxes").AsDouble().Nullable().WithDefaultValue(0)
               ;

        }
    }
}