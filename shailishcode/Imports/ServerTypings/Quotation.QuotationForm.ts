namespace AdvanceCRM.Quotation {
    export interface QuotationForm {
        ContactsId: Serenity.LookupEditor;
        ContactsContactType: Serenity.IntegerEditor;
        ContactsName: Serenity.StringEditor;
        ContactsPhone: Serenity.StringEditor;
        ContactsWhatsapp: Serenity.StringEditor;
        ContactsAddress: Serenity.StringEditor;
        ContactPersonId: Serenity.LookupEditor;
        ContactPersonName: Serenity.StringEditor;
        ContactPersonPhone: Serenity.StringEditor;
        ContactPersonWhatsapp: Serenity.StringEditor;
        ContactPersonProject: Serenity.StringEditor;
        ContactPersonAddress: Serenity.StringEditor;
        ProjectId: Serenity.LookupEditor;
        DealerId: Serenity.LookupEditor;
        Products: QuotationProductsEditor;
        Total: Serenity.DecimalEditor;
        GrandTotal: Serenity.DecimalEditor;
        PerDiscount: Serenity.DecimalEditor;
        DiscountAmt: Serenity.DecimalEditor;
        DisGrandTotal: Serenity.DecimalEditor;
        Roundup: Serenity.DecimalEditor;
        CurrencyConversion: BooleanSwitchEditor;
        Conversion: Serenity.DecimalEditor;
        FromCurrency: Serenity.EnumEditor;
        ToCurrency: Serenity.EnumEditor;
        ChargesList: Serenity.LookupEditor;
        ConcessionList: Serenity.LookupEditor;
        QuotationNo: Serenity.IntegerEditor;
        QuotationN: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        ExpectedClosingDate: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        ClosingDate: Serenity.DateEditor;
        ClosingType: Serenity.EnumEditor;
        LostReason: Serenity.StringEditor;
        SourceId: Serenity.LookupEditor;
        StageId: Serenity.LookupEditor;
        Type: Serenity.EnumEditor;
        BranchId: Serenity.LookupEditor;
        WinPercentage: Serenity.EnumEditor;
        Taxable: BooleanSwitchEditor;
        ReferenceName: Serenity.StringEditor;
        ReferencePhone: Serenity.MaskedEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        AdditionalInfo2: Serenity.TextAreaEditor;
        QuotationAddinfoList: Serenity.LookupEditor;
        Attachment: Serenity.MultipleImageUploadEditor;
        Subject: Serenity.StringEditor;
        Reference: Serenity.StringEditor;
        MessageId: Serenity.LookupEditor;
        Lines: Serenity.IntegerEditor;
        TermsList: Serenity.LookupEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
        MultiAssignList: Serenity.LookupEditor;
        CompanyId: Serenity.LookupEditor;
        NoteList: Common.NotesEditor;
        Timeline: Common.TimelineEditor;
    }

    export class QuotationForm extends Serenity.PrefixedContext {
        static formKey = 'Quotation.Quotation';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!QuotationForm.init)  {
                QuotationForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.StringEditor;
                var w3 = QuotationProductsEditor;
                var w4 = s.DecimalEditor;
                var w5 = BooleanSwitchEditor;
                var w6 = s.EnumEditor;
                var w7 = s.DateEditor;
                var w8 = s.MaskedEditor;
                var w9 = s.TextAreaEditor;
                var w10 = s.MultipleImageUploadEditor;
                var w11 = Administration.UserEditor;
                var w12 = Common.NotesEditor;
                var w13 = Common.TimelineEditor;

                Q.initFormType(QuotationForm, [
                    'ContactsId', w0,
                    'ContactsContactType', w1,
                    'ContactsName', w2,
                    'ContactsPhone', w2,
                    'ContactsWhatsapp', w2,
                    'ContactsAddress', w2,
                    'ContactPersonId', w0,
                    'ContactPersonName', w2,
                    'ContactPersonPhone', w2,
                    'ContactPersonWhatsapp', w2,
                    'ContactPersonProject', w2,
                    'ContactPersonAddress', w2,
                    'ProjectId', w0,
                    'DealerId', w0,
                    'Products', w3,
                    'Total', w4,
                    'GrandTotal', w4,
                    'PerDiscount', w4,
                    'DiscountAmt', w4,
                    'DisGrandTotal', w4,
                    'Roundup', w4,
                    'CurrencyConversion', w5,
                    'Conversion', w4,
                    'FromCurrency', w6,
                    'ToCurrency', w6,
                    'ChargesList', w0,
                    'ConcessionList', w0,
                    'QuotationNo', w1,
                    'QuotationN', w2,
                    'Date', w7,
                    'ExpectedClosingDate', w7,
                    'Status', w6,
                    'ClosingDate', w7,
                    'ClosingType', w6,
                    'LostReason', w2,
                    'SourceId', w0,
                    'StageId', w0,
                    'Type', w6,
                    'BranchId', w0,
                    'WinPercentage', w6,
                    'Taxable', w5,
                    'ReferenceName', w2,
                    'ReferencePhone', w8,
                    'AdditionalInfo', w9,
                    'AdditionalInfo2', w9,
                    'QuotationAddinfoList', w0,
                    'Attachment', w10,
                    'Subject', w2,
                    'Reference', w2,
                    'MessageId', w0,
                    'Lines', w1,
                    'TermsList', w0,
                    'OwnerId', w11,
                    'AssignedId', w11,
                    'MultiAssignList', w0,
                    'CompanyId', w0,
                    'NoteList', w12,
                    'Timeline', w13
                ]);
            }
        }
    }
}
