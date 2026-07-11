using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20261020000000)]
    public class DefaultDB_20261020_000000_EBBCheck : Migration
    {
        public override void Up()
        {
            Create.Table("EBBCheck")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(200).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("Status").AsInt32().Nullable()
                .WithColumn("TimeStamp").AsDateTime().Nullable()
                .WithColumn("UserId").AsInt32().Nullable().ForeignKey("FK_EBBCheck_UserId_Users_UserId", "dbo", "Users", "UserId")
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_EBBCheck_OwnerId_Users_UserId", "dbo", "Users", "UserId");
        }

        public override void Down()
        {
            Delete.Table("EBBCheck");
        }
    }
}
