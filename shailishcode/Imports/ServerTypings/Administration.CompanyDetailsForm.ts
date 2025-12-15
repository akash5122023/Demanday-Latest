namespace AdvanceCRM.Administration {
    export interface CompanyDetailsForm {
        Name: Serenity.StringEditor;
        Slogan: Serenity.StringEditor;
        Address: Serenity.TextAreaEditor;
        Phone: Serenity.MaskedEditor;
        GSTIN: Serenity.StringEditor;
        PANNo: Serenity.StringEditor;
        StateId: Serenity.LookupEditor;
        Country: Serenity.EnumEditor;
        PassportDetails: Serenity.BooleanEditor;
        HSN: Serenity.BooleanEditor;
        Code: Serenity.BooleanEditor;
        Unit: Serenity.BooleanEditor;
        OpeningStock: Serenity.BooleanEditor;
        RawMaterial: Serenity.BooleanEditor;
        Group: Serenity.BooleanEditor;
        ToInvoice: Serenity.BooleanEditor;
        ToPerforma: Serenity.BooleanEditor;
        Capacity: Serenity.BooleanEditor;
        MRP: Serenity.BooleanEditor;
        SellingPrice: Serenity.BooleanEditor;
        Travels: Serenity.BooleanEditor;
        Logo: Serenity.ImageUploadEditor;
        LogoHeight: Serenity.IntegerEditor;
        LogoWidth: Serenity.IntegerEditor;
        AdditionalImages: Serenity.MultipleImageUploadEditor;
        CompanyDetails: BSSwitchEditor;
        YearInPrefix: Serenity.EnumEditor;
        EnquirySuffix: Serenity.StringEditor;
        EnquiryPrefix: Serenity.StringEditor;
        EnqStartNo: Serenity.IntegerEditor;
        EnqEditNo: BooleanSwitchEditor;
        QuotationTemplate: Serenity.EnumEditor;
        QuotationTaxColumnIncluded: BooleanSwitchEditor;
        QuotationDiscountedPriceIncluded: BooleanSwitchEditor;
        QuotationContactIncluded: BooleanSwitchEditor;
        QuotationSuffix: Serenity.StringEditor;
        QuotationPrefix: Serenity.StringEditor;
        QuoStartNo: Serenity.IntegerEditor;
        QuoEditNo: BooleanSwitchEditor;
        HeaderContent: Serenity.HtmlContentEditor;
        HeaderImage: Serenity.ImageUploadEditor;
        HeaderHeight: Serenity.IntegerEditor;
        HeaderWidth: Serenity.IntegerEditor;
        FooterContent: Serenity.HtmlContentEditor;
        FooterImage: Serenity.ImageUploadEditor;
        FooterHeight: Serenity.IntegerEditor;
        FooterWidth: Serenity.IntegerEditor;
        InvoiceTemplate: Serenity.EnumEditor;
        InvoiceTaxColumnIncluded: BooleanSwitchEditor;
        InvoiceDiscountedPriceIncluded: BooleanSwitchEditor;
        InvoiceSuffix: Serenity.StringEditor;
        InvoicePrefix: Serenity.StringEditor;
        InvStartNo: Serenity.IntegerEditor;
        InvEditNo: BooleanSwitchEditor;
        InvoiceHeaderContent: Serenity.HtmlContentEditor;
        InvoiceHeaderImage: Serenity.ImageUploadEditor;
        InvoiceHeaderHeight: Serenity.IntegerEditor;
        InvoiceHeaderWidth: Serenity.IntegerEditor;
        InvoiceFooterContent: Serenity.HtmlContentEditor;
        InvoiceFooterImage: Serenity.ImageUploadEditor;
        InvoiceFooterHeight: Serenity.IntegerEditor;
        InvoiceFooterWidth: Serenity.IntegerEditor;
        CmsSuffix: Serenity.StringEditor;
        CmSprefix: Serenity.StringEditor;
        CmsStartNo: Serenity.IntegerEditor;
        CmsEditNo: BooleanSwitchEditor;
        ChallanTaxColumnIncluded: BooleanSwitchEditor;
        ChallanSuffix: Serenity.StringEditor;
        ChallanPrefix: Serenity.StringEditor;
        DcStartNo: Serenity.IntegerEditor;
        DcEditNo: BooleanSwitchEditor;
        DcHeaderContent: Serenity.HtmlContentEditor;
        DcHeaderImage: Serenity.ImageUploadEditor;
        DcHeaderHeight: Serenity.IntegerEditor;
        DcHeaderWidth: Serenity.IntegerEditor;
        DcFooterContent: Serenity.HtmlContentEditor;
        DcFooterImage: Serenity.ImageUploadEditor;
        DcFooterHeight: Serenity.IntegerEditor;
        DcFooterWidth: Serenity.IntegerEditor;
        ItineraryHeaderImage: Serenity.ImageUploadEditor;
        ItineraryHeaderHeight: Serenity.IntegerEditor;
        ItineraryHeaderWidth: Serenity.IntegerEditor;
        ItineraryFooterImage: Serenity.ImageUploadEditor;
        ItineraryFooterHeight: Serenity.IntegerEditor;
        ItineraryFooterWidth: Serenity.IntegerEditor;
    }

    export class CompanyDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.CompanyDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CompanyDetailsForm.init)  {
                CompanyDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.MaskedEditor;
                var w3 = s.LookupEditor;
                var w4 = s.EnumEditor;
                var w5 = s.BooleanEditor;
                var w6 = s.ImageUploadEditor;
                var w7 = s.IntegerEditor;
                var w8 = s.MultipleImageUploadEditor;
                var w9 = BSSwitchEditor;
                var w10 = BooleanSwitchEditor;
                var w11 = s.HtmlContentEditor;

                Q.initFormType(CompanyDetailsForm, [
                    'Name', w0,
                    'Slogan', w0,
                    'Address', w1,
                    'Phone', w2,
                    'GSTIN', w0,
                    'PANNo', w0,
                    'StateId', w3,
                    'Country', w4,
                    'PassportDetails', w5,
                    'HSN', w5,
                    'Code', w5,
                    'Unit', w5,
                    'OpeningStock', w5,
                    'RawMaterial', w5,
                    'Group', w5,
                    'ToInvoice', w5,
                    'ToPerforma', w5,
                    'Capacity', w5,
                    'MRP', w5,
                    'SellingPrice', w5,
                    'Travels', w5,
                    'Logo', w6,
                    'LogoHeight', w7,
                    'LogoWidth', w7,
                    'AdditionalImages', w8,
                    'CompanyDetails', w9,
                    'YearInPrefix', w4,
                    'EnquirySuffix', w0,
                    'EnquiryPrefix', w0,
                    'EnqStartNo', w7,
                    'EnqEditNo', w10,
                    'QuotationTemplate', w4,
                    'QuotationTaxColumnIncluded', w10,
                    'QuotationDiscountedPriceIncluded', w10,
                    'QuotationContactIncluded', w10,
                    'QuotationSuffix', w0,
                    'QuotationPrefix', w0,
                    'QuoStartNo', w7,
                    'QuoEditNo', w10,
                    'HeaderContent', w11,
                    'HeaderImage', w6,
                    'HeaderHeight', w7,
                    'HeaderWidth', w7,
                    'FooterContent', w11,
                    'FooterImage', w6,
                    'FooterHeight', w7,
                    'FooterWidth', w7,
                    'InvoiceTemplate', w4,
                    'InvoiceTaxColumnIncluded', w10,
                    'InvoiceDiscountedPriceIncluded', w10,
                    'InvoiceSuffix', w0,
                    'InvoicePrefix', w0,
                    'InvStartNo', w7,
                    'InvEditNo', w10,
                    'InvoiceHeaderContent', w11,
                    'InvoiceHeaderImage', w6,
                    'InvoiceHeaderHeight', w7,
                    'InvoiceHeaderWidth', w7,
                    'InvoiceFooterContent', w11,
                    'InvoiceFooterImage', w6,
                    'InvoiceFooterHeight', w7,
                    'InvoiceFooterWidth', w7,
                    'CmsSuffix', w0,
                    'CmSprefix', w0,
                    'CmsStartNo', w7,
                    'CmsEditNo', w10,
                    'ChallanTaxColumnIncluded', w10,
                    'ChallanSuffix', w0,
                    'ChallanPrefix', w0,
                    'DcStartNo', w7,
                    'DcEditNo', w10,
                    'DcHeaderContent', w11,
                    'DcHeaderImage', w6,
                    'DcHeaderHeight', w7,
                    'DcHeaderWidth', w7,
                    'DcFooterContent', w11,
                    'DcFooterImage', w6,
                    'DcFooterHeight', w7,
                    'DcFooterWidth', w7,
                    'ItineraryHeaderImage', w6,
                    'ItineraryHeaderHeight', w7,
                    'ItineraryHeaderWidth', w7,
                    'ItineraryFooterImage', w6,
                    'ItineraryFooterHeight', w7,
                    'ItineraryFooterWidth', w7
                ]);
            }
        }
    }
}
