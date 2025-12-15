using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170213130700)]
    public class DefaultDB_20170213_130200_AMC : Migration
    {
        public override void Up()
        {
            Create.Table("AMC")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_AMCContacts_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("StartDate").AsDateTime().NotNullable()
                .WithColumn("EndDate").AsDateTime().NotNullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_AMCOUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_AMCAUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("Attachment").AsString(500).Nullable()
                .WithColumn("Lines").AsInt32().Nullable()
                ;

            Create.Table("AMCProducts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_AMCProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("SerialNo").AsString(350).Nullable()
                .WithColumn("Rate").AsDouble().NotNullable()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("Quantity").AsInt32().Nullable()
                .WithColumn("Visits").AsInt32().Nullable()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0)
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("AMCId").AsInt32().NotNullable().ForeignKey("FK_AMCProducts_AMCId", "dbo", "AMC", "Id")
                .WithColumn("DiscountAmount").AsDouble().WithDefaultValue(0)
                ;

            Create.Table("AMCVisitPlanner")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("VisitDate").AsDateTime().NotNullable()
                .WithColumn("AssignedTo").AsInt32().NotNullable().ForeignKey("FK_AMCVisitPlannerUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("CompletionDate").AsDateTime().Nullable()
                .WithColumn("VisitDetails").AsString(700).Nullable()
                .WithColumn("AMCId").AsInt32().NotNullable().ForeignKey("FK_AMCVisitPlanner_AMCId", "dbo", "AMC", "Id")
                .WithColumn("Serial").AsString(200).Nullable()
                .WithColumn("Attachment").AsString(1000).Nullable()
                .WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("FK_AMCVisitPlanner_UserId", "dbo", "Users", "UserId")
                ;
        }

        public override void Down()
        {

        }
    }
}