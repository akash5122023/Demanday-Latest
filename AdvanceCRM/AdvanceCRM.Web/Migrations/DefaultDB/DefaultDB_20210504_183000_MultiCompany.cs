using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210504183000)]
    public class DefaultDB_20210504_183000_MultiCompany : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("Enquiry").Column("CompanyId").Exists())
                Alter.Table("Enquiry").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_ECompanyDetails_CompanyId", "dbo", "CompanyDetails", "Id");

            if (!Schema.Table("Quotation").Column("CompanyId").Exists())
                Alter.Table("Quotation").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_QCompanyDetails_CompanyId", "dbo", "CompanyDetails", "Id");

            if (!Schema.Table("Invoice").Column("CompanyId").Exists())
                Alter.Table("Invoice").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_ICompanyDetails_CompanyId", "dbo", "CompanyDetails", "Id");

            if (!Schema.Table("Sales").Column("CompanyId").Exists())
                Alter.Table("Sales").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_SCompanyDetails_CompanyId", "dbo", "CompanyDetails", "Id");


        }

        public override void Down()
        {

        }
    }
}