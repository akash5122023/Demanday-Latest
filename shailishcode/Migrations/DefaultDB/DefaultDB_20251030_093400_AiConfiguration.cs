using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251030093400)]
    public class DefaultDB_20251030_093400_AiConfiguration : Migration
    {
        public override void Up()
        {
            Create.Table("AiConfiguration")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("AI_KEY").AsString(255).NotNullable();

        }

        public override void Down()
        {
            Delete.Table("AiConfiguration");
        }
    }
}