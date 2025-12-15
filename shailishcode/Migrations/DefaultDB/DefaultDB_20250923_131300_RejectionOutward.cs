using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250923131300)]
    public class DefaultDB_20250923_131300_RejectionOutward : Migration

    {
        public override void Up()
        {
            Create.Table("RejectionOutward")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("QCNumber").AsInt32().Nullable()
                .WithColumn("ProductId").AsInt32().Nullable().ForeignKey("FK_RejectionOutward_ProductsId", "dbo", "Products", "Id")
                .WithColumn("QtyRejected").AsInt32().Nullable()
                .WithColumn("PurchaseFromId").AsInt32().Nullable().ForeignKey("FK_RejectionOutward_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Status").AsInt32().Nullable()
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_RejectionOutward_BranchId", "dbo", "Branch", "Id")
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("Attachments").AsString(1024).Nullable()
                .WithColumn("SentToSupplier").AsBoolean().WithDefaultValue(false)
                .WithColumn("SentDate").AsDateTime().Nullable()
                .WithColumn("ClosingDate").AsDateTime().Nullable()

            ;

        }


        public override void Down()
        {

        }
    }
}