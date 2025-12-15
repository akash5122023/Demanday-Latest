using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250901000000)]
    public class DefaultDB_20250901_000000_AddPlanToTenantUser : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Tenants")
                .AddColumn("Plan").AsString(100).Nullable();

            Alter.Table("Users")
                .AddColumn("Plan").AsString(100).Nullable();
        }
    }
}
