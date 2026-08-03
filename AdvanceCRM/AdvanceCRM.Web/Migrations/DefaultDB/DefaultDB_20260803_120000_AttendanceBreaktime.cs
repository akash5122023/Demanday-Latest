using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260803120000)]
    public class DefaultDB_20260803_120000_AttendanceBreaktime : Migration
    {
        public override void Up()
        {
            Alter.Table("Attendance")
                .AddColumn("BreakStart").AsDateTime().Nullable()
                .AddColumn("BreakEnd").AsDateTime().Nullable()
                .AddColumn("BreakMinutes").AsInt32().Nullable();
        }

        public override void Down()
        {
        }
    }
}
