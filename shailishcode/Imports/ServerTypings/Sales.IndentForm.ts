namespace AdvanceCRM.Sales {
    export interface IndentForm {
        ContactsId: Serenity.LookupEditor;
        ContactsPhone: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        Products: IndentProductsEditor;
        InvoiceNo: Serenity.StringEditor;
        Status: Serenity.EnumEditor;
        BranchId: Serenity.LookupEditor;
        AdditionalInfo: Serenity.StringEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
    }

    export class IndentForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.Indent';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!IndentForm.init)  {
                IndentForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateEditor;
                var w3 = IndentProductsEditor;
                var w4 = s.EnumEditor;
                var w5 = Administration.UserEditor;

                Q.initFormType(IndentForm, [
                    'ContactsId', w0,
                    'ContactsPhone', w1,
                    'Date', w2,
                    'Products', w3,
                    'InvoiceNo', w1,
                    'Status', w4,
                    'BranchId', w0,
                    'AdditionalInfo', w1,
                    'OwnerId', w5,
                    'AssignedId', w5
                ]);
            }
        }
    }
}
