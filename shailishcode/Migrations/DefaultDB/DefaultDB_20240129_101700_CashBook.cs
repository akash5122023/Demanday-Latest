using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20240129101700)]
    public class DefaultDB_20240129_101700_CashBook : Migration
    {
        public override void Up()
        {
            Alter.Table("CashBook")                
                .AddColumn("ProjectId").AsInt32().Nullable().ForeignKey("FK_Cashbook_ProjectId","dbo","Project","Id")
                .AddColumn("EmployeeId").AsInt32().Nullable().ForeignKey("FK_Cashbook_EmployeeId", "dbo", "Employee", "Id");
        }

        public override void Down()
        {

        }
    }
}