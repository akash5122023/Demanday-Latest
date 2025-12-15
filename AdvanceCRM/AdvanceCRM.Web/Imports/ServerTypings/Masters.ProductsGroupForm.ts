namespace AdvanceCRM.Masters {
    export interface ProductsGroupForm {
        ProductsGroup: Serenity.StringEditor;
    }

    export class ProductsGroupForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ProductsGroup';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProductsGroupForm.init)  {
                ProductsGroupForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ProductsGroupForm, [
                    'ProductsGroup', w0
                ]);
            }
        }
    }
}
