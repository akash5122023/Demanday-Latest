using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170118110000)]
    public class DefaultDB_20170118_110000_Contacts : Migration
    {
        public override void Up()
        {
            Create.Table("Contacts").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactType").AsInt32().Nullable()
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("Phone").AsString(100).Nullable()
                .WithColumn("Email").AsString(100).Nullable()
                .WithColumn("Address").AsString(500).Nullable()
                .WithColumn("CityId").AsInt32().Nullable().ForeignKey("FK_CCity_CityId", "dbo", "City", "Id")
                .WithColumn("StateId").AsInt32().Nullable().ForeignKey("FK_CState_StateId", "dbo", "State", "Id")
                .WithColumn("Pin").AsString(50).Nullable()
                .WithColumn("Country").AsInt32().Nullable()
                .WithColumn("Website").AsString(150).Nullable()
                .WithColumn("AdditionalInfo").AsString(5000).Nullable()
                .WithColumn("ResidentialPhone").AsString(100).Nullable()
                .WithColumn("OfficePhone").AsString(100).Nullable()
                .WithColumn("Gender").AsInt32().Nullable()
                .WithColumn("Religion").AsInt32().Nullable()
                .WithColumn("AreaId").AsInt32().Nullable().ForeignKey("FK_CArea_AreaId", "dbo", "Area", "Id")
                .WithColumn("MaritalStatus").AsInt32().Nullable()
                .WithColumn("MarriageAnniversary").AsDateTime().Nullable()
                .WithColumn("Birthdate").AsDateTime().Nullable()
                .WithColumn("DateOfIncorporation").AsDateTime().Nullable()
                .WithColumn("CategoryId").AsInt32().Nullable().ForeignKey("FK_CCategory_CategoryId", "dbo", "Category", "Id")
                .WithColumn("GradeId").AsInt32().Nullable().ForeignKey("FK_CGrade_GradeId", "dbo", "Grade", "Id")
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_COUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_CAUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("ChannelCategory").AsInt32().Nullable()
                .WithColumn("NationalDistributor").AsInt32().Nullable().ForeignKey("FK_NationalDId_ConatctsId", "dbo", "Contacts", "Id")
                .WithColumn("Stockist").AsInt32().Nullable().ForeignKey("FK_StockistId_ConatctsId", "dbo", "Contacts", "Id")
                .WithColumn("Distributor").AsInt32().Nullable().ForeignKey("FK_DistributorId_ConatctsId", "dbo", "Contacts", "Id")
                 .WithColumn("Dealer").AsInt32().Nullable().ForeignKey("FK_DealerId_ConatctsId", "dbo", "Contacts", "Id")
                .WithColumn("Wholesaler").AsInt32().Nullable().ForeignKey("FK_WholesalerId_ConatctsId", "dbo", "Contacts", "Id")
                .WithColumn("Reseller").AsInt32().Nullable().ForeignKey("FK_ResllerId_ConatctsId", "dbo", "Contacts", "Id")
                .WithColumn("GSTIN").AsString(50).Nullable()
                .WithColumn("PANNo").AsString(50).Nullable()
                .WithColumn("CCEmails").AsString(500).Nullable()
                .WithColumn("BCCEmails").AsString(500).Nullable()
                .WithColumn("Attachment").AsString(500).Nullable()
                .WithColumn("EComGSTIN").AsString(100).Nullable()
                .WithColumn("CreditorsOpening").AsDouble().Nullable()
                .WithColumn("DebtorsOpening").AsDouble().Nullable()
                .WithColumn("BankName").AsString(100).Nullable()
                .WithColumn("AccountNumber").AsString(100).Nullable()
                .WithColumn("IFSC").AsString(100).Nullable()
                .WithColumn("BankType").AsString(100).Nullable()
                .WithColumn("Branch").AsString(100).Nullable()
                .WithColumn("AccountsEmail").AsString(100).Nullable()
                .WithColumn("PurchaseEmail").AsString(100).Nullable()
                .WithColumn("ServiceEmail").AsString(100).Nullable()
                .WithColumn("SalesEmail").AsString(100).Nullable()
                .WithColumn("CreditDays").AsInt32().Nullable()
                .WithColumn("CustomerType").AsInt32().Nullable().WithColumn("TrasportationId").AsInt32().Nullable().ForeignKey("FK_TrasportationId_Contacts_Id", "dbo", "Transportation", "Id")
                .WithColumn("TehsilId").AsInt32().Nullable().ForeignKey("FK_Contacts_TehsilId", "dbo", "Tehsil", "Id")
                .WithColumn("VillageId").AsInt32().Nullable().ForeignKey("FK_Contacts_VillageId", "dbo", "Village", "Id")
                ;

            Create.Table("SubContacts").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("Phone").AsString(100).Nullable()
                .WithColumn("ResidentialPhone").AsString(100).Nullable()
                .WithColumn("Email").AsString(100).Nullable()
                .WithColumn("Designation").AsString(100).Nullable()
                .WithColumn("Address").AsString(500).Nullable()
                .WithColumn("Gender").AsInt32().Nullable()
                .WithColumn("Religion").AsInt32().Nullable()
                .WithColumn("MaritalStatus").AsInt32().Nullable()
                .WithColumn("MarriageAnniversary").AsDateTime().Nullable()
                .WithColumn("Birthdate").AsDateTime().Nullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_SCContacts_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Project").AsString(256).Nullable()
                ;

            Create.Table("MultiRepContacts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_MultiRepContacts_UserId", "dbo", "Users", "UserId")
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_MultiRepContacts_ContactsId", "dbo", "Contacts", "Id")
                ;

        }

        public override void Down()
        {

        }
    }
}