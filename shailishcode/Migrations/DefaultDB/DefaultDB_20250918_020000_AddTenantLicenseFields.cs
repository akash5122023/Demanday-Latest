using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250918020000)]
    public class DefaultDB_20250918_020000_AddTenantLicenseFields : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Tenants")
                .AddColumn("Modules").AsString(1000).Nullable()
                .AddColumn("LicenseStartDate").AsDateTime().Nullable()
                .AddColumn("LicenseEndDate").AsDateTime().Nullable();
        }
    }
}
