using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    /// <summary>
    /// Backing tables for the Email Verification tool:
    ///  - EmailVerificationResult: a shared cache of every email that has been verified, so any
    ///    user can see a previously-known Valid/Invalid result without spending a fresh check.
    ///  - EmailVerificationQuota: the per-user search limit (AllowedCount) and how much of it has
    ///    been consumed (UsedCount). AllowedCount is set by an admin.
    /// </summary>
    [Migration(20261101000000)]
    public class DefaultDB_20261101_000000_EmailVerification : Migration
    {
        public override void Up()
        {
            Create.Table("EmailVerificationResult")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Email").AsString(320).NotNullable()
                .WithColumn("Status").AsString(50).Nullable()
                .WithColumn("SubStatus").AsString(100).Nullable()
                .WithColumn("Message").AsString(500).Nullable()
                .WithColumn("VerifiedByUserId").AsInt32().Nullable()
                    .ForeignKey("FK_EmailVerificationResult_VerifiedByUserId_Users_UserId", "dbo", "Users", "UserId")
                .WithColumn("VerifiedDate").AsDateTime().Nullable();

            // One cached result per email. Search / verify both look the email up by this key.
            Execute.Sql(
                "CREATE UNIQUE INDEX [UX_EmailVerificationResult_Email] " +
                "ON [dbo].[EmailVerificationResult] ([Email])");

            Create.Table("EmailVerificationQuota")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                    .ForeignKey("FK_EmailVerificationQuota_UserId_Users_UserId", "dbo", "Users", "UserId")
                .WithColumn("AllowedCount").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("UsedCount").AsInt32().NotNullable().WithDefaultValue(0);

            // One quota row per user.
            Execute.Sql(
                "CREATE UNIQUE INDEX [UX_EmailVerificationQuota_UserId] " +
                "ON [dbo].[EmailVerificationQuota] ([UserId])");
        }

        public override void Down()
        {
            Delete.Table("EmailVerificationQuota");
            Delete.Table("EmailVerificationResult");
        }
    }
}
