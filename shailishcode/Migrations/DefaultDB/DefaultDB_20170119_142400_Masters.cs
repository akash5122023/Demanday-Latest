using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170119142400)]
    public class DefaultDB_20170119_142400_Masters : Migration
    {
        public override void Up()
        {
            Create.Table("Source").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Source").AsString(200).NotNullable();

            Create.Table("Stage").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Stage").AsString(200).NotNullable()
                .WithColumn("Type").AsInt32().NotNullable().WithDefaultValue(1);

            Create.Table("QuotationTermsMaster").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Terms").AsString(5000).NotNullable();

            Create.Table("BankMaster")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("BankName").AsString(100).NotNullable()
                .WithColumn("AccountNumber").AsString(100).NotNullable()
                .WithColumn("IFSC").AsString(100).Nullable()
                .WithColumn("Type").AsString(100).Nullable()
                .WithColumn("Branch").AsString(100).Nullable()
                .WithColumn("AdditionalInfo").AsString(400).Nullable()
                ;
        }

        public override void Down()
        {

        }
    }
}