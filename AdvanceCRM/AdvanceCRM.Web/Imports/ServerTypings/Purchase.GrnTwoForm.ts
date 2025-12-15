namespace AdvanceCRM.Purchase {
    export interface GrnTwoForm {
        ContactsId: Serenity.LookupEditor;
        ContactsPhone: Serenity.StringEditor;
        GrnDate: Serenity.DateEditor;
        GrnType: Serenity.EnumEditor;
        Po: Serenity.StringEditor;
        PoDate: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        InvoiceNo: Serenity.StringEditor;
        InvoiceDate: Serenity.DateEditor;
        Products: GrnProductsTwoEditor;
        Description: Serenity.TextAreaEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
    }

    export class GrnTwoForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.GrnTwo';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!GrnTwoForm.init)  {
                GrnTwoForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateEditor;
                var w3 = s.EnumEditor;
                var w4 = GrnProductsTwoEditor;
                var w5 = s.TextAreaEditor;
                var w6 = Administration.UserEditor;

                Q.initFormType(GrnTwoForm, [
                    'ContactsId', w0,
                    'ContactsPhone', w1,
                    'GrnDate', w2,
                    'GrnType', w3,
                    'Po', w1,
                    'PoDate', w2,
                    'Status', w3,
                    'InvoiceNo', w1,
                    'InvoiceDate', w2,
                    'Products', w4,
                    'Description', w5,
                    'OwnerId', w6,
                    'AssignedId', w6
                ]);
            }
        }
    }
}
