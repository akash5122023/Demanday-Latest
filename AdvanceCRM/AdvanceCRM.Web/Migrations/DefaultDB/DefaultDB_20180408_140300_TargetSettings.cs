using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180408140300)]
    public class DefaultDB_20180408_140300_TargetSettings : Migration
    {
        public override void Up()
        {

            Create.Table("TargetSetting")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("MonthlyTarget").AsInt32().Nullable()
                .WithColumn("MonthlyTargetAmount").AsDouble().Nullable()
                .WithColumn("Representative").AsInt32().NotNullable().ForeignKey("FK_TargetSettings_UserId", "dbo", "Users", "UserId")
                ;
        }

        public override void Down()
        {

        }
    }
}