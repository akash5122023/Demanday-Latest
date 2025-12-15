using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210720110000)]
    public class DefaultDB_20210720_110000_Employee : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Employee").Column("EmpCode").Exists())
            {
               Delete.Column("EmpCode").FromTable("Employee");
            }


            Alter.Table("Employee")
                .AddColumn("EmpCode").AsString(20).Nullable();
        }

        public override void Down()
        {

        }
    }
}