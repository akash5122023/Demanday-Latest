using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701020000)]
    public class DefaultDB_20260701_020000_Bom : Migration
    {
        public override void Up()
        {
            //Execute.Sql(@"
            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BPBom_BomId')
            //    BEGIN
            //        ALTER TABLE [dbo].[BomProducts] DROP CONSTRAINT [FK_BPBom_BomId];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BPProducts_ProductsId')
            //    BEGIN
            //        ALTER TABLE [dbo].[BomProducts] DROP CONSTRAINT [FK_BPProducts_ProductsId];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BomProducts')
            //    BEGIN
            //        DROP TABLE [dbo].[BomProducts];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Bom_ProductsId_ProductId')
            //    BEGIN
            //        ALTER TABLE [dbo].[Bom] DROP CONSTRAINT [FK_Bom_ProductsId_ProductId];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Bom_SubContactsId')
            //    BEGIN
            //        ALTER TABLE [dbo].[Bom] DROP CONSTRAINT [FK_Bom_SubContactsId];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BAUserId_UserId')
            //    BEGIN
            //        ALTER TABLE [dbo].[Bom] DROP CONSTRAINT [FK_BAUserId_UserId];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BOUserId_UserId')
            //    BEGIN
            //        ALTER TABLE [dbo].[Bom] DROP CONSTRAINT [FK_BOUserId_UserId];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BBranch_BranchId')
            //    BEGIN
            //        ALTER TABLE [dbo].[Bom] DROP CONSTRAINT [FK_BBranch_BranchId];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BContacts_ContactsId')
            //    BEGIN
            //        ALTER TABLE [dbo].[Bom] DROP CONSTRAINT [FK_BContacts_ContactsId];
            //    END

            //    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Bom')
            //    BEGIN
            //        DROP TABLE [dbo].[Bom];
            //    END
            //");

            if (Schema.Table("BomProducts").Constraint("FK_BPBom_BomId").Exists())
                Delete.ForeignKey("FK_BPBom_BomId").OnTable("BomProducts");

            if (Schema.Table("BomProducts").Constraint("FK_BPProducts_ProductsId").Exists())
                Delete.ForeignKey("FK_BPProducts_ProductsId").OnTable("BomProducts");

            // Drop BomProducts table if it exists
            if (Schema.Table("BomProducts").Exists())
                Delete.Table("BomProducts");

            // Drop foreign key constraints from Bom table if they exist
            if (Schema.Table("Bom").Constraint("FK_Bom_ProductsId_ProductId").Exists())
                Delete.ForeignKey("FK_Bom_ProductsId_ProductId").OnTable("Bom");

            if (Schema.Table("Bom").Constraint("FK_Bom_SubContactsId").Exists())
                Delete.ForeignKey("FK_Bom_SubContactsId").OnTable("Bom");

            if (Schema.Table("Bom").Constraint("FK_BAUserId_UserId").Exists())
                Delete.ForeignKey("FK_BAUserId_UserId").OnTable("Bom");

            if (Schema.Table("Bom").Constraint("FK_BOUserId_UserId").Exists())
                Delete.ForeignKey("FK_BOUserId_UserId").OnTable("Bom");

            if (Schema.Table("Bom").Constraint("FK_BBranch_BranchId").Exists())
                Delete.ForeignKey("FK_BBranch_BranchId").OnTable("Bom");

            if (Schema.Table("Bom").Constraint("FK_BContacts_ContactsId").Exists())
                Delete.ForeignKey("FK_BContacts_ContactsId").OnTable("Bom");

            // Drop Bom table if it exists
            if (Schema.Table("Bom").Exists())
                Delete.Table("Bom");


            if (!Schema.Table("Bom").Exists())
            {

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

            }

            if (!Schema.Table("BomProducts").Exists())
            {
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
            }
            //        Execute.Sql(@"
            //            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BContacts_ContactsId')
            //                ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_BContacts_ContactsId] FOREIGN KEY ([ContactsId]) REFERENCES [dbo].[Contacts] ([Id]);

            //            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BBranch_BranchId')
            //                ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_BBranch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([Id]);

            //            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BOUserId_UserId')
            //                ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_BOUserId_UserId] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Users] ([UserId]);

            //            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BAUserId_UserId')
            //                ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_BAUserId_UserId] FOREIGN KEY ([AssignedId]) REFERENCES [dbo].[Users] ([UserId]);

            //            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Bom_SubContactsId')
            //                ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_Bom_SubContactsId] FOREIGN KEY ([ContactPersonId]) REFERENCES [dbo].[SubContacts] ([Id]);

            //            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Bom_ProductsId_ProductId')
            //                ALTER TABLE [dbo].[Bom] ADD CONSTRAINT [FK_Bom_ProductsId_ProductId] FOREIGN KEY ([ProductsId]) REFERENCES [dbo].[Products] ([Id]);


            //            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BPProducts_ProductsId')
            //ALTER TABLE [dbo].[BomProducts] ADD CONSTRAINT [FK_BPProducts_ProductsId] FOREIGN KEY ([ProductsId]) REFERENCES [dbo].[Products] ([Id]);

            //            IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_BPBom_BomId')
            //                ALTER TABLE [dbo].[BomProducts] ADD CONSTRAINT [FK_BPBom_BomId] FOREIGN KEY ([BomId]) REFERENCES [dbo].[Bom] ([Id]);

            //        ");


            // Add foreign key constraints if they don't exist
            if (Schema.Table("Bom").Exists() && Schema.Table("Contacts").Exists())
            {
                if (!Schema.Table("Bom").Constraint("FK_BContacts_ContactsId").Exists())
                    Create.ForeignKey("FK_BContacts_ContactsId")
                        .FromTable("Bom").ForeignColumn("ContactsId")
                        .ToTable("Contacts").PrimaryColumn("Id");
            }

            if (Schema.Table("Bom").Exists() && Schema.Table("Branch").Exists())
            {
                if (!Schema.Table("Bom").Constraint("FK_BBranch_BranchId").Exists())
                    Create.ForeignKey("FK_BBranch_BranchId")
                        .FromTable("Bom").ForeignColumn("BranchId")
                        .ToTable("Branch").PrimaryColumn("Id");
            }

            if (Schema.Table("Bom").Exists() && Schema.Table("Users").Exists())
            {
                if (!Schema.Table("Bom").Constraint("FK_BOUserId_UserId").Exists())
                    Create.ForeignKey("FK_BOUserId_UserId")
                        .FromTable("Bom").ForeignColumn("OwnerId")
                        .ToTable("Users").PrimaryColumn("UserId");

                if (!Schema.Table("Bom").Constraint("FK_BAUserId_UserId").Exists())
                    Create.ForeignKey("FK_BAUserId_UserId")
                        .FromTable("Bom").ForeignColumn("AssignedId")
                        .ToTable("Users").PrimaryColumn("UserId");
            }

            if (Schema.Table("Bom").Exists() && Schema.Table("SubContacts").Exists())
            {
                if (!Schema.Table("Bom").Constraint("FK_Bom_SubContactsId").Exists())
                    Create.ForeignKey("FK_Bom_SubContactsId")
                        .FromTable("Bom").ForeignColumn("ContactPersonId")
                        .ToTable("SubContacts").PrimaryColumn("Id");
            }

            if (Schema.Table("Bom").Exists() && Schema.Table("Products").Exists())
            {
                if (!Schema.Table("Bom").Constraint("FK_Bom_ProductsId_ProductId").Exists())
                    Create.ForeignKey("FK_Bom_ProductsId_ProductId")
                        .FromTable("Bom").ForeignColumn("ProductsId")
                        .ToTable("Products").PrimaryColumn("Id");
            }

            if (Schema.Table("BomProducts").Exists() && Schema.Table("Products").Exists())
            {
                if (!Schema.Table("BomProducts").Constraint("FK_BPProducts_ProductsId").Exists())
                    Create.ForeignKey("FK_BPProducts_ProductsId")
                        .FromTable("BomProducts").ForeignColumn("ProductsId")
                        .ToTable("Products").PrimaryColumn("Id");
            }

            if (Schema.Table("BomProducts").Exists() && Schema.Table("Bom").Exists())
            {
                if (!Schema.Table("BomProducts").Constraint("FK_BPBom_BomId").Exists())
                    Create.ForeignKey("FK_BPBom_BomId")
                        .FromTable("BomProducts").ForeignColumn("BomId")
                        .ToTable("Bom").PrimaryColumn("Id");
            }



        }

        public override void Down()
        {
    
        }
    }
}
