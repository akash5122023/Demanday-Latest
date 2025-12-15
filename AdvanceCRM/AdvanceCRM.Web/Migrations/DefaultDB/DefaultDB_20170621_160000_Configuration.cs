using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170621160000)]
    public class DefaultDB_20170621_160000_Configuration : Migration
    {
        public override void Up()
        {
            Create.Table("SMSConfiguration")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Username").AsString(50).NotNullable()
                .WithColumn("Password").AsString(50).NotNullable()
                .WithColumn("SenderId").AsString(50).NotNullable()
                .WithColumn("Key").AsString(50).Nullable()
                .WithColumn("API").AsString(2048).NotNullable().WithDefaultValue("API_Url")
                ;

            Insert.IntoTable("SMSConfiguration").Row(new
            {
                Username = "Username",
                Password = "Password",
                SenderId = "SenderId"
            });

            Create.Table("IVRConfiguration")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("IVRNumber").AsString(500).Nullable()
                .WithColumn("APIKey").AsString(150).Nullable()
                .WithColumn("Plan").AsString(20).Nullable()
                ;

            Insert.IntoTable("IVRConfiguration").Row(new
            {
                IVRNumber = "+919000000000",
                APIKey = "bafed806-a7a7-11e4-8f1d-22000a949eb5",
                Plan = "Basic"
            });

            Create.Table("IndiaMART")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("MobileNumber").AsString(20).Nullable()
                .WithColumn("APIKey").AsString(100).Nullable()
                ;

            Insert.IntoTable("IndiaMART").Row(new
            {
                MobileNumber = "9000000000",
                APIKey = "%3COTg4M5MSM0MzUDSH765HG==%3E"
            });

            Create.Table("MailChimp")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("APIKey").AsString(200).Nullable()

                .WithColumn("CompanyName").AsString(200).Nullable()
                .WithColumn("Address").AsString(500).Nullable()
                .WithColumn("City").AsString(100).Nullable()
                .WithColumn("State").AsString(100).Nullable()
                .WithColumn("Zip").AsString(100).Nullable()
                .WithColumn("Country").AsInt32().Nullable()
                .WithColumn("Phone").AsString(100).Nullable()
                .WithColumn("Reminder").AsString(500).Nullable()
                .WithColumn("ContactFromEmail").AsString(50).Nullable()
                .WithColumn("ContactFromName").AsString(50).Nullable()
                .WithColumn("ContactSubject").AsString(50).Nullable()
                .WithColumn("EnquiryFromEmail").AsString(50).Nullable()
                .WithColumn("EnquiryFromName").AsString(50).Nullable()
                .WithColumn("EnquirySubject").AsString(50).Nullable()
                .WithColumn("QuotationFromEmail").AsString(50).Nullable()
                .WithColumn("QuotationFromName").AsString(50).Nullable()
                .WithColumn("QuotationSubject").AsString(50).Nullable()
                .WithColumn("SaleFromEmail").AsString(50).Nullable()
                .WithColumn("SaleFromName").AsString(50).Nullable()
                .WithColumn("SaleSubject").AsString(50).Nullable()
                ;

            Insert.IntoTable("MailChimp").Row(new
            {
                APIKey = "APIKey"
            });
        }

        public override void Down()
        {

        }
    }
}