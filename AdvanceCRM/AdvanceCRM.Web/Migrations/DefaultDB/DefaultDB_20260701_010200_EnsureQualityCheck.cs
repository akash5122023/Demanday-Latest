using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701010200)]
    public class DefaultDB_20260701_010200_EnsureQualityCheck : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("QualityCheck").Exists())
            {
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

                return;
            }

            EnsureColumn("QualityCheck", "QCNumber", () => Alter.Table("QualityCheck").AddColumn("QCNumber").AsInt32().Nullable());
            EnsureColumn("QualityCheck", "PurchaseDate", () => Alter.Table("QualityCheck").AddColumn("PurchaseDate").AsDateTime().Nullable());
            EnsureColumn("QualityCheck", "ProductName", () => Alter.Table("QualityCheck").AddColumn("ProductName").AsString(255).Nullable());
            EnsureColumn("QualityCheck", "QCDate", () => Alter.Table("QualityCheck").AddColumn("QCDate").AsDateTime().Nullable());
            EnsureColumn("QualityCheck", "InspectionCriteria", () => Alter.Table("QualityCheck").AddColumn("InspectionCriteria").AsString(200).Nullable());
            EnsureColumn("QualityCheck", "QtyInspected", () => Alter.Table("QualityCheck").AddColumn("QtyInspected").AsInt32().Nullable());
            EnsureColumn("QualityCheck", "QtyPassed", () => Alter.Table("QualityCheck").AddColumn("QtyPassed").AsInt32().Nullable());
            EnsureColumn("QualityCheck", "QtyRejected", () => Alter.Table("QualityCheck").AddColumn("QtyRejected").AsInt32().Nullable());
            EnsureColumn("QualityCheck", "DepositionAction", () => Alter.Table("QualityCheck").AddColumn("DepositionAction").AsString(200).Nullable());
            EnsureColumn("QualityCheck", "AdditionalInfo", () => Alter.Table("QualityCheck").AddColumn("AdditionalInfo").AsString(200).Nullable());
            EnsureColumn("QualityCheck", "Attachments", () => Alter.Table("QualityCheck").AddColumn("Attachments").AsString(1024).Nullable());
            EnsureColumn("QualityCheck", "ProductId", () => Alter.Table("QualityCheck").AddColumn("ProductId").AsInt32().Nullable());
            EnsureColumn("QualityCheck", "PurchaseFromId", () => Alter.Table("QualityCheck").AddColumn("PurchaseFromId").AsInt32().Nullable());
        }

        private void EnsureColumn(string tableName, string columnName, System.Action addColumn)
        {
            if (!Schema.Table(tableName).Column(columnName).Exists())
            {
                addColumn();

                if (columnName == "ProductId")
                {
                    Create.ForeignKey("FK_QualityCheck_ProductId")
                        .FromTable(tableName).ForeignColumn(columnName)
                        .ToTable("Products").InSchema("dbo").PrimaryColumn("Id");
                }
                else if (columnName == "PurchaseFromId")
                {
                    Create.ForeignKey("FK_QualityCheck_ContactsId")
                        .FromTable(tableName).ForeignColumn(columnName)
                        .ToTable("Contacts").InSchema("dbo").PrimaryColumn("Id");
                }
            }
        }

        public override void Down()
        {
            // Intentionally left empty. Dropping the table could remove user data.
        }
    }
}
