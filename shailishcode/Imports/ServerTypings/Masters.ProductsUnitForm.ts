namespace AdvanceCRM.Masters {
    export interface ProductsUnitForm {
        ProductsUnit: Serenity.StringEditor;
    }

    export class ProductsUnitForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ProductsUnit';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProductsUnitForm.init)  {
                ProductsUnitForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ProductsUnitForm, [
                    'ProductsUnit', w0
                ]);
            }
        }
    }
}
