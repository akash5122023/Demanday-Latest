using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210120110000)]
    public class DefaultDB_20210120_110000_GRN : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("GRN").WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_GContacts_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("GRNDate").AsDate().NotNullable()
                .WithColumn("PO").AsString().NotNullable()
                .WithColumn("PODate").AsDate().NotNullable()
                .WithColumn("DeliveryLocation").AsString(200).Nullable()
               .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_GOUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_GAUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Description").AsString(500).Nullable();

            Create.Table("GRNProducts").WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
              .WithColumn("ProductId").AsInt32().NotNullable().ForeignKey("FK_GRNProducts_ProductId", "dbo", "Products", "Id")
              .WithColumn("Code").AsString(20).Nullable()
              .WithColumn("PackSize").AsInt32().NotNullable()
              .WithColumn("Price").AsInt32().NotNullable()
              .WithColumn("OrderQuatity").AsInt32().NotNullable()
              .WithColumn("DeliveredQuantity").AsInt32().NotNullable()
              .WithColumn("Description").AsString(400).Nullable()
              .WithColumn("GRNId").AsInt32().NotNullable().ForeignKey("FK_GRN_GRNId", "dbo", "GRN", "Id");
        }
    }
}