using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220909162900)]
    public class DefaultDB_20220909_162900_RawTelecalling : Migration
    {
        public override void Up()
        {
            Create.Table("RawTelecall")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("CompanyName").AsString(150).Nullable()
                .WithColumn("Name").AsString(150).Nullable()
                .WithColumn("Phone").AsInt32().Nullable()
                .WithColumn("Email").AsString(50).Nullable()
                .WithColumn("Details").AsString(1000).Nullable()
                .WithColumn("CreatedBy").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_RawUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedTo").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_RawAUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);
        }

        public override void Down()
        {

        }
    }
}