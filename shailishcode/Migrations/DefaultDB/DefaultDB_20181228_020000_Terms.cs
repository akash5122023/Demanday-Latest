using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20181228020000)]
    public class DefaultDB_20181228_020000_Terms : Migration
    {
        public override void Up()
        {

            Create.Table("QuotationTerms").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("TermsId").AsInt32().NotNullable().ForeignKey("FK_QTQuotationTermsMaster_QuotationTermsMastersId", "dbo", "QuotationTermsMaster", "Id")
                .WithColumn("QuotationId").AsInt32().NotNullable().ForeignKey("FK_QTQuotation_QuotationId", "dbo", "Quotation", "Id")
                ;
            Create.Table("InvoiceTerms").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("TermsId").AsInt32().NotNullable().ForeignKey("FK_INQuotationTermsMaster_QuotationTermsMastersId", "dbo", "QuotationTermsMaster", "Id")
                .WithColumn("InvoiceId").AsInt32().NotNullable().ForeignKey("FK_INInvoice_InvoiceId", "dbo", "Invoice", "Id")
                ;
            Create.Table("AMCTerms").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("TermsId").AsInt32().NotNullable().ForeignKey("FK_AMCQuotationTermsMaster_QuotationTermsMastersId", "dbo", "QuotationTermsMaster", "Id")
                .WithColumn("AMCId").AsInt32().NotNullable().ForeignKey("FK_AMC_AMCId", "dbo", "AMC", "Id")
                ;
            Create.Table("SalesTerms").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("TermsId").AsInt32().NotNullable().ForeignKey("FK_SAQuotationTermsMaster_QuotationTermsMastersId", "dbo", "QuotationTermsMaster", "Id")
                .WithColumn("SalesId").AsInt32().NotNullable().ForeignKey("FK_SASales_SalesId", "dbo", "Sales", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}