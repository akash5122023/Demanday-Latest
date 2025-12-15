using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210316131000)]
    public class DefaultDB_20210316_131000_AttendanceA : Migration
    {
        public override void Up()
        {
            Alter.Table("Attendance")
               .AddColumn("PunchIn").AsDateTime().Nullable()
                .AddColumn("PunchOut").AsDateTime().Nullable()
                .AddColumn("Distance").AsDouble().Nullable();
        }

        public override void Down()
        {

        }
    }
}