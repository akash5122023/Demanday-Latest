using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701013500)]
    public class DefaultDB_20260701_013500_OutwardProductsForeignKeys : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ChallanProducts')
                BEGIN
                    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_ChallanProducts_ProductsId')
                        ALTER TABLE [dbo].[ChallanProducts] DROP CONSTRAINT [FK_ChallanProducts_ProductsId];

                    ALTER TABLE [dbo].[ChallanProducts] ADD CONSTRAINT [FK_ChallanProducts_ProductsId]
                        FOREIGN KEY ([ProductsId]) REFERENCES [dbo].[Products] ([Id]);
                END

                IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OutwardProducts')
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_OutwardProducts_ProductsId')
                        ALTER TABLE [dbo].[OutwardProducts] ADD CONSTRAINT [FK_OutwardProducts_ProductsId]
                            FOREIGN KEY ([ProductsId]) REFERENCES [dbo].[Products] ([Id]);
                END
            ");
        }

        public override void Down()
        {
            Execute.Sql(@"
                IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OutwardProducts')
                BEGIN
                    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_OutwardProducts_ProductsId')
                        ALTER TABLE [dbo].[OutwardProducts] DROP CONSTRAINT [FK_OutwardProducts_ProductsId];
                END
            ");
        }
    }
}
