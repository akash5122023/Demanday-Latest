using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210705183100)]
    public class DefaultDB_20210705_183100_Quotation : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("ValidDate").AsBoolean().Nullable().WithDefaultValue(true)
                .AddColumn("DealerInCMS").AsBoolean().Nullable().WithDefaultValue(true);

            Create.Table("Dealer")
             .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
             .WithColumn("DealerName").AsString(150).NotNullable()
             .WithColumn("Phone").AsString(100).Nullable()
             .WithColumn("Email").AsString(100).Nullable()
             .WithColumn("Address").AsString(500).Nullable()
             .WithColumn("CityId").AsInt32().Nullable().ForeignKey("FK_DCity_CityId", "dbo", "City", "Id")
             .WithColumn("StateId").AsInt32().Nullable().ForeignKey("FK_DState_StateId", "dbo", "State", "Id")
             .WithColumn("Pin").AsString(50).Nullable()
             .WithColumn("Country").AsInt32().Nullable()
             .WithColumn("AdditionalInfo").AsString(200).Nullable();

            Alter.Table("CMS")
                .AddColumn("DealerId").AsInt32().Nullable().ForeignKey("FK_CDealer_DealerId", "dbo", "Dealer", "Id");


        }

        public override void Down()
        {

        }
    }
}