using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170117191000)]
    public class DefaultDB_20170117_191000_ContactsMaster : Migration
    {
        public override void Up()
        {
            Create.Table("Grade").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Grade").AsString(350).NotNullable();

            Create.Table("Category").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Category").AsString(100).NotNullable()
                .WithColumn("Type").AsInt32().NotNullable().WithDefaultValue(1);

            Create.Table("Branch").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Branch").AsString(200).NotNullable()
                .WithColumn("Phone").AsString(50).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("Address").AsString(800).Nullable()
                .WithColumn("CityId").AsInt32().Nullable().ForeignKey("FK_City_CityId", "dbo", "City", "Id")
                .WithColumn("StateId").AsInt32().Nullable().ForeignKey("FK_State_StateId", "dbo", "State", "Id")
                .WithColumn("Pin").AsString(50).Nullable()
                .WithColumn("Country").AsInt32().Nullable()
                ;

            Create.Table("Transportation")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Address").AsString(1000)
                .WithColumn("Phone").AsString(50)
                .WithColumn("Email").AsString(200)
                .WithColumn("ContactPerson").AsString(100).Nullable()
                .WithColumn("ContactPersonPhone").AsString(100).Nullable()
                .WithColumn("GSTIN").AsString(100).Nullable()
            ;
        }

        public override void Down()
        {

        }
    }
}