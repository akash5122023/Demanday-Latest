namespace AdvanceCRM.Purchase {
    export interface GrnProductsTwoForm {
        ProductsId: Serenity.LookupEditor;
        Code: Serenity.StringEditor;
        BranchId: Serenity.LookupEditor;
        Price: Serenity.IntegerEditor;
        OrderQuantity: Serenity.DecimalEditor;
        ReceivedQuantity: Serenity.DecimalEditor;
        ExtraQuantity: Serenity.DecimalEditor;
        RejectedQuantity: Serenity.DecimalEditor;
        Description: Serenity.TextAreaEditor;
    }

    export class GrnProductsTwoForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.GrnProductsTwo';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!GrnProductsTwoForm.init)  {
                GrnProductsTwoForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.IntegerEditor;
                var w3 = s.DecimalEditor;
                var w4 = s.TextAreaEditor;

                Q.initFormType(GrnProductsTwoForm, [
                    'ProductsId', w0,
                    'Code', w1,
                    'BranchId', w0,
                    'Price', w2,
                    'OrderQuantity', w3,
                    'ReceivedQuantity', w3,
                    'ExtraQuantity', w3,
                    'RejectedQuantity', w3,
                    'Description', w4
                ]);
            }
        }
    }
}
