using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201201124800)]
    public class DefaultDB_20201201_124800_QuotationMessage : AutoReversingMigration
    {

        public override void Up()
        {

            Create.Table("MessageMaster").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Message").AsString(5000).NotNullable();

            Alter.Table("Quotation")
                .AddColumn("MessageId").AsInt32().Nullable().ForeignKey("FK_QTMessageMaster_MessageMastersId", "dbo", "MessageMaster", "Id");

            Alter.Table("CompanyDetails")
                .AddColumn("QuotationContactIncluded").AsBoolean().Nullable().WithDefaultValue(true);

            Rename.Column("CompanyDetailsInQuotation").OnTable("CompanyDetails").To("CompanyDetails");
            Rename.Column("QuotationHeaderContent").OnTable("CompanyDetails").To("HeaderContent");
            Rename.Column("QuotationFooterContent").OnTable("CompanyDetails").To("FooterContent");
        }
    }
}