using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250407105400)]
    public class DefaultDB_20250407_105400_LeadAssignment : Migration
    {
        public override void Up()
        {
            Create.Table("LeadAssignment")
                 .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("LastAssignedUserId").AsInt32().Nullable().ForeignKey("FK_LeadAssignment_UserId", "dbo", "Users", "UserId")
                 .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                 .WithColumn("UpdatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);

        }

        public override void Down()
        {
            Delete.Table("LeadAssignment");
        }
    }
}