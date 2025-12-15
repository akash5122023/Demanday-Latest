using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260630120000)]
    public class DefaultDB_20260630_120000_RejectionOutward : Migration
    {
        public override void Up()
        {
            if (Schema.Table("RejectionOutward").Exists())
                return;

            Create.Table("RejectionOutward")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("QCNumber").AsInt32().Nullable()
                .WithColumn("ProductId").AsInt32().Nullable().ForeignKey("FK_RejectionOutward_ProductId", "dbo", "Products", "Id")
                .WithColumn("QtyRejected").AsInt32().Nullable()
                .WithColumn("PurchaseFromId").AsInt32().Nullable().ForeignKey("FK_RejectionOutward_PurchaseFromId", "dbo", "Contacts", "Id")
                .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue(1)
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_RejectionOutward_BranchId", "dbo", "Branch", "Id")
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("Attachments").AsString(1024).Nullable()
                .WithColumn("SentToSupplier").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("SentDate").AsDateTime().Nullable()
                .WithColumn("ClosingDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            if (Schema.Table("RejectionOutward").Exists())
                Delete.Table("RejectionOutward");
        }
    }
}
