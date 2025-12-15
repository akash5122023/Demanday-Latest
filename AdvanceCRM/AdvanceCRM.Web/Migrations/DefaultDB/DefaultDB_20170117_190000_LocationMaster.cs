using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170117190000)]
    public class DefaultDB_20170117_190000_LocationMaster : Migration
    {
        public override void Up()
        {
            Create.Table("State").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("State").AsString(200).NotNullable();

            Create.Table("City").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("City").AsString(200).NotNullable()
                .WithColumn("StateId").AsInt32().NotNullable().ForeignKey("FK_City_StateId", "dbo", "State", "Id").WithDefaultValue(1);

            Create.Table("Tehsil").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Tehsil").AsString(200).NotNullable()
                .WithColumn("StateId").AsInt32().NotNullable().ForeignKey("FK_Tehsil_StateId", "dbo", "State", "Id")
                .WithColumn("CityId").AsInt32().NotNullable().ForeignKey("FK_Tehsil_CityId", "dbo", "City", "Id");

            Create.Table("Village").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Village").AsString(200).NotNullable()
                .WithColumn("StateId").AsInt32().NotNullable().ForeignKey("FK_Village_StateId", "dbo", "State", "Id")
                .WithColumn("CityId").AsInt32().NotNullable().ForeignKey("FK_Village_CityId", "dbo", "City", "Id")
                .WithColumn("TehsilId").AsInt32().NotNullable().ForeignKey("FK_Village_TehsilId", "dbo", "Tehsil", "Id");

            Create.Table("Area").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Area").AsString(350).NotNullable();
        }

        public override void Down()
        {

        }
    }
}