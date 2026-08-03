using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260803130000)]
    public class DefaultDB_20260803_130000_UserDepartmentGenderDOJ : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("Gender").AsInt32().Nullable()
                .AddColumn("DateOfJoining").AsDateTime().Nullable();
        }

        public override void Down()
        {
        }
    }
}
