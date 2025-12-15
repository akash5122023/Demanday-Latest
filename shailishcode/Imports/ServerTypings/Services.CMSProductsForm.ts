namespace AdvanceCRM.Services {
    export interface CMSProductsForm {
        ProductsId: Serenity.LookupEditor;
        Price: Serenity.DecimalEditor;
        Quantity: Serenity.DecimalEditor;
    }

    export class CMSProductsForm extends Serenity.PrefixedContext {
        static formKey = 'Services.CMSProducts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CMSProductsForm.init)  {
                CMSProductsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DecimalEditor;

                Q.initFormType(CMSProductsForm, [
                    'ProductsId', w0,
                    'Price', w1,
                    'Quantity', w1
                ]);
            }
        }
    }
}
