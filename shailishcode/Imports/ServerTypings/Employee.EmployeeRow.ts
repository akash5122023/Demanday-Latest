namespace AdvanceCRM.Employee {
    export interface EmployeeRow {
        Id?: number;
        EmpCode?: string;
        DepartmentId?: number;
        Name?: string;
        Phone?: string;
        Email?: string;
        Address?: string;
        ProfessionalEmail?: string;
        CityId?: number;
        StateId?: number;
        Pin?: string;
        Country?: Masters.CountryMaster;
        AdditionalInfo?: string;
        Gender?: Masters.GenderMaster;
        Religion?: Masters.ReligionMaster;
        AreaId?: number;
        MaritalStatus?: Masters.MaritalMaster;
        MarriageAnniversary?: string;
        Birthdate?: string;
        DateOfJoining?: string;
        CompanyId?: number;
        RolesId?: number;
        OwnerId?: number;
        AdhaarNo?: string;
        PanNo?: string;
        Attachment?: string;
        BankName?: string;
        AccountNumber?: string;
        Ifsc?: string;
        BankType?: string;
        Branch?: string;
        TehsilId?: number;
        VillageId?: number;
        Department?: string;
        City?: string;
        CityStateId?: number;
        State?: string;
        Area?: string;
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
        RolesRoleName?: string;
        RolesCompanyId?: number;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
        OwnerEmail?: string;
        OwnerSource?: string;
        OwnerPasswordHash?: string;
        OwnerPasswordSalt?: string;
        OwnerLastDirectoryUpdate?: string;
        OwnerUserImage?: string;
        OwnerInsertDate?: string;
        OwnerInsertUserId?: number;
        OwnerUpdateDate?: string;
        OwnerUpdateUserId?: number;
        OwnerIsActive?: number;
        OwnerUpperLevel?: number;
        OwnerUpperLevel2?: number;
        OwnerUpperLevel3?: number;
        OwnerUpperLevel4?: number;
        OwnerUpperLevel5?: number;
        OwnerHost?: string;
        OwnerPort?: number;
        OwnerSsl?: boolean;
        OwnerEmailId?: string;
        OwnerEmailPassword?: string;
        OwnerPhone?: string;
        OwnerMcsmtpServer?: string;
        OwnerMcsmtpPort?: number;
        OwnerMcimapServer?: string;
        OwnerMcimapPort?: number;
        OwnerMcUsername?: string;
        OwnerMcPassword?: string;
        OwnerStartTime?: string;
        OwnerEndTime?: string;
        OwnerUid?: string;
        OwnerNonOperational?: boolean;
        OwnerBranchId?: number;
        OwnerCompanyId?: number;
        Tehsil?: string;
        TehsilStateId?: number;
        TehsilCityId?: number;
        Village?: string;
        VillageStateId?: number;
        VillageCityId?: number;
        VillageTehsilId?: number;
        VillagePin?: string;
    }

    export namespace EmployeeRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Employee.Employee';
        export const lookupKey = 'Employee.Employee';

        export function getLookup(): Q.Lookup<EmployeeRow> {
            return Q.getLookup<EmployeeRow>('Employee.Employee');
        }
        export const deletePermission = 'Employee:Delete';
        export const insertPermission = 'Employee:Insert';
        export const readPermission = 'Employee:Read';
        export const updatePermission = 'Employee:Update';

        export declare const enum Fields {
            Id = "Id",
            EmpCode = "EmpCode",
            DepartmentId = "DepartmentId",
            Name = "Name",
            Phone = "Phone",
            Email = "Email",
            Address = "Address",
            ProfessionalEmail = "ProfessionalEmail",
            CityId = "CityId",
            StateId = "StateId",
            Pin = "Pin",
            Country = "Country",
            AdditionalInfo = "AdditionalInfo",
            Gender = "Gender",
            Religion = "Religion",
            AreaId = "AreaId",
            MaritalStatus = "MaritalStatus",
            MarriageAnniversary = "MarriageAnniversary",
            Birthdate = "Birthdate",
            DateOfJoining = "DateOfJoining",
            CompanyId = "CompanyId",
            RolesId = "RolesId",
            OwnerId = "OwnerId",
            AdhaarNo = "AdhaarNo",
            PanNo = "PanNo",
            Attachment = "Attachment",
            BankName = "BankName",
            AccountNumber = "AccountNumber",
            Ifsc = "Ifsc",
            BankType = "BankType",
            Branch = "Branch",
            TehsilId = "TehsilId",
            VillageId = "VillageId",
            Department = "Department",
            City = "City",
            CityStateId = "CityStateId",
            State = "State",
            Area = "Area",
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
            RolesRoleName = "RolesRoleName",
            RolesCompanyId = "RolesCompanyId",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName",
            OwnerEmail = "OwnerEmail",
            OwnerSource = "OwnerSource",
            OwnerPasswordHash = "OwnerPasswordHash",
            OwnerPasswordSalt = "OwnerPasswordSalt",
            OwnerLastDirectoryUpdate = "OwnerLastDirectoryUpdate",
            OwnerUserImage = "OwnerUserImage",
            OwnerInsertDate = "OwnerInsertDate",
            OwnerInsertUserId = "OwnerInsertUserId",
            OwnerUpdateDate = "OwnerUpdateDate",
            OwnerUpdateUserId = "OwnerUpdateUserId",
            OwnerIsActive = "OwnerIsActive",
            OwnerUpperLevel = "OwnerUpperLevel",
            OwnerUpperLevel2 = "OwnerUpperLevel2",
            OwnerUpperLevel3 = "OwnerUpperLevel3",
            OwnerUpperLevel4 = "OwnerUpperLevel4",
            OwnerUpperLevel5 = "OwnerUpperLevel5",
            OwnerHost = "OwnerHost",
            OwnerPort = "OwnerPort",
            OwnerSsl = "OwnerSsl",
            OwnerEmailId = "OwnerEmailId",
            OwnerEmailPassword = "OwnerEmailPassword",
            OwnerPhone = "OwnerPhone",
            OwnerMcsmtpServer = "OwnerMcsmtpServer",
            OwnerMcsmtpPort = "OwnerMcsmtpPort",
            OwnerMcimapServer = "OwnerMcimapServer",
            OwnerMcimapPort = "OwnerMcimapPort",
            OwnerMcUsername = "OwnerMcUsername",
            OwnerMcPassword = "OwnerMcPassword",
            OwnerStartTime = "OwnerStartTime",
            OwnerEndTime = "OwnerEndTime",
            OwnerUid = "OwnerUid",
            OwnerNonOperational = "OwnerNonOperational",
            OwnerBranchId = "OwnerBranchId",
            OwnerCompanyId = "OwnerCompanyId",
            Tehsil = "Tehsil",
            TehsilStateId = "TehsilStateId",
            TehsilCityId = "TehsilCityId",
            Village = "Village",
            VillageStateId = "VillageStateId",
            VillageCityId = "VillageCityId",
            VillageTehsilId = "VillageTehsilId",
            VillagePin = "VillagePin"
        }
    }
}
