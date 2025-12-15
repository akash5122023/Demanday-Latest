using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // Version must be in yyyyMMddHHmmss format (14 digits). Using explicit ctor to avoid any potential truncation.
    [Migration(2025, 09, 30, 15, 00, 00)]
    public class DefaultDB_20250930150000_SaaSSettings : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("SassApplicationSetting").Exists())
            {
                Create.Table("SassApplicationSetting")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Key").AsString(100).NotNullable().Unique()
                    .WithColumn("Value").AsString(500).Nullable();
            }
        }

        public override void Down()
        {
            if (Schema.Table("SassApplicationSetting").Exists())
                Delete.Table("SassApplicationSetting");
        }
    }
}
