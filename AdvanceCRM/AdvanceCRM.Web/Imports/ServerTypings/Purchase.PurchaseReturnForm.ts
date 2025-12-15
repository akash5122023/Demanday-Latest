namespace AdvanceCRM.Purchase {
    export interface PurchaseReturnForm {
        ContactsId: Serenity.LookupEditor;
        Date: Serenity.DateEditor;
        InvoiceNo: Serenity.StringEditor;
        InvoiceDate: Serenity.DateEditor;
        BranchId: Serenity.LookupEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Products: PurchaseReturnProductsEditor;
        Roundup: Serenity.DecimalEditor;
        Amount: Serenity.DecimalEditor;
        Lines: Serenity.IntegerEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
        NoteList: Common.NotesEditor;
    }

    export class PurchaseReturnForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.PurchaseReturn';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!PurchaseReturnForm.init)  {
                PurchaseReturnForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DateEditor;
                var w2 = s.StringEditor;
                var w3 = s.TextAreaEditor;
                var w4 = PurchaseReturnProductsEditor;
                var w5 = s.DecimalEditor;
                var w6 = s.IntegerEditor;
                var w7 = Administration.UserEditor;
                var w8 = Common.NotesEditor;

                Q.initFormType(PurchaseReturnForm, [
                    'ContactsId', w0,
                    'Date', w1,
                    'InvoiceNo', w2,
                    'InvoiceDate', w1,
                    'BranchId', w0,
                    'AdditionalInfo', w3,
                    'Products', w4,
                    'Roundup', w5,
                    'Amount', w5,
                    'Lines', w6,
                    'OwnerId', w7,
                    'AssignedId', w7,
                    'NoteList', w8
                ]);
            }
        }
    }
}
