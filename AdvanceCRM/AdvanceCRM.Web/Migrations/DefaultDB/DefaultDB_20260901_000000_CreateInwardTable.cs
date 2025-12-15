using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260901000000)]
    public class DefaultDB_20260901_000000_CreateInwardTable : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Inward").Exists())
                return;

            Create.Table("Inward")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_Inward_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("OtherAddress").AsBoolean().Nullable()
                .WithColumn("ShippingAddress").AsString(1000).Nullable()
                .WithColumn("PackagingCharges").AsDouble().NotNullable().WithDefaultValue(0)
                .WithColumn("FreightCharges").AsDouble().NotNullable().WithDefaultValue(0)
                .WithColumn("Advacne").AsDouble().NotNullable().WithDefaultValue(0)
                .WithColumn("DueDate").AsDateTime().NotNullable()
                .WithColumn("DispatchDetails").AsString(1000).Nullable()
                .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue(1)
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("SourceId").AsInt32().NotNullable().ForeignKey("FK_Inward_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().NotNullable().ForeignKey("FK_Inward_StageId", "dbo", "Stage", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_Inward_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_Inward_OwnerId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_Inward_AssignedId", "dbo", "Users", "UserId")
                .WithColumn("Total").AsDouble().Nullable()
                .WithColumn("InvoiceMade").AsBoolean().Nullable()
                .WithColumn("ContactPersonId").AsInt32().Nullable().ForeignKey("FK_Inward_ContactPersonId", "dbo", "SubContacts", "Id")
                .WithColumn("QuotationNo").AsInt32().Nullable()
                .WithColumn("QuotationDate").AsDateTime().Nullable()
                .WithColumn("ClosingDate").AsDateTime().NotNullable()
                .WithColumn("Attachments").AsString(1000).Nullable()
                .WithColumn("ChallanNo").AsInt32().Nullable()
                .WithColumn("ApprovedBy").AsInt32().Nullable()
                .WithColumn("OutwardId").AsInt32().Nullable();
        }

        public override void Down()
        {
            if (Schema.Table("Inward").Exists())
            {
                Delete.Table("Inward");
            }
        }
    }
}
