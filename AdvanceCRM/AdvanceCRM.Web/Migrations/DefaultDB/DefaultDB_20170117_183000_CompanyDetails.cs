using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170117183000)]
    public class DefaultDB_20170117_183000_CompanyDetails : Migration
    {
        public override void Up()
        {
            Create.Table("CompanyDetails")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Slogan").AsString(250).Nullable()
                .WithColumn("Address").AsString(500).NotNullable()
                .WithColumn("Phone").AsString(50).NotNullable()
                .WithColumn("Logo").AsString(500).Nullable()
                .WithColumn("LogoHeight").AsInt32().Nullable()
                .WithColumn("LogoWidth").AsInt32().Nullable()
                .WithColumn("HeaderImage").AsString(500).Nullable()
                .WithColumn("HeaderHeight").AsInt32().Nullable()
                .WithColumn("HeaderWidth").AsInt32().Nullable()
                .WithColumn("FooterImage").AsString(500).Nullable()
                .WithColumn("FooterHeight").AsInt32().Nullable()
                .WithColumn("FooterWidth").AsInt32().Nullable()

                .WithColumn("GSTIN").AsString(100).Nullable()
                .WithColumn("PANNo").AsString(100).Nullable()
                .WithColumn("EnquiryFollwupMandatory").AsBoolean().Nullable()
                .WithColumn("QuotationFollwupMandatory").AsBoolean().Nullable()
                .WithColumn("EnquiryProductsMandatory").AsBoolean().Nullable()
                .WithColumn("QuotationProductsMandatory").AsBoolean().Nullable()
                .WithColumn("RoundupInSales").AsBoolean().Nullable()
                .WithColumn("PackagingInSales").AsBoolean().Nullable()
                .WithColumn("FreightInSales").AsBoolean().Nullable()
                .WithColumn("DueDateInSales").AsBoolean().Nullable()
                .WithColumn("DispatchInSales").AsBoolean().Nullable()
                .WithColumn("GSTDetailsInSales").AsBoolean().Nullable()
                .WithColumn("FollowupsInSales").AsBoolean().Nullable()
                .WithColumn("TermsInSales").AsBoolean().Nullable()
                .WithColumn("AdvanceInSales").AsBoolean().Nullable()
                .WithColumn("StageInSales").AsBoolean().Nullable()
                .WithColumn("CodeInSales").AsBoolean().Nullable()
                .WithColumn("SerialInSales").AsBoolean().Nullable()
                .WithColumn("BatchInSales").AsBoolean().Nullable()
                .WithColumn("DiscountInSales").AsBoolean().Nullable()
                .WithColumn("TAXInSales").AsBoolean().Nullable()
                .WithColumn("WarrantyInSales").AsBoolean().Nullable()
                .WithColumn("DescriptionInSales").AsBoolean().Nullable()
                .WithColumn("AppointmentInEnquiry").AsBoolean().Nullable()
                .WithColumn("AppointmentInQuotation").AsBoolean().Nullable()
                .WithColumn("AppointmentInProforma").AsBoolean().Nullable()
                .WithColumn("AppointmentInSales").AsBoolean().Nullable()
                .WithColumn("AppointmentInTeleCalling").AsBoolean().Nullable()
                .WithColumn("RequirementInEnquiry").AsBoolean().Nullable()
                .WithColumn("TAXInStockTransfer").AsBoolean().Nullable()
                .WithColumn("PhoneCompulsory").AsBoolean().Nullable()
                .WithColumn("EmailCompulsory").AsBoolean().Nullable()
                .WithColumn("AutoSMSAppointments").AsBoolean().Nullable()
                .WithColumn("AllowMovingNonClosedRecords").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("StateCompulsoryInContacts").AsBoolean().Nullable().WithDefaultValue(true)
                .WithColumn("EnableAddressInTransactions").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoEmailEnquiry").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoSMSEnquiry").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoEmailQuotation").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoSMSQuotation").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoEmailProforma").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoSMSProforma").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoEmailInvoice").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("AutoSMSInvoice").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("HideDescriptionInSales").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("HideDescriptionInProforma").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("HideDescriptionInChallan").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("QuotationTemplate").AsInt32().Nullable()
                .WithColumn("Country").AsInt32().Nullable()
                .WithColumn("MultiCurrency").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("ProjectWithContacts").AsBoolean().NotNullable().WithDefaultValue(false)
                ;

            Insert.IntoTable("CompanyDetails").Row(new
            {
                Name = "Demo Company",
                Slogan = "Company Slogan",
                Address = "Company Address",
                Phone = "Company Phone(s)",
                HeaderWidth = 750,
                FooterWidth = 750
            });
        }

        public override void Down()
        {

        }
    }
}