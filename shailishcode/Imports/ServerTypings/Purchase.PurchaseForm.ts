namespace AdvanceCRM.Purchase {
    export interface PurchaseForm {
        InvoiceNo: Serenity.StringEditor;
        PurchaseFromId: Serenity.LookupEditor;
        PurchaseFromPhone: Serenity.StringEditor;
        InvoiceDate: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        BranchId: Serenity.LookupEditor;
        Products: PurchaseProductsEditor;
        Total: Serenity.DecimalEditor;
        Roundup: Serenity.DecimalEditor;
        Type: Serenity.EnumEditor;
        ReverseCharge: Serenity.BooleanEditor;
        InvoiceType: Serenity.EnumEditor;
        ITCEligibility: Serenity.EnumEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
        OwnerId: Serenity.LookupEditor;
        AssignedId: Serenity.LookupEditor;
        NoteList: Common.NotesEditor;
    }

    export class PurchaseForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.Purchase';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!PurchaseForm.init)  {
                PurchaseForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = s.DateEditor;
                var w3 = s.EnumEditor;
                var w4 = PurchaseProductsEditor;
                var w5 = s.DecimalEditor;
                var w6 = s.BooleanEditor;
                var w7 = s.TextAreaEditor;
                var w8 = s.MultipleImageUploadEditor;
                var w9 = Common.NotesEditor;

                Q.initFormType(PurchaseForm, [
                    'InvoiceNo', w0,
                    'PurchaseFromId', w1,
                    'PurchaseFromPhone', w0,
                    'InvoiceDate', w2,
                    'Status', w3,
                    'BranchId', w1,
                    'Products', w4,
                    'Total', w5,
                    'Roundup', w5,
                    'Type', w3,
                    'ReverseCharge', w6,
                    'InvoiceType', w3,
                    'ITCEligibility', w3,
                    'AdditionalInfo', w7,
                    'Attachments', w8,
                    'OwnerId', w1,
                    'AssignedId', w1,
                    'NoteList', w9
                ]);
            }
        }
    }
}
