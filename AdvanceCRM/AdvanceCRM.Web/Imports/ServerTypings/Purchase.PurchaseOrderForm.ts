namespace AdvanceCRM.Purchase {
    export interface PurchaseOrderForm {
        PurchaseOrderNo: Serenity.IntegerEditor;
        ContactsId: Serenity.LookupEditor;
        ContactsName: Serenity.StringEditor;
        ContactsPhone: Serenity.StringEditor;
        ContactsWhatsapp: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        DueDate: Serenity.DateEditor;
        BranchId: Serenity.LookupEditor;
        Description: Serenity.TextAreaEditor;
        ShippingAddress: Serenity.TextAreaEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
        Products: PurchaseOrderProductsEditor;
        Total: Serenity.DecimalEditor;
        Roundup: Serenity.DecimalEditor;
        CurrencyConversion: BooleanSwitchEditor;
        Conversion: Serenity.DecimalEditor;
        FromCurrency: Serenity.EnumEditor;
        ToCurrency: Serenity.EnumEditor;
        Lines: Serenity.IntegerEditor;
        TermsList: Serenity.LookupEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
        NoteList: Common.NotesEditor;
    }

    export class PurchaseOrderForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.PurchaseOrder';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!PurchaseOrderForm.init)  {
                PurchaseOrderForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;
                var w3 = s.DateEditor;
                var w4 = s.EnumEditor;
                var w5 = s.TextAreaEditor;
                var w6 = s.MultipleImageUploadEditor;
                var w7 = PurchaseOrderProductsEditor;
                var w8 = s.DecimalEditor;
                var w9 = BooleanSwitchEditor;
                var w10 = Administration.UserEditor;
                var w11 = Common.NotesEditor;

                Q.initFormType(PurchaseOrderForm, [
                    'PurchaseOrderNo', w0,
                    'ContactsId', w1,
                    'ContactsName', w2,
                    'ContactsPhone', w2,
                    'ContactsWhatsapp', w2,
                    'Date', w3,
                    'Status', w4,
                    'DueDate', w3,
                    'BranchId', w1,
                    'Description', w5,
                    'ShippingAddress', w5,
                    'AdditionalInfo', w5,
                    'Attachments', w6,
                    'Products', w7,
                    'Total', w8,
                    'Roundup', w8,
                    'CurrencyConversion', w9,
                    'Conversion', w8,
                    'FromCurrency', w4,
                    'ToCurrency', w4,
                    'Lines', w0,
                    'TermsList', w1,
                    'OwnerId', w10,
                    'AssignedId', w10,
                    'NoteList', w11
                ]);
            }
        }
    }
}
