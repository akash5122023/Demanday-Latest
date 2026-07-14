using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // Runtime settings for the Email Verification tool (ZeroBounce API key + default quota),
    // so an admin can add / change / remove the API key from the UI without touching
    // appsettings.json on the server. A single row holds the current values.
    [Migration(20261102000000)]
    public class DefaultDB_20261102_000000_EmailVerificationSettings : Migration
    {
        public override void Up()
        {
            Create.Table("EmailVerificationSettings")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("ApiKey").AsString(200).Nullable()
                .WithColumn("DefaultQuota").AsInt32().Nullable()
                .WithColumn("UpdatedByUserId").AsInt32().Nullable()
                .WithColumn("UpdatedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("EmailVerificationSettings");
        }
    }
}
