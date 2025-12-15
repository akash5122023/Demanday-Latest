using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701011000)]
    public class DefaultDB_20260701_011000_GrnTwo : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("GrnTwo").Exists())
            {
                Create.Table("GrnTwo").InSchema("dbo")
                    .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                    .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_GrnTwo_ContactsId", "dbo", "Contacts", "Id")
                    .WithColumn("GrnDate").AsDate().NotNullable()
                    .WithColumn("GrnType").AsInt32().Nullable()
                    .WithColumn("Po").AsString().Nullable()
                    .WithColumn("PoDate").AsDate().Nullable()
                    .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_GrnTwo_OwnerId", "dbo", "Users", "UserId")
                    .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_GrnTwo_AssignedId", "dbo", "Users", "UserId")
                    .WithColumn("Status").AsInt32().NotNullable()
                    .WithColumn("Description").AsString(500).Nullable()
                    .WithColumn("InvoiceNo").AsString(50).Nullable()
                    .WithColumn("InvoiceDate").AsDateTime().Nullable();
            }

            if (!Schema.Table("GrnProductsTwo").Exists())
            {
                Create.Table("GrnProductsTwo").InSchema("dbo")
                    .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                    .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_GrnProductsTwo_ProductsId", "dbo", "Products", "Id")
                    .WithColumn("Code").AsString(20).Nullable()
                    .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_GrnProductsTwo_Branch", "dbo", "Branch", "Id")
                    .WithColumn("Price").AsInt32().NotNullable()
                    .WithColumn("OrderQuantity").AsDouble().NotNullable()
                    .WithColumn("ReceivedQuantity").AsDouble().NotNullable()
                    .WithColumn("ExtraQuantity").AsDouble().WithDefaultValue(0)
                    .WithColumn("RejectedQuantity").AsDouble().WithDefaultValue(0)
                    .WithColumn("Description").AsString(400).Nullable()
                    .WithColumn("GrnId").AsInt32().Nullable();

                Create.ForeignKey("FK_GrnProductsTwo_GrnId")
                    .FromTable("GrnProductsTwo").InSchema("dbo").ForeignColumn("GrnId")
                    .ToTable("GrnTwo").InSchema("dbo").PrimaryColumn("Id");
            }
        }

        public override void Down()
        {
            if (Schema.Table("GrnProductsTwo").Exists())
                Delete.Table("GrnProductsTwo").InSchema("dbo");

            if (Schema.Table("GrnTwo").Exists())
                Delete.Table("GrnTwo").InSchema("dbo");
        }
    }
}
