namespace AdvanceCRM.BizMail {
    export interface BizMailTradeIdiaRow {
        Id?: number;
        Rule?: Masters.BizMailRulesMaster;
        BmListId?: number;
        Status?: boolean;
        CompanyId?: number;
        Description?: string;
        BmListListId?: string;
        BmListCompanyName?: string;
        BmListName?: string;
        BmListCity?: string;
        BmListDisplayName?: string;
        BmListDescription?: string;
        BmListFrom?: string;
        BmListReplyTo?: string;
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
        CompanyInvoiceHeaderContent?: string;
        CompanyInvoiceFooterContent?: string;
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
        CompanyEnqStartNo?: number;
        CompanyEnqEditNo?: boolean;
        CompanyQuoStartNo?: number;
        CompanyQuoEditNo?: boolean;
        CompanyInvStartNo?: number;
        CompanyInvEditNo?: boolean;
        CompanyDcStartNo?: number;
        CompanyDcEditNo?: boolean;
        CompanyDealerInEnquiry?: boolean;
        CompanyDealerInQuotation?: boolean;
        CompanyDealerInSales?: boolean;
        CompanyDealerInInvoice?: boolean;
        CompanyProjectInCms?: boolean;
        CompanyCmsStartNo?: number;
        CompanyCmsEditNo?: boolean;
        CompanyCmsSuffix?: string;
        CompanyCmSprefix?: string;
        CompanyProductsInCms?: boolean;
        CompanyRemovePurchaseDate?: boolean;
        CompanyRemoveInvoiceNo?: boolean;
        CompanyRemoveGtColumn?: boolean;
        CompanyMailToSubContacts?: boolean;
        CompanyMailToOrganisation?: boolean;
    }

    export namespace BizMailTradeIdiaRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Description';
        export const localTextPrefix = 'BizMail.BizMailTradeIdia';
        export const deletePermission = 'BizMail:Delete';
        export const insertPermission = 'BizMail:Insert';
        export const readPermission = 'BizMail:Read';
        export const updatePermission = 'BizMail:Update';

        export declare const enum Fields {
            Id = "Id",
            Rule = "Rule",
            BmListId = "BmListId",
            Status = "Status",
            CompanyId = "CompanyId",
            Description = "Description",
            BmListListId = "BmListListId",
            BmListCompanyName = "BmListCompanyName",
            BmListName = "BmListName",
            BmListCity = "BmListCity",
            BmListDisplayName = "BmListDisplayName",
            BmListDescription = "BmListDescription",
            BmListFrom = "BmListFrom",
            BmListReplyTo = "BmListReplyTo",
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
            CompanyInvoiceHeaderContent = "CompanyInvoiceHeaderContent",
            CompanyInvoiceFooterContent = "CompanyInvoiceFooterContent",
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
            CompanyEnqStartNo = "CompanyEnqStartNo",
            CompanyEnqEditNo = "CompanyEnqEditNo",
            CompanyQuoStartNo = "CompanyQuoStartNo",
            CompanyQuoEditNo = "CompanyQuoEditNo",
            CompanyInvStartNo = "CompanyInvStartNo",
            CompanyInvEditNo = "CompanyInvEditNo",
            CompanyDcStartNo = "CompanyDcStartNo",
            CompanyDcEditNo = "CompanyDcEditNo",
            CompanyDealerInEnquiry = "CompanyDealerInEnquiry",
            CompanyDealerInQuotation = "CompanyDealerInQuotation",
            CompanyDealerInSales = "CompanyDealerInSales",
            CompanyDealerInInvoice = "CompanyDealerInInvoice",
            CompanyProjectInCms = "CompanyProjectInCms",
            CompanyCmsStartNo = "CompanyCmsStartNo",
            CompanyCmsEditNo = "CompanyCmsEditNo",
            CompanyCmsSuffix = "CompanyCmsSuffix",
            CompanyCmSprefix = "CompanyCmSprefix",
            CompanyProductsInCms = "CompanyProductsInCms",
            CompanyRemovePurchaseDate = "CompanyRemovePurchaseDate",
            CompanyRemoveInvoiceNo = "CompanyRemoveInvoiceNo",
            CompanyRemoveGtColumn = "CompanyRemoveGtColumn",
            CompanyMailToSubContacts = "CompanyMailToSubContacts",
            CompanyMailToOrganisation = "CompanyMailToOrganisation"
        }
    }
}
