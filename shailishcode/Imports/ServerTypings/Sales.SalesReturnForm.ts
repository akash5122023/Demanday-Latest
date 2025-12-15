namespace AdvanceCRM.Sales {
    export interface SalesReturnForm {
        ContactsId: Serenity.LookupEditor;
        Date: Serenity.DateEditor;
        InvoiceNo: Serenity.StringEditor;
        InvoiceDate: Serenity.DateEditor;
        BranchId: Serenity.LookupEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Products: SalesReturnProductsEditor;
        Roundup: Serenity.DecimalEditor;
        Amount: Serenity.DecimalEditor;
        Lines: Serenity.IntegerEditor;
        OwnerId: Serenity.LookupEditor;
        AssignedId: Serenity.LookupEditor;
        NoteList: Common.NotesEditor;
    }

    export class SalesReturnForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.SalesReturn';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SalesReturnForm.init)  {
                SalesReturnForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DateEditor;
                var w2 = s.StringEditor;
                var w3 = s.TextAreaEditor;
                var w4 = SalesReturnProductsEditor;
                var w5 = s.DecimalEditor;
                var w6 = s.IntegerEditor;
                var w7 = Common.NotesEditor;

                Q.initFormType(SalesReturnForm, [
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
                    'OwnerId', w0,
                    'AssignedId', w0,
                    'NoteList', w7
                ]);
            }
        }
    }
}
