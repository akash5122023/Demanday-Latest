namespace AdvanceCRM.ThirdParty {
    export interface InstamojoDetailsRow {
        Id?: number;
        Name?: string;
        Phone?: string;
        Address?: string;
        Email?: string;
        Purpose?: string;
        PaymentMode?: string;
        Status?: string;
        PayoutDate?: string;
        IsMoved?: boolean;
        InstaId?: string;
        CompanyId?: number;
        CompanyName?: string;
        CompanySlogan?: string;
        CompanyAddress?: string;
        CompanyPhone?: string;
        CompanyLogo?: string;
        CompanyLogoHeight?: number;
        CompanyLogoWidth?: number;
        CompanyHeaderImage?: string;
        CompanyHeaderHeight?: number;
        CompanyHeaderWidth?: number;
        CompanyFooterImage?: string;
        CompanyFooterHeight?: number;
        CompanyFooterWidth?: number;
        CompanyGstin?: string;
        CompanyPanNo?: string;
        CompanyEnquiryFollwupMandatory?: boolean;
        CompanyQuotationFollwupMandatory?: boolean;
        CompanyEnquiryProductsMandatory?: boolean;
        CompanyQuotationProductsMandatory?: boolean;
        CompanyRoundupInSales?: boolean;
        CompanyPackagingInSales?: boolean;
        CompanyFreightInSales?: boolean;
        CompanyDueDateInSales?: boolean;
        CompanyDispatchInSales?: boolean;
        CompanyGstDetailsInSales?: boolean;
        CompanyFollowupsInSales?: boolean;
        CompanyTermsInSales?: boolean;
        CompanyAdvanceInSales?: boolean;
        CompanyStageInSales?: boolean;
        CompanyCodeInSales?: boolean;
        CompanySerialInSales?: boolean;
        CompanyBatchInSales?: boolean;
        CompanyDiscountInSales?: boolean;
        CompanyTaxInSales?: boolean;
        CompanyWarrantyInSales?: boolean;
        CompanyDescriptionInSales?: boolean;
        CompanyAppointmentInEnquiry?: boolean;
        CompanyAppointmentInQuotation?: boolean;
        CompanyAppointmentInProforma?: boolean;
        CompanyAppointmentInSales?: boolean;
        CompanyAppointmentInTeleCalling?: boolean;
        CompanyRequirementInEnquiry?: boolean;
        CompanyTaxInStockTransfer?: boolean;
        CompanyPhoneCompulsory?: boolean;
        CompanyEmailCompulsory?: boolean;
        CompanyAutoSmsAppointments?: boolean;
        CompanyAllowMovingNonClosedRecords?: boolean;
        CompanyStateCompulsoryInContacts?: boolean;
        CompanyEnableAddressInTransactions?: boolean;
        CompanyAutoEmailEnquiry?: boolean;
        CompanyAutoSmsEnquiry?: boolean;
        CompanyAutoEmailQuotation?: boolean;
        CompanyAutoSmsQuotation?: boolean;
        CompanyAutoEmailProforma?: boolean;
        CompanyAutoSmsProforma?: boolean;
        CompanyAutoEmailInvoice?: boolean;
        CompanyAutoSmsInvoice?: boolean;
        CompanyHideDescriptionInSales?: boolean;
        CompanyHideDescriptionInProforma?: boolean;
        CompanyHideDescriptionInChallan?: boolean;
        CompanyQuotationTemplate?: number;
        CompanyCountry?: number;
        CompanyMultiCurrency?: boolean;
        CompanyProjectWithContacts?: boolean;
        CompanyStateId?: number;
        CompanyRoundupInPurchase?: boolean;
        CompanyInvoiceTemplate?: number;
        CompanyCompanyDetails?: boolean;
        CompanyEnableAdditionalCharges?: boolean;
        CompanyEnableAdditionalConcessions?: boolean;
        CompanyAdditionalImages?: string;
        CompanyHeaderContent?: string;
        CompanyFooterContent?: string;
        CompanyQuotationSuffix?: string;
        CompanyQuotationPrefix?: string;
        CompanyInvoiceSuffix?: string;
        CompanyInvoicePrefix?: string;
        CompanyChallanSuffix?: string;
        CompanyChallanPrefix?: string;
        CompanyQuotationTaxColumnIncluded?: boolean;
        CompanyInvoiceTaxColumnIncluded?: boolean;
        CompanyQuotationDiscountedPriceIncluded?: boolean;
        CompanyInvoiceDiscountedPriceIncluded?: boolean;
        CompanyRoundupInQuotation?: boolean;
        CompanyQuotationContactIncluded?: boolean;
        CompanyCompanyType?: number;
        CompanyEnquirySuffix?: string;
        CompanyEnquiryPrefix?: string;
        CompanyCountryMandatory?: boolean;
        CompanyPincodeMandatory?: boolean;
        CompanyCityMandatory?: boolean;
        CompanyCapacityInProducts?: boolean;
        CompanyYearInPrefix?: number;
        CompanyInvoiceHeaderImage?: string;
        CompanyInvoiceHeaderHeight?: number;
        CompanyInvoiceHeaderWidth?: number;
        CompanyInvoiceFooterImage?: string;
        CompanyInvoiceFooterHeight?: number;
        CompanyInvoiceFooterWidth?: number;
        CompanyDcHeaderContent?: string;
        CompanyDcFooterContent?: string;
        CompanyDcHeaderImage?: string;
        CompanyDcHeaderHeight?: number;
        CompanyDcHeaderWidth?: number;
        CompanyDcFooterImage?: string;
        CompanyDcFooterHeight?: number;
        CompanyDcFooterWidth?: number;
        CompanyAddinfo2?: boolean;
        CompanyMultiAddInfo?: boolean;
        CompanyQuotationTotal?: boolean;
        CompanyValidDate?: boolean;
        CompanyDealerInCms?: boolean;
        CompanyServicePerson?: boolean;
        CompanyInvoiceHeaderContent?: string;
        CompanyInvoiceFootercontent?: string;
        CompanyStartDate?: string;
        CompanyEndDate?: string;
    }

    export namespace InstamojoDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'ThirdParty.InstamojoDetails';
        export const deletePermission = 'Instamojo:Inbox';
        export const insertPermission = 'Instamojo:Inbox';
        export const readPermission = 'Instamojo:Inbox';
        export const updatePermission = 'Instamojo:Inbox';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Phone = "Phone",
            Address = "Address",
            Email = "Email",
            Purpose = "Purpose",
            PaymentMode = "PaymentMode",
            Status = "Status",
            PayoutDate = "PayoutDate",
            IsMoved = "IsMoved",
            InstaId = "InstaId",
            CompanyId = "CompanyId",
            CompanyName = "CompanyName",
            CompanySlogan = "CompanySlogan",
            CompanyAddress = "CompanyAddress",
            CompanyPhone = "CompanyPhone",
            CompanyLogo = "CompanyLogo",
            CompanyLogoHeight = "CompanyLogoHeight",
            CompanyLogoWidth = "CompanyLogoWidth",
            CompanyHeaderImage = "CompanyHeaderImage",
            CompanyHeaderHeight = "CompanyHeaderHeight",
            CompanyHeaderWidth = "CompanyHeaderWidth",
            CompanyFooterImage = "CompanyFooterImage",
            CompanyFooterHeight = "CompanyFooterHeight",
            CompanyFooterWidth = "CompanyFooterWidth",
            CompanyGstin = "CompanyGstin",
            CompanyPanNo = "CompanyPanNo",
            CompanyEnquiryFollwupMandatory = "CompanyEnquiryFollwupMandatory",
            CompanyQuotationFollwupMandatory = "CompanyQuotationFollwupMandatory",
            CompanyEnquiryProductsMandatory = "CompanyEnquiryProductsMandatory",
            CompanyQuotationProductsMandatory = "CompanyQuotationProductsMandatory",
            CompanyRoundupInSales = "CompanyRoundupInSales",
            CompanyPackagingInSales = "CompanyPackagingInSales",
            CompanyFreightInSales = "CompanyFreightInSales",
            CompanyDueDateInSales = "CompanyDueDateInSales",
            CompanyDispatchInSales = "CompanyDispatchInSales",
            CompanyGstDetailsInSales = "CompanyGstDetailsInSales",
            CompanyFollowupsInSales = "CompanyFollowupsInSales",
            CompanyTermsInSales = "CompanyTermsInSales",
            CompanyAdvanceInSales = "CompanyAdvanceInSales",
            CompanyStageInSales = "CompanyStageInSales",
            CompanyCodeInSales = "CompanyCodeInSales",
            CompanySerialInSales = "CompanySerialInSales",
            CompanyBatchInSales = "CompanyBatchInSales",
            CompanyDiscountInSales = "CompanyDiscountInSales",
            CompanyTaxInSales = "CompanyTaxInSales",
            CompanyWarrantyInSales = "CompanyWarrantyInSales",
            CompanyDescriptionInSales = "CompanyDescriptionInSales",
            CompanyAppointmentInEnquiry = "CompanyAppointmentInEnquiry",
            CompanyAppointmentInQuotation = "CompanyAppointmentInQuotation",
            CompanyAppointmentInProforma = "CompanyAppointmentInProforma",
            CompanyAppointmentInSales = "CompanyAppointmentInSales",
            CompanyAppointmentInTeleCalling = "CompanyAppointmentInTeleCalling",
            CompanyRequirementInEnquiry = "CompanyRequirementInEnquiry",
            CompanyTaxInStockTransfer = "CompanyTaxInStockTransfer",
            CompanyPhoneCompulsory = "CompanyPhoneCompulsory",
            CompanyEmailCompulsory = "CompanyEmailCompulsory",
            CompanyAutoSmsAppointments = "CompanyAutoSmsAppointments",
            CompanyAllowMovingNonClosedRecords = "CompanyAllowMovingNonClosedRecords",
            CompanyStateCompulsoryInContacts = "CompanyStateCompulsoryInContacts",
            CompanyEnableAddressInTransactions = "CompanyEnableAddressInTransactions",
            CompanyAutoEmailEnquiry = "CompanyAutoEmailEnquiry",
            CompanyAutoSmsEnquiry = "CompanyAutoSmsEnquiry",
            CompanyAutoEmailQuotation = "CompanyAutoEmailQuotation",
            CompanyAutoSmsQuotation = "CompanyAutoSmsQuotation",
            CompanyAutoEmailProforma = "CompanyAutoEmailProforma",
            CompanyAutoSmsProforma = "CompanyAutoSmsProforma",
            CompanyAutoEmailInvoice = "CompanyAutoEmailInvoice",
            CompanyAutoSmsInvoice = "CompanyAutoSmsInvoice",
            CompanyHideDescriptionInSales = "CompanyHideDescriptionInSales",
            CompanyHideDescriptionInProforma = "CompanyHideDescriptionInProforma",
            CompanyHideDescriptionInChallan = "CompanyHideDescriptionInChallan",
            CompanyQuotationTemplate = "CompanyQuotationTemplate",
            CompanyCountry = "CompanyCountry",
            CompanyMultiCurrency = "CompanyMultiCurrency",
            CompanyProjectWithContacts = "CompanyProjectWithContacts",
            CompanyStateId = "CompanyStateId",
            CompanyRoundupInPurchase = "CompanyRoundupInPurchase",
            CompanyInvoiceTemplate = "CompanyInvoiceTemplate",
            CompanyCompanyDetails = "CompanyCompanyDetails",
            CompanyEnableAdditionalCharges = "CompanyEnableAdditionalCharges",
            CompanyEnableAdditionalConcessions = "CompanyEnableAdditionalConcessions",
            CompanyAdditionalImages = "CompanyAdditionalImages",
            CompanyHeaderContent = "CompanyHeaderContent",
            CompanyFooterContent = "CompanyFooterContent",
            CompanyQuotationSuffix = "CompanyQuotationSuffix",
            CompanyQuotationPrefix = "CompanyQuotationPrefix",
            CompanyInvoiceSuffix = "CompanyInvoiceSuffix",
            CompanyInvoicePrefix = "CompanyInvoicePrefix",
            CompanyChallanSuffix = "CompanyChallanSuffix",
            CompanyChallanPrefix = "CompanyChallanPrefix",
            CompanyQuotationTaxColumnIncluded = "CompanyQuotationTaxColumnIncluded",
            CompanyInvoiceTaxColumnIncluded = "CompanyInvoiceTaxColumnIncluded",
            CompanyQuotationDiscountedPriceIncluded = "CompanyQuotationDiscountedPriceIncluded",
            CompanyInvoiceDiscountedPriceIncluded = "CompanyInvoiceDiscountedPriceIncluded",
            CompanyRoundupInQuotation = "CompanyRoundupInQuotation",
            CompanyQuotationContactIncluded = "CompanyQuotationContactIncluded",
            CompanyCompanyType = "CompanyCompanyType",
            CompanyEnquirySuffix = "CompanyEnquirySuffix",
            CompanyEnquiryPrefix = "CompanyEnquiryPrefix",
            CompanyCountryMandatory = "CompanyCountryMandatory",
            CompanyPincodeMandatory = "CompanyPincodeMandatory",
            CompanyCityMandatory = "CompanyCityMandatory",
            CompanyCapacityInProducts = "CompanyCapacityInProducts",
            CompanyYearInPrefix = "CompanyYearInPrefix",
            CompanyInvoiceHeaderImage = "CompanyInvoiceHeaderImage",
            CompanyInvoiceHeaderHeight = "CompanyInvoiceHeaderHeight",
            CompanyInvoiceHeaderWidth = "CompanyInvoiceHeaderWidth",
            CompanyInvoiceFooterImage = "CompanyInvoiceFooterImage",
            CompanyInvoiceFooterHeight = "CompanyInvoiceFooterHeight",
            CompanyInvoiceFooterWidth = "CompanyInvoiceFooterWidth",
            CompanyDcHeaderContent = "CompanyDcHeaderContent",
            CompanyDcFooterContent = "CompanyDcFooterContent",
            CompanyDcHeaderImage = "CompanyDcHeaderImage",
            CompanyDcHeaderHeight = "CompanyDcHeaderHeight",
            CompanyDcHeaderWidth = "CompanyDcHeaderWidth",
            CompanyDcFooterImage = "CompanyDcFooterImage",
            CompanyDcFooterHeight = "CompanyDcFooterHeight",
            CompanyDcFooterWidth = "CompanyDcFooterWidth",
            CompanyAddinfo2 = "CompanyAddinfo2",
            CompanyMultiAddInfo = "CompanyMultiAddInfo",
            CompanyQuotationTotal = "CompanyQuotationTotal",
            CompanyValidDate = "CompanyValidDate",
            CompanyDealerInCms = "CompanyDealerInCms",
            CompanyServicePerson = "CompanyServicePerson",
            CompanyInvoiceHeaderContent = "CompanyInvoiceHeaderContent",
            CompanyInvoiceFootercontent = "CompanyInvoiceFootercontent",
            CompanyStartDate = "CompanyStartDate",
            CompanyEndDate = "CompanyEndDate"
        }
    }
}
