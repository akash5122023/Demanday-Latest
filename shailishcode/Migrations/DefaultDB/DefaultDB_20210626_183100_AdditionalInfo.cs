using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210626183100)]
    public class DefaultDB_20210626_183100_AdditionalInfo : Migration
    {
        public override void Up()
        {
            Create.Table("AdditionalInfo")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().Nullable()
                .WithColumn("AdditionalInfo").AsString(2000).NotNullable();

            Create.Table("EnquiryMultiInfo")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("AdditionalInfoId").AsInt32().NotNullable().ForeignKey("FK_EnquiryMultiInfo_AdditionalInfoId", "dbo", "AdditionalInfo", "Id")
              .WithColumn("EnquiryId").AsInt32().NotNullable().ForeignKey("FK_EnquiryMultiInfo_EnquiryId", "dbo", "Enquiry", "Id");

            Create.Table("ContactsMultiInfo")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("AdditionalInfoId").AsInt32().NotNullable().ForeignKey("FK_ContactsMultiInfo_AdditionalInfoId", "dbo", "AdditionalInfo", "Id")
              .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_ConytactsMultiInfo_ContactsId", "dbo", "Contacts", "Id");

            Create.Table("QuotationMultiInfo")
             .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
             .WithColumn("AdditionalInfoId").AsInt32().NotNullable().ForeignKey("FK_QuotationMultiInfo_AdditionalInfoId", "dbo", "AdditionalInfo", "Id")
             .WithColumn("QuotationId").AsInt32().NotNullable().ForeignKey("FK_QuotationMultiInfo_QuotationId", "dbo", "Quotation", "Id");

            Alter.Table("Contacts")
                .AddColumn("AdditionalInfo2").AsString(2000).Nullable()
                .AddColumn("DateCreated").AsDateTime().Nullable();
                Alter.Table("Enquiry")
               .AddColumn("AdditionalInfo2").AsString(2000).Nullable();
            Alter.Table("Quotation")
               .AddColumn("AdditionalInfo2").AsString(2000).Nullable();

        }

        public override void Down()
        {

        }
    }
}