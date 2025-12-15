using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250901010000)]
    public class DefaultDB_20250901_010000_AddDnsStatusToTenants : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Tenants")
                .AddColumn("DnsStatus").AsString(2000).Nullable();
        }
    }
}
