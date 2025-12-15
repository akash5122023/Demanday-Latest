using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210426183000)]
    public class DefaultDB_20210426_183000_ComAddtional : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("EnquirySuffix").AsString(20).WithDefaultValue("").Nullable()
                .AddColumn("EnquiryPrefix").AsString(20).WithDefaultValue("").Nullable()
                .AddColumn("CountryMandatory").AsBoolean().Nullable()
                .AddColumn("PincodeMandatory").AsBoolean().Nullable()
                .AddColumn("CityMandatory").AsBoolean().Nullable();

            Alter.Table("Enquiry")          
           .AddColumn("EnquiryN").AsString(100).NotNullable().WithDefaultValue(0);

            Alter.Table("Quotation")
          .AddColumn("QuotationN").AsString(100).NotNullable().WithDefaultValue(0);


        }

        public override void Down()
        {

        }
    }
}