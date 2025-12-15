using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701010000)]
    public class DefaultDB_20260701_010000_QualityCheck : Migration
    {
        public override void Up()
        {
            if (Schema.Table("QualityCheck").Exists())
                return;

            Create.Table("QualityCheck")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("QCNumber").AsInt32().Nullable()
                .WithColumn("PurchaseDate").AsDateTime().Nullable()
                .WithColumn("ProductName").AsString(255).Nullable()
                .WithColumn("QCDate").AsDateTime().Nullable()
                .WithColumn("InspectionCriteria").AsString(200).Nullable()
                .WithColumn("QtyInspected").AsInt32().Nullable()
                .WithColumn("QtyPassed").AsInt32().Nullable()
                .WithColumn("QtyRejected").AsInt32().Nullable()
                .WithColumn("DepositionAction").AsString(200).Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("Attachments").AsString(1024).Nullable()
                .WithColumn("ProductId").AsInt32().Nullable().ForeignKey("FK_QualityCheck_ProductId", "dbo", "Products", "Id")
                .WithColumn("PurchaseFromId").AsInt32().Nullable().ForeignKey("FK_QualityCheck_ContactsId", "dbo", "Contacts", "Id");
        }

        public override void Down()
        {
            if (Schema.Table("QualityCheck").Exists())
                Delete.Table("QualityCheck");
        }
    }
}
