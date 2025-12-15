using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210429110000)]
    public class DefaultDB_20210429_110000_Multiassign : Migration
    {
        public override void Up()
        {
            Create.Table("MultiRepEnquiry")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_MultiRepEnquiry_UserId", "dbo", "Users", "UserId")
               .WithColumn("EnquiryId").AsInt32().NotNullable().ForeignKey("FK_MultiRepEnquiry_EnquiryId", "dbo", "Enquiry", "Id")
               ;
            Create.Table("MultiRepQuotation")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_MultiRepQuotation_UserId", "dbo", "Users", "UserId")
               .WithColumn("QuotationId").AsInt32().NotNullable().ForeignKey("FK_MultiRepQuotation_QuotationId", "dbo", "Quotation", "Id")
               ;

        }

        public override void Down()
        {

        }
    }
}