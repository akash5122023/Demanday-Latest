using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210719183100)]
    public class DefaultDB_20210719_183100_CMS : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("ServicePerson").AsBoolean().Nullable().WithDefaultValue(true);
               

            Alter.Table("CMS")
               .AddColumn("PurchaseDate").AsDateTime().Nullable()
               .AddColumn("InvoiceNo").AsString(20).Nullable()
                .AddColumn("EmployeeId").AsInt32().Nullable().ForeignKey("FK_CEmployee_EmployeeId", "dbo", "Employee", "Id");


        }

        public override void Down()
        {

        }
    }
}