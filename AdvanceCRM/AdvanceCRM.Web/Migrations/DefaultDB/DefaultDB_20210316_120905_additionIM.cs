using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210316120905)]
    public class DefaultDB_20210316_120905_additionIM : Migration
    {
        public override void Up()
        {

            Alter.Table("SIndiaMartDetails")
                .AddColumn("IsMoved").AsBoolean().Nullable()
               .AddColumn("Source").AsInt32().NotNullable().WithDefaultValue(1)
                .AlterColumn("DateRe").AsDateTime();

        }
        public override void Down()
        {
        }
    }
}