using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200706014513)]
    public class DefaultDB_20200706_014513_AdditionalCharges : AutoReversingMigration
    {

        public override void Up()
        {
            Create.Table("AdditionalCharges").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(5000).NotNullable()
                .WithColumn("Percentage").AsDouble().NotNullable().WithDefaultValue(0);

            Create.Table("AdditionalConcession").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(5000).NotNullable()
                .WithColumn("Percentage").AsDouble().NotNullable().WithDefaultValue(0);

            Create.Table("QuotationCharges").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ChargesId").AsInt32().NotNullable().ForeignKey("FK_QTAdditionalCharges_AdditionalChargessId", "dbo", "AdditionalCharges", "Id")
                .WithColumn("QuotationId").AsInt32().NotNullable().ForeignKey("FK_QTCharges_QuotationId", "dbo", "Quotation", "Id")
                ;
            Create.Table("InvoiceCharges").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ChargesId").AsInt32().NotNullable().ForeignKey("FK_INAdditionalCharges_AdditionalChargessId", "dbo", "AdditionalCharges", "Id")
                .WithColumn("InvoiceId").AsInt32().NotNullable().ForeignKey("FK_INCharges_InvoiceId", "dbo", "Invoice", "Id")
                ;
            Create.Table("SalesCharges").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ChargesId").AsInt32().NotNullable().ForeignKey("FK_SAAdditionalCharges_AdditionalChargessId", "dbo", "AdditionalCharges", "Id")
                .WithColumn("SalesId").AsInt32().NotNullable().ForeignKey("FK_SACharges_SalesId", "dbo", "Sales", "Id")
                ;

            Create.Table("QuotationConcession").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ConcessionId").AsInt32().NotNullable().ForeignKey("FK_QTAdditionalConcession_AdditionalConcessionsId", "dbo", "AdditionalConcession", "Id")
                .WithColumn("QuotationId").AsInt32().NotNullable().ForeignKey("FK_QTConcession_QuotationId", "dbo", "Quotation", "Id")
                ;
            Create.Table("InvoiceConcession").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ConcessionId").AsInt32().NotNullable().ForeignKey("FK_INAdditionalConcession_AdditionalConcessionsId", "dbo", "AdditionalConcession", "Id")
                .WithColumn("InvoiceId").AsInt32().NotNullable().ForeignKey("FK_INConcession_InvoiceId", "dbo", "Invoice", "Id")
                ;
            Create.Table("SalesConcession").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ConcessionId").AsInt32().NotNullable().ForeignKey("FK_SAAdditionalConcession_AdditionalConcessionsId", "dbo", "AdditionalConcession", "Id")
                .WithColumn("SalesId").AsInt32().NotNullable().ForeignKey("FK_SAConcession_SalesId", "dbo", "Sales", "Id")
                ;

            Alter.Table("CompanyDetails").AddColumn("EnableAdditionalCharges").AsBoolean().WithDefaultValue(false).Nullable()
                .AddColumn("EnableAdditionalConcessions").AsBoolean().WithDefaultValue(false).Nullable();
        }
    }
}