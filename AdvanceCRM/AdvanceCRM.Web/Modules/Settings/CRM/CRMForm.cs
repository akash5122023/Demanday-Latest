
namespace AdvanceCRM.Settings.Forms
{
    using AdvanceCRM.Administration;
    using Serenity.ComponentModel;
    using System;
    using System.ComponentModel;

    [FormScript("Settings.CRMForm")]
    [BasedOnRow(typeof(CompanyDetailsRow))]
    public class CRMForm
    {

        [Category("General")]
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean PhoneCompulsory { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean EmailCompulsory { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean MultiCurrency { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean ProjectWithContacts { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AllowMovingNonClosedRecords { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean StateCompulsoryInContacts { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean EnableAddressInTransactions { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean TAXInStockTransfer { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean EnableAdditionalCharges { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean EnableAdditionalConcessions { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean CountryMandatory { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean PincodeMandatory { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean CityMandatory { get; set; }

        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean CapacityInProducts { get; set; }
        [OneThirdWidth,DisplayName("Remove AdditionalInfo2")]
        [BooleanSwitchEditor]
        public Boolean Addinfo2 { get; set; }
        [OneThirdWidth, DisplayName("Remove Multi AdditionalInfo")]
        [BooleanSwitchEditor]
        public Boolean MultiAddInfo { get; set; }
        [OneThirdWidth, DisplayName("Mail-To Subcontacts")]
        [BooleanSwitchEditor]
        public Boolean MailToSubContacts { get; set; }
        [OneThirdWidth, DisplayName("Mail-To Organisation")]
        [BooleanSwitchEditor]
        public Boolean MailToOrganisation { get; set; }

        [Category("Products Details")]
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean PassportDetails { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean HSN { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean Code { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean Unit { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean OpeningStock { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean RawMaterial { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean Group { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
      
        public Boolean Capacity { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean MRP { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean SellingPrice { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean Travels { get; set; }

        [Category("Enquiry")]
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean DealerInEnquiry { get; set; }    
       
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean EnquiryFollwupMandatory { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean EnquiryProductsMandatory { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean RequirementInEnquiry { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AutoEmailEnquiry { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AutoSMSEnquiry { get; set; }
        [OneThirdWidth, DisplayName("Win Percentage In Enquiry")]
        [BooleanSwitchEditor]
        public Boolean WinPercentageInEnquiry { get; set; }
        [OneThirdWidth, DisplayName("Expected Closing Date In Enquiry")]
        [BooleanSwitchEditor]
        public Boolean ExpectedClosingDateInEnquiry { get; set; }


        [Category("Quotation")]
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean RoundupInQuotation { get; set; }
        [OneThirdWidth,DisplayName("Enable Total In Quotation")]
        [BooleanSwitchEditor]
        public Boolean QuotationTotal { get; set; }
        [OneThirdWidth, DisplayName("Remove Grand Total Column")]
        [BooleanSwitchEditor]
        public Boolean RemoveGtColumn { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean QuotationFollwupMandatory { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean QuotationProductsMandatory { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AutoEmailQuotation { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AutoSMSQuotation { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean DealerInQuotation { get; set; }

        [OneThirdWidth, DisplayName("Win Percentage In Quotation")]
        [BooleanSwitchEditor]
        public Boolean WinPercentageInQuotation { get; set; }
        [OneThirdWidth, DisplayName("Expected Closing Date In Quotation")]
        [BooleanSwitchEditor]
        public Boolean ExpectedClosingDateInQuotation { get; set; }



        [Category("Proforma")]
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean ValidDate { get; set; }

        [Category("Sales")]
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean RoundupInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean PackagingInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean FreightInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean DueDateInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean DispatchInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean GSTDetailsInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean FollowupsInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean TermsInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AdvanceInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean StageInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean CodeInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean SerialInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean BatchInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean DiscountInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean TAXInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean WarrantyInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean DescriptionInSales { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AutoEmailInvoice { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AutoSMSInvoice { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean DealerInSales { get; set; }

       
        [Category("CMS")]
        [OneThirdWidth,DisplayName("Enable Dealer")]
        [BooleanSwitchEditor]
        public Boolean DealerInCms { get; set; }
        [OneThirdWidth, DisplayName("Enable Service Provider/Employee")]
        [BooleanSwitchEditor]
        public Boolean ServicePerson { get; set; }
      
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean ProjectInCms { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean ProductsInCms { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean RemovePurchaseDate { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean RemoveInvoiceNo { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor, Hidden]
        public Boolean DealerInInvoice { get; set; }
        [Category("Purchase")]
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean RoundupInPurchase { get; set; }
        [Category("Appointments")]
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AppointmentInEnquiry { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AppointmentInQuotation { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AppointmentInProforma { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AppointmentInTeleCalling { get; set; }
        [OneThirdWidth]
        [BooleanSwitchEditor]
        public Boolean AutoSMSAppointments { get; set; }



        [Category("Tasks")]
        [OneThirdWidth, DisplayName("Task Title In Task")]
        [BooleanSwitchEditor]
        public Boolean TaskTitleInTask { get; set; }

        [OneThirdWidth, DisplayName("Task Master In Task")]
        [BooleanSwitchEditor]
        public Boolean TaskMasterInTask { get; set; }

    }
}