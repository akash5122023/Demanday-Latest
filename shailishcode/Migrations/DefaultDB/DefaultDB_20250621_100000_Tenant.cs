using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250621100000)]
    public class DefaultDB_20250621_100000_Tenant : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Tenants")
                .WithColumn("TenantId").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(200).NotNullable();

            Alter.Table("Users")
                .AddColumn("TenantId").AsInt32().Nullable()
                    .ForeignKey("FK_Users_TenantId", "dbo", "Tenants", "TenantId");
        }
    }
}
