namespace AdvanceCRM.Sales {
    export interface IndentProductsForm {
        ProductsId: Serenity.LookupEditor;
        Quantity: Serenity.DecimalEditor;
        Description: Serenity.StringEditor;
    }

    export class IndentProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.IndentProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!IndentProductsForm.init)  {
                IndentProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.StringEditor;

                Q.initFormType(IndentProductsForm, [
                    'ProductsId', w0,
                    'Quantity', w1,
                    'Description', w2
                ]);
            }
        }
    }
}
