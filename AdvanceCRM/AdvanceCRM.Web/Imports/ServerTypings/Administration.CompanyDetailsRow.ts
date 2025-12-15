namespace AdvanceCRM.Administration {
    export interface CompanyDetailsRow {
        Id?: number;
        Name?: string;
        Slogan?: string;
        Address?: string;
        Phone?: string;
        Logo?: string;
        LogoHeight?: number;
        LogoWidth?: number;
        HeaderImage?: string;
        HeaderHeight?: number;
        HeaderWidth?: number;
        FooterImage?: string;
        FooterHeight?: number;
        FooterWidth?: number;
        GSTIN?: string;
        PANNo?: string;
        Addinfo2?: boolean;
        MultiAddInfo?: boolean;
        EnquiryFollwupMandatory?: boolean;
        QuotationFollwupMandatory?: boolean;
        EnquiryProductsMandatory?: boolean;
        QuotationProductsMandatory?: boolean;
        RoundupInSales?: boolean;
        PackagingInSales?: boolean;
        FreightInSales?: boolean;
        DueDateInSales?: boolean;
        DispatchInSales?: boolean;
        GstDetailsInSales?: boolean;
        FollowupsInSales?: boolean;
        TermsInSales?: boolean;
        AdvanceInSales?: boolean;
        StageInSales?: boolean;
        CodeInSales?: boolean;
        SerialInSales?: boolean;
        BatchInSales?: boolean;
        DiscountInSales?: boolean;
        TAXInSales?: boolean;
        WarrantyInSales?: boolean;
        DescriptionInSales?: boolean;
        AppointmentInEnquiry?: boolean;
        AppointmentInQuotation?: boolean;
        AppointmentInProforma?: boolean;
        AppointmentInSales?: boolean;
        AppointmentInTeleCalling?: boolean;
        RequirementInEnquiry?: boolean;
        TaxInStockTransfer?: boolean;
        PhoneCompulsory?: boolean;
        EmailCompulsory?: boolean;
        AutoSMSAppointments?: boolean;
        AllowMovingNonClosedRecords?: boolean;
        StateCompulsoryInContacts?: boolean;
        EnableAddressInTransactions?: boolean;
        AutoEmailEnquiry?: boolean;
        AutoSMSEnquiry?: boolean;
        AutoEmailQuotation?: boolean;
        AutoSMSQuotation?: boolean;
        AutoEmailProforma?: boolean;
        AutoSMSProforma?: boolean;
        AutoEmailInvoice?: boolean;
        AutoSMSInvoice?: boolean;
        HideDescriptionInSales?: boolean;
        HideDescriptionInProforma?: boolean;
        HideDescriptionInChallan?: boolean;
        QuotationTemplate?: Masters.PrintTemplates;
        Country?: Masters.CountryMaster;
        QuotationTotal?: boolean;
        MultiCurrency?: boolean;
        ProjectWithContacts?: boolean;
        StateId?: number;
        RoundupInPurchase?: boolean;
        InvoiceTemplate?: Masters.PrintTemplates;
        CompanyDetails?: boolean;
        EnableAdditionalCharges?: boolean;
        EnableAdditionalConcessions?: boolean;
        AdditionalImages?: string;
        HeaderContent?: string;
        FooterContent?: string;
        QuotationSuffix?: string;
        QuotationPrefix?: string;
        InvoiceSuffix?: string;
        InvoicePrefix?: string;
        ChallanSuffix?: string;
        ChallanPrefix?: string;
        QuotationTaxColumnIncluded?: boolean;
        InvoiceTaxColumnIncluded?: boolean;
        QuotationDiscountedPriceIncluded?: boolean;
        InvoiceDiscountedPriceIncluded?: boolean;
        RoundupInQuotation?: boolean;
        QuotationContactIncluded?: boolean;
        EnquirySuffix?: string;
        EnquiryPrefix?: string;
        CountryMandatory?: boolean;
        PincodeMandatory?: boolean;
        CityMandatory?: boolean;
        CapacityInProducts?: boolean;
        YearInPrefix?: Masters.YearInPrefix;
        InvoiceHeaderContent?: string;
        InvoiceFooterContent?: string;
        InvoiceHeaderImage?: string;
        InvoiceHeaderHeight?: number;
        InvoiceHeaderWidth?: number;
        InvoiceFooterImage?: string;
        InvoiceFooterHeight?: number;
        InvoiceFooterWidth?: number;
        DcHeaderContent?: string;
        DcFooterContent?: string;
        DcHeaderImage?: string;
        DcHeaderHeight?: number;
        DcHeaderWidth?: number;
        DcFooterImage?: string;
        DcFooterHeight?: number;
        DcFooterWidth?: number;
        ValidDate?: number;
        DealerInCms?: number;
        ServicePerson?: number;
        EnqStartNo?: number;
        EnqEditNo?: boolean;
        QuoStartNo?: number;
        RemoveGtColumn?: boolean;
        QuoEditNo?: boolean;
        InvStartNo?: number;
        InvEditNo?: boolean;
        DcStartNo?: number;
        DcEditNo?: boolean;
        DealerInEnquiry?: boolean;
        DealerInQuotation?: boolean;
        DealerInSales?: boolean;
        DealerInInvoice?: boolean;
        ProjectInCms?: boolean;
        CmsStartNo?: number;
        CmsEditNo?: boolean;
        CmsSuffix?: string;
        CmSprefix?: string;
        ProductsInCms?: boolean;
        RemovePurchaseDate?: boolean;
        RemoveInvoiceNo?: boolean;
        MailToSubContacts?: boolean;
        MailToOrganisation?: boolean;
        State?: string;
        ChallanTaxColumnIncluded?: boolean;
        PassportDetails?: boolean;
        TaskTitleInTask?: boolean;
        TaskMasterInTask?: boolean;
        WinPercentageInEnquiry?: boolean;
        ExpectedClosingDateInEnquiry?: boolean;
        WinPercentageInQuotation?: boolean;
        ExpectedClosingDateInQuotation?: boolean;
        HSN?: boolean;
        Code?: boolean;
        Unit?: boolean;
        OpeningStock?: boolean;
        RawMaterial?: boolean;
        Group?: boolean;
        ToInvoice?: boolean;
        ToPerforma?: boolean;
        Capacity?: boolean;
        MRP?: boolean;
        SellingPrice?: boolean;
        Travels?: boolean;
        ItineraryHeaderContent?: string;
        ItineraryFooterContent?: string;
        ItineraryHeaderImage?: string;
        ItineraryHeaderHeight?: number;
        ItineraryHeaderWidth?: number;
        ItineraryFooterImage?: string;
        ItineraryFooterHeight?: number;
        ItineraryFooterWidth?: number;
    }

    export namespace CompanyDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Administration.CompanyDetails';
        export const lookupKey = 'Administration.CompanyDetails';

        export function getLookup(): Q.Lookup<CompanyDetailsRow> {
            return Q.getLookup<CompanyDetailsRow>('Administration.CompanyDetails');
        }
        export const deletePermission = 'Administration:Company';
        export const insertPermission = 'Administration:Company';
        export const readPermission = 'Administration:General';
        export const updatePermission = 'Administration:General';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Slogan = "Slogan",
            Address = "Address",
            Phone = "Phone",
            Logo = "Logo",
            LogoHeight = "LogoHeight",
            LogoWidth = "LogoWidth",
            HeaderImage = "HeaderImage",
            HeaderHeight = "HeaderHeight",
            HeaderWidth = "HeaderWidth",
            FooterImage = "FooterImage",
            FooterHeight = "FooterHeight",
            FooterWidth = "FooterWidth",
            GSTIN = "GSTIN",
            PANNo = "PANNo",
            Addinfo2 = "Addinfo2",
            MultiAddInfo = "MultiAddInfo",
            EnquiryFollwupMandatory = "EnquiryFollwupMandatory",
            QuotationFollwupMandatory = "QuotationFollwupMandatory",
            EnquiryProductsMandatory = "EnquiryProductsMandatory",
            QuotationProductsMandatory = "QuotationProductsMandatory",
            RoundupInSales = "RoundupInSales",
            PackagingInSales = "PackagingInSales",
            FreightInSales = "FreightInSales",
            DueDateInSales = "DueDateInSales",
            DispatchInSales = "DispatchInSales",
            GstDetailsInSales = "GstDetailsInSales",
            FollowupsInSales = "FollowupsInSales",
            TermsInSales = "TermsInSales",
            AdvanceInSales = "AdvanceInSales",
            StageInSales = "StageInSales",
            CodeInSales = "CodeInSales",
            SerialInSales = "SerialInSales",
            BatchInSales = "BatchInSales",
            DiscountInSales = "DiscountInSales",
            TAXInSales = "TAXInSales",
            WarrantyInSales = "WarrantyInSales",
            DescriptionInSales = "DescriptionInSales",
            AppointmentInEnquiry = "AppointmentInEnquiry",
            AppointmentInQuotation = "AppointmentInQuotation",
            AppointmentInProforma = "AppointmentInProforma",
            AppointmentInSales = "AppointmentInSales",
            AppointmentInTeleCalling = "AppointmentInTeleCalling",
            RequirementInEnquiry = "RequirementInEnquiry",
            TaxInStockTransfer = "TaxInStockTransfer",
            PhoneCompulsory = "PhoneCompulsory",
            EmailCompulsory = "EmailCompulsory",
            AutoSMSAppointments = "AutoSMSAppointments",
            AllowMovingNonClosedRecords = "AllowMovingNonClosedRecords",
            StateCompulsoryInContacts = "StateCompulsoryInContacts",
            EnableAddressInTransactions = "EnableAddressInTransactions",
            AutoEmailEnquiry = "AutoEmailEnquiry",
            AutoSMSEnquiry = "AutoSMSEnquiry",
            AutoEmailQuotation = "AutoEmailQuotation",
            AutoSMSQuotation = "AutoSMSQuotation",
            AutoEmailProforma = "AutoEmailProforma",
            AutoSMSProforma = "AutoSMSProforma",
            AutoEmailInvoice = "AutoEmailInvoice",
            AutoSMSInvoice = "AutoSMSInvoice",
            HideDescriptionInSales = "HideDescriptionInSales",
            HideDescriptionInProforma = "HideDescriptionInProforma",
            HideDescriptionInChallan = "HideDescriptionInChallan",
            QuotationTemplate = "QuotationTemplate",
            Country = "Country",
            QuotationTotal = "QuotationTotal",
            MultiCurrency = "MultiCurrency",
            ProjectWithContacts = "ProjectWithContacts",
            StateId = "StateId",
            RoundupInPurchase = "RoundupInPurchase",
            InvoiceTemplate = "InvoiceTemplate",
            CompanyDetails = "CompanyDetails",
            EnableAdditionalCharges = "EnableAdditionalCharges",
            EnableAdditionalConcessions = "EnableAdditionalConcessions",
            AdditionalImages = "AdditionalImages",
            HeaderContent = "HeaderContent",
            FooterContent = "FooterContent",
            QuotationSuffix = "QuotationSuffix",
            QuotationPrefix = "QuotationPrefix",
            InvoiceSuffix = "InvoiceSuffix",
            InvoicePrefix = "InvoicePrefix",
            ChallanSuffix = "ChallanSuffix",
            ChallanPrefix = "ChallanPrefix",
            QuotationTaxColumnIncluded = "QuotationTaxColumnIncluded",
            InvoiceTaxColumnIncluded = "InvoiceTaxColumnIncluded",
            QuotationDiscountedPriceIncluded = "QuotationDiscountedPriceIncluded",
            InvoiceDiscountedPriceIncluded = "InvoiceDiscountedPriceIncluded",
            RoundupInQuotation = "RoundupInQuotation",
            QuotationContactIncluded = "QuotationContactIncluded",
            EnquirySuffix = "EnquirySuffix",
            EnquiryPrefix = "EnquiryPrefix",
            CountryMandatory = "CountryMandatory",
            PincodeMandatory = "PincodeMandatory",
            CityMandatory = "CityMandatory",
            CapacityInProducts = "CapacityInProducts",
            YearInPrefix = "YearInPrefix",
            InvoiceHeaderContent = "InvoiceHeaderContent",
            InvoiceFooterContent = "InvoiceFooterContent",
            InvoiceHeaderImage = "InvoiceHeaderImage",
            InvoiceHeaderHeight = "InvoiceHeaderHeight",
            InvoiceHeaderWidth = "InvoiceHeaderWidth",
            InvoiceFooterImage = "InvoiceFooterImage",
            InvoiceFooterHeight = "InvoiceFooterHeight",
            InvoiceFooterWidth = "InvoiceFooterWidth",
            DcHeaderContent = "DcHeaderContent",
            DcFooterContent = "DcFooterContent",
            DcHeaderImage = "DcHeaderImage",
            DcHeaderHeight = "DcHeaderHeight",
            DcHeaderWidth = "DcHeaderWidth",
            DcFooterImage = "DcFooterImage",
            DcFooterHeight = "DcFooterHeight",
            DcFooterWidth = "DcFooterWidth",
            ValidDate = "ValidDate",
            DealerInCms = "DealerInCms",
            ServicePerson = "ServicePerson",
            EnqStartNo = "EnqStartNo",
            EnqEditNo = "EnqEditNo",
            QuoStartNo = "QuoStartNo",
            RemoveGtColumn = "RemoveGtColumn",
            QuoEditNo = "QuoEditNo",
            InvStartNo = "InvStartNo",
            InvEditNo = "InvEditNo",
            DcStartNo = "DcStartNo",
            DcEditNo = "DcEditNo",
            DealerInEnquiry = "DealerInEnquiry",
            DealerInQuotation = "DealerInQuotation",
            DealerInSales = "DealerInSales",
            DealerInInvoice = "DealerInInvoice",
            ProjectInCms = "ProjectInCms",
            CmsStartNo = "CmsStartNo",
            CmsEditNo = "CmsEditNo",
            CmsSuffix = "CmsSuffix",
            CmSprefix = "CmSprefix",
            ProductsInCms = "ProductsInCms",
            RemovePurchaseDate = "RemovePurchaseDate",
            RemoveInvoiceNo = "RemoveInvoiceNo",
            MailToSubContacts = "MailToSubContacts",
            MailToOrganisation = "MailToOrganisation",
            State = "State",
            ChallanTaxColumnIncluded = "ChallanTaxColumnIncluded",
            PassportDetails = "PassportDetails",
            TaskTitleInTask = "TaskTitleInTask",
            TaskMasterInTask = "TaskMasterInTask",
            WinPercentageInEnquiry = "WinPercentageInEnquiry",
            ExpectedClosingDateInEnquiry = "ExpectedClosingDateInEnquiry",
            WinPercentageInQuotation = "WinPercentageInQuotation",
            ExpectedClosingDateInQuotation = "ExpectedClosingDateInQuotation",
            HSN = "HSN",
            Code = "Code",
            Unit = "Unit",
            OpeningStock = "OpeningStock",
            RawMaterial = "RawMaterial",
            Group = "Group",
            ToInvoice = "ToInvoice",
            ToPerforma = "ToPerforma",
            Capacity = "Capacity",
            MRP = "MRP",
            SellingPrice = "SellingPrice",
            Travels = "Travels",
            ItineraryHeaderContent = "ItineraryHeaderContent",
            ItineraryFooterContent = "ItineraryFooterContent",
            ItineraryHeaderImage = "ItineraryHeaderImage",
            ItineraryHeaderHeight = "ItineraryHeaderHeight",
            ItineraryHeaderWidth = "ItineraryHeaderWidth",
            ItineraryFooterImage = "ItineraryFooterImage",
            ItineraryFooterHeight = "ItineraryFooterHeight",
            ItineraryFooterWidth = "ItineraryFooterWidth"
        }
    }
}
