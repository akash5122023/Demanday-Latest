using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210628183100)]
    public class DefaultDB_20210628_183100_ConComId : Migration
    {
        public override void Up()
        {

            Create.Table("KnowlarityIVR")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("Name").AsString(50).Nullable()
                 .WithColumn("Mobile").AsString(100).Nullable()
                 .WithColumn("EmpMobile").AsString(2048).Nullable()
                 .WithColumn("IVRNo").AsString(2048).Nullable()
                 .WithColumn("Recording").AsString(2000).Nullable()
                 .WithColumn("Date").AsString(200).Nullable()
                 .WithColumn("Duration").AsString(10).Nullable();
        }

        public override void Down()
        {

        }
    }
}