using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250919121900)]
    public class DefaultDB_20250919_121900_Indent : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("Indent").Exists())
            {
                Create.Table("Indent")
                    .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                    .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_IndContacts_ContactsId", "dbo", "Contacts", "Id")
                    .WithColumn("Date").AsDateTime().NotNullable()
                    .WithColumn("Status").AsInt32().NotNullable()
                    .WithColumn("AdditionalInfo").AsString(200).Nullable()
                    .WithColumn("InvoiceNo").AsString(100).Nullable().WithDefaultValue("")
                    .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_IndBranch_BranchId", "dbo", "Branch", "Id")
                    .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_IndOUserId_UserId", "dbo", "Users", "UserId")
                    .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_IndAUserId_UserId", "dbo", "Users", "UserId")
                    .WithColumn("Subject").AsString(1000).Nullable()
                    .WithColumn("Reference").AsString(1000).Nullable()
                    .WithColumn("ContactPersonId").AsInt32().Nullable().ForeignKey("FK_Indent_SubContactsId", "dbo", "SubContacts", "Id")
                    .WithColumn("Lines").AsInt32().Nullable();
            }

            if (!Schema.Table("IndentProducts").Exists())
            {
                Create.Table("IndentProducts")
                    .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                    .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_IndProducts_ProductsId", "dbo", "Products", "Id")
                    .WithColumn("IndentId").AsInt32().NotNullable().ForeignKey("FK_IPrIndent_IndentId", "dbo", "Indent", "Id")
                    .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                    .WithColumn("Description").AsString(2000).Nullable();
            }
        }

        public override void Down()
        {

        }
    }
}