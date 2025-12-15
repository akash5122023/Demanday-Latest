using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250621110000)]
    public class DefaultDB_20250621_110000_TenantSubdomain : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Tenants")
                .AddColumn("Subdomain").AsString(200).Nullable()
                .AddColumn("DbName").AsString(200).Nullable()
                .AddColumn("Port").AsInt32().Nullable();
        }
    }
}
