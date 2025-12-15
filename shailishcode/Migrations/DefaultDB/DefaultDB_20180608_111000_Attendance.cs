using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180608111000)]
    public class DefaultDB_20180608_111000_Attendance : Migration
    {
        public override void Up()
        {
            Create.Table("Attendance")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsInt32().NotNullable().ForeignKey("FK_AttendanceName_UserId", "dbo", "Users", "UserId")
                .WithColumn("DateNTime").AsDateTime().NotNullable()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("Coordinates").AsString(200).NotNullable()
                .WithColumn("Location").AsString(2000).NotNullable()
                .WithColumn("ApprovedBy").AsInt32().Nullable().ForeignKey("FK_AttendanceApproved_UserId", "dbo", "Users", "UserId")
                ;
        }

        public override void Down()
        {

        }
    }
}