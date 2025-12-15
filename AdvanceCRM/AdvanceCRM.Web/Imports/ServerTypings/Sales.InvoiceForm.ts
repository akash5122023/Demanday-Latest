namespace AdvanceCRM.Sales {
    export interface InvoiceForm {
        ContactsId: Serenity.LookupEditor;
        ContactsContactType: Serenity.IntegerEditor;
        ContactsName: Serenity.StringEditor;
        ContactsPhone: Serenity.StringEditor;
        ContactsWhatsapp: Serenity.StringEditor;
        ContactsAddress: Serenity.TextAreaEditor;
        ContactPersonId: Serenity.LookupEditor;
        ContactPersonName: Serenity.StringEditor;
        ContactPersonPhone: Serenity.StringEditor;
        ContactPersonWhatsapp: Serenity.StringEditor;
        ContactPersonProject: Serenity.StringEditor;
        ContactPersonAddress: Serenity.TextAreaEditor;
        DealerId: Serenity.IntegerEditor;
        Products: InvoiceProductsEditor;
        PackagingCharges: Serenity.DecimalEditor;
        FreightCharges: Serenity.DecimalEditor;
        Advacne: Serenity.DecimalEditor;
        GrandTotal: Serenity.DecimalEditor;
        Roundup: Serenity.DecimalEditor;
        CurrencyConversion: BooleanSwitchEditor;
        Conversion: Serenity.DecimalEditor;
        FromCurrency: Serenity.EnumEditor;
        ToCurrency: Serenity.EnumEditor;
        ChargesList: Serenity.LookupEditor;
        ConcessionList: Serenity.LookupEditor;
        InvoiceNo: Serenity.IntegerEditor;
        InvoiceN: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        DueDate: Serenity.DateEditor;
        ClosingDate: Serenity.DateEditor;
        Type: Serenity.EnumEditor;
        SourceId: Serenity.LookupEditor;
        StageId: Serenity.LookupEditor;
        BranchId: Serenity.LookupEditor;
        PurchaseOrderNo: Serenity.StringEditor;
        PurchaseOrderDate: Serenity.DateEditor;
        Taxable: BooleanSwitchEditor;
        BillingAddress: BooleanSwitchEditor;
        OtherAddress: BooleanSwitchEditor;
        ShippingAddress: Serenity.TextAreaEditor;
        DispatchDetails: Serenity.TextAreaEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
        Subject: Serenity.StringEditor;
        Reference: Serenity.StringEditor;
        MessageId: Serenity.LookupEditor;
        Lines: Serenity.IntegerEditor;
        TermsList: Serenity.LookupEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
        CompanyId: Serenity.LookupEditor;
        NoteList: Common.NotesEditor;
        Timeline: Common.TimelineEditor;
    }

    export class InvoiceForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.Invoice';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InvoiceForm.init)  {
                InvoiceForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.StringEditor;
                var w3 = s.TextAreaEditor;
                var w4 = InvoiceProductsEditor;
                var w5 = s.DecimalEditor;
                var w6 = BooleanSwitchEditor;
                var w7 = s.EnumEditor;
                var w8 = s.DateEditor;
                var w9 = s.MultipleImageUploadEditor;
                var w10 = Administration.UserEditor;
                var w11 = Common.NotesEditor;
                var w12 = Common.TimelineEditor;

                Q.initFormType(InvoiceForm, [
                    'ContactsId', w0,
                    'ContactsContactType', w1,
                    'ContactsName', w2,
                    'ContactsPhone', w2,
                    'ContactsWhatsapp', w2,
                    'ContactsAddress', w3,
                    'ContactPersonId', w0,
                    'ContactPersonName', w2,
                    'ContactPersonPhone', w2,
                    'ContactPersonWhatsapp', w2,
                    'ContactPersonProject', w2,
                    'ContactPersonAddress', w3,
                    'DealerId', w1,
                    'Products', w4,
                    'PackagingCharges', w5,
                    'FreightCharges', w5,
                    'Advacne', w5,
                    'GrandTotal', w5,
                    'Roundup', w5,
                    'CurrencyConversion', w6,
                    'Conversion', w5,
                    'FromCurrency', w7,
                    'ToCurrency', w7,
                    'ChargesList', w0,
                    'ConcessionList', w0,
                    'InvoiceNo', w1,
                    'InvoiceN', w2,
                    'Date', w8,
                    'Status', w7,
                    'DueDate', w8,
                    'ClosingDate', w8,
                    'Type', w7,
                    'SourceId', w0,
                    'StageId', w0,
                    'BranchId', w0,
                    'PurchaseOrderNo', w2,
                    'PurchaseOrderDate', w8,
                    'Taxable', w6,
                    'BillingAddress', w6,
                    'OtherAddress', w6,
                    'ShippingAddress', w3,
                    'DispatchDetails', w3,
                    'AdditionalInfo', w3,
                    'Attachments', w9,
                    'Subject', w2,
                    'Reference', w2,
                    'MessageId', w0,
                    'Lines', w1,
                    'TermsList', w0,
                    'OwnerId', w10,
                    'AssignedId', w10,
                    'CompanyId', w0,
                    'NoteList', w11,
                    'Timeline', w12
                ]);
            }
        }
    }
}
