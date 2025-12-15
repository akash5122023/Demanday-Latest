using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250925162700)]
    public class DefaultDB_20250925_162700_GrnTwo : Migration

    {
        public override void Up()
        {
            Create.Table("GrnTwo")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_GrnContacts_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("GrnDate").AsDate().NotNullable()
                .WithColumn("GrnType").AsInt32().Nullable()
                .WithColumn("Po").AsString().Nullable()
                .WithColumn("PoDate").AsDate().Nullable()
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_GrnOwUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_GrnAsUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("InvoiceNo").AsString(50).Nullable()
                .WithColumn("InvoiceDate").AsDateTime().Nullable();

            Create.Table("GrnProductsTwo")
              .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
              .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_GrnProductsTwo_ProductsId", "dbo", "Products", "Id")
              .WithColumn("Code").AsString(20).Nullable()
              .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_GrnProductsTwo_Branch", "Branch", "Id")
              .WithColumn("Price").AsInt32().NotNullable()
              .WithColumn("OrderQuantity").AsDouble().NotNullable()
              .WithColumn("ReceivedQuantity").AsDouble().NotNullable()
              .WithColumn("ExtraQuantity").AsDouble().WithDefaultValue(0)
              .WithColumn("RejectedQuantity").AsDouble().WithDefaultValue(0)
              .WithColumn("Description").AsString(400).Nullable()
              .WithColumn("GrnId").AsInt32().Nullable();

            Create.ForeignKey("FK_GrnProductsTwo_GrnId")
                .FromTable("GrnProductsTwo").ForeignColumn("GrnId")
                .ToTable("GrnTwo").PrimaryColumn("Id");
        }



        public override void Down()
        {

        }
    }
}