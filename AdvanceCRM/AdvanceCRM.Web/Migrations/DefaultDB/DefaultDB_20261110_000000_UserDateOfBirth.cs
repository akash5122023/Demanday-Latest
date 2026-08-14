using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    /// <summary>
    /// Date of Birth on a user, so the Attendance calendar can tell the team whose birthday it is
    /// today. Nullable: it is not known for anyone until someone fills it in.
    /// </summary>
    [Migration(20261110000000)]
    public class DefaultDB_20261110_000000_UserDateOfBirth : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("DateOfBirth").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Column("DateOfBirth").FromTable("Users");
        }
    }
}
