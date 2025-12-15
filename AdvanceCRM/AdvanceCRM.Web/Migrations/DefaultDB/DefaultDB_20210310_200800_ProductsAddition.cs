using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210310200800)]
    public class DefaultDB_20210310_200800_ProductsAddition : AutoReversingMigration
    {
        public override void Up()
        {
           
           
            Alter.Table("Products")             
              .AddColumn("ProductTypeId").AsInt32().ForeignKey("FK_PProductType_ProductTypeId", "dbo", "ProductType", "Id")
               .AddColumn("ModelSegmentId").AsInt32().ForeignKey("FK_PPModelSegment_ModelSegmentId", "dbo", "ModelSegment", "Id")
              .AddColumn("ModelNameID").AsInt32().ForeignKey("FK_PPModelName_ModelNameId", "dbo", "ModelName", "Id")
             .AddColumn("ModelCodeId").AsInt32().ForeignKey("FK_PPModelCode_ModelCodeId", "dbo", "ModelCode", "Id")
               .AddColumn("ModelVarientId").AsInt32().ForeignKey("FK_PPModelVarient_ModelVarientId", "dbo", "ModelVarient", "Id")
                .AddColumn("ModelColorId").AsInt32().ForeignKey("FK_PPModelColor_ModelColorId", "dbo", "ModelColor", "Id")               
                .AddColumn("SerialNo").AsString(200).NotNullable()
                .AddColumn("ExShowroomPrice").AsDouble().Nullable().WithDefaultValue(0)
                .AddColumn("InsuranceAmount").AsDouble().Nullable().WithDefaultValue(0)
                .AddColumn("RegistrationAmount").AsDouble().Nullable().WithDefaultValue(0)
                .AddColumn("RoadTax").AsDouble().Nullable().WithDefaultValue(0)
                .AddColumn("OnRoadPrice").AsDouble().Nullable().WithDefaultValue(0)
                .AddColumn("OtherTaxes").AsDouble().Nullable().WithDefaultValue(0)

               ;


        }
    }
}