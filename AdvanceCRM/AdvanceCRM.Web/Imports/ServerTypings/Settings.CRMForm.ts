namespace AdvanceCRM.Settings {
    export interface CRMForm {
        PhoneCompulsory: BooleanSwitchEditor;
        EmailCompulsory: BooleanSwitchEditor;
        MultiCurrency: BooleanSwitchEditor;
        ProjectWithContacts: BooleanSwitchEditor;
        AllowMovingNonClosedRecords: BooleanSwitchEditor;
        StateCompulsoryInContacts: BooleanSwitchEditor;
        EnableAddressInTransactions: BooleanSwitchEditor;
        TAXInStockTransfer: BooleanSwitchEditor;
        EnableAdditionalCharges: BooleanSwitchEditor;
        EnableAdditionalConcessions: BooleanSwitchEditor;
        CountryMandatory: BooleanSwitchEditor;
        PincodeMandatory: BooleanSwitchEditor;
        CityMandatory: BooleanSwitchEditor;
        CapacityInProducts: BooleanSwitchEditor;
        Addinfo2: BooleanSwitchEditor;
        MultiAddInfo: BooleanSwitchEditor;
        MailToSubContacts: BooleanSwitchEditor;
        MailToOrganisation: BooleanSwitchEditor;
        PassportDetails: BooleanSwitchEditor;
        HSN: BooleanSwitchEditor;
        Code: BooleanSwitchEditor;
        Unit: BooleanSwitchEditor;
        OpeningStock: BooleanSwitchEditor;
        RawMaterial: BooleanSwitchEditor;
        Group: BooleanSwitchEditor;
        Capacity: BooleanSwitchEditor;
        MRP: BooleanSwitchEditor;
        SellingPrice: BooleanSwitchEditor;
        Travels: BooleanSwitchEditor;
        DealerInEnquiry: BooleanSwitchEditor;
        EnquiryFollwupMandatory: BooleanSwitchEditor;
        EnquiryProductsMandatory: BooleanSwitchEditor;
        RequirementInEnquiry: BooleanSwitchEditor;
        AutoEmailEnquiry: BooleanSwitchEditor;
        AutoSMSEnquiry: BooleanSwitchEditor;
        WinPercentageInEnquiry: BooleanSwitchEditor;
        ExpectedClosingDateInEnquiry: BooleanSwitchEditor;
        RoundupInQuotation: BooleanSwitchEditor;
        QuotationTotal: BooleanSwitchEditor;
        RemoveGtColumn: BooleanSwitchEditor;
        QuotationFollwupMandatory: BooleanSwitchEditor;
        QuotationProductsMandatory: BooleanSwitchEditor;
        AutoEmailQuotation: BooleanSwitchEditor;
        AutoSMSQuotation: BooleanSwitchEditor;
        DealerInQuotation: BooleanSwitchEditor;
        WinPercentageInQuotation: BooleanSwitchEditor;
        ExpectedClosingDateInQuotation: BooleanSwitchEditor;
        ValidDate: BooleanSwitchEditor;
        RoundupInSales: BooleanSwitchEditor;
        PackagingInSales: BooleanSwitchEditor;
        FreightInSales: BooleanSwitchEditor;
        DueDateInSales: BooleanSwitchEditor;
        DispatchInSales: BooleanSwitchEditor;
        GSTDetailsInSales: BooleanSwitchEditor;
        FollowupsInSales: BooleanSwitchEditor;
        TermsInSales: BooleanSwitchEditor;
        AdvanceInSales: BooleanSwitchEditor;
        StageInSales: BooleanSwitchEditor;
        CodeInSales: BooleanSwitchEditor;
        SerialInSales: BooleanSwitchEditor;
        BatchInSales: BooleanSwitchEditor;
        DiscountInSales: BooleanSwitchEditor;
        TAXInSales: BooleanSwitchEditor;
        WarrantyInSales: BooleanSwitchEditor;
        DescriptionInSales: BooleanSwitchEditor;
        AutoEmailInvoice: BooleanSwitchEditor;
        AutoSMSInvoice: BooleanSwitchEditor;
        DealerInSales: BooleanSwitchEditor;
        DealerInCms: BooleanSwitchEditor;
        ServicePerson: BooleanSwitchEditor;
        ProjectInCms: BooleanSwitchEditor;
        ProductsInCms: BooleanSwitchEditor;
        RemovePurchaseDate: BooleanSwitchEditor;
        RemoveInvoiceNo: BooleanSwitchEditor;
        DealerInInvoice: BooleanSwitchEditor;
        RoundupInPurchase: BooleanSwitchEditor;
        AppointmentInEnquiry: BooleanSwitchEditor;
        AppointmentInQuotation: BooleanSwitchEditor;
        AppointmentInProforma: BooleanSwitchEditor;
        AppointmentInTeleCalling: BooleanSwitchEditor;
        AutoSMSAppointments: BooleanSwitchEditor;
        TaskTitleInTask: BooleanSwitchEditor;
        TaskMasterInTask: BooleanSwitchEditor;
    }

    export class CRMForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.CRMForm';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CRMForm.init)  {
                CRMForm.init = true;

                var s = Serenity;
                var w0 = BooleanSwitchEditor;

                Q.initFormType(CRMForm, [
                    'PhoneCompulsory', w0,
                    'EmailCompulsory', w0,
                    'MultiCurrency', w0,
                    'ProjectWithContacts', w0,
                    'AllowMovingNonClosedRecords', w0,
                    'StateCompulsoryInContacts', w0,
                    'EnableAddressInTransactions', w0,
                    'TAXInStockTransfer', w0,
                    'EnableAdditionalCharges', w0,
                    'EnableAdditionalConcessions', w0,
                    'CountryMandatory', w0,
                    'PincodeMandatory', w0,
                    'CityMandatory', w0,
                    'CapacityInProducts', w0,
                    'Addinfo2', w0,
                    'MultiAddInfo', w0,
                    'MailToSubContacts', w0,
                    'MailToOrganisation', w0,
                    'PassportDetails', w0,
                    'HSN', w0,
                    'Code', w0,
                    'Unit', w0,
                    'OpeningStock', w0,
                    'RawMaterial', w0,
                    'Group', w0,
                    'Capacity', w0,
                    'MRP', w0,
                    'SellingPrice', w0,
                    'Travels', w0,
                    'DealerInEnquiry', w0,
                    'EnquiryFollwupMandatory', w0,
                    'EnquiryProductsMandatory', w0,
                    'RequirementInEnquiry', w0,
                    'AutoEmailEnquiry', w0,
                    'AutoSMSEnquiry', w0,
                    'WinPercentageInEnquiry', w0,
                    'ExpectedClosingDateInEnquiry', w0,
                    'RoundupInQuotation', w0,
                    'QuotationTotal', w0,
                    'RemoveGtColumn', w0,
                    'QuotationFollwupMandatory', w0,
                    'QuotationProductsMandatory', w0,
                    'AutoEmailQuotation', w0,
                    'AutoSMSQuotation', w0,
                    'DealerInQuotation', w0,
                    'WinPercentageInQuotation', w0,
                    'ExpectedClosingDateInQuotation', w0,
                    'ValidDate', w0,
                    'RoundupInSales', w0,
                    'PackagingInSales', w0,
                    'FreightInSales', w0,
                    'DueDateInSales', w0,
                    'DispatchInSales', w0,
                    'GSTDetailsInSales', w0,
                    'FollowupsInSales', w0,
                    'TermsInSales', w0,
                    'AdvanceInSales', w0,
                    'StageInSales', w0,
                    'CodeInSales', w0,
                    'SerialInSales', w0,
                    'BatchInSales', w0,
                    'DiscountInSales', w0,
                    'TAXInSales', w0,
                    'WarrantyInSales', w0,
                    'DescriptionInSales', w0,
                    'AutoEmailInvoice', w0,
                    'AutoSMSInvoice', w0,
                    'DealerInSales', w0,
                    'DealerInCms', w0,
                    'ServicePerson', w0,
                    'ProjectInCms', w0,
                    'ProductsInCms', w0,
                    'RemovePurchaseDate', w0,
                    'RemoveInvoiceNo', w0,
                    'DealerInInvoice', w0,
                    'RoundupInPurchase', w0,
                    'AppointmentInEnquiry', w0,
                    'AppointmentInQuotation', w0,
                    'AppointmentInProforma', w0,
                    'AppointmentInTeleCalling', w0,
                    'AutoSMSAppointments', w0,
                    'TaskTitleInTask', w0,
                    'TaskMasterInTask', w0
                ]);
            }
        }
    }
}
