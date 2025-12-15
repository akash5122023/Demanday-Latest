using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251003114500)]
    public class DefaultDB_20251003_114500_Bom : Migration
    {
        public override void Up()
        {
            // First, drop existing tables if they exist (in correct order due to FK dependencies)
            Execute.Sql(@"
                IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BomProducts')
                BEGIN
                    DROP TABLE [dbo].[BomProducts]
                END

                IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Bom')
                BEGIN
                    DROP TABLE [dbo].[Bom]
                END
            ");

            // --- Create Bom table ---
            Create.Table("Bom")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().Nullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("Status").AsInt32().Nullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("BranchId").AsInt32().Nullable()
                .WithColumn("OwnerId").AsInt32().NotNullable()
                .WithColumn("AssignedId").AsInt32().NotNullable()
                .WithColumn("OtherAddress").AsBoolean().Nullable()
                .WithColumn("ShippingAddress").AsString(1000).Nullable()
                .WithColumn("PackagingCharges").AsDouble().Nullable()
                .WithColumn("FreightCharges").AsDouble().Nullable()
                .WithColumn("Advacne").AsDouble().Nullable()
                .WithColumn("DueDate").AsDateTime().Nullable()
                .WithColumn("DispatchDetails").AsString(1000).Nullable()
                .WithColumn("Roundup").AsDouble().Nullable()
                .WithColumn("Subject").AsString(1000).Nullable()
                .WithColumn("Reference").AsString(1000).Nullable()
                .WithColumn("ContactPersonId").AsInt32().Nullable()
                .WithColumn("Lines").AsInt32().Nullable()
                .WithColumn("QuotationNo").AsInt32().Nullable()
                .WithColumn("QuotationDate").AsDateTime().Nullable()
                .WithColumn("Conversion").AsDouble().Nullable()
                .WithColumn("PurchaseOrderNo").AsString(1024).Nullable()
                .WithColumn("ItemName").AsString(1024).Nullable()
                .WithColumn("OperationCost").AsInt32().Nullable()
                .WithColumn("RawMaterialCost").AsInt32().Nullable()
                .WithColumn("ScrapMaterialCost").AsInt32().Nullable()
                .WithColumn("TotalMaterialCost").AsInt32().Nullable()
                .WithColumn("OperationName").AsString(1024).Nullable()
                .WithColumn("WorkStationName").AsString(1024).Nullable()
                .WithColumn("OperatinngTime").AsString(100).Nullable()
                .WithColumn("OperatingCost").AsInt32().Nullable()
                .WithColumn("ProcessLoss").AsInt32().Nullable()
                .WithColumn("ProcessLossQty").AsInt32().Nullable()
                .WithColumn("Attachments").AsString(1024).Nullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Taxable").AsInt32().Nullable()
                .WithColumn("Quantity").AsDouble().Nullable()
                .WithColumn("MRP").AsDouble().WithDefaultValue(0)
                .WithColumn("SellingPrice").AsDouble().WithDefaultValue(0).Nullable()
                .WithColumn("Price").AsDouble().WithDefaultValue(0)
                .WithColumn("Discount").AsDouble().WithDefaultValue(0).Nullable()
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("WarrantyStart").AsDateTime().Nullable()
                .WithColumn("WarrantyEnd").AsDateTime().Nullable()
                .WithColumn("DiscountAmount").AsDouble().WithDefaultValue(0).Nullable()
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("Unit").AsString(128).Nullable()
                .WithColumn("Image").AsString(500).Nullable()
                .WithColumn("TechSpecs").AsString(2000).Nullable()
                .WithColumn("HSN").AsString(100).Nullable()
                .WithColumn("Code").AsString(100).Nullable()
                .WithColumn("ProductsId").AsInt32().Nullable();

            // --- Create BomProducts table ---
            Create.Table("BomProducts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable()
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("MRP").AsDouble().Nullable()
                .WithColumn("SellingPrice").AsDouble().Nullable()
                .WithColumn("Price").AsDouble().Nullable()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0).Nullable()
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("WarrantyStart").AsDateTime().Nullable()
                .WithColumn("WarrantyEnd").AsDateTime().Nullable()
                .WithColumn("BomId").AsInt32().NotNullable()
                .WithColumn("DiscountAmount").AsDouble().WithDefaultValue(0).Nullable()
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("Unit").AsString(128).Nullable();

            // --- Add foreign keys using manual SQL to avoid naming conflicts ---
            Execute.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BContacts_ContactsId')
                BEGIN
                    ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_BContacts_ContactsId] FOREIGN KEY ([ContactsId]) REFERENCES [dbo].[Contacts] ([Id])
                END

                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BBranch_BranchId')
                BEGIN
                    ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_BBranch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([Id])
                END

                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BOUserId_UserId')
                BEGIN
                    ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_BOUserId_UserId] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Users] ([UserId])
                END

                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BAUserId_UserId')
                BEGIN
                    ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_BAUserId_UserId] FOREIGN KEY ([AssignedId]) REFERENCES [dbo].[Users] ([UserId])
                END

                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Bom_SubContactsId')
                BEGIN
                    ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_Bom_SubContactsId] FOREIGN KEY ([ContactPersonId]) REFERENCES [dbo].[SubContacts] ([Id])
                END

                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Bom_ProductsId_ProductId')
                BEGIN
                    ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_Bom_ProductsId_ProductId] FOREIGN KEY ([ProductsId]) REFERENCES [dbo].[Products] ([Id])
                END

                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BPProducts_ProductsId')
                BEGIN
                    ALTER TABLE [dbo].[BomProducts] ADD CONSTRAINT [FK_BPProducts_ProductsId] FOREIGN KEY ([ProductsId]) REFERENCES [dbo].[Products] ([Id])
                END

                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BPBom_BomId')
                BEGIN
                    ALTER TABLE [dbo].[BomProducts] ADD CONSTRAINT [FK_BPBom_BomId] FOREIGN KEY ([BomId]) REFERENCES [dbo].[Bom] ([Id])
                END
            ");
        }

        public override void Down()
        {
            // Drop child table first
            if (Schema.Table("BomProducts").Exists())
                Delete.Table("BomProducts");

            if (Schema.Table("Bom").Exists())
                Delete.Table("Bom");
        }
    }
}