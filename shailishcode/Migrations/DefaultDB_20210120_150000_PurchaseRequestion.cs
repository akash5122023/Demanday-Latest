using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210120150000)]
    public class DefaultDB_20210120_150000_PurchaseRequestion : AutoReversingMigration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists("Department", table => table
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("DepartmentName").AsString(20).NotNullable());

            Create.Table("PurchaseRequestion").WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("PRDate").AsDate().NotNullable()
               .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_PRContacts_ContactsId", "dbo", "Contacts", "Id")
               .WithColumn("DepartmentId").AsInt32().NotNullable().ForeignKey("FK_Department_DepartmentId", "dbo", "Department", "Id")
                 .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_POUserId_UserId", "dbo", "Users", "UserId")
               .WithColumn("ClientDeliveryDate").AsDate().Nullable()
               .WithColumn("Mode").AsString(20).Nullable()
               .WithColumn("Status").AsInt32().NotNullable()
               .WithColumn("StageId").AsInt32().NotNullable().ForeignKey("FK_PRStage_StageId", "dbo", "Stage", "Id")
               .WithColumn("ClientPaymentTerms").AsString(2000).Nullable()
               .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_PAUserId_UserId", "dbo", "Users", "UserId")
               .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("EnquiryRefNo").AsInt32().Nullable()
                .WithColumn("EnquiryRefDate").AsDate().Nullable()
                .WithColumn("QuotationRefNo").AsInt32().Nullable()
                .WithColumn("QuotationRefDate").AsDate().Nullable()
                .WithColumn("SalesOrderRefNo").AsInt32().Nullable()
                .WithColumn("SalesOrderRefDate").AsDate().Nullable()
                .WithColumn("Attachments").AsString(2000).Nullable();

            Create.Table("PurchaseRequestionProducts").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
              .WithColumn("ProductId").AsInt32().NotNullable().ForeignKey("FK_PRProducts_ProductId", "dbo", "Products", "Id")
              .WithColumn("Quantity").AsInt32().Nullable()
              .WithColumn("UnitRate").AsInt32().Nullable()
              .WithColumn("CustomerDeliveryDate").AsDate().Nullable()
              .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_PRPContacts_ContactsId", "dbo", "Contacts", "Id")
              .WithColumn("SupplierunitCost").AsInt32().Nullable()
              .WithColumn("InCOTerms").AsString(2000).Nullable()
              .WithColumn("PurchaseOrderRefNo").AsInt32().Nullable()
              .WithColumn("Status").AsInt32().Nullable()
              .WithColumn("ActualDeliveryDate").AsDate().Nullable()
              .WithColumn("Remarks").AsString(2000).Nullable()
              .WithColumn("PurchaseRequestionId").AsInt32().NotNullable().ForeignKey("FK_PurchaseRequestion_PurchaseRequestionId", "dbo", "PurchaseRequestion", "Id");
        }
    }
}