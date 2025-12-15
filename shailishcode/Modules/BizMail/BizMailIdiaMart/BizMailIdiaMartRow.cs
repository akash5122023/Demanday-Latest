
namespace AdvanceCRM.BizMail
{
    using AdvanceCRM.Administration;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("BizMail"), TableName("[dbo].[BizMailIdiaMart]")]
    [DisplayName("Biz Mail Idia Mart"), InstanceName("Biz Mail Idia Mart")]
    [ReadPermission("BizMail:Read")]
    [InsertPermission("BizMail:Insert")]
    [UpdatePermission("BizMail:Update")]
    [DeletePermission("BizMail:Delete")]
    public sealed class BizMailIdiaMartRow : Row<BizMailIdiaMartRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Rule"), NotNull]
        public Masters.BizMailRulesMaster? Rule
        {
            get { return (Masters.BizMailRulesMaster?)Fields.Rule[this]; }
            set { Fields.Rule[this] = (Int32?)value; }
        }

        [DisplayName("Bm List"), Column("BMListId"), NotNull, ForeignKey("[dbo].[BMList]", "Id"), LeftJoin("jBmList"), TextualField("BmListListId")]
        [LookupEditor(typeof(BmListRow), InplaceAdd = false)]
        public Int32? BmListId
        {
            get { return Fields.BmListId[this]; }
            set { Fields.BmListId[this] = value; }
        }

        [DisplayName("Status"), NotNull]
        public Boolean? Status
        {
            get { return Fields.Status[this]; }
            set { Fields.Status[this] = value; }
        }

        [DisplayName("Company"), NotNull, ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName")]
        [LookupEditor(typeof(CompanyDetailsRow), InplaceAdd = false)]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }

        [DisplayName("Description"), Size(2000), QuickSearch,NameProperty]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        [DisplayName("Bm List List Id"), Expression("jBmList.[ListId]")]
        public String BmListListId
        {
            get { return Fields.BmListListId[this]; }
            set { Fields.BmListListId[this] = value; }
        }

        [DisplayName("Bm List Company Name"), Expression("jBmList.[CompanyName]")]
        public String BmListCompanyName
        {
            get { return Fields.BmListCompanyName[this]; }
            set { Fields.BmListCompanyName[this] = value; }
        }

        [DisplayName("Bm List Name"), Expression("jBmList.[Name]")]
        public String BmListName
        {
            get { return Fields.BmListName[this]; }
            set { Fields.BmListName[this] = value; }
        }

        [DisplayName("Bm List City"), Expression("jBmList.[City]")]
        public String BmListCity
        {
            get { return Fields.BmListCity[this]; }
            set { Fields.BmListCity[this] = value; }
        }

        [DisplayName("Bm List Display Name"), Expression("jBmList.[DisplayName]")]
        public String BmListDisplayName
        {
            get { return Fields.BmListDisplayName[this]; }
            set { Fields.BmListDisplayName[this] = value; }
        }

        [DisplayName("Bm List Description"), Expression("jBmList.[Description]")]
        public String BmListDescription
        {
            get { return Fields.BmListDescription[this]; }
            set { Fields.BmListDescription[this] = value; }
        }

        [DisplayName("Bm List From"), Expression("jBmList.[From]")]
        public String BmListFrom
        {
            get { return Fields.BmListFrom[this]; }
            set { Fields.BmListFrom[this] = value; }
        }

        [DisplayName("Bm List Reply To"), Expression("jBmList.[ReplyTo]")]
        public String BmListReplyTo
        {
            get { return Fields.BmListReplyTo[this]; }
            set { Fields.BmListReplyTo[this] = value; }
        }

        [DisplayName("Company Name"), Expression("jCompany.[Name]")]
        public String CompanyName
        {
            get { return Fields.CompanyName[this]; }
            set { Fields.CompanyName[this] = value; }
        }

        [DisplayName("Company Slogan"), Expression("jCompany.[Slogan]")]
        public String CompanySlogan
        {
            get { return Fields.CompanySlogan[this]; }
            set { Fields.CompanySlogan[this] = value; }
        }

        [DisplayName("Company Address"), Expression("jCompany.[Address]")]
        public String CompanyAddress
        {
            get { return Fields.CompanyAddress[this]; }
            set { Fields.CompanyAddress[this] = value; }
        }

        [DisplayName("Company Phone"), Expression("jCompany.[Phone]")]
        public String CompanyPhone
        {
            get { return Fields.CompanyPhone[this]; }
            set { Fields.CompanyPhone[this] = value; }
        }

        [DisplayName("Company Logo"), Expression("jCompany.[Logo]")]
        public String CompanyLogo
        {
            get { return Fields.CompanyLogo[this]; }
            set { Fields.CompanyLogo[this] = value; }
        }

        [DisplayName("Company Logo Height"), Expression("jCompany.[LogoHeight]")]
        public Int32? CompanyLogoHeight
        {
            get { return Fields.CompanyLogoHeight[this]; }
            set { Fields.CompanyLogoHeight[this] = value; }
        }

        [DisplayName("Company Logo Width"), Expression("jCompany.[LogoWidth]")]
        public Int32? CompanyLogoWidth
        {
            get { return Fields.CompanyLogoWidth[this]; }
            set { Fields.CompanyLogoWidth[this] = value; }
        }

        [DisplayName("Company Header Image"), Expression("jCompany.[HeaderImage]")]
        public String CompanyHeaderImage
        {
            get { return Fields.CompanyHeaderImage[this]; }
            set { Fields.CompanyHeaderImage[this] = value; }
        }

        [DisplayName("Company Header Height"), Expression("jCompany.[HeaderHeight]")]
        public Int32? CompanyHeaderHeight
        {
            get { return Fields.CompanyHeaderHeight[this]; }
            set { Fields.CompanyHeaderHeight[this] = value; }
        }

        [DisplayName("Company Header Width"), Expression("jCompany.[HeaderWidth]")]
        public Int32? CompanyHeaderWidth
        {
            get { return Fields.CompanyHeaderWidth[this]; }
            set { Fields.CompanyHeaderWidth[this] = value; }
        }

        [DisplayName("Company Footer Image"), Expression("jCompany.[FooterImage]")]
        public String CompanyFooterImage
        {
            get { return Fields.CompanyFooterImage[this]; }
            set { Fields.CompanyFooterImage[this] = value; }
        }

        [DisplayName("Company Footer Height"), Expression("jCompany.[FooterHeight]")]
        public Int32? CompanyFooterHeight
        {
            get { return Fields.CompanyFooterHeight[this]; }
            set { Fields.CompanyFooterHeight[this] = value; }
        }

        [DisplayName("Company Footer Width"), Expression("jCompany.[FooterWidth]")]
        public Int32? CompanyFooterWidth
        {
            get { return Fields.CompanyFooterWidth[this]; }
            set { Fields.CompanyFooterWidth[this] = value; }
        }

        [DisplayName("Company Gstin"), Expression("jCompany.[GSTIN]")]
        public String CompanyGstin
        {
            get { return Fields.CompanyGstin[this]; }
            set { Fields.CompanyGstin[this] = value; }
        }

        [DisplayName("Company Pan No"), Expression("jCompany.[PANNo]")]
        public String CompanyPanNo
        {
            get { return Fields.CompanyPanNo[this]; }
            set { Fields.CompanyPanNo[this] = value; }
        }

        [DisplayName("Company Enquiry Follwup Mandatory"), Expression("jCompany.[EnquiryFollwupMandatory]")]
        public Boolean? CompanyEnquiryFollwupMandatory
        {
            get { return Fields.CompanyEnquiryFollwupMandatory[this]; }
            set { Fields.CompanyEnquiryFollwupMandatory[this] = value; }
        }

        [DisplayName("Company Quotation Follwup Mandatory"), Expression("jCompany.[QuotationFollwupMandatory]")]
        public Boolean? CompanyQuotationFollwupMandatory
        {
            get { return Fields.CompanyQuotationFollwupMandatory[this]; }
            set { Fields.CompanyQuotationFollwupMandatory[this] = value; }
        }

        [DisplayName("Company Enquiry Products Mandatory"), Expression("jCompany.[EnquiryProductsMandatory]")]
        public Boolean? CompanyEnquiryProductsMandatory
        {
            get { return Fields.CompanyEnquiryProductsMandatory[this]; }
            set { Fields.CompanyEnquiryProductsMandatory[this] = value; }
        }

        [DisplayName("Company Quotation Products Mandatory"), Expression("jCompany.[QuotationProductsMandatory]")]
        public Boolean? CompanyQuotationProductsMandatory
        {
            get { return Fields.CompanyQuotationProductsMandatory[this]; }
            set { Fields.CompanyQuotationProductsMandatory[this] = value; }
        }

        [DisplayName("Company Roundup In Sales"), Expression("jCompany.[RoundupInSales]")]
        public Boolean? CompanyRoundupInSales
        {
            get { return Fields.CompanyRoundupInSales[this]; }
            set { Fields.CompanyRoundupInSales[this] = value; }
        }

        [DisplayName("Company Packaging In Sales"), Expression("jCompany.[PackagingInSales]")]
        public Boolean? CompanyPackagingInSales
        {
            get { return Fields.CompanyPackagingInSales[this]; }
            set { Fields.CompanyPackagingInSales[this] = value; }
        }

        [DisplayName("Company Freight In Sales"), Expression("jCompany.[FreightInSales]")]
        public Boolean? CompanyFreightInSales
        {
            get { return Fields.CompanyFreightInSales[this]; }
            set { Fields.CompanyFreightInSales[this] = value; }
        }

        [DisplayName("Company Due Date In Sales"), Expression("jCompany.[DueDateInSales]")]
        public Boolean? CompanyDueDateInSales
        {
            get { return Fields.CompanyDueDateInSales[this]; }
            set { Fields.CompanyDueDateInSales[this] = value; }
        }

        [DisplayName("Company Dispatch In Sales"), Expression("jCompany.[DispatchInSales]")]
        public Boolean? CompanyDispatchInSales
        {
            get { return Fields.CompanyDispatchInSales[this]; }
            set { Fields.CompanyDispatchInSales[this] = value; }
        }

        [DisplayName("Company Gst Details In Sales"), Expression("jCompany.[GSTDetailsInSales]")]
        public Boolean? CompanyGstDetailsInSales
        {
            get { return Fields.CompanyGstDetailsInSales[this]; }
            set { Fields.CompanyGstDetailsInSales[this] = value; }
        }

        [DisplayName("Company Followups In Sales"), Expression("jCompany.[FollowupsInSales]")]
        public Boolean? CompanyFollowupsInSales
        {
            get { return Fields.CompanyFollowupsInSales[this]; }
            set { Fields.CompanyFollowupsInSales[this] = value; }
        }

        [DisplayName("Company Terms In Sales"), Expression("jCompany.[TermsInSales]")]
        public Boolean? CompanyTermsInSales
        {
            get { return Fields.CompanyTermsInSales[this]; }
            set { Fields.CompanyTermsInSales[this] = value; }
        }

        [DisplayName("Company Advance In Sales"), Expression("jCompany.[AdvanceInSales]")]
        public Boolean? CompanyAdvanceInSales
        {
            get { return Fields.CompanyAdvanceInSales[this]; }
            set { Fields.CompanyAdvanceInSales[this] = value; }
        }

        [DisplayName("Company Stage In Sales"), Expression("jCompany.[StageInSales]")]
        public Boolean? CompanyStageInSales
        {
            get { return Fields.CompanyStageInSales[this]; }
            set { Fields.CompanyStageInSales[this] = value; }
        }

        [DisplayName("Company Code In Sales"), Expression("jCompany.[CodeInSales]")]
        public Boolean? CompanyCodeInSales
        {
            get { return Fields.CompanyCodeInSales[this]; }
            set { Fields.CompanyCodeInSales[this] = value; }
        }

        [DisplayName("Company Serial In Sales"), Expression("jCompany.[SerialInSales]")]
        public Boolean? CompanySerialInSales
        {
            get { return Fields.CompanySerialInSales[this]; }
            set { Fields.CompanySerialInSales[this] = value; }
        }

        [DisplayName("Company Batch In Sales"), Expression("jCompany.[BatchInSales]")]
        public Boolean? CompanyBatchInSales
        {
            get { return Fields.CompanyBatchInSales[this]; }
            set { Fields.CompanyBatchInSales[this] = value; }
        }

        [DisplayName("Company Discount In Sales"), Expression("jCompany.[DiscountInSales]")]
        public Boolean? CompanyDiscountInSales
        {
            get { return Fields.CompanyDiscountInSales[this]; }
            set { Fields.CompanyDiscountInSales[this] = value; }
        }

        [DisplayName("Company Tax In Sales"), Expression("jCompany.[TAXInSales]")]
        public Boolean? CompanyTaxInSales
        {
            get { return Fields.CompanyTaxInSales[this]; }
            set { Fields.CompanyTaxInSales[this] = value; }
        }

        [DisplayName("Company Warranty In Sales"), Expression("jCompany.[WarrantyInSales]")]
        public Boolean? CompanyWarrantyInSales
        {
            get { return Fields.CompanyWarrantyInSales[this]; }
            set { Fields.CompanyWarrantyInSales[this] = value; }
        }

        [DisplayName("Company Description In Sales"), Expression("jCompany.[DescriptionInSales]")]
        public Boolean? CompanyDescriptionInSales
        {
            get { return Fields.CompanyDescriptionInSales[this]; }
            set { Fields.CompanyDescriptionInSales[this] = value; }
        }

        [DisplayName("Company Appointment In Enquiry"), Expression("jCompany.[AppointmentInEnquiry]")]
        public Boolean? CompanyAppointmentInEnquiry
        {
            get { return Fields.CompanyAppointmentInEnquiry[this]; }
            set { Fields.CompanyAppointmentInEnquiry[this] = value; }
        }

        [DisplayName("Company Appointment In Quotation"), Expression("jCompany.[AppointmentInQuotation]")]
        public Boolean? CompanyAppointmentInQuotation
        {
            get { return Fields.CompanyAppointmentInQuotation[this]; }
            set { Fields.CompanyAppointmentInQuotation[this] = value; }
        }

        [DisplayName("Company Appointment In Proforma"), Expression("jCompany.[AppointmentInProforma]")]
        public Boolean? CompanyAppointmentInProforma
        {
            get { return Fields.CompanyAppointmentInProforma[this]; }
            set { Fields.CompanyAppointmentInProforma[this] = value; }
        }

        [DisplayName("Company Appointment In Sales"), Expression("jCompany.[AppointmentInSales]")]
        public Boolean? CompanyAppointmentInSales
        {
            get { return Fields.CompanyAppointmentInSales[this]; }
            set { Fields.CompanyAppointmentInSales[this] = value; }
        }

        [DisplayName("Company Appointment In Tele Calling"), Expression("jCompany.[AppointmentInTeleCalling]")]
        public Boolean? CompanyAppointmentInTeleCalling
        {
            get { return Fields.CompanyAppointmentInTeleCalling[this]; }
            set { Fields.CompanyAppointmentInTeleCalling[this] = value; }
        }

        [DisplayName("Company Requirement In Enquiry"), Expression("jCompany.[RequirementInEnquiry]")]
        public Boolean? CompanyRequirementInEnquiry
        {
            get { return Fields.CompanyRequirementInEnquiry[this]; }
            set { Fields.CompanyRequirementInEnquiry[this] = value; }
        }

        [DisplayName("Company Tax In Stock Transfer"), Expression("jCompany.[TAXInStockTransfer]")]
        public Boolean? CompanyTaxInStockTransfer
        {
            get { return Fields.CompanyTaxInStockTransfer[this]; }
            set { Fields.CompanyTaxInStockTransfer[this] = value; }
        }

        [DisplayName("Company Phone Compulsory"), Expression("jCompany.[PhoneCompulsory]")]
        public Boolean? CompanyPhoneCompulsory
        {
            get { return Fields.CompanyPhoneCompulsory[this]; }
            set { Fields.CompanyPhoneCompulsory[this] = value; }
        }

        [DisplayName("Company Email Compulsory"), Expression("jCompany.[EmailCompulsory]")]
        public Boolean? CompanyEmailCompulsory
        {
            get { return Fields.CompanyEmailCompulsory[this]; }
            set { Fields.CompanyEmailCompulsory[this] = value; }
        }

        [DisplayName("Company Auto Sms Appointments"), Expression("jCompany.[AutoSMSAppointments]")]
        public Boolean? CompanyAutoSmsAppointments
        {
            get { return Fields.CompanyAutoSmsAppointments[this]; }
            set { Fields.CompanyAutoSmsAppointments[this] = value; }
        }

        [DisplayName("Company Allow Moving Non Closed Records"), Expression("jCompany.[AllowMovingNonClosedRecords]")]
        public Boolean? CompanyAllowMovingNonClosedRecords
        {
            get { return Fields.CompanyAllowMovingNonClosedRecords[this]; }
            set { Fields.CompanyAllowMovingNonClosedRecords[this] = value; }
        }

        [DisplayName("Company State Compulsory In Contacts"), Expression("jCompany.[StateCompulsoryInContacts]")]
        public Boolean? CompanyStateCompulsoryInContacts
        {
            get { return Fields.CompanyStateCompulsoryInContacts[this]; }
            set { Fields.CompanyStateCompulsoryInContacts[this] = value; }
        }

        [DisplayName("Company Enable Address In Transactions"), Expression("jCompany.[EnableAddressInTransactions]")]
        public Boolean? CompanyEnableAddressInTransactions
        {
            get { return Fields.CompanyEnableAddressInTransactions[this]; }
            set { Fields.CompanyEnableAddressInTransactions[this] = value; }
        }

        [DisplayName("Company Auto Email Enquiry"), Expression("jCompany.[AutoEmailEnquiry]")]
        public Boolean? CompanyAutoEmailEnquiry
        {
            get { return Fields.CompanyAutoEmailEnquiry[this]; }
            set { Fields.CompanyAutoEmailEnquiry[this] = value; }
        }

        [DisplayName("Company Auto Sms Enquiry"), Expression("jCompany.[AutoSMSEnquiry]")]
        public Boolean? CompanyAutoSmsEnquiry
        {
            get { return Fields.CompanyAutoSmsEnquiry[this]; }
            set { Fields.CompanyAutoSmsEnquiry[this] = value; }
        }

        [DisplayName("Company Auto Email Quotation"), Expression("jCompany.[AutoEmailQuotation]")]
        public Boolean? CompanyAutoEmailQuotation
        {
            get { return Fields.CompanyAutoEmailQuotation[this]; }
            set { Fields.CompanyAutoEmailQuotation[this] = value; }
        }

        [DisplayName("Company Auto Sms Quotation"), Expression("jCompany.[AutoSMSQuotation]")]
        public Boolean? CompanyAutoSmsQuotation
        {
            get { return Fields.CompanyAutoSmsQuotation[this]; }
            set { Fields.CompanyAutoSmsQuotation[this] = value; }
        }

        [DisplayName("Company Auto Email Proforma"), Expression("jCompany.[AutoEmailProforma]")]
        public Boolean? CompanyAutoEmailProforma
        {
            get { return Fields.CompanyAutoEmailProforma[this]; }
            set { Fields.CompanyAutoEmailProforma[this] = value; }
        }

        [DisplayName("Company Auto Sms Proforma"), Expression("jCompany.[AutoSMSProforma]")]
        public Boolean? CompanyAutoSmsProforma
        {
            get { return Fields.CompanyAutoSmsProforma[this]; }
            set { Fields.CompanyAutoSmsProforma[this] = value; }
        }

        [DisplayName("Company Auto Email Invoice"), Expression("jCompany.[AutoEmailInvoice]")]
        public Boolean? CompanyAutoEmailInvoice
        {
            get { return Fields.CompanyAutoEmailInvoice[this]; }
            set { Fields.CompanyAutoEmailInvoice[this] = value; }
        }

        [DisplayName("Company Auto Sms Invoice"), Expression("jCompany.[AutoSMSInvoice]")]
        public Boolean? CompanyAutoSmsInvoice
        {
            get { return Fields.CompanyAutoSmsInvoice[this]; }
            set { Fields.CompanyAutoSmsInvoice[this] = value; }
        }

        [DisplayName("Company Hide Description In Sales"), Expression("jCompany.[HideDescriptionInSales]")]
        public Boolean? CompanyHideDescriptionInSales
        {
            get { return Fields.CompanyHideDescriptionInSales[this]; }
            set { Fields.CompanyHideDescriptionInSales[this] = value; }
        }

        [DisplayName("Company Hide Description In Proforma"), Expression("jCompany.[HideDescriptionInProforma]")]
        public Boolean? CompanyHideDescriptionInProforma
        {
            get { return Fields.CompanyHideDescriptionInProforma[this]; }
            set { Fields.CompanyHideDescriptionInProforma[this] = value; }
        }

        [DisplayName("Company Hide Description In Challan"), Expression("jCompany.[HideDescriptionInChallan]")]
        public Boolean? CompanyHideDescriptionInChallan
        {
            get { return Fields.CompanyHideDescriptionInChallan[this]; }
            set { Fields.CompanyHideDescriptionInChallan[this] = value; }
        }

        [DisplayName("Company Quotation Template"), Expression("jCompany.[QuotationTemplate]")]
        public Int32? CompanyQuotationTemplate
        {
            get { return Fields.CompanyQuotationTemplate[this]; }
            set { Fields.CompanyQuotationTemplate[this] = value; }
        }

        [DisplayName("Company Country"), Expression("jCompany.[Country]")]
        public Int32? CompanyCountry
        {
            get { return Fields.CompanyCountry[this]; }
            set { Fields.CompanyCountry[this] = value; }
        }

        [DisplayName("Company Multi Currency"), Expression("jCompany.[MultiCurrency]")]
        public Boolean? CompanyMultiCurrency
        {
            get { return Fields.CompanyMultiCurrency[this]; }
            set { Fields.CompanyMultiCurrency[this] = value; }
        }

        [DisplayName("Company Project With Contacts"), Expression("jCompany.[ProjectWithContacts]")]
        public Boolean? CompanyProjectWithContacts
        {
            get { return Fields.CompanyProjectWithContacts[this]; }
            set { Fields.CompanyProjectWithContacts[this] = value; }
        }

        [DisplayName("Company State Id"), Expression("jCompany.[StateId]")]
        public Int32? CompanyStateId
        {
            get { return Fields.CompanyStateId[this]; }
            set { Fields.CompanyStateId[this] = value; }
        }

        [DisplayName("Company Roundup In Purchase"), Expression("jCompany.[RoundupInPurchase]")]
        public Boolean? CompanyRoundupInPurchase
        {
            get { return Fields.CompanyRoundupInPurchase[this]; }
            set { Fields.CompanyRoundupInPurchase[this] = value; }
        }

        [DisplayName("Company Invoice Template"), Expression("jCompany.[InvoiceTemplate]")]
        public Int32? CompanyInvoiceTemplate
        {
            get { return Fields.CompanyInvoiceTemplate[this]; }
            set { Fields.CompanyInvoiceTemplate[this] = value; }
        }

        [DisplayName("Company Company Details"), Expression("jCompany.[CompanyDetails]")]
        public Boolean? CompanyCompanyDetails
        {
            get { return Fields.CompanyCompanyDetails[this]; }
            set { Fields.CompanyCompanyDetails[this] = value; }
        }

        [DisplayName("Company Enable Additional Charges"), Expression("jCompany.[EnableAdditionalCharges]")]
        public Boolean? CompanyEnableAdditionalCharges
        {
            get { return Fields.CompanyEnableAdditionalCharges[this]; }
            set { Fields.CompanyEnableAdditionalCharges[this] = value; }
        }

        [DisplayName("Company Enable Additional Concessions"), Expression("jCompany.[EnableAdditionalConcessions]")]
        public Boolean? CompanyEnableAdditionalConcessions
        {
            get { return Fields.CompanyEnableAdditionalConcessions[this]; }
            set { Fields.CompanyEnableAdditionalConcessions[this] = value; }
        }

        [DisplayName("Company Additional Images"), Expression("jCompany.[AdditionalImages]")]
        public String CompanyAdditionalImages
        {
            get { return Fields.CompanyAdditionalImages[this]; }
            set { Fields.CompanyAdditionalImages[this] = value; }
        }

        [DisplayName("Company Header Content"), Expression("jCompany.[HeaderContent]")]
        public String CompanyHeaderContent
        {
            get { return Fields.CompanyHeaderContent[this]; }
            set { Fields.CompanyHeaderContent[this] = value; }
        }

        [DisplayName("Company Footer Content"), Expression("jCompany.[FooterContent]")]
        public String CompanyFooterContent
        {
            get { return Fields.CompanyFooterContent[this]; }
            set { Fields.CompanyFooterContent[this] = value; }
        }

        [DisplayName("Company Quotation Suffix"), Expression("jCompany.[QuotationSuffix]")]
        public String CompanyQuotationSuffix
        {
            get { return Fields.CompanyQuotationSuffix[this]; }
            set { Fields.CompanyQuotationSuffix[this] = value; }
        }

        [DisplayName("Company Quotation Prefix"), Expression("jCompany.[QuotationPrefix]")]
        public String CompanyQuotationPrefix
        {
            get { return Fields.CompanyQuotationPrefix[this]; }
            set { Fields.CompanyQuotationPrefix[this] = value; }
        }

        [DisplayName("Company Invoice Suffix"), Expression("jCompany.[InvoiceSuffix]")]
        public String CompanyInvoiceSuffix
        {
            get { return Fields.CompanyInvoiceSuffix[this]; }
            set { Fields.CompanyInvoiceSuffix[this] = value; }
        }

        [DisplayName("Company Invoice Prefix"), Expression("jCompany.[InvoicePrefix]")]
        public String CompanyInvoicePrefix
        {
            get { return Fields.CompanyInvoicePrefix[this]; }
            set { Fields.CompanyInvoicePrefix[this] = value; }
        }

        [DisplayName("Company Challan Suffix"), Expression("jCompany.[ChallanSuffix]")]
        public String CompanyChallanSuffix
        {
            get { return Fields.CompanyChallanSuffix[this]; }
            set { Fields.CompanyChallanSuffix[this] = value; }
        }

        [DisplayName("Company Challan Prefix"), Expression("jCompany.[ChallanPrefix]")]
        public String CompanyChallanPrefix
        {
            get { return Fields.CompanyChallanPrefix[this]; }
            set { Fields.CompanyChallanPrefix[this] = value; }
        }

        [DisplayName("Company Quotation Tax Column Included"), Expression("jCompany.[QuotationTaxColumnIncluded]")]
        public Boolean? CompanyQuotationTaxColumnIncluded
        {
            get { return Fields.CompanyQuotationTaxColumnIncluded[this]; }
            set { Fields.CompanyQuotationTaxColumnIncluded[this] = value; }
        }

        [DisplayName("Company Invoice Tax Column Included"), Expression("jCompany.[InvoiceTaxColumnIncluded]")]
        public Boolean? CompanyInvoiceTaxColumnIncluded
        {
            get { return Fields.CompanyInvoiceTaxColumnIncluded[this]; }
            set { Fields.CompanyInvoiceTaxColumnIncluded[this] = value; }
        }

        [DisplayName("Company Quotation Discounted Price Included"), Expression("jCompany.[QuotationDiscountedPriceIncluded]")]
        public Boolean? CompanyQuotationDiscountedPriceIncluded
        {
            get { return Fields.CompanyQuotationDiscountedPriceIncluded[this]; }
            set { Fields.CompanyQuotationDiscountedPriceIncluded[this] = value; }
        }

        [DisplayName("Company Invoice Discounted Price Included"), Expression("jCompany.[InvoiceDiscountedPriceIncluded]")]
        public Boolean? CompanyInvoiceDiscountedPriceIncluded
        {
            get { return Fields.CompanyInvoiceDiscountedPriceIncluded[this]; }
            set { Fields.CompanyInvoiceDiscountedPriceIncluded[this] = value; }
        }

        [DisplayName("Company Roundup In Quotation"), Expression("jCompany.[RoundupInQuotation]")]
        public Boolean? CompanyRoundupInQuotation
        {
            get { return Fields.CompanyRoundupInQuotation[this]; }
            set { Fields.CompanyRoundupInQuotation[this] = value; }
        }

        [DisplayName("Company Quotation Contact Included"), Expression("jCompany.[QuotationContactIncluded]")]
        public Boolean? CompanyQuotationContactIncluded
        {
            get { return Fields.CompanyQuotationContactIncluded[this]; }
            set { Fields.CompanyQuotationContactIncluded[this] = value; }
        }

        [DisplayName("Company Company Type"), Expression("jCompany.[CompanyType]")]
        public Int32? CompanyCompanyType
        {
            get { return Fields.CompanyCompanyType[this]; }
            set { Fields.CompanyCompanyType[this] = value; }
        }

        [DisplayName("Company Enquiry Suffix"), Expression("jCompany.[EnquirySuffix]")]
        public String CompanyEnquirySuffix
        {
            get { return Fields.CompanyEnquirySuffix[this]; }
            set { Fields.CompanyEnquirySuffix[this] = value; }
        }

        [DisplayName("Company Enquiry Prefix"), Expression("jCompany.[EnquiryPrefix]")]
        public String CompanyEnquiryPrefix
        {
            get { return Fields.CompanyEnquiryPrefix[this]; }
            set { Fields.CompanyEnquiryPrefix[this] = value; }
        }

        [DisplayName("Company Country Mandatory"), Expression("jCompany.[CountryMandatory]")]
        public Boolean? CompanyCountryMandatory
        {
            get { return Fields.CompanyCountryMandatory[this]; }
            set { Fields.CompanyCountryMandatory[this] = value; }
        }

        [DisplayName("Company Pincode Mandatory"), Expression("jCompany.[PincodeMandatory]")]
        public Boolean? CompanyPincodeMandatory
        {
            get { return Fields.CompanyPincodeMandatory[this]; }
            set { Fields.CompanyPincodeMandatory[this] = value; }
        }

        [DisplayName("Company City Mandatory"), Expression("jCompany.[CityMandatory]")]
        public Boolean? CompanyCityMandatory
        {
            get { return Fields.CompanyCityMandatory[this]; }
            set { Fields.CompanyCityMandatory[this] = value; }
        }

        [DisplayName("Company Capacity In Products"), Expression("jCompany.[CapacityInProducts]")]
        public Boolean? CompanyCapacityInProducts
        {
            get { return Fields.CompanyCapacityInProducts[this]; }
            set { Fields.CompanyCapacityInProducts[this] = value; }
        }

        [DisplayName("Company Year In Prefix"), Expression("jCompany.[YearInPrefix]")]
        public Int32? CompanyYearInPrefix
        {
            get { return Fields.CompanyYearInPrefix[this]; }
            set { Fields.CompanyYearInPrefix[this] = value; }
        }

        [DisplayName("Company Invoice Header Content"), Expression("jCompany.[InvoiceHeaderContent]")]
        public String CompanyInvoiceHeaderContent
        {
            get { return Fields.CompanyInvoiceHeaderContent[this]; }
            set { Fields.CompanyInvoiceHeaderContent[this] = value; }
        }

        [DisplayName("Company Invoice Footer Content"), Expression("jCompany.[InvoiceFooterContent]")]
        public String CompanyInvoiceFooterContent
        {
            get { return Fields.CompanyInvoiceFooterContent[this]; }
            set { Fields.CompanyInvoiceFooterContent[this] = value; }
        }

        [DisplayName("Company Invoice Header Image"), Expression("jCompany.[InvoiceHeaderImage]")]
        public String CompanyInvoiceHeaderImage
        {
            get { return Fields.CompanyInvoiceHeaderImage[this]; }
            set { Fields.CompanyInvoiceHeaderImage[this] = value; }
        }

        [DisplayName("Company Invoice Header Height"), Expression("jCompany.[InvoiceHeaderHeight]")]
        public Int32? CompanyInvoiceHeaderHeight
        {
            get { return Fields.CompanyInvoiceHeaderHeight[this]; }
            set { Fields.CompanyInvoiceHeaderHeight[this] = value; }
        }

        [DisplayName("Company Invoice Header Width"), Expression("jCompany.[InvoiceHeaderWidth]")]
        public Int32? CompanyInvoiceHeaderWidth
        {
            get { return Fields.CompanyInvoiceHeaderWidth[this]; }
            set { Fields.CompanyInvoiceHeaderWidth[this] = value; }
        }

        [DisplayName("Company Invoice Footer Image"), Expression("jCompany.[InvoiceFooterImage]")]
        public String CompanyInvoiceFooterImage
        {
            get { return Fields.CompanyInvoiceFooterImage[this]; }
            set { Fields.CompanyInvoiceFooterImage[this] = value; }
        }

        [DisplayName("Company Invoice Footer Height"), Expression("jCompany.[InvoiceFooterHeight]")]
        public Int32? CompanyInvoiceFooterHeight
        {
            get { return Fields.CompanyInvoiceFooterHeight[this]; }
            set { Fields.CompanyInvoiceFooterHeight[this] = value; }
        }

        [DisplayName("Company Invoice Footer Width"), Expression("jCompany.[InvoiceFooterWidth]")]
        public Int32? CompanyInvoiceFooterWidth
        {
            get { return Fields.CompanyInvoiceFooterWidth[this]; }
            set { Fields.CompanyInvoiceFooterWidth[this] = value; }
        }

        [DisplayName("Company Dc Header Content"), Expression("jCompany.[DCHeaderContent]")]
        public String CompanyDcHeaderContent
        {
            get { return Fields.CompanyDcHeaderContent[this]; }
            set { Fields.CompanyDcHeaderContent[this] = value; }
        }

        [DisplayName("Company Dc Footer Content"), Expression("jCompany.[DCFooterContent]")]
        public String CompanyDcFooterContent
        {
            get { return Fields.CompanyDcFooterContent[this]; }
            set { Fields.CompanyDcFooterContent[this] = value; }
        }

        [DisplayName("Company Dc Header Image"), Expression("jCompany.[DCHeaderImage]")]
        public String CompanyDcHeaderImage
        {
            get { return Fields.CompanyDcHeaderImage[this]; }
            set { Fields.CompanyDcHeaderImage[this] = value; }
        }

        [DisplayName("Company Dc Header Height"), Expression("jCompany.[DCHeaderHeight]")]
        public Int32? CompanyDcHeaderHeight
        {
            get { return Fields.CompanyDcHeaderHeight[this]; }
            set { Fields.CompanyDcHeaderHeight[this] = value; }
        }

        [DisplayName("Company Dc Header Width"), Expression("jCompany.[DCHeaderWidth]")]
        public Int32? CompanyDcHeaderWidth
        {
            get { return Fields.CompanyDcHeaderWidth[this]; }
            set { Fields.CompanyDcHeaderWidth[this] = value; }
        }

        [DisplayName("Company Dc Footer Image"), Expression("jCompany.[DCFooterImage]")]
        public String CompanyDcFooterImage
        {
            get { return Fields.CompanyDcFooterImage[this]; }
            set { Fields.CompanyDcFooterImage[this] = value; }
        }

        [DisplayName("Company Dc Footer Height"), Expression("jCompany.[DCFooterHeight]")]
        public Int32? CompanyDcFooterHeight
        {
            get { return Fields.CompanyDcFooterHeight[this]; }
            set { Fields.CompanyDcFooterHeight[this] = value; }
        }

        [DisplayName("Company Dc Footer Width"), Expression("jCompany.[DCFooterWidth]")]
        public Int32? CompanyDcFooterWidth
        {
            get { return Fields.CompanyDcFooterWidth[this]; }
            set { Fields.CompanyDcFooterWidth[this] = value; }
        }

        [DisplayName("Company Addinfo2"), Expression("jCompany.[Addinfo2]")]
        public Boolean? CompanyAddinfo2
        {
            get { return Fields.CompanyAddinfo2[this]; }
            set { Fields.CompanyAddinfo2[this] = value; }
        }

        [DisplayName("Company Multi Add Info"), Expression("jCompany.[MultiAddInfo]")]
        public Boolean? CompanyMultiAddInfo
        {
            get { return Fields.CompanyMultiAddInfo[this]; }
            set { Fields.CompanyMultiAddInfo[this] = value; }
        }

        [DisplayName("Company Quotation Total"), Expression("jCompany.[QuotationTotal]")]
        public Boolean? CompanyQuotationTotal
        {
            get { return Fields.CompanyQuotationTotal[this]; }
            set { Fields.CompanyQuotationTotal[this] = value; }
        }

        [DisplayName("Company Valid Date"), Expression("jCompany.[ValidDate]")]
        public Boolean? CompanyValidDate
        {
            get { return Fields.CompanyValidDate[this]; }
            set { Fields.CompanyValidDate[this] = value; }
        }

        [DisplayName("Company Dealer In Cms"), Expression("jCompany.[DealerInCMS]")]
        public Boolean? CompanyDealerInCms
        {
            get { return Fields.CompanyDealerInCms[this]; }
            set { Fields.CompanyDealerInCms[this] = value; }
        }

        [DisplayName("Company Service Person"), Expression("jCompany.[ServicePerson]")]
        public Boolean? CompanyServicePerson
        {
            get { return Fields.CompanyServicePerson[this]; }
            set { Fields.CompanyServicePerson[this] = value; }
        }

        [DisplayName("Company Enq Start No"), Expression("jCompany.[EnqStartNo]")]
        public Int32? CompanyEnqStartNo
        {
            get { return Fields.CompanyEnqStartNo[this]; }
            set { Fields.CompanyEnqStartNo[this] = value; }
        }

        [DisplayName("Company Enq Edit No"), Expression("jCompany.[EnqEditNo]")]
        public Boolean? CompanyEnqEditNo
        {
            get { return Fields.CompanyEnqEditNo[this]; }
            set { Fields.CompanyEnqEditNo[this] = value; }
        }

        [DisplayName("Company Quo Start No"), Expression("jCompany.[QuoStartNo]")]
        public Int32? CompanyQuoStartNo
        {
            get { return Fields.CompanyQuoStartNo[this]; }
            set { Fields.CompanyQuoStartNo[this] = value; }
        }

        [DisplayName("Company Quo Edit No"), Expression("jCompany.[QuoEditNo]")]
        public Boolean? CompanyQuoEditNo
        {
            get { return Fields.CompanyQuoEditNo[this]; }
            set { Fields.CompanyQuoEditNo[this] = value; }
        }

        [DisplayName("Company Inv Start No"), Expression("jCompany.[InvStartNo]")]
        public Int32? CompanyInvStartNo
        {
            get { return Fields.CompanyInvStartNo[this]; }
            set { Fields.CompanyInvStartNo[this] = value; }
        }

        [DisplayName("Company Inv Edit No"), Expression("jCompany.[InvEditNo]")]
        public Boolean? CompanyInvEditNo
        {
            get { return Fields.CompanyInvEditNo[this]; }
            set { Fields.CompanyInvEditNo[this] = value; }
        }

        [DisplayName("Company Dc Start No"), Expression("jCompany.[DCStartNo]")]
        public Int32? CompanyDcStartNo
        {
            get { return Fields.CompanyDcStartNo[this]; }
            set { Fields.CompanyDcStartNo[this] = value; }
        }

        [DisplayName("Company Dc Edit No"), Expression("jCompany.[DCEditNo]")]
        public Boolean? CompanyDcEditNo
        {
            get { return Fields.CompanyDcEditNo[this]; }
            set { Fields.CompanyDcEditNo[this] = value; }
        }

        [DisplayName("Company Dealer In Enquiry"), Expression("jCompany.[DealerInEnquiry]")]
        public Boolean? CompanyDealerInEnquiry
        {
            get { return Fields.CompanyDealerInEnquiry[this]; }
            set { Fields.CompanyDealerInEnquiry[this] = value; }
        }

        [DisplayName("Company Dealer In Quotation"), Expression("jCompany.[DealerInQuotation]")]
        public Boolean? CompanyDealerInQuotation
        {
            get { return Fields.CompanyDealerInQuotation[this]; }
            set { Fields.CompanyDealerInQuotation[this] = value; }
        }

        [DisplayName("Company Dealer In Sales"), Expression("jCompany.[DealerInSales]")]
        public Boolean? CompanyDealerInSales
        {
            get { return Fields.CompanyDealerInSales[this]; }
            set { Fields.CompanyDealerInSales[this] = value; }
        }

        [DisplayName("Company Dealer In Invoice"), Expression("jCompany.[DealerInInvoice]")]
        public Boolean? CompanyDealerInInvoice
        {
            get { return Fields.CompanyDealerInInvoice[this]; }
            set { Fields.CompanyDealerInInvoice[this] = value; }
        }

        [DisplayName("Company Project In Cms"), Expression("jCompany.[ProjectInCMS]")]
        public Boolean? CompanyProjectInCms
        {
            get { return Fields.CompanyProjectInCms[this]; }
            set { Fields.CompanyProjectInCms[this] = value; }
        }

        [DisplayName("Company Cms Start No"), Expression("jCompany.[CMSStartNo]")]
        public Int32? CompanyCmsStartNo
        {
            get { return Fields.CompanyCmsStartNo[this]; }
            set { Fields.CompanyCmsStartNo[this] = value; }
        }

        [DisplayName("Company Cms Edit No"), Expression("jCompany.[CMSEditNo]")]
        public Boolean? CompanyCmsEditNo
        {
            get { return Fields.CompanyCmsEditNo[this]; }
            set { Fields.CompanyCmsEditNo[this] = value; }
        }

        [DisplayName("Company Cms Suffix"), Expression("jCompany.[CMSSuffix]")]
        public String CompanyCmsSuffix
        {
            get { return Fields.CompanyCmsSuffix[this]; }
            set { Fields.CompanyCmsSuffix[this] = value; }
        }

        [DisplayName("Company Cm Sprefix"), Expression("jCompany.[CMSprefix]")]
        public String CompanyCmSprefix
        {
            get { return Fields.CompanyCmSprefix[this]; }
            set { Fields.CompanyCmSprefix[this] = value; }
        }

        [DisplayName("Company Products In Cms"), Expression("jCompany.[ProductsInCMS]")]
        public Boolean? CompanyProductsInCms
        {
            get { return Fields.CompanyProductsInCms[this]; }
            set { Fields.CompanyProductsInCms[this] = value; }
        }

        [DisplayName("Company Remove Purchase Date"), Expression("jCompany.[RemovePurchaseDate]")]
        public Boolean? CompanyRemovePurchaseDate
        {
            get { return Fields.CompanyRemovePurchaseDate[this]; }
            set { Fields.CompanyRemovePurchaseDate[this] = value; }
        }

        [DisplayName("Company Remove Invoice No"), Expression("jCompany.[RemoveInvoiceNo]")]
        public Boolean? CompanyRemoveInvoiceNo
        {
            get { return Fields.CompanyRemoveInvoiceNo[this]; }
            set { Fields.CompanyRemoveInvoiceNo[this] = value; }
        }

        [DisplayName("Company Remove Gt Column"), Expression("jCompany.[RemoveGTColumn]")]
        public Boolean? CompanyRemoveGtColumn
        {
            get { return Fields.CompanyRemoveGtColumn[this]; }
            set { Fields.CompanyRemoveGtColumn[this] = value; }
        }

        [DisplayName("Company Mail To Sub Contacts"), Expression("jCompany.[MailToSubContacts]")]
        public Boolean? CompanyMailToSubContacts
        {
            get { return Fields.CompanyMailToSubContacts[this]; }
            set { Fields.CompanyMailToSubContacts[this] = value; }
        }

        [DisplayName("Company Mail To Organisation"), Expression("jCompany.[MailToOrganisation]")]
        public Boolean? CompanyMailToOrganisation
        {
            get { return Fields.CompanyMailToOrganisation[this]; }
            set { Fields.CompanyMailToOrganisation[this] = value; }
        }

       

      

        public BizMailIdiaMartRow()
            : base(Fields)
        {
        }
        
        public BizMailIdiaMartRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field Rule;
            public Int32Field BmListId;
            public BooleanField Status;
            public Int32Field CompanyId;
            public StringField Description;

            public StringField BmListListId;
            public StringField BmListCompanyName;
            public StringField BmListName;
            public StringField BmListCity;
            public StringField BmListDisplayName;
            public StringField BmListDescription;
            public StringField BmListFrom;
            public StringField BmListReplyTo;

            public StringField CompanyName;
            public StringField CompanySlogan;
            public StringField CompanyAddress;
            public StringField CompanyPhone;
            public StringField CompanyLogo;
            public Int32Field CompanyLogoHeight;
            public Int32Field CompanyLogoWidth;
            public StringField CompanyHeaderImage;
            public Int32Field CompanyHeaderHeight;
            public Int32Field CompanyHeaderWidth;
            public StringField CompanyFooterImage;
            public Int32Field CompanyFooterHeight;
            public Int32Field CompanyFooterWidth;
            public StringField CompanyGstin;
            public StringField CompanyPanNo;
            public BooleanField CompanyEnquiryFollwupMandatory;
            public BooleanField CompanyQuotationFollwupMandatory;
            public BooleanField CompanyEnquiryProductsMandatory;
            public BooleanField CompanyQuotationProductsMandatory;
            public BooleanField CompanyRoundupInSales;
            public BooleanField CompanyPackagingInSales;
            public BooleanField CompanyFreightInSales;
            public BooleanField CompanyDueDateInSales;
            public BooleanField CompanyDispatchInSales;
            public BooleanField CompanyGstDetailsInSales;
            public BooleanField CompanyFollowupsInSales;
            public BooleanField CompanyTermsInSales;
            public BooleanField CompanyAdvanceInSales;
            public BooleanField CompanyStageInSales;
            public BooleanField CompanyCodeInSales;
            public BooleanField CompanySerialInSales;
            public BooleanField CompanyBatchInSales;
            public BooleanField CompanyDiscountInSales;
            public BooleanField CompanyTaxInSales;
            public BooleanField CompanyWarrantyInSales;
            public BooleanField CompanyDescriptionInSales;
            public BooleanField CompanyAppointmentInEnquiry;
            public BooleanField CompanyAppointmentInQuotation;
            public BooleanField CompanyAppointmentInProforma;
            public BooleanField CompanyAppointmentInSales;
            public BooleanField CompanyAppointmentInTeleCalling;
            public BooleanField CompanyRequirementInEnquiry;
            public BooleanField CompanyTaxInStockTransfer;
            public BooleanField CompanyPhoneCompulsory;
            public BooleanField CompanyEmailCompulsory;
            public BooleanField CompanyAutoSmsAppointments;
            public BooleanField CompanyAllowMovingNonClosedRecords;
            public BooleanField CompanyStateCompulsoryInContacts;
            public BooleanField CompanyEnableAddressInTransactions;
            public BooleanField CompanyAutoEmailEnquiry;
            public BooleanField CompanyAutoSmsEnquiry;
            public BooleanField CompanyAutoEmailQuotation;
            public BooleanField CompanyAutoSmsQuotation;
            public BooleanField CompanyAutoEmailProforma;
            public BooleanField CompanyAutoSmsProforma;
            public BooleanField CompanyAutoEmailInvoice;
            public BooleanField CompanyAutoSmsInvoice;
            public BooleanField CompanyHideDescriptionInSales;
            public BooleanField CompanyHideDescriptionInProforma;
            public BooleanField CompanyHideDescriptionInChallan;
            public Int32Field CompanyQuotationTemplate;
            public Int32Field CompanyCountry;
            public BooleanField CompanyMultiCurrency;
            public BooleanField CompanyProjectWithContacts;
            public Int32Field CompanyStateId;
            public BooleanField CompanyRoundupInPurchase;
            public Int32Field CompanyInvoiceTemplate;
            public BooleanField CompanyCompanyDetails;
            public BooleanField CompanyEnableAdditionalCharges;
            public BooleanField CompanyEnableAdditionalConcessions;
            public StringField CompanyAdditionalImages;
            public StringField CompanyHeaderContent;
            public StringField CompanyFooterContent;
            public StringField CompanyQuotationSuffix;
            public StringField CompanyQuotationPrefix;
            public StringField CompanyInvoiceSuffix;
            public StringField CompanyInvoicePrefix;
            public StringField CompanyChallanSuffix;
            public StringField CompanyChallanPrefix;
            public BooleanField CompanyQuotationTaxColumnIncluded;
            public BooleanField CompanyInvoiceTaxColumnIncluded;
            public BooleanField CompanyQuotationDiscountedPriceIncluded;
            public BooleanField CompanyInvoiceDiscountedPriceIncluded;
            public BooleanField CompanyRoundupInQuotation;
            public BooleanField CompanyQuotationContactIncluded;
            public Int32Field CompanyCompanyType;
            public StringField CompanyEnquirySuffix;
            public StringField CompanyEnquiryPrefix;
            public BooleanField CompanyCountryMandatory;
            public BooleanField CompanyPincodeMandatory;
            public BooleanField CompanyCityMandatory;
            public BooleanField CompanyCapacityInProducts;
            public Int32Field CompanyYearInPrefix;
            public StringField CompanyInvoiceHeaderContent;
            public StringField CompanyInvoiceFooterContent;
            public StringField CompanyInvoiceHeaderImage;
            public Int32Field CompanyInvoiceHeaderHeight;
            public Int32Field CompanyInvoiceHeaderWidth;
            public StringField CompanyInvoiceFooterImage;
            public Int32Field CompanyInvoiceFooterHeight;
            public Int32Field CompanyInvoiceFooterWidth;
            public StringField CompanyDcHeaderContent;
            public StringField CompanyDcFooterContent;
            public StringField CompanyDcHeaderImage;
            public Int32Field CompanyDcHeaderHeight;
            public Int32Field CompanyDcHeaderWidth;
            public StringField CompanyDcFooterImage;
            public Int32Field CompanyDcFooterHeight;
            public Int32Field CompanyDcFooterWidth;
            public BooleanField CompanyAddinfo2;
            public BooleanField CompanyMultiAddInfo;
            public BooleanField CompanyQuotationTotal;
            public BooleanField CompanyValidDate;
            public BooleanField CompanyDealerInCms;
            public BooleanField CompanyServicePerson;
            public Int32Field CompanyEnqStartNo;
            public BooleanField CompanyEnqEditNo;
            public Int32Field CompanyQuoStartNo;
            public BooleanField CompanyQuoEditNo;
            public Int32Field CompanyInvStartNo;
            public BooleanField CompanyInvEditNo;
            public Int32Field CompanyDcStartNo;
            public BooleanField CompanyDcEditNo;
            public BooleanField CompanyDealerInEnquiry;
            public BooleanField CompanyDealerInQuotation;
            public BooleanField CompanyDealerInSales;
            public BooleanField CompanyDealerInInvoice;
            public BooleanField CompanyProjectInCms;
            public Int32Field CompanyCmsStartNo;
            public BooleanField CompanyCmsEditNo;
            public StringField CompanyCmsSuffix;
            public StringField CompanyCmSprefix;
            public BooleanField CompanyProductsInCms;
            public BooleanField CompanyRemovePurchaseDate;
            public BooleanField CompanyRemoveInvoiceNo;
            public BooleanField CompanyRemoveGtColumn;
            public BooleanField CompanyMailToSubContacts;
            public BooleanField CompanyMailToOrganisation;
        }
    }
}
